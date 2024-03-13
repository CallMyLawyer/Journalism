using Journalism.Services.Categories.Contracts.Dtos;

namespace Journalism.Services.Categories.Contracts;

public interface AuthorCategoryService
{
        Task Add(AddCategoryDto dto);
        List<GetCategoryDto> GetAll();
        IEnumerable<GetCategoryDto> GetOne(int id);
        Task Delete(int id);
}