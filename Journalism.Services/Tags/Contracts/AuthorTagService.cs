using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Tags.Contracts;

public interface AuthorTagService : Service
{
    Task Add(AddTagDto dto);
    List<GetTagDto> GetAll();
    GetTagDto GetOne(int id);
    Task Delete(int id);
    Task AddTagToNews(AddTagToNewsDto dto);
}