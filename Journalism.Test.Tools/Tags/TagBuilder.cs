using Journalism.Entites.Tags;

namespace Journalism.Test.Tools.Tags;

public class TagBuilder
{
    private Tag _tag;

    public TagBuilder()
    {
        _tag = new Tag()
        {
            Title = "کریم",
            CategoryId = 1,
        };
    }

    public TagBuilder WithTitle(string title)
    {
        _tag.Title = title;
        return this;
    }

    public TagBuilder WithCategoryId(int categoryId)
    {
        _tag.CategoryId = categoryId;
        return this;
    }

    public Tag Build()
    {
        return _tag;
    }
}