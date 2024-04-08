using Journalism.Entites.NewsPapers;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.NewsPapers.Contracts;

public interface AuthorNewsPapersRepository : Repository
{
    void Add(NewsPaper newsPaper);
    bool DuplicateTitle(string title);
    List<GetNewsPapersDto> GetAll();
    void Publish(NewsPaper newsPaper);
    bool IsWeight100OrNot(int id);
    void AddCategoryToNewsPaper(int newsPaperId, int categoryId);
    NewsPaper FindNewsPaper(int id);
    IQueryable<GetNewsPapersDto> GetOne(int id);
    bool IsExistNewsPaperId(int id);
    bool IsNewsPaperNewsWeightsFull(int id);
}