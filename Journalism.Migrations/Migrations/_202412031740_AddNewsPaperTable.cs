using FluentMigrator;

namespace Migrations.Migrations;
[Migration(202412031740)]
public class _202412031740_AddNewsPaperTable : Migration
{
    public override void Up()
    {
        Create.Table("NewsPapers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(50).NotNullable()
            .WithColumn("Weight").AsInt32().Nullable()
            .WithColumn("Views").AsInt32().Nullable()
            .WithColumn("PublishedAt").AsDateTime().Nullable()
            .WithColumn("NewsWeight").AsInt32().Nullable();
    }

    public override void Down()
    {
        Delete.Table("NewsPapers");
    }
}