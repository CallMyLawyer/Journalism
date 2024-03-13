using FluentMigrator;

namespace Migrations.Migrations;
[Migration(202413042342)]
public class _202413032342_AddPublishedNewsPaperTable : Migration
{
    public override void Up()
    {
        Create.Table("PublishedNewsPpapers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Published").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("PublishedNewsPpapers");
    }
}