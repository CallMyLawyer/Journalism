using Journalism.Entites.NewsPapers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journalism.Persistence.EF.NewsPapers;

public class NewsPaperEntityMap : IEntityTypeConfiguration<NewsPaper>
{
    public void Configure(EntityTypeBuilder<NewsPaper> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.Weight);
        builder.Property(_ => _.Views);
        builder.Property(_ => _.PublishedAt);
        builder.Property(_ => _.CategoryId).IsRequired();
    }
}