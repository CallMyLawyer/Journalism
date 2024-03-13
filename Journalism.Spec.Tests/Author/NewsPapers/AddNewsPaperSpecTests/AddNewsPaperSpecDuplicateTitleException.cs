using FluentAssertions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Xunit;

namespace Journalism.Spec.Tests.Author.NewsPapers.AddNewsPaperSpecTests;
[Scenario("عدم ثبت روزنامه با عنوان تکراری")]
[Story("",
    AsA = "خبرنگار",
    IWantTo = "یک روزنامه با عنوان تکراری ثبت کنم",
    InOrderTo = "خطایی با عنوان\"خطا!روزنامه ای با این عنوان قبلا در فهرست روزنامه ها ثبت شده است!\" را دریافت کنم.")]
public class AddNewsPaperSpecDuplicateTitleException : BusinessIntegrationTest
{
    private readonly AuthorNewsPapersService _sut;
    private Func<Task> _act;

    public AddNewsPaperSpecDuplicateTitleException()
    {
        _sut = AuthorNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given(" فهرست روزنامه دارای یک روزنامه با عنوان \"کریم\" است.")]
    public void Given()
    {
        var karim = new NewsPaperBuilder()
            .WithTitle("کریم").Build();
        DbContext.Save(karim);
    }

    [When("یک روزنامه با عنوان \"کریم\" را اضافه میکنم.")]
    public async Task When()
    {
        var newKarim = new AddNewsPaperDto
        {
            Title = "کریم"
        };
        _act =() => _sut.Add(newKarim);
    }

    [Then("باید خطایی با عنوان" +
          "\"خطا!روزنامه ای با این عنوان قبلا در فهرست روزنامه ها ثبت شده است!\"" +
          " را دریافت کنم.")]
    public void Then()
    {
        _act.Should().ThrowExactlyAsync<ThisTitleAlreadyExistsException>();
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