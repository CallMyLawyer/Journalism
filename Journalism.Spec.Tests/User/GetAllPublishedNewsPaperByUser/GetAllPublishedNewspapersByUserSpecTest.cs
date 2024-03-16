using FluentAssertions;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Exceptions;
using Journalism.Services.Users.Contracts;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Journalism.Test.Tools.Users;
using Xunit;

namespace Journalism.Spec.Tests.User.GetAllPublishedNewsPaperByUser;
[Scenario("مشاهده فهرست روزنامه های منتشر شده")]
[Story("",
    AsA = "کاربر",
    IWantTo = "فهرست روزنامه های منتشر شده را مشاهده کنم",
    InOrderTo = "از بین انها روزنامه ای را انتخاب کنم")]
public class GetAllPublishedNewspapersByUserSpecTest : BusinessIntegrationTest
{
    private readonly UserService _sut;
    private List<GetPublishedNewspapersDto> _act;
    private NewsPaper _newspaper;

    public GetAllPublishedNewspapersByUserSpecTest()
    {
        _sut = UserServiceFactory.Create(SetupContext);
    }

    [Given("یک روزنامه با عنوان " +
           "\"کریم\" در فهرست روزنامه های منتشر شده وجود دارد که تعداد بازدید های ان صفر است.")]
    public void Given()
    {
        var category = new CategoryBuilder().WithWeight(10).WithTitle("mio").Build();
        DbContext.Save(category);
        _newspaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).WithTitle("کریم").WithCategoryId(category.Id).Build();
        DbContext.Save(_newspaper);
        _newspaper.PublishedAt = DateTime.Today;
        var published = new PublishedNewsPaperBuilder()
            .WithNewsPaper(_newspaper).WithPublished(true).Build();
        DbContext.Save(published);
    }

    [When("فهرست روزنامه ها را مشاهده میکنم")]
    public async Task When()
    {
        _act = _sut.GetAll();
    }

    [Then(" باید روزنامه ای با عنوان و مشخصات مذکور را مشاهده کنم.")]
    public void Then()
    {
        _act.First().NewsPaper.Title.Should().Be("کریم");
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