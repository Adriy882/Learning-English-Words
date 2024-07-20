using Learn_English_Words.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using System.IO;
using Learn_English_Words.EntityConfigurations;
namespace Learn_English_Words.DBContext
{
    public class MyWordsContext : DbContext
    {

        public MyWordsContext(DbContextOptions options) : base(options) 
        {
        }
        public DbSet<Word> Words { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

 
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WordConfiguration());
            modelBuilder.ApplyConfiguration(new ChapterConfiguration());
        }
    }
}
