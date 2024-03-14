using FluentAssertions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Xunit;

namespace Journalism.Spec.Tests.Author.NewsPapers.AddNewsPaperSpecTests;
[Scenario("ثبت یک روزنامه")]
[Story("",
    AsA = "یک خبرنگار",
    IWantTo = "روزنامه ای راثبت کنم",
    InOrderTo = "ان را انتشار دهم")]
public class AddNewsPaperSpecTest : BusinessIntegrationTest
{
    private readonly AuthorNewsPapersService _sut;

    public AddNewsPaperSpecTest()
    {
        _sut = AuthorNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given("فهرست روزنامه ها خالی است.")]
    public void Given(){}

    [When("روزنامه ای با نام \"کریم\" را به ان اضافه میکنم.")]
    public async Task When()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);
        var karim = new AddNewsPaperDto()
        {
            Title = "کریم",
        };
        await _sut.Add(karim);
    }

    [Then("باید یک روزنامه با نام \"کریم\" در فهرست روزنامه ها وجود داشته باشد.")]
    public void Then()
    {
        var act = ReadContext.NewsPapers.Single();

        act.Title.Should().Be("کریم");
    }

    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _=> Given(),
            _=> When().Wait(),
            _=> Then());
    }
}