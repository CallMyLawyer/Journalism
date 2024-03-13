using System.Runtime.InteropServices.ComTypes;
using Journalism.Entites.Categories;
using Journalism.Entites.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journalism.Persistence.EF.Categories;

public class CategoryEntityMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).IsRequired();
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.Views);
        builder.Property(_ => _.Weight).IsRequired();
    }
}