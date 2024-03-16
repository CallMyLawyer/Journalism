using FluentAssertions;
using Journalism.Entites.Tags;
using Journalism.Services.Tags.Contracts;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;
using Xunit;

namespace Journalism.Spec.Tests.Author.Tags.DeleteTagSpecTests;
[Scenario("حذف کردن تگ")]
[Story("",
    AsA = "نویسنده خبر",
    IWantTo = "یک تگ را حذف کنم",
    InOrderTo = "انباشتگی داده ایجاد نشود.")]
public class DeleteTagSpecTest : BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;
    private Tag _karim;

    public DeleteTagSpecTest()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Given("در فهرست تگ ها یک تگ با نام \"کریم\"  وجود دارد.")]
    public void Given()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);
        _karim = new TagBuilder().WithTitle("کریم")
            .WithCategoryId(category.Id).Build();
        DbContext.Save(_karim);
    }
    [When("تگ مذکور را حذف میکنم")]
    public async Task When()
    {
        await _sut.Delete(_karim.Id);
    }

    [Then("فهرست تگ ها باید خالی باشد.")]
    public void Then()
    {
        var act = ReadContext.Tags;

        act.Should().BeNullOrEmpty();
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