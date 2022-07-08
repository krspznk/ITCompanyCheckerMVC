using ITCompanyCheckerMVC.Areas.Identity.Data;
using System.Linq;
using System.Threading.Tasks;
using UnitTestApp.Tests.Support;
using Xunit;

namespace UnitTestApp.Tests
{
    public class DbContextTest : TestDbContextSqlite<ApplicationDbContext>
    {
        [Fact]
        public async Task DatabasesAreAvailableAndCanBeConnectedTo()
        {
            Assert.True(await ApplicationContext.Database.CanConnectAsync());
        }
    }
}
