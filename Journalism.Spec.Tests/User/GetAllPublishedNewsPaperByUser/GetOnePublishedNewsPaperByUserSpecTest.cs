using System.Runtime.CompilerServices;
using FluentAssertions;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.Users;
using Journalism.Services.Users.Contracts;
using Journalism.Services.Users.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Journalism.Test.Tools.Users;
using Xunit;

namespace Journalism.Spec.Tests.User.GetAllPublishedNewsPaperByUser;
[Scenario("مشاهده یک روزنامه منتشر شده")]
[Story("",
    AsA = "کاربر",
    IWantTo = "روزنامه مورد نظرم را ببینم",
    InOrderTo = "تعداد بازدید ان روزنامه زیاد شود.")]
public class GetOnePublishedNewsPaperByUserSpecTest : BusinessIntegrationTest
{
    private readonly UserService _sut;
    private NewsPaper _newspaper;
    private PublishedNewsPaper _published;
    private GetPublishedNewspapersDto _act;

    public GetOnePublishedNewsPaperByUserSpecTest()
    {
     _sut = UserServiceFactory.Create(SetupContext);   
    }

    [Given("فهرست روزنامه هاس منتشر شده دارای یک روزنامه" +
           " با نام \"کریم\" است که تعداد بازدید های ان 1 است.")]
    public void Given()
    {
        var category = new CategoryBuilder().WithWeight(10).WithTitle("mio").Build();
        DbContext.Save(category);
        _newspaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).WithCategoryId(category.Id).WithTitle("کریم").Build();
        DbContext.Save(_newspaper);
        _newspaper.PublishedAt = DateTime.Today;
        _published = new PublishedNewsPaperBuilder()
            .WithNewsPaper(_newspaper).WithPublished(true).Build();
        DbContext.Save(_published);
    }

    [When(" روزنامه مذکور را مشاهده میکنم.")]
    public async Task When()
    {
        var dto = new FilterByIdDto()
        {
            Id = _newspaper.Id
        };
        _act = _sut.GetOne(dto);
    }

    [Then("باید تعداد بازدید های روزنامه مذکور یکی افزایش یابد.")]
    public void Then()
    {
        _act.NewsPaper.Views.Should().Be(2);
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