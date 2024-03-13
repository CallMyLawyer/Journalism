using FluentMigrator;

namespace Migrations.Migrations;
[Migration(202412031747)]
public class _202412031747_AddTagTable : Migration
{
    public override void Up()
    {
        Create.Table("Tags")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("CategoryId").AsInt32().ForeignKey
                ("FK_Tags_Categories", "Categories", "Id")
            .NotNullable()
            .WithColumn("Title").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Tags");
    }
}