using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;

using Money.Data.Dto;
using Money.Data.Entities;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Money.Data.DataAccess;

public sealed class WriteOnlyData : DataAccessBase, IWriteOnlyData
{
    public int ChunkSize => _ChunkSize;

    public WriteOnlyData(IDatabaseFileLocator databaseLocator) : base(databaseLocator)
    {
    }

    private static ulong CreateId()
    {
        byte[] buffer = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        byte[] random = RandomNumberGenerator.GetBytes(buffer.Length);
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] ^= random[i];
        }
        return BitConverter.ToUInt64(buffer);

    }

    public async Task<(bool success, ulong id)> InsertAsync(double ammount,
                                                            string text,
                                                            DateOnly date,
                                                            string category)
    {
        using MoneyContext db = ConnectDatabase();
        Category? cat = await GetCategory(db, category);

        if (cat == null)
            return (false, 0);

        Spending entity = new Spending
        {
            Id = CreateId(),
            Date = date,
            Description = text,
            Ammount = ammount,
            AddedOn = DateTime.Now,
            Category = cat,
        };

        db.Spendings.Add(entity);

        bool result = await db.SaveChangesAsync() == 1;

        return (result, entity.Id);
    }

    public Task<(bool success, ulong id)> CreateCategoryAsync(string categoryName)
    {
        using MoneyContext db = ConnectDatabase();
        return CreateCategoryAsync(categoryName, db);
    }

    private async Task<(bool success, ulong id)> CreateCategoryAsync(string categoryName, MoneyContext db)
    {
        bool exists = await GetCategory(db, categoryName) != null;

        if (exists)
            return (false, 0);

        Category toInsert = new Category
        {
            Id = CreateId(),
            Description = categoryName.ToLower(),
        };

        db.Categories.Add(toInsert);

        bool result = await db.SaveChangesAsync() == 1;

        return (result, toInsert.Id);
    }

    public async Task<bool> RenameCategoryAsync(string oldCategoryName, string newCategoryName)
    {
        using MoneyContext db = ConnectDatabase();
        Category? cat = await GetCategory(db, oldCategoryName);

        if (cat == null)
            return false;

        cat.Description = newCategoryName;

        db.Categories.Update(cat);
        return await db.SaveChangesAsync() == 1;
    }

    public Task<int> ClearDb()
    {
        using MoneyContext db = ConnectDatabase();

        db.Spendings.RemoveRange(db.Spendings);
        db.Categories.RemoveRange(db.Categories);

        return db.SaveChangesAsync();

    }

    private async Task<int> CreateCategories(MoneyContext context, IEnumerable<IDataRowBase> rows)
    {
        int createdCategories = 0;
        foreach (string? category in rows.Select(x => x.CategoryName.ToLower()).Distinct())
        {
            (bool success, ulong id) = await CreateCategoryAsync(category, context);

            if (success)
                ++createdCategories;
        }
        return createdCategories;
    }

    public async Task<(int createdCategory, int createdEntry)> ImportAsync(IEnumerable<DataRowExcel> rows)
    {
        using MoneyContext db = ConnectDatabase();
        int categoryCount = await CreateCategories(db, rows);

        Dictionary<string, Category> categories = db.Categories.ToDictionary(x => x.Description, x => x);

        IEnumerable<Spending> bulk = rows
            .Select(data => DtoAdapter.ToSpending(data, CreateId, categories[data.CategoryName.ToLower()]));

        db.Spendings.AddRange(bulk);
        int createdEntry = await db.SaveChangesAsync();

        return (categoryCount, createdEntry);

    }

    public async Task<(int createdCategory, int createdEntry)> ImportBackupAsync(IEnumerable<DataRowBackup> rows)
    {
        int createdCategory = 0;
        int createdEntry = 0;
        using MoneyContext db = ConnectDatabase();
        Dictionary<string, Category> categories = await db.Categories.ToDictionaryAsync(x => x.Description.ToLower(), x => x);

        int limit = ChunkSize * 10;
        List<Spending> buffer = new(limit);

        foreach (var row in rows)
        {
            if (buffer.Count > limit - 1)
            {
                db.Spendings.AddRange(buffer);
                createdEntry += await db.SaveChangesAsync();
                buffer.Clear();
            }

            string categoryName = row.CategoryName.ToLower();

            if (categories.TryGetValue(categoryName, out Category? category))
            {
                buffer.Add(DtoAdapter.ToSpending(row, CreateId, category));
            }
            else
            {
                Category newCategory = new Category
                {
                    Id = CreateId(),
                    Description = categoryName,
                };
                categories.Add(categoryName, newCategory);
                createdCategory++;
                db.Categories.Add(newCategory);
                buffer.Add(DtoAdapter.ToSpending(row, CreateId, newCategory));
            }
        }

        if (buffer.Count > 0)
        {
            db.Spendings.AddRange(buffer);
            createdEntry += await db.SaveChangesAsync();
            buffer.Clear();
        }

        return (createdCategory, createdEntry);

    }
}
