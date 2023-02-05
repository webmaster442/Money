
using System.Diagnostics;

namespace Money.Tests
{
    [TestFixture]
    internal class ImportExcelCommandTest : CommandTestBase<ImportExcelCommand, ImportSetting>
    {
        protected override ImportExcelCommand CreateSut()
        {
            return new ImportExcelCommand(base.WriteOnlyData);
        }

        [Test]
        public async Task Test_Import()
        {
            var settings = new ImportSetting
            {
                FileName = DataFiles.ImportData1000,
            };

            int result = await Sut.ExecuteAsync(DefaultContext, settings);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(0));
                Assert.That(TestTb.SpendingCount, Is.EqualTo(1000));
                Assert.That(TestTb.CategoryCount, Is.EqualTo(4));
            });
        }

        [Test]
        public async Task Test_ImportPerformance()
        {
            var settings = new ImportSetting
            {
                FileName = DataFiles.ImportData1000,
            };


            Stopwatch stopwatch = Stopwatch.StartNew();
            int result = await Sut.ExecuteAsync(DefaultContext, settings);
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 1700)
                Assert.Warn("1000k rows took longer than 1700ms");
        }
    }
}
