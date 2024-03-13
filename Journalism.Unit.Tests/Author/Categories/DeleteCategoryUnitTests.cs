using FluentAssertions;
using Journalism.Services.Categories.Contracts;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using SQLitePCL;

namespace Journalism.Unit.Tests.Author.Categories;

public class DeleteCategoryUnitTests : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;

    public DeleteCategoryUnitTests()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }
    [Fact]
    public async Task Delete_delete_a_category_properly()
    {
        var category = new CategoryBuilder().WithTitle("karim").Build();
        DbContext.Save(category);
        await _sut.Delete(category.Id);

        var act = ReadContext.Categories;

        act.Should().BeNullOrEmpty();
    }
}