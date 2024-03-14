using FluentAssertions;
using Journalism.Entites.NewsPapers;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Xunit;
using Xunit.Sdk;

namespace Journalism.Spec.Tests.Author.NewsPapers.GetAllNewsPapersSpecTests;
[Scenario("مشاهده یک روزنامه")]
[Story("",
    AsA = "خبرنگار",
    IWantTo = "یک روزنامه را مشاهده کنم",
    InOrderTo = "خبر خود را در ان روزنامه ثبت کنم.")]
public class GetOneNewspaperSpecTest : BusinessIntegrationTest
{
    private readonly AuthorNewsPapersService _sut;
    private IQueryable<GetNewsPapersDto> _act;
    private NewsPaper _newsPaper;

    public GetOneNewspaperSpecTest()
    {
        _sut = AuthorNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given("روزنامه ای با نام \"کریم\" در فهرست روزنامه ها وجود دارد.")]
    public void Given()
    {
        _newsPaper = new NewsPaperBuilder()
            .WithTitle("کریم").Build();
        DbContext.Save(_newsPaper);
    }

    [When(" درخوایت مشاهده روزنامه مذکور را ثبت میکنم.")]
    public async Task When()
    {
        _act = _sut.GetOne(_newsPaper.Id);
    }

    [Then(" باید روزنامه ای با نام \"کریم\" را مشاهده کنم.")]
    public void Then()
    {
        _act.First().Title.Should().Be("کریم");
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