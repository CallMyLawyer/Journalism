using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Entites.Tags;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace Journalism.Spec.Tests.Author.Categories.DeleteCategorySpecTests;
[Scenario("عدم حذف دسته بندی دارای تگ")]
[Story("",
    AsA = "نویسنده خبر",
    IWantTo = "یک دسته بندی با نام " +
              "\"کریم\" که دارای یک تگ با نام \"کریم2\" است و" +
              "در فهرست دسته بندی ها وجود دارد را حذف کنم." ,
    InOrderTo = " خطایی با عنوان \" خطا ! دسته بندی دارای تگ میباشد!\" را دریافت کنم.")]
public class DeleteCategorySpecExceptionTest : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;
    private Category _karim;
    private Func<Task> _act;

    public DeleteCategorySpecExceptionTest()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }

    [Given("یک دسته بندی با نام" +
           " \"کریم\" که دارای یک تگ با نام \"کریم2\"" +
           " است در فهرست دسته بندی ها وجود دارد. ")]
    public void Given()
    {
        _karim = new CategoryBuilder().WithTitle("کریم")
            .WithTag(new Tag()
            {
                Title = "کریم2",
                CategoryId = 1
            }).Build();
        DbContext.Save(_karim);
    }

    [When("شماره شناسایی دسته بندی مذکور را برای حذف شدن ثبت میکنم. ")]
    public async Task When()
    {
        _act =()=> _sut.Delete(_karim.Id);
    }

    [Then("باید خطایی با عنوان \" خطا ! دسته بندی دارای تگ میباشد!\" را دریافت کنم.")]
    public void Then()
    {
        _act.Should().ThrowExactlyAsync<CategoryHasTagException>();
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