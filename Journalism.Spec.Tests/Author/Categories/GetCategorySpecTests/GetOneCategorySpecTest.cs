using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Entites.Tags;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;
using Xunit;

namespace Journalism.Spec.Tests.Author.Categories.GetCategorySpecTests;
[Scenario("مشاهده اطلاعات یک دسته بندی بر اساس نام")]
[Story("" , 
    AsA = "نویسنده خبر",
    IWantTo = "یک دسته بندی از فهرست دسته بندی ها را مشاهده کنم." ,
    InOrderTo = "تا بتوانم از تگ های ان در خبر هایم استفاده کنم.")]
public class GetOneCategorySpecTest : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;
    private Category _karim;
    private IEnumerable<GetCategoryDto> _act;

    public GetOneCategorySpecTest()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }

    [Given("فهرست دسته بندی ها دارای یک دسته بندی با نام \"کریم\" و وزن \"20\" و تگ \"اختراعات\" است.")]
    public void Given()
    {
        _karim = new CategoryBuilder()
            .WithWeight(20).Build();
        DbContext.Save(_karim);
        var tag = new TagBuilder().WithTitle("اختراعات")
            .WithCategoryId(_karim.Id).Build();
        _karim.Tags?.Add(tag);
        DbContext.Save(tag);
    }

    [When("درخواست دیدن دسته بندی ای با نام دسته بندی مذکور را ثبت میکنم.")]
    public async Task When()
    {
        _act = _sut.GetOne(_karim.Id);
    }

    [Then(" باید یک دسته بندی با نام \"کریم\" و وزن \"20\" و تگ \"اختراعات\" را مشاهده کنم.")]
    public void Then()
    {
        _act.First().Tags?.First().Title.Should().Be("اختراعات");
        _act.First().Title.Should().Be("کریم");
        _act.First().Weight.Should().Be(20);
    }
    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _=> Given(),
            _=> When().Wait() ,
            _=> Then());
    }
}