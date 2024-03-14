using Journalism.Entites.PublishedNewsPaper;

namespace Journalism.Services.PublishedNewsPapers.Contracts;

public interface PublishedNewsPapersRepository
{
    void Add(PublishedNewsPaper newsPaper);
    bool IsAnyNewsPaperPublishedToDay();
    bool NewsPaperPublishedBefore(int id);
}