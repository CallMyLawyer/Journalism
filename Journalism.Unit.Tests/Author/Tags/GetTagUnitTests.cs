using FluentAssertions;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;

namespace Journalism.Unit.Tests.Author.Tags;

public class GetTagUnitTests : BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;

    public GetTagUnitTests()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Fact]
    public void Get_get_all_shows_details_properly()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);

        var tag1 = new TagBuilder().WithTitle("karim")
            .WithCategoryId(category.Id).Build();
        DbContext.Save(tag1);

        var tag2 = new TagBuilder().WithTitle("hashem")
            .WithCategoryId(category.Id).Build();
        DbContext.Save(tag2);

        var act = _sut.GetAll();

        act.First().Title.Should().Be("karim");
        act.First().CategoryId.Should().Be(category.Id);
        act.First().Id.Should().Be(tag1.Id);
        
        act.Last().Title.Should().Be("hashem");
        act.Last().CategoryId.Should().Be(category.Id);
        act.Last().Id.Should().Be(tag2.Id);
        
    }

    [Fact]
    public void Get_get_One_tag_properly()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);

        var tag = new TagBuilder().WithTitle("karim")
            .WithCategoryId(category.Id).Build();
        DbContext.Save(tag);

        var act = _sut.GetOne(tag.Id);
        
        act.Title.Should().Be("karim");
        act.CategoryId.Should().Be(category.Id);
        act.Id.Should().Be(tag.Id);
    }

    [Fact]
    public void Get_throws_exception_when_tag_id_does_not_exist()
    {
        var act = () => _sut.GetOne(10);

        act.Should().ThrowExactly<TagIdDoesNotExistException>();
    }
}