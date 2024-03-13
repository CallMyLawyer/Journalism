using FluentAssertions;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;
using Xunit;

namespace Journalism.Spec.Tests.Author.Tags.GetTagSpecTests;
[Scenario("مشاهده کردن فهرست تگ ها ")]
[Story("",
    AsA = "نویسنده خبر",
    IWantTo = "فهرست تگ ها را مشاهده کنم",
    InOrderTo = "از انها استفاده کنم")]
public class GetAllTagSpecTest : BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;
    private List<GetTagDto> _act;

    public GetAllTagSpecTest()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Given("در فهرست تگ ها تگی با نام \"کریم\" وجود دارد.")]
    public void Given()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);
        var tag = new TagBuilder().WithTitle("کریم").WithCategoryId(category.Id).Build();
        DbContext.Save(tag);
    }

    [When("درخواست مشاهده فهرست تگ ها را ثبت میکنم.")]
    public async Task When()
    {
        _act =_sut.GetAll();
    }

    [Then("باید تنها یک تگ با نام \"کریم\" را مشاهده کنم.")]
    public void Then()
    {
        _act.First().Title.Should().Be("کریم");
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