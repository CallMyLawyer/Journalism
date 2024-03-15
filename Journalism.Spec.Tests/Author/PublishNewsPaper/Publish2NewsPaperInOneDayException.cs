using FluentAssertions;
using Journalism.Entites.NewsPapers;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Exceptions;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using NuGet.Frameworks;
using Xunit;

namespace Journalism.Spec.Tests.Author.PublishNewsPaper;
[Scenario("عدم انتشار دو روزنامه در یک روز")]
[Story("" ,
    AsA = "یک نویسنده روزنامه" , 
    IWantTo = "در یک روز دو روزنامه را منتشر کنم",
    InOrderTo = "خطایی با عنوان  در یک روز فقط یک روزنامه میتواند منتشر شود!\"دریافت کنم.")]
public class Publish2NewsPaperInOneDayException : BusinessIntegrationTest
{
    private readonly PublishedNewsPapersService _sut;
    private Func<Task> _act;
    private NewsPaper _newsPaper;

    public Publish2NewsPaperInOneDayException()
    {
        _sut = PublishedNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given("یک رزونامه در فهرست روزنامه ها وجود دارد و این روزنامه امروز منتشر شده است.")]
    public void Given()
    {
        _newsPaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).Build();
        DbContext.Save(_newsPaper);
    }

    [When("روزنامه ای دیگر را منتشر میکنم.")]
    public async Task When()
    {
        var publish = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = _newsPaper.Id,
            Published = false
        };
        await _sut.Add(publish);
        var newspaper = new NewsPaperBuilder().WithTitle("mioooo")
            .WithWeight(100).WithNewsWeight(100).Build();
        DbContext.Save(newspaper);
        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = newspaper.Id,
            Published = false
        };
        _act = () => _sut.Add(dto);
    }

    [Then("باید خطایی با عنوان \"خطا! در یک روز فقط یک روزنامه میتواند منتشر شود!\" را دریافت کنم.")]
    public void Then()
    {
        _act.Should().ThrowExactlyAsync<ToDaysNewsPaperHasAlreadyPublishedGetReadyForTomorrowException>();
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