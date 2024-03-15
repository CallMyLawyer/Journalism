using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;

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

    public List<GetPublishedNewspapersDto> GetAll()
    {
        
        var all = _context.PublishedNewsPapers.Select(_ => new GetPublishedNewspapersDto()
        {
            Id = _.Id,
            NewsPaper = _.NewsPaper,
            Categories = _.NewsPaper.Categories,
            Published = _.Published
        }).ToList();
        return all;
    }

    public void DefaultWeightsAranged(int id)
    {
        var publishedNewspaper = _context.PublishedNewsPapers.First(_ => _.Id == id);
        var newspaper = _context.NewsPapers.Include(newsPaper => newsPaper.Categories).First(_ => _.Id == publishedNewspaper.NewsPaper.Id);
        var categories = newspaper.Categories;
        foreach (var item in categories)
        {
            item.Weight = item.DefaultWeight;
        }
    }
}