using Learn_English_Words.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn_English_Words.EntityConfigurations
{
    public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.Property(c => c.NameChapter)
                   .HasMaxLength(30);

            builder.Property(c => c.Description)
                   .HasMaxLength(255);

            builder.HasKey(c => c.NameChapter);
        }
    }
}
