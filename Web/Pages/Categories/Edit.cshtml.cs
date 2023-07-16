﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Money.Web.Models;
using Money.Web.Services;

namespace Money.Web.Pages.Categories
{
    [Authorize]
    internal class EditModel : PageModel
    {
        private readonly CategoryServices _categoryServices;

        public EditModel(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [BindProperty]
        public CategoryViewModel Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetCategory(HttpContext.User, id.Value);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (!await _categoryServices.Modify(HttpContext.User, Category))
                {
                    return RedirectToPage("/ErrorDb");
                }
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("/ErrorDb");
            }

            return RedirectToPage("./Index");
        }
    }
}
