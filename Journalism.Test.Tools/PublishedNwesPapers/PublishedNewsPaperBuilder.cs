using Journalism.Entites.NewsPapers;
using Journalism.Entites.PublishedNewsPaper;

namespace Journalism.Test.Tools.PublishedNwesPapers;

public class PublishedNewsPaperBuilder
{
    private PublishedNewsPaper _publishedNewsPaper;

    public PublishedNewsPaperBuilder()
    {
        _publishedNewsPaper = new PublishedNewsPaper()
        {
            NewsPaper = new NewsPaper(),
            Published = false,
        };
    }

    public PublishedNewsPaperBuilder WithNewsPaper(NewsPaper newspaper)
    {
        _publishedNewsPaper.NewsPaper = newspaper;
        return this;
    }

    public PublishedNewsPaperBuilder WithId(int id)
    {
        _publishedNewsPaper.NewsPaper.Id = id;
        return this;
    }

    public PublishedNewsPaperBuilder WithPublished(bool mio)
    {
        _publishedNewsPaper.Published = mio;
        return this;
    }

    public PublishedNewsPaper Build()
    {
        return _publishedNewsPaper;
    }
}