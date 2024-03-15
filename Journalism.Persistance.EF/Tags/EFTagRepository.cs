using System.Diagnostics;
using Journalism.Entites.Tags;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Persistence.EF.Tags;

public class EFTagRepository : AuthorTagRepository
{
    private readonly EFDataContext _context;

    public EFTagRepository(EFDataContext context)
    {
        _context = context;
    }
    public void Add(Tag tag)
    {
        _context.Tags.Add(tag);
        var category = _context.Categories.Include(_=>_.Tags).First(_ => _.Id == tag.CategoryId);
        category.Tags?.Add(tag);
    }

    public bool DuplicateTitle(string title)
    {
        if (_context.Tags.Any(_=>_.Title==title))
        {
            return true;
        }

        return false;
    }

    public List<GetTagDto> GetAll()
    {
        var tags = _context.Tags.Select(_ => new GetTagDto()
        {
            Id = _.Id,
            Title = _.Title,
            CategoryId = _.CategoryId

        }).ToList();
        return tags;
    }

    public GetTagDto GetOne(int id)
    {
        var tags = _context.Tags.Select(_ => new GetTagDto()
        {
            Id = _.Id,
            Title = _.Title,
            CategoryId = _.CategoryId

        }).ToList();
        var tag = tags.First(_ => _.Id == id);
        return tag;
    }

    public void Delete(int id)
    {
        var tag = _context.Tags.First(_ => _.Id == id);
        _context.Tags.Remove(tag);
    }

    public bool IsExistTagId(int id)
    {
        if (_context.Tags.Any(_=>_.Id==id))
        {
            return false;
        }

        return true;
    }

    public async Task AddTagToNews(Tag tag , int? newsId)
    {
        var news = _context.News.First(_ => _.Id == newsId);
        news.Tags?.Add(tag);
    }
    public bool ExistTag(int id, string title)
    {
        var category = _context.Categories.Include(category => category.Tags).First(_ => _.Id == id);
        if (category.Tags.Any(_=>_.Title==title))
        {
            return false;
        }

        return true;
    }
}