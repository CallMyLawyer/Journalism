using Journalism.Entites.Categories;
using Journalism.Services.News.Contracts;
using Microsoft.EntityFrameworkCore.Internal;

namespace Journalism.Persistence.EF.News;

public class EFNewsRepository : AuthorNewsRepository
{
    private readonly EFDataContext _context;

    public EFNewsRepository(EFDataContext context)
    {
        _context = context;
    }

    public void Add(Entites.News.News news)
    {
        _context.News.Add(news);
        var newsPaper = _context.NewsPapers.First(_ => _.Id == news.NewsPaperId);
        newsPaper.NewsList?.Add(news);
    }

    public Category FindCategory(int id)
    {
        return _context.Categories.First(_ => _.Id == id);
    }
}