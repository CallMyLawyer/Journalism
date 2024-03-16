using FluentAssertions;
using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.Managment.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Managment;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Xunit;

namespace Journalism.Spec.Tests.Managment;
[Scenario("مشاهده بربازدیدترین روزنامه منتشرشده")]
[Story("",
    AsA = "مدیر",
    IWantTo = "روزنامه ای که بیشترین تعداد بازدید را داشته است مشاهده کنم.",
    InOrderTo = "تا از تگ ها و دسته بندی های ان در اینده بیشتر استفاده کنم.")]
public class ManagerGetTheNewsPaperWithMustViewsSpecTest : BusinessIntegrationTest
{
    private readonly ManagerService _sut;
    private GetPublishedNewspapersDto _act;

    public ManagerGetTheNewsPaperWithMustViewsSpecTest()
    {
        _sut = ManagerServiceFactory.Create(SetupContext);
    }

    [Given("فهرست روزنامه های منتشر شده دارای روزنامه ای با عنوان \"کریم\" است و تعداد بازدید \"1\" است " +
           "و در فهرست روزنامه های منتشر شده روزنامه دیگری با عنوان \"کریم2\" و تعداد بازدید \"5\" وجود دارد.")]
    public void Given()
    {
        var newsPaper1 = new NewsPaperBuilder()
            .WithViews(1)
            .WithTitle("کریم").WithNewsWeight(100)
            .WithWeight(100).Build();
        DbContext.Save(newsPaper1);
        var publish1 = new PublishedNewsPaperBuilder()
            .WithNewsPaper(newsPaper1).WithPublished(true).Build();
        DbContext.Save(publish1);
        
        
        var newsPaper2 = new NewsPaperBuilder()
            .WithViews(5)
            .WithTitle("کریم2").WithNewsWeight(100)
            .WithWeight(100).Build();
        DbContext.Save(newsPaper2);
        var publish2= new PublishedNewsPaperBuilder()
            .WithNewsPaper(newsPaper2).WithPublished(true).Build();
        DbContext.Save(publish2);
        
    }

    [When("درخواست مشاهده روزنامه با بیشترین تعداد بازدید را میدهم.")]
    public async Task When()
    {
        _act = _sut.GetTheNewsPaperWithMustViews();
    }

    [Then("باید روزنامه ای با عنوان \"کریم2\" . تعداد بازدید \"5\" را مشاهده کنم.")]
    public void Then()
    {
        _act.NewsPaper.Title.Should().Be("کریم2");
        _act.NewsPaper.Views.Should().Be(5);
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