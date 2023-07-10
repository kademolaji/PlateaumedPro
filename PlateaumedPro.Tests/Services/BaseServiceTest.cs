using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using PlateaumedPro.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace PlateaumedPro.Tests.Services
{
    public abstract class BaseServiceTest : IDisposable
    {
        protected DataContext MockContext;

        protected BaseServiceTest()
        {
            MockContext = new DataContext(GetOptionsBuilder());
        }

        public void Dispose()
        {
            MockContext.Database.EnsureDeleted();
            MockContext = new DataContext(GetOptionsBuilder());
        }

        private static DbContextOptions<DataContext> GetOptionsBuilder()
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }
    }
}
