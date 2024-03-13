using Journalism.Entites.Users;
using Journalism.Services.Users.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journalism.Persistence.EF.Users;

public class UserEntityMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(_ => _.FullName).HasMaxLength(100).IsRequired();
    }
}