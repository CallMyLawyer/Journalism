using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.PublishedNewsPapers.Contracts;

public interface PublishedNewsPapersRepository : Repository
{
    void Add(PublishedNewsPaper newsPaper);
    bool IsAnyNewsPaperPublishedToDay();
    bool NewsPaperPublishedBefore(int id);
    List<GetPublishedNewspapersDto> GetAll();
}