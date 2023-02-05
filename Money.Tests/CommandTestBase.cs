using Microsoft.Data.Sqlite;

using Money.CommandsSettings;

using Moq;

using Spectre.Console.Cli;

namespace Money.Tests
{
    internal abstract class CommandTestBase<TCommand, TCommandArgs>
        where TCommand : AsyncCommand<TCommandArgs>
        where TCommandArgs : CommandSettings
    {
        private string _dbFile;
        private Mock<IDatabaseFileLocator> _dbLocator;

        protected ReadOnlyData ReadOnlyData { get; private set; }
        protected WriteOnlyData WriteOnlyData { get; private set; }
       
        protected DataFiles DataFiles { get; private set; }
        protected TCommand Sut { get; private set; }

        protected CommandContext DefaultContext { get; private set; }

        protected TestTb TestTb { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            _dbFile = Path.GetTempFileName();
            _dbLocator = new Mock<IDatabaseFileLocator>(MockBehavior.Strict);
            _dbLocator.SetupGet(x => x.DatabasePath).Returns(_dbFile);

            ReadOnlyData = new ReadOnlyData(_dbLocator.Object);
            WriteOnlyData = new WriteOnlyData(_dbLocator.Object);
            TestTb = new TestTb(_dbLocator.Object);

            DefaultContext = CreateDefaultContext();

            DataFiles = new DataFiles();
            Sut = CreateSut();
        }

        private CommandContext CreateDefaultContext()
        {
            ILookup<string, string?> parsed = Array.Empty<string>().ToLookup(p => p, p => p);

            Mock<IRemainingArguments> remainingMock = new Mock<IRemainingArguments>(MockBehavior.Strict);
            remainingMock.SetupGet(x => x.Raw).Returns(Array.Empty<string>());
            remainingMock.SetupGet(x => x.Parsed).Returns(parsed);

            return new CommandContext(remainingMock.Object, nameof(TCommand), null);
        }

        [TearDown]
        public virtual void TearDown()
        {
            ReadOnlyData = null;
            WriteOnlyData = null;
            TestTb = null;
            SqliteConnection.ClearAllPools();

            if (File.Exists(_dbFile))
            {
                File.Delete(_dbFile);
            }
        }

        protected abstract TCommand CreateSut();
    }
}