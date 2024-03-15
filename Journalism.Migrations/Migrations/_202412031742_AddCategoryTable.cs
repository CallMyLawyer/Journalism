using FluentMigrator;

namespace Migrations.Migrations;
[Migration(202412031742)]
public class _202412031742_AddCategoryTableb : Migration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(50).NotNullable()
            .WithColumn("Weight").AsInt32().NotNullable()
            .WithColumn("Views").AsInt32()
            .WithColumn("NewsPaperId").AsInt32().Nullable()
            .WithColumn("DefaultWeight").AsInt32().Nullable();
    }

    public override void Down()
    {
        Delete.Table("Categories");
    }
}