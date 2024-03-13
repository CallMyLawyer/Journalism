using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace Journalism.Spec.Tests.Author.Categories.AddCategorySpecTests;
[Scenario("عدم اضافه شدن دسته بندی با نام تکراری ")]
[Story("",
    AsA = "به عنوان یک نویسنده خبر",
    IWantTo = "یک دسته بندی با نام تکراری به فهرست دسته بندی ها اضافه کنم.",
    InOrderTo = "خطایی با عنوان \"خطا! این نام دسته بندی در حال حاضر در فهرست دسته بندی ها وجود دارد!\" را دریافت کنم.")]
public class AddCategoryExceptionWhenTitleIsDuplicated : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;
    private Category _category;
    private AddCategoryDto _dto;

    public AddCategoryExceptionWhenTitleIsDuplicated()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }

    [Given("در فهرست دسته بندی ها یک دسته بندی با نام \"کریم\" وجود دارد.")]
    private async Task Given()
    {
        _category = new CategoryBuilder().WithTitle("کریم").Build();
        DbContext.Save(_category);
    }

    [When("یک دسته بندی جدید با نام \"کریم\" را به فهرست دساه بندی ها اضافه میکنم.")]
    public async Task When()
    {
        _dto= new AddCategoryDto()
        {
            Title = "کریم",
            Weight = 30
        };
    }

    [Then("باید خطایی با عنوان \"خطا! این نام دسته بندی در حال حاضر در فهرست دسته بندی ها وجود دارد!\" را دریافت کنم.")]
    public void Then()
    {
        var act = () => _sut.Add(_dto);
        act.Should().ThrowExactlyAsync<ThisCategoryNameAlreadyExistsInCategoriesException>();
        

    }

    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _=> Given(),
            _ => When().Wait(),
            _ => Then());
    }
}