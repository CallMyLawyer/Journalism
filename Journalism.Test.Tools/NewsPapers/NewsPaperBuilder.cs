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
            NewsList = new List<News?>(),
            Category = null,
            CategoryId = 1,
            PublishedAt = null,
            Views = 1,
            Weight = 100
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
        _newsPaper.CategoryId = id;
        return this;
    }

    public NewsPaper Build()
    {
        return _newsPaper;
    }
}