using FluentAssertions;
using Journalism.Entites.Tags;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Journalism.Spec.Tests.Author.Tags.GetTagSpecTests;
[Scenario("مشاهده کردن یک تگ از طریق شماره شناسایی")]
[Story("",
    AsA = "نویسنده خبر",
    IWantTo = "یک تگ را مشاهده کنم",
    InOrderTo = "از ان در خبر استفاده کنم")]
public class GetOneTagSpecTest : BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;
    private Tag _karim;
    private GetTagDto _act;

    public GetOneTagSpecTest()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Given("در فهرست تگ ها تگی با  . نام \"کریم\" وجود دارد.")]
    public void Given()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);
        _karim = new TagBuilder().WithTitle("کریم").WithCategoryId(category.Id).Build();
        DbContext.Save(_karim);
    }

    [When("درخواست مشاهده تگ مذکور را ثبت میکنم.")]
    public async Task When()
    {
        _act = _sut.GetOne(_karim.Id);
    }

    [Then(" باید یک تگ با نام \"کریم\" را مشاهده کنم.")]
    public void Then()
    {
        _act.Title.Should().Be("کریم");
        _act.CategoryId.Should().Be(_karim.CategoryId);
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