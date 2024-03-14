using Journalism.Services.NewsPapers.Contracts.Dtos;

namespace Journalism.Services.NewsPapers.Contracts;

public interface AuthorNewsPapersService
{
    Task Add(AddNewsPaperDto dto);
    List<GetNewsPapersDto> GetAll();
    Task AddCategoryToNewspaper(int categoryId, int newspaperId);
    IQueryable<GetNewsPapersDto> GetOne(int id);
}