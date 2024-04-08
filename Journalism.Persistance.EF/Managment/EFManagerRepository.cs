using Journalism.Entites.Tags;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Managment.Contracts;
using Journalism.Services.Managment.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Journalism.Persistence.EF.Managment;

public class EFManagerRepository : ManagerRepository 
{
    private readonly EFDataContext _context;

    public EFManagerRepository(EFDataContext context)
    {
        _context = context;
    }

    public GetPublishedNewspapersDto GetTheNewsPaperWithMustViews()
    {
        var max = 0;
        var views = _context.PublishedNewsPapers.Select(_ => _.NewsPaper.Views).ToList();
        foreach (var view in views)
        {
            if (view>max)
            {
                max = view;
            }
        }

        var newspaper = _context.PublishedNewsPapers
            .Include(publishedNewsPaper => publishedNewsPaper.NewsPaper)
            .First(_ => _.NewsPaper.Views == max);
        var get = new GetPublishedNewspapersDto()
        {
            Id = newspaper.Id,
            NewsPaper = newspaper.NewsPaper,
            Published = newspaper.Published
        };
        return get;
    }

    public GetCategoryDto2 GetTheCategoryWithMustViews()
    {
        var max = 0;
        var categories = _context.Categories
            .Include(category => category.Tags).ToList();
        foreach (var category in categories)
        {
            if (category.Views>max)
            {
                max = category.Views;
            }
        }

        var getCategory = categories.First(_ => _.Views == max);
        var get = new GetCategoryDto2()
        {
            Category = getCategory
        };
        return get;
    }

    public GetNewsDto GetTheNewsWithMustViews()
    {
        var max = 0;
        var news = _context.News
            .Include(category => category.Tags).ToList();
        foreach (var News in news)
        {
            if (News.Views>max)
            {
                max = News.Views;
            }
        }

        var getNews = news.First(_ => _.Views == max);
        var get = new GetNewsDto()
        {
             News = getNews
        };
        return get; 
    }

}