using Journalism.Entites.Categories;
using Journalism.Entites.NewsPapers;
using Journalism.Services.News.Contracts;
using Journalism.TaavContracts.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    public List<Entites.News.News> FindNews(int id)
    {
        var news = _context.News.Where(_ => _.Id == id).ToList();
        return news;
    }

    public bool DuplicateTitle(string title)
    {
        if (_context.News.Any(_=>_.Title==title))
        {
            return true;
        }

        return false;
    }
}