using FluentAssertions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Xunit;

namespace Journalism.Spec.Tests.Author.NewsPapers.GetAllNewsPapersSpecTests;
[Scenario("مشاهده فهرست روزنامه ها")]
[Story("",
    AsA = "یک خبرنگار" ,
    IWantTo = "فهرست روزنامه ها را مشاهده کنم.",
    InOrderTo = "بتوانم خبر هایم را در ان ثبت کنم.")]
public class GetAllNewsPapersSpecTest : BusinessIntegrationTest
{
    private readonly AuthorNewsPapersService _sut;
    private List<GetNewsPapersDto> _act;

    public GetAllNewsPapersSpecTest()
    {
        _sut = AuthorNewsPaperServiceFactory.Create(SetupContext);
    }

    [Given("یک روزنامه با عنوان \"کریم\" در فهرست روزنامه ها وجود دارد.")]
    public void Given()
    {
        var newspaper = new NewsPaperBuilder().WithTitle("کریم").Build();
        DbContext.Save(newspaper);
    }

    [When("درخواست مشاهده فهرست روزنامه ها را ثبت میکنم.")]
    public async Task When()
    {
        _act = _sut.GetAll();
    }

    [Then("بنابراین : باید یک روزنامه با عنوان مذکور را مشاهده کنم.")]
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