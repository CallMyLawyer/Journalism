using Journalism.Entites.Categories;
using Journalism.Entites.NewsPapers;
using Journalism.Services.Categories.Contracts.Dtos;

namespace Journalism.Services.Categories.Contracts;

public interface AuthorCategoryRepository
{
    void Add(Category category);
    bool IsExistTitle(string title);
    
    List<GetCategoryDto> GetAll();
    IEnumerable<GetCategoryDto> GetOne(int id);
    bool IsExistId(int id); 
    void Delete(int id);
    bool TagExistInCategory(int id);
    Category FindCategory(int id);
    bool WeightLessThan100(int? id);

}