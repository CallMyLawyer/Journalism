using Journalism.Entites.NewsPapers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journalism.Persistence.EF.News;

public class NewsEntityMap : IEntityTypeConfiguration<Entites.News.News>
{
    public void Configure(EntityTypeBuilder<Entites.News.News> builder)
    {
        builder.HasOne<NewsPaper>()
            .WithMany(_ => _.NewsList)
            .HasForeignKey(_ => _.NewsPaperId);
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(_ => _.Author).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.Text).IsRequired();
        builder.Property(_ => _.Weight).IsRequired();
        builder.Property(_ => _.Views).HasDefaultValue(0);
        builder.Property(_ => _.NewsPaperId).IsRequired();

    }
}