using FluentAssertions;
using Journalism.Entites.News;
using Journalism.Services.Managment.Contracts;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Managment;
using Journalism.Test.Tools.News;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Unit.Tests.Managment;

public class ManagerGetUnitTests : BusinessIntegrationTest
{
    private readonly ManagerService _sut;

    public ManagerGetUnitTests()
    {
        _sut = ManagerServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task Manager_get_the_news_with_must_views_properly()
    {

        var newspaper = new NewsPaperBuilder().WithWeight(100).Build();
        DbContext.Save(newspaper);
        var news1 = new NewsBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم").WithViews(2).Build();
        DbContext.Save(news1);
        var news2 = new NewsBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم2").WithViews(5).Build();
        DbContext.Save(news2);
        
        var act = _sut.GetTheNewsWithMustViews();
        
        act.News.Title.Should().Be("کریم2");
        act.News.Views.Should().Be(5);
    }

    [Fact]
    public async Task Manager_get_the_newspaper_with_must_views_properly()
    {
        var newsPaper1 = new NewsPaperBuilder()
            .WithViews(1)
            .WithTitle("کریم").WithNewsWeight(100)
            .WithWeight(100).Build();
        DbContext.Save(newsPaper1);
        var publish1 = new PublishedNewsPaperBuilder()
            .WithNewsPaper(newsPaper1).WithPublished(true).Build();
        DbContext.Save(publish1);
        
        
        var newsPaper2 = new NewsPaperBuilder()
            .WithViews(5)
            .WithTitle("کریم2").WithNewsWeight(100)
            .WithWeight(100).Build();
        DbContext.Save(newsPaper2);
        var publish2= new PublishedNewsPaperBuilder()
            .WithNewsPaper(newsPaper2).WithPublished(true).Build();
        DbContext.Save(publish2);
        
        var act = _sut.GetTheNewsPaperWithMustViews();
        
        act.NewsPaper.Title.Should().Be("کریم2");
        act.NewsPaper.Views.Should().Be(5);
    }

    [Fact]
    public async Task Manager_get_the_category_with_must_views_properly()
    {
        var newspaper = new NewsPaperBuilder().Build();
        DbContext.Save(newspaper);
        var category1 = new CategoryBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم").WithViews(1).Build();
        DbContext.Save(category1);
        var category2 = new CategoryBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم2").WithViews(12).Build();
        DbContext.Save(category2);
        
        var act = _sut.GetTheCategoryWithMustViews();
        
        act.Category.Title.Should().Be("کریم2");
        act.Category.Views.Should().Be(12);
    }
}