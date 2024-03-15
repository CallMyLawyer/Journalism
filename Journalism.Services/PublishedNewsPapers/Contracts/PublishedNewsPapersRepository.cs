using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;

namespace Journalism.Services.PublishedNewsPapers.Contracts;

public interface PublishedNewsPapersRepository
{
    void Add(PublishedNewsPaper newsPaper);
    bool IsAnyNewsPaperPublishedToDay();
    bool NewsPaperPublishedBefore(int id);
    List<GetPublishedNewspapersDto> GetAll();
}