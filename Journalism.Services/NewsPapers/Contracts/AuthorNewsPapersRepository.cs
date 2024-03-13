using Journalism.Entites.NewsPapers;
using Journalism.Services.NewsPapers.Contracts.Dtos;

namespace Journalism.Services.NewsPapers.Contracts;

public interface AuthorNewsPapersRepository
{
    void Add(NewsPaper newsPaper);
    bool DuplicateTitle(string title);
    List<GetNewsPapersDto> GetAll();
    void Publish(NewsPaper newsPaper);
}