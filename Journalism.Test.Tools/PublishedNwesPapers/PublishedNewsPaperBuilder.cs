using Journalism.Entites.PublishedNewsPaper;

namespace Journalism.Test.Tools.PublishedNwesPapers;

public class PublishedNewsPaperBuilder
{
    private PublishedNewsPaper _publishedNewsPaper;

    public PublishedNewsPaperBuilder()
    {
        _publishedNewsPaper = new PublishedNewsPaper()
        {
            Published = false,
        };
    }
}