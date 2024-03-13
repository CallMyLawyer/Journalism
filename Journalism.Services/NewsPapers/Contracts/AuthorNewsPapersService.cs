using Journalism.Services.NewsPapers.Contracts.Dtos;

namespace Journalism.Services.NewsPapers.Contracts;

public interface AuthorNewsPapersService
{
    Task Add(AddNewsPaperDto dto);
    List<GetNewsPapersDto> GetAll();
}