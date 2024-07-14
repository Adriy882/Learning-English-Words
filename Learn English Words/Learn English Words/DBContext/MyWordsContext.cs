using Learn_English_Words.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Learn_English_Words.DBContext
{
    internal class MyWordsContext : DbContext
    {
        private static IConfigurationRoot configuration;
        public DbSet<Word> Words { get; set; }
        public DbSet<Chapter> Chapters { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (configuration == null)
                {
                    // Встановлення каталогу для пошуку appsettings.json
                    configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                }

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Word entity
            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(w => w.EnglishWord)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(w => w.TranslateWord)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(w => w.Description)
                      .HasMaxLength(255);

                // Define the relationship with Chapter
                entity.HasOne(w => w.Chapter)
                      .WithMany(c => c.Words)
                      .HasForeignKey(w => w.NameChapter)
                      .HasPrincipalKey(c => c.NameChapter);
            });

            // Configure Chapter entity
            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.Property(c => c.NameChapter)
                      .HasMaxLength(30);

                entity.Property(c => c.Description)
                      .HasMaxLength(255);

                entity.HasKey(c => c.NameChapter);
            });

        }
    }
}
