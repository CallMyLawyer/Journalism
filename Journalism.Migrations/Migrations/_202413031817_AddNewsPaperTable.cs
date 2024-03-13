using FluentMigrator;

namespace Migrations.Migrations;
[Migration(202413031817)]
public class _202413031817_AddNewsPaperTable : Migration
{
    public override void Up()
    {
        Create.Table("NewsPapers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(50).NotNullable()
            .WithColumn("Weight").AsInt32().Nullable()
            .WithColumn("Views").AsInt32().Nullable()
            .WithColumn("PublishedAt").AsDateTime().Nullable()
            .WithColumn("CategoryId").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("NewsPapers");
    }
}