using FluentAssertions;
using Journalism.Entites.Categories;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Categories.Dtos;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Journalism.Spec.Tests.Author.Tags.AddTagSpecTests;
[Scenario("ثبت کردن تگ جدید")]
[Story("",
    AsA = "نویسنده خبر",
    IWantTo = "میخواهم یک تگ جدید را به یک دسته بندی اضافه کنم",
    InOrderTo = "تا بتوانم از ان تگ در اخبار جدید استفاده کنم.")]
public class AddTagSpecTest : BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;
    private Category _karim;

    public AddTagSpecTest()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Given("یک دسته بندی با نام" +
           " \"کریم\"" +
           "در فهرست دسته بندی ها وجود دارد که فهرست تگ هایش خالی است.")]
    public void Given()
    {
        _karim= new CategoryBuilder()
            .WithTitle("کریم").Build();
        Save(_karim);
    }

    [When("تگ جدیدی با نام \"کریم2\" و " +
          " را به دسته بندی مذکور اضافه میکنم.")]
    public async Task When()
    {
        var karim2 = new AddTagDto()
        {
            Title = "کریم2",
            CategoryId = _karim.Id,
        };
        await _sut.Add(karim2);
    }

    [Then("دسته بندی با نام " +
          "\"کریم\" باید دارای یک تگ جدیدی" +
          " با نام \"کریم2\" شد. ")]
    public void Then()
    {
        var act = ReadContext.Categories.
            Include(category => category.Tags).Single();
            act.Tags.FirstOrDefault()?.Title.Should().Be("کریم2");
    }
    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _=> Given(),
            _=> When().Wait() ,
            _=> Then() );
    }
}