using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_English_Words.DBContext
{
    public class LearnEnglishWordsDesignTimeDbContextFactory:IDesignTimeDbContextFactory<MyWordsContext>
    {
        public MyWordsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyWordsContext>();
            optionsBuilder.UseSqlite("Data Source=mywords.db");

            return new MyWordsContext(optionsBuilder.Options);
        }
    }
}
