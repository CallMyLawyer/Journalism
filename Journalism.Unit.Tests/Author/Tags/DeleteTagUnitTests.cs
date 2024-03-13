using System.Configuration;
using FluentAssertions;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.Tags;

namespace Journalism.Unit.Tests.Author.Tags;

public class DeleteTagUnitTests : BusinessIntegrationTest
{
    private readonly AuthorTagService _sut;

    public DeleteTagUnitTests()
    {
        _sut = AuthorTagServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task Delete_delete_a_tag_properly()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);

        var tag = new TagBuilder().WithTitle("karim")
            .WithCategoryId(category.Id).Build();
        DbContext.Save(tag);

        await _sut.Delete(tag.Id);

        var act = ReadContext.Tags;

        act.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Delete_throws_exception_when_tagId_does_not_exist()
    {
        var act = () => _sut.Delete(10);
        await act.Should().ThrowExactlyAsync<TagIdDoesNotExistException>();
    }
}