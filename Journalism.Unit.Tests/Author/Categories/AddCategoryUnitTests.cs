using FluentAssertions;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Categories.Dtos;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;

namespace Journalism.Unit.Tests.Author.Categories;

public class AddCategoryUnitTests : BusinessIntegrationTest
{
    private readonly AuthorCategoryService _sut;

    public AddCategoryUnitTests()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }
    [Fact]
    public async Task Add_add_a_category_properly()
    {
        var newsPaper = new NewsPaperBuilder().WithWeight(70).Build();
        DbContext.Save(newsPaper);
        var dto = new AddCategoryDto()
        {
          Title = "karim",
          Weight = 30,
          NewsPaperId = newsPaper.Id
        };

        await _sut.Add(dto);

        var act = ReadContext.Categories.Single();

        act.Title.Should().Be(dto.Title);
        act.Weight.Should().Be(dto.Weight);
    }

    [Fact]
    public async Task Add_add_throws_exception_when_title_is_duplicated()
    {
        var category = new CategoryBuilder().WithTitle("کریم").Build();
        DbContext.Save(category);

        var dto = AddCategoryDtoFactory.Create();

        var act = () => _sut.Add(dto);

        await act.Should().ThrowExactlyAsync<ThisCategoryNameAlreadyExistsInCategoriesException>();
    }

    [Fact]
    public async Task Add_add_throws_exception_when_the_weight_is_zero_or_less()
    {
        var dto = new AddCategoryDto()
        {
         Title = "karim",
         Weight = -1
        };
        var act = () => _sut.Add(dto);

        await act.Should().ThrowExactlyAsync<TheWeightCanNotBeLessThanZeroException>();
    }
}