using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Categories.Contracts;

public interface AuthorCategoryService : Service
{
        Task Add(AddCategoryDto dto);
        List<GetCategoryDto> GetAll();
        IEnumerable<GetCategoryDto> GetOne(int id);
        Task Delete(int id);
}