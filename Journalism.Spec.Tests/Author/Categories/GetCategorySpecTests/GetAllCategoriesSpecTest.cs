using FluentAssertions;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace Journalism.Spec.Tests.Author.Categories.GetCategorySpecTests;
[Scenario("مشاهده اطلاعات تمام دسته بندی ها ")]
[Story("",
    AsA = "یک نویسنده خبر" ,
    IWantTo = "فهرست دسته بندی ها را ببینم",
    InOrderTo = "تا از تگ انها در خبرهایم استغاده کنم.")]
public class GetAllCategoriesSpecTest : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;
    private List<GetCategoryDto> _act;

    public GetAllCategoriesSpecTest()
    {
       _sut = CategoryServiceFactory.Create(SetupContext); 
    }

    [Given("فهرست دسته بندی ها دارای سه دسته بندی با نام های" +
           " \"کریم\" با وزن \"20\" و" +
           " \"قاسم\" با وزن \"25\" و " +
           "\"هاشم\" با وزن \"30\" است.")]
    public void Given()
    {
        var karim = new CategoryBuilder()
            .WithTitle("کریم").WithWeight(20).Build();
        DbContext.Save(karim);
        
        var ghasem = new CategoryBuilder()
            .WithTitle("قاسم").WithWeight(25).Build();
        DbContext.Save(ghasem);

        var hashem = new CategoryBuilder()
            .WithTitle("هاشم").WithWeight(30).Build();
        DbContext.Save(hashem);
        
    }

    [When("درخواست دیدن فهرست دسته بندی ها را ثبت میکنم.")]
    public async Task When()
    {
        _act = _sut.GetAll();
    }

    [Then("باید فهرستی از دسته بندی ها را مشاهده کنم که شامله تنها سه دسته بندی به نام های" +
          " \"کریم\" با وزن\"20\"  " +
          "و \"قاسم\" با وزن \"25\" و " +
          "\"هاشم\" با وزن \"30\" باشد.")]
    public void Then()
    {
        _act.First().Title.Should().Be("کریم");
        _act.First().Weight.Should().Be(20);

        _act.Last().Title.Should().Be("هاشم");
        _act.Last().Weight.Should().Be(30);
    }

    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _=> Given()
            ,_=> When().Wait()
            ,_=>Then());
    }
    
}