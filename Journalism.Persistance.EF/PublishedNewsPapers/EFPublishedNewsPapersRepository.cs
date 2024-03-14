using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Exceptions;

namespace Journalism.Persistence.EF.PublishedNewsPapers;

public class EFPublishedNewsPapersRepository : PublishedNewsPapersRepository
{
    private readonly EFDataContext _context;

    public EFPublishedNewsPapersRepository(EFDataContext context)
    {
        _context = context;
    }
    public void Add(PublishedNewsPaper newsPaper)
    {
        _context.PublishedNewsPapers.Add(newsPaper);
    }

    public bool IsAnyNewsPaperPublishedToDay()
    {
        if (_context.PublishedNewsPapers.Any(_=>_.NewsPaper.PublishedAt==DateTime.Today))
        {
            return true;
        }

        return false;
    }

    public bool NewsPaperPublishedBefore(int id)
    {
        var newsPaper = _context.NewsPapers.First(_ => _.Id == id);
        if (newsPaper.PublishedAt!=null)
        {
            return true;
        }

        return false;
    }
}