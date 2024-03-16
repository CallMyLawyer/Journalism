using Journalism.Entites.PublishedNewsPaper;
using Journalism.Entites.Users;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.Users.Contracts;
using Journalism.Services.Users.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Persistence.EF.Users;

public class EFUserRepository : UserRepository
{
    private readonly EFDataContext _context;

    public EFUserRepository(EFDataContext context)
    {
        _context = context;
    }

    public List<GetPublishedNewspapersDto> GetAll()
    {
        var publishedNewspapers =
            _context.PublishedNewsPapers.Select(_ => new GetPublishedNewspapersDto()
            {
                Id = _.Id,
                NewsPaper = _.NewsPaper,
                Published = _.Published

            }).ToList();
        return publishedNewspapers;
    }

    public GetPublishedNewspapersDto GetOne(FilterByIdDto title)
    {
        var publishedNewspapers =
            _context.PublishedNewsPapers.Select(_ => new GetPublishedNewspapersDto()
            {
                Id = _.Id,
                NewsPaper = _.NewsPaper,
                Published = _.Published

            }).ToList();
        var newspaper = publishedNewspapers.First(_ => _.NewsPaper.Id == title.Id);
        var newsPaper = _context.NewsPapers.Include(newsPaper => newsPaper.Categories)
            .Include(newsPaper => newsPaper.NewsList).First(_ => _.Id == title.Id);
        return newspaper;
    }
}