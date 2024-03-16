using FluentAssertions;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Managment.Contracts;
using Journalism.Services.Managment.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Managment;
using Journalism.Test.Tools.NewsPapers;
using Xunit;

namespace Journalism.Spec.Tests.Managment;
[Scenario("مشاهده بربازدیدترین دسته بندی")]
[Story("",
    AsA = "مدیر",
    IWantTo = "دسته بندی ای که بیشترین تعداد بازدید را دارد مشاهده کنم.",
    InOrderTo = "از ان در خبر های اینده بیشتر استفاده کنم.")]
public class ManagerGetTheCategoryWithMustViewsSpecTest : BusinessIntegrationTest
{
    private readonly ManagerService _sut;
    private GetCategoryDto2 _act;

    public ManagerGetTheCategoryWithMustViewsSpecTest()
    {
        _sut = ManagerServiceFactory.Create(SetupContext);
    }

    [Given("در فهرست دسته بندی ها دسته بندی تی با عنوان \"کریم\" و تعداد بازدید \"1\" وجود دارد " +
           "و دسته بندی دیگری با عنوان \"کریم2\" و تعداد بازدید \"12\" وجود دارد.")]
    public void Given()
    {
        var newspaper = new NewsPaperBuilder().Build();
        DbContext.Save(newspaper);
        var category1 = new CategoryBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم").WithViews(1).Build();
        DbContext.Save(category1);
        var category2 = new CategoryBuilder().WithNewsPaperId(newspaper.Id)
            .WithTitle("کریم2").WithViews(12).Build();
        DbContext.Save(category2);
    }

    [When("درخواست مشاهده دسته بندی با بیشترین تعداد بازدید را میدهم.")]
    public async Task When()
    {
        _act = _sut.GetTheCategoryWithMustViews();
    }

    [Then("باید دسته بندی ای با عنوان \"کریم2\" و تعداد بازدید \"12\" را مشاهده کنم.")]
    public void Then()
    {
        _act.Category.Title.Should().Be("کریم2");
        _act.Category.Views.Should().Be(12);
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