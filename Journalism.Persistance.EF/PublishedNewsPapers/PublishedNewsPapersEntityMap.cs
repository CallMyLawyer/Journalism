using Journalism.Entites.NewsPapers;
using Journalism.Entites.PublishedNewsPaper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journalism.Persistence.EF.PublishedNewsPapers;

public class PublishedNewsPapersEntityMap : IEntityTypeConfiguration<PublishedNewsPaper>
{
    public void Configure(EntityTypeBuilder<PublishedNewsPaper> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Published).HasDefaultValue(false).IsRequired();
    }
}