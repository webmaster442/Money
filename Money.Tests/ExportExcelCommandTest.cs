
namespace Money.Tests
{
    [TestFixture]
    internal class ExportExcelCommandTest : CommandTestBase<ImportExcelCommand, ImportSetting>
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
    }
}
