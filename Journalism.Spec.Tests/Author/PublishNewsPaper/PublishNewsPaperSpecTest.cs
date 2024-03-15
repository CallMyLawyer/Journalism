using FluentAssertions;
using Journalism.Entites.NewsPapers;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Journalism.Spec.Tests.Author.PublishNewsPaper;
[Scenario("انتشار روزنامه ")]
[Story("",
    AsA = "روزنامه نگار" ,
    IWantTo = "یک روزنامه را منتشر کنم" ,
    InOrderTo = "خبر هایم دیده شوند")]
public class PublishNewsPaperSpecTest : BusinessIntegrationTest
{
    private readonly PublishedNewsPapersService _sut;
    private NewsPaper _newsPaper;

    public PublishNewsPaperSpecTest()
    {
        _sut = PublishedNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given("یک روزنامه یا عنوان \"کریم\" و وزن \"100\" که وزن خبرهایش \"100\" است در فهرست روزنامه ها وجود دارد.")]
    public void Given()
    {
        _newsPaper = new NewsPaperBuilder()
            .WithTitle("کریم").WithWeight(100)
            .WithNewsWeight(100).Build();
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
        await _sut.Add(dto);
    }

    [Then("باید روزنامه ای با نام روزنامه مذکور در فهرست روزنامه های منتشر شده وجود داشته باشد.")]
    public void Then()
    {
        var act = ReadContext.PublishedNewsPapers.Include(publishedNewsPaper => publishedNewsPaper.NewsPaper).Single();

        act.NewsPaper.Title.Should().Be("کریم");
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