using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Services.Categories.Contracts;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace Journalism.Spec.Tests.Author.Categories.DeleteCategorySpecTests;
[Scenario("حذف کردن یک دسته بندی")]
[Story("" ,
    AsA = "نویسنده خبر" ,
    IWantTo = "یک دسته بندی را حذف کنم",
    InOrderTo = "برایم کارایی ندارد و انباشتگی داده به وجود نیاید")]
public class DeleteCategorySpecTest : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;
    private Category _karim;

    public DeleteCategorySpecTest()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }

    [Given(" فهرست دسته بندی ها دارای یک دسته بندی با نام \"کریم\"است.")]
    private void Given()
    {
        _karim = new CategoryBuilder()
            .WithTitle("کریم").Build();
        DbContext.Save(_karim);
    }

    [When("دسته بندی مذکور را حذف میکنم .")]
    public async Task When()
    {
        await _sut.Delete(_karim.Id);
    }

    [Then("فهرست دسته بندی ها باید خالی باشد.")]
    public void Then()
    {
        var act = ReadContext.Categories;
        act.Should().BeNullOrEmpty();
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