using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.Tags;
using Journalism.Services.News.Contracts;
using Journalism.Services.News.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.News;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.Tags;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Journalism.Spec.Tests.Author.News.AddNewsSpecTests;
[Scenario("اضافه شدن یک خبر به لیست خبر های یک رزونامه")]
[Story("",
    AsA = "خبرنگار" ,
     IWantTo = "خبری را به روزنامه اضافه کنم." ,
    InOrderTo = "تا خبرم با روزنامه منتشر شود.")]
public class AddNewsSpecTest : BusinessIntegrationTest
{
    private readonly AuthorNewsService _sut;
    private NewsPaper _newsPaper;
    private Category _category;
    public AddNewsSpecTest()
    {
        _sut = NewsServiceFactory.Create(SetupContext);
    }

    [Given("یک روزنامه با نام \"کریم\" در فهرست رزونامه ها وجود دارد که فهرست خبرهایش خالی است." +
           "و یک دسته بندی در فهرست دسته بندی ها وجود دارد.")]
    public void Given()
    {
        _newsPaper = new NewsPaperBuilder()
            .WithTitle("کریم").Build();
        DbContext.Save(_newsPaper);
        _category = new CategoryBuilder().Build();
        DbContext.Save(_category);
    }

    [When(" یک خبر با نام \"کریم\" را به روزنامه مذکور اضافه میکنم.")]
    public async Task When()
    {
        var news = new AddNewsDto()
        {
            Title = "کریم",
            Author = "karim",
            Text = "miobio",
            Tags = new List<Tag?>(),
            Views = 1,
            Weight = 10,
            NewsPaperId = _newsPaper.Id,
            CategoryId = _category.Id

        };
        await _sut.Add(news);
    }

    [Then("فهرست خبر های روزنامه مذکور باید دارای یک خبر با عنوان \"کریم\" باشد")]
    public void Then()
    {
        var act = ReadContext.News.Single();
        var act2 = ReadContext.NewsPapers.Include(newsPaper => newsPaper.NewsList).Single();

        act2.NewsList.First().Title.Should().Be("کریم");
        act.Title.Should().Be("کریم");
    }

    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _=>Given(),
            _=>When().Wait(),
            _=>Then());
    }
    
}