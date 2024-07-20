using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_English_Words.DBContext
{
    public class MyWordsDbContextFactory
    {
        private readonly string _connectionString;
        public MyWordsDbContextFactory(string connectionString) {
            _connectionString = connectionString;
        }
        public MyWordsContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new MyWordsContext(options);
        }
    }
}
