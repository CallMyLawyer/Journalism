using Journalism.Entites.Tags;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Tags.Contracts;

public interface AuthorTagRepository : Repository
{
    void Add(Tag tag);
    bool DuplicateTitle(string title);
    List<GetTagDto> GetAll();
    GetTagDto GetOne(int id);
    void Delete(int id);
    bool IsExistTagId(int id);
    Task AddTagToNews(Tag tag , int? newsId);
    bool ExistTag(int id , string title);
}