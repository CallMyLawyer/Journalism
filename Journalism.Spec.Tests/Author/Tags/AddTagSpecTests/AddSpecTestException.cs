using System.Data;
using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.Services.Tags.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace Journalism.Spec.Tests.Author.Tags.AddTagSpecTests;
[Scenario("عدم ثبت تگ با نام تکراری")]
[Story("",
    AsA = "یک نویسنده خبر",
    IWantTo = "به دسته بندی تگ با نام تکراری اضافه کنم",
    InOrderTo = "تا خطایی با عنوان \"خطا! نام تگ نکراری است!\" را دریافت کنم. ")]
public class AddSpecTestException :BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;
    private Category _karim;
    private Func<Task> _act;
    

    public AddSpecTestException()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Given("یک دسته بندی با نام" +
           " \"کریم\" و شماره شناسایی \"1\" " +
           "در فهرست دسته بندی ها وجود دارد که فهرست تگ هایش یک تگ با نام" +
           " \"کریم2\" وجود دارد.")]
    public void Given()
    {
        _karim = new CategoryBuilder().WithTitle("کریم").Build();
        DbContext.Save(_karim);
        var tag = new TagBuilder().WithTitle("کریم2")
            .WithCategoryId(_karim.Id).Build();
        DbContext.Save(tag);
    }

    [When("تگ جدیدی با نام \"کریم2\" را به فهرست تگ ها اضافه میکنم.")]
    public async Task When()
    {
        var tag = new AddTagDto()
        {
          Title = "کریم2",
          CategoryId = _karim.Id
        };
        _act = () => _sut.Add(tag);
    }

    [Then("باید خطایی با عنوان \"خطا! نام تگ نکراری است!\" را دریافت کنم.")]
    public void Then()
    {
        _act.Should().ThrowExactlyAsync<TitleAlreadyExistsInTagsException>();
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