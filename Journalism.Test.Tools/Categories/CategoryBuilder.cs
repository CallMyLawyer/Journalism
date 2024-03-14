using Journalism.Entites.Categories;
using Journalism.Entites.Tags;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Journalism.Test.Tools.Categories;

public class CategoryBuilder
{
    private readonly Category _category;

    public CategoryBuilder()
    {
        _category = new Category()
        {
            Views = 1,
          Title = "کریم",
          Weight = 100,
          NewsPaperId = 1,
          Tags = new List<Tag?>()
          
        };
        
    }

    public CategoryBuilder WithTitle(string title)
    {
        _category.Title = title;
        return this;
    }

    public CategoryBuilder WithId(int id)
    {
        _category.Id = id;
        return this;
    }

    public CategoryBuilder WithNewsPaperId(int id)
    {
        _category.NewsPaperId = id;
        return this;
    }

    public CategoryBuilder WithViews(int views)
    {
        _category.Views = views;
        return this;
    }

    public CategoryBuilder WithTag(Tag tag)
    {
        _category.Tags?.Add(tag);
        return this;
    }

    public CategoryBuilder WithWeight(int weight)
    {
        _category.Weight = weight;
        return this;
    }

    public Category Build()
    {
        return _category;
    }

}