using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestApp.Tests.Support
{
    public class TestDbContextSqlite<T> : IDisposable 
        where T : DbContext
    {
        private const string SqliteConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _sqliteFirstConnection;

        protected readonly T ApplicationContext;

        protected TestDbContextSqlite()
        {
            _sqliteFirstConnection = new SqliteConnection(SqliteConnectionString);
            _sqliteFirstConnection.Open();

            var applicationDbOptions = new DbContextOptionsBuilder<T>()
            .UseSqlite(_sqliteFirstConnection)
            .Options;

            ApplicationContext = (T)Activator.CreateInstance(typeof(T), applicationDbOptions);
            ApplicationContext?.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _sqliteFirstConnection.Close();
        }
    }
}
