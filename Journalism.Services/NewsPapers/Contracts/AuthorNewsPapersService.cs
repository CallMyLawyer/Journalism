using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.NewsPapers.Contracts;

public interface AuthorNewsPapersService : Service
{
    Task Add(AddNewsPaperDto dto);
    List<GetNewsPapersDto> GetAll();
    Task AddCategoryToNewspaper(int categoryId, int newspaperId);
    IQueryable<GetNewsPapersDto> GetOne(int id);
}