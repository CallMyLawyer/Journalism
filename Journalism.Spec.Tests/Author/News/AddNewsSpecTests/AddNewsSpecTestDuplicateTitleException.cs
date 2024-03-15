using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.Tags;
using Journalism.Services.News.Contracts;
using Journalism.Services.News.Contracts.Dtos;
using Journalism.Services.News.Contracts.Exceptions;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.News;
using Journalism.Test.Tools.NewsPapers;
using Xunit;

namespace Journalism.Spec.Tests.Author.News.AddNewsSpecTests;
[Scenario("عدم ثبت خبر با عنوان تکراری")]
[Story("",
    AsA = "نویسنده خبر",
    IWantTo = "خبری با عنوان تکراری در فهرست خبرهای روزنامه ثبت کنم.",
    InOrderTo = "خطایی با عنوان عنوان \"این نام در حال حاضر در فهرست خبر ها وجود دارد!\" را مشاهده کنم")]
public class AddNewsSpecTestDuplicateTitleException : BusinessIntegrationTest
{
    private readonly AuthorNewsService _sut;
    private Func<Task> _act;
    private Category _category;
    private NewsPaper _newsPaper;

    public AddNewsSpecTestDuplicateTitleException()
    {
        _sut = NewsServiceFactory.Create(SetupContext);
    }

    [Given(" یک رزونامه با نام \"کریم\" " +
           "در فهرست روزنامه ها وجود دارد که در فهرست دسته بندی های ان یک دسته بندی با نام \"کریم\" " +
           "وجود دارد که این روزنامه دارای یک خبر با عنوان \"کریم\" است.")]
    public void Given()
    {
        _newsPaper = new NewsPaperBuilder().WithWeight(100).WithTitle("کریم").Build();
        DbContext.Save(_newsPaper);
        _category = new CategoryBuilder().WithWeight(20).WithTitle("کریم").Build();
        DbContext.Save(_category);
        var news = new NewsBuilder().WithTitle("کریم").WithAuthor("mio").WithNewsPaperId(_newsPaper.Id).Build();
        DbContext.Save(news);
    }

    [When("یک خبر با نام \"کریم \" را به فهرست خبر های روزنامه اضافه میکنم.")]
    public async Task When()
    {
        var karim = new AddNewsDto()
        {
            Title = "کریم",
            Author = "karim",
            CategoryId = _category.Id,
            NewsPaperId = _newsPaper.Id,
            Tags = new List<Tag?>(),
            Text = "karim",
            Views = 1,
            Weight = 10

        };

        _act = () => _sut.Add(karim);
    }

    [Then("باید خطایی با عنوان \"این نام در حال حاضر در فهرست خبر ها وجود دارد!\" را مشاهده کنم.")]
    public void Then()
    {
        _act.Should().ThrowExactlyAsync<NewsTitleAlreadyExistsInNewsListException>();
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