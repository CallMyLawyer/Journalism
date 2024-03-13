using FluentAssertions;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.Services.Tags.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Unit.Tests.Author.Tags;

public class AddTagUnitTests : BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;

    public AddTagUnitTests()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task Add_add_a_tag_properly()
    {
        var category = new CategoryBuilder().WithTitle("karim").Build();
        DbContext.Save(category);

        var tag = new AddTagDto()
        {
         Title = "karim2",
         CategoryId = category.Id
        };

        await _sut.Add(tag);

        var act = ReadContext.Categories.Include(category => category.Tags).Single();

        act.Tags.FirstOrDefault().Title.Should().Be(tag.Title);
        act.Tags.First().CategoryId.Should().Be(tag.CategoryId);

    }

    [Fact]
    public async Task Add_throws_exception_when_title_duplicated()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);
        var tag1 = new TagBuilder().WithTitle("karim").WithCategoryId(category.Id).Build();
        DbContext.Save(tag1);

        var tag2 = new AddTagDto()
        {
         Title = "karim",
         CategoryId = category.Id
        };
        var act =()=> _sut.Add(tag2);

        await act.Should().ThrowExactlyAsync<TitleAlreadyExistsInTagsException>();
    }

    [Fact]
    public async Task Add_throws_exception_when_categoryId_does_not_exist()
    {
        var tag = new AddTagDto()
        {
            Title = "karim",
            CategoryId = 100
        };
        var act = () => _sut.Add(tag);
        await act.Should().ThrowExactlyAsync<CategoryIdDoesNotExistException>();
    }
}