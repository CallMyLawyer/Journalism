using Journalism.Entites.Categories;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Categories.Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Persistence.EF.Categories;

public class EFCategoryRepository :  AuthorCategoryRepository
{
    private readonly EFDataContext _context;

    public EFCategoryRepository(EFDataContext context)
    {
        _context = context;
    }
    public void Add(Category category)
    {
        _context.Categories.Add(category);
    }

    public bool IsExistTitle(string title)
    {
        if ( _context.Categories.Any(_=>_.Title==title))
        {
            return true;
        }

        return false;
    }

    public List<GetCategoryDto> GetAll()
    {
        var categories = _context.Categories.Select(_ => new GetCategoryDto()
        {
            Id = _.Id,
            Title = _.Title,
            Tags = _.Tags,
            Views = _.Views,
            Weight = _.Weight,

        }).ToList();
        return categories;
    }

    public IEnumerable<GetCategoryDto> GetOne(int id)
    {
        var categories = _context.Categories.Select(_ => new GetCategoryDto()
        {
            Id = _.Id,
            Title = _.Title,
            Tags = _.Tags,
            Views = _.Views,
            Weight = _.Weight,

        }).ToList(); 
        var category = categories.Where(_ => _.Id == id);
        return category;
    }

    public bool IsExistId(int id)
    {
        if (_context.Categories.Any(_=>_.Id==id))
        {
            return false;
        }

        return true;
    }

    public void Delete(int id)
    {
       var category =  _context.Categories.First(_ => _.Id == id);
       _context.Categories.Remove(category);
    }

    public bool TagExistInCategory(int id)
    {
        var category = _context.Categories.First(_ => _.Id == id);
        if (category.Tags!=null)
        {
            return true;
        }

        return false;
    }

    public Category FindCategory(int id)
    {
        var category = _context.Categories.First(_ => _.Id == id);
        return category;
    }
}