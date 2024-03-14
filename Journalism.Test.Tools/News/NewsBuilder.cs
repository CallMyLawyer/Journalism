using Journalism.Entites.Tags;

namespace Journalism.Test.Tools.News;

public class NewsBuilder
{
    private Entites.News.News _news;

    public NewsBuilder()
    {
        _news = new Entites.News.News()
        {
            Title = "karim",
            Author = "mmd",
            NewsPaperId = 1,
            Tags = new List<Tag?>(),
            Text = "miobio",
            Views = 1,
            Weight = 10

        };
    }

    public NewsBuilder WithTitle(string title)
    {
        _news.Title = title;
        return this;
    }

    public NewsBuilder WithAuthor(string author)
    {
        _news.Author = author;
        return this;
    }

    public NewsBuilder WithNewsPaperId(int id)
    {
        _news.NewsPaperId = id;
        return this;
    }

    public NewsBuilder WithText(string text)
    {
        _news.Text = text;
        return this;
    }

    public NewsBuilder WithWeight(int weight)
    {
        _news.Weight = weight;
        return this;
    }

    public NewsBuilder WithViews(int views)
    {
        _news.Views = views;
        return this;
    }

    public Entites.News.News Build()
    {
        return _news;
    }
}