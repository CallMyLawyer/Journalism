using FluentAssertions;
using Journalism.Entites.NewsPapers;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Exceptions;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Xunit;

namespace Journalism.Spec.Tests.Author.PublishNewsPaper;
[Scenario("عدم انتشار روزنامه ای که وزنش 100 نیست")]
[Story("" ,
    AsA = "یک منتشر کننده روزنامه" ,
    IWantTo = "روزنامه ای را منتشر کنم که وزنش 100 نیست",
    InOrderTo = "خطایی با عنوان \"خطا! وزن روزنامه برای انتشار باید 100 باشد!\" را دریافت کنم.")]
public class PublishANewspaperWeightIsnt100SpecTestException : BusinessIntegrationTest
{
    private readonly PublishedNewsPapersService _sut;
    private NewsPaper _newsPaper;
    private Func<Task> _act;

    public PublishANewspaperWeightIsnt100SpecTestException()
    {
        _sut = PublishedNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given("فهرست روزنامه های منتشر شده خالی است " +
           "و یک روزنامه با عنوان \"کریم\" و وزن \"80\" در فهرست روزنامه ها وجود دارد.")]
    public void Given()
    {
        _newsPaper = new NewsPaperBuilder()
            .WithTitle("کریم").WithWeight(80).Build();
        DbContext.Save(_newsPaper);
    }

    [When(" روزنامه مذکور را منتشر میکنم.")]
    public async Task When()
    {
        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = _newsPaper.Id,
            Published = false
        };
        _act = () => _sut.Add(dto);
    }
    [Then("خطایی با عنوان \"خطا! وزن روزنامه برای انتشار باید 100 باشد!\" را دریافت کنم.")]
    public void Then()
    {
        _act.Should().ThrowExactlyAsync<NewsPaperWeightMustBe100ForPublishException>();
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