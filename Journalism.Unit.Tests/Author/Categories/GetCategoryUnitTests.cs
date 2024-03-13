using FluentAssertions;
using Journalism.Services.Categories.Contracts;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

namespace Journalism.Unit.Tests.Author.Categories;

public class GetCategoryUnitTests : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;

    public GetCategoryUnitTests()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }

    [Fact]
    public void Get_get_all_categories_properly()
    {
        var karim = new CategoryBuilder()
            .WithTitle("karim").WithWeight(20).Build();
        DbContext.Save(karim);

        var hashem = new CategoryBuilder()
            .WithTitle("hashem").WithWeight(25).Build();
        DbContext.Save(hashem);

        var ghasem = new CategoryBuilder()
            .WithTitle("ghasem").WithWeight(30).Build();
        DbContext.Save(ghasem);

        var act = _sut.GetAll();
        
        act.First().Title.Should().Be("karim");
        act.First().Weight.Should().Be(20);
        
        act.Last().Title.Should().Be("ghasem");
        act.Last().Weight.Should().Be(30);
        
        
        
    }
}