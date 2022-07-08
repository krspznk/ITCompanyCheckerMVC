using Xunit;
using System;
using ITCompanyCheckerMVC.Controllers;
using UnitTestApp.Tests.Support;
using ITCompanyCheckerMVC.Areas.Identity.Data;

namespace UnitTestApp.Tests
{
    public class MainControllerTests : TestDbContextSqlite<ApplicationDbContext>
    {
        [Fact]
        public void IndexViewUsers()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
