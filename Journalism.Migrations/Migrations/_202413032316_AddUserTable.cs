using FluentMigrator;

namespace Migrations.Migrations;
[Migration(202413032316)]
public class _202413032316_AddUserTable : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity().Identity()
            .WithColumn("FullName").AsString(100).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}