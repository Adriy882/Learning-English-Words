using Learn_English_Words.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn_English_Words.EntityConfigurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.Property(w => w.EnglishWord)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(w => w.TranslateWord)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(w => w.Description)
                   .HasMaxLength(255);

            builder.HasOne(w => w.Chapter)
                   .WithMany(c => c.Words)
                   .HasForeignKey(w => w.NameChapter)
                   .HasPrincipalKey(c => c.NameChapter);
        }
    }
}
