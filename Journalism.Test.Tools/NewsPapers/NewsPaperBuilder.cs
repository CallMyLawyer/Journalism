using Journalism.Entites.Categories;
using Journalism.Entites.News;
using Journalism.Entites.NewsPapers;

namespace Journalism.Test.Tools.NewsPapers;

public class NewsPaperBuilder
{
    private NewsPaper _newsPaper;

    public NewsPaperBuilder()
    {
        _newsPaper = new NewsPaper()
        {
            Title = "karim",
            NewsList = new List<Entites.News.News?>(),
            Categories= new List<Category?>(),
            PublishedAt = null,
            Views = 1,
        };
    }

    public NewsPaperBuilder WithTitle(string title)
    {
        _newsPaper.Title = title;
        return this;
    }

    public NewsPaperBuilder WithWeight(int weight)
    {
        _newsPaper.Weight = weight;
        return this;
    }

    public NewsPaperBuilder WithCategoryId(int id)
    {
        return this;
    }

    public NewsPaperBuilder WithNewsWeight(int weight)
    {
        _newsPaper.NewsWeight = weight;
        return this;
    }

    public NewsPaperBuilder WithViews(int view)
    {
        _newsPaper.Views = view;
        return this;
    }

    public NewsPaper Build()
    {
        return _newsPaper;
    }
}