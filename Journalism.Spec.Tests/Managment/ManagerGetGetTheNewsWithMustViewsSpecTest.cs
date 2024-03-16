using FluentAssertions;
using Journalism.Entites.NewsPapers;
using Journalism.Services.Managment.Contracts;
using Journalism.Services.Managment.Contracts.Dtos;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Managment;
using Journalism.Test.Tools.News;
using Journalism.Test.Tools.NewsPapers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace Journalism.Spec.Tests.Managment;

[Scenario(" مشاهده بربازدیدترین خبر")]
[Story("",
    AsA = "مدیر",
    IWantTo = "خبری که بیشتریت تعداد بازدید را دارد مشاهده کنم.",
    InOrderTo = "تا به نویسنده ان خبر باداش دهم.")]
public class ManagerGetGetTheNewsWithMustViewsSpecTest : BusinessIntegrationTest
{
    private readonly ManagerService _sut;
    private GetNewsDto _act;

    public ManagerGetGetTheNewsWithMustViewsSpecTest()
    {
        _sut = ManagerServiceFactory.Create(SetupContext);
    }

    [Given("در فهرست خبرها خبری با عنوان \"کریم\" و تعداد بازدید \"2\" وجود دارد " +
           "و خبر دیگری با عنوان \"کریم2\" و تعداد بازدید\"5\" وجود دارد.")]
    public void Given()
    {
        var newspaper = new NewsPaperBuilder().WithWeight(100).Build();
        DbContext.Save(newspaper);
        var news1 = new NewsBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم").WithViews(2).Build();
        DbContext.Save(news1);
        var news2 = new NewsBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم2").WithViews(5).Build();
        DbContext.Save(news2);
    }

    [When("درخواست مشاهده خبری با بیشترین تعداد بازدید را میدهم.")]
    public async Task When()
    {
        _act = _sut.GetTheNewsWithMustViews();
    }

    [Then("باید خبری با عنوان \"کریم2\" و تعداد بازدید \"5\" را مشاهده کنم.")]
    public void Then()
    {
        _act.News.Title.Should().Be("کریم2");
        _act.News.Views.Should().Be(5);
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