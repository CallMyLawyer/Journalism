using Journalism.Entites.Categories;
using Journalism.Entites.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journalism.Persistence.EF.Tags;

public class TagEntityMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasOne<Category>()
            .WithMany(_=>_.Tags)
            .HasForeignKey(_=>_.CategoryId);
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(_=>_.Title).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.CategoryId).IsRequired();
        builder.HasOne<Entites.News.News>().WithMany(_ => _.Tags)
            .HasForeignKey(_ => _.NewsId);
        builder.Property(_ => _.NewsId);
    }
}