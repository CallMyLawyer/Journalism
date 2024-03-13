using FluentAssertions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;

namespace Journalism.Unit.Tests.Author.NewsPaper;

public class AddNewsPaperUnitTests : BusinessIntegrationTest
{
    private readonly AuthorNewsPapersService _sut;

    public AddNewsPaperUnitTests()
    {
        _sut = AuthorNewsPaperServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task Add_add_a_newsPaper_properly()
    {
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);
        
        var newsPaper = new AddNewsPaperDto()
        {
            Title = "karim"
            ,CategoryId = category.Id
        };
        await _sut.Add(newsPaper);

        var act = ReadContext.NewsPapers.Single();

        act.Title.Should().Be(newsPaper.Title);
    }

    [Fact]
    public async Task Add_throws_exception_when_title_is_duplicated()
    {
        var category = new CategoryBuilder()
            .WithTitle("mio").Build();
        DbContext.Save(category);
        var karim = new NewsPaperBuilder()
            .WithTitle("karim").WithCategoryId(category.Id).Build();
        DbContext.Save(karim);

        var dto = new AddNewsPaperDto()
        {
          Title = "karim" 
        };

        var act = () => _sut.Add(dto);

        await act.Should().ThrowExactlyAsync<ThisTitleAlreadyExistsException>();
    }
}