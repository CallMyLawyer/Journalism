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
[Scenario("عدم انتشار دوباره یک روزنامه")]
[Story("",
    AsA = "منتشر کننده خبر",
    IWantTo = "یک خبر که قبلا منتشر شده است را دوباره منتشر کنم.",
    InOrderTo = "خطایی با عنوان \"خطا! نمیتوان روزنامه ای که قبلا منتشر شده است را دوباره منتشر کرد!\" را دریافت کنم.")]
public class PublishANewsPaperTwiceException : BusinessIntegrationTest
{
    private readonly PublishedNewsPapersService _sut;
    private NewsPaper _newsPaper;
    private Func<Task> _act;

    public PublishANewsPaperTwiceException()
    {
        _sut = PublishedNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given("یک روزنامه در فهرست روزنامه ها وجود دارد و روزنامه مذکور یک بار منتشر شده است.")]
    public void Given()
    {
        _newsPaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).Build();
        DbContext.Save(_newsPaper);
    }

    [When(" روزنامه مذکور را بار دیگر منتشر میکنم.")]
    public async Task When()
    {
        var publish = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = _newsPaper.Id,
            Published = false
        };
        await _sut.Add(publish);
        _act = ()=> _sut.Add(publish);
    }

    [Then("باید خطایی با عنوان \"خطا! نمیتوان روزنامه ای که قبلا منتشر شده است را دوباره منتشر کرد!\" را دریافت کنم.")]
    public void Then()
    {
        _act.Should().ThrowExactlyAsync<ThisNewsPaperHasPublishedBeforeAndItCantPublishAgainWriteANewOneException>();
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