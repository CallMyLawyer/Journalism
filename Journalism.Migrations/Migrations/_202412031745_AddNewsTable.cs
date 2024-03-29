﻿
using FluentMigrator;
[Migration(202412031745)]
public class _202412031745_AddNewsTable : Migration
{
    public override void Up()
    {
        Create.Table("News")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Author").AsString(50).NotNullable()
            .WithColumn("Title").AsString(50).NotNullable()
            .WithColumn("Text").AsString().NotNullable()
            .WithColumn("Weight").AsInt32().NotNullable()
            .WithColumn("Views").AsInt32().Nullable()
            .WithColumn("NewsPaperId").AsInt32().ForeignKey
                ("FK_News_NewsPapers", "NewsPapers", "Id").NotNullable();
    }

    public override void Down()
    {
        Delete.Table("News");
    }
}