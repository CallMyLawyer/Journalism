using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Tags.Contracts.Dtos;

namespace Journalism.Services.Tags.Contracts;

public interface AuthorTagService
{
    Task Add(AddTagDto dto);
    List<GetTagDto> GetAll();
    GetTagDto GetOne(int id);
    Task Delete(int id);
    Task AddTagToNews(AddTagToNewsDto dto);
}