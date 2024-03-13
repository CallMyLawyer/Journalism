using Journalism.Entites.Tags;
using Journalism.Services.Tags.Contracts.Dtos;

namespace Journalism.Services.Tags.Contracts;

public interface AuthorTagRepository
{
    void Add(Tag tag);
    bool DuplicateTitle(string title);
    List<GetTagDto> GetAll();
    GetTagDto GetOne(int id);
    void Delete(int id);
    bool IsExistTagId(int id);
}