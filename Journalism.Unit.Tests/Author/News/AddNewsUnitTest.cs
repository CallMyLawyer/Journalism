using FluentAssertions;
using Journalism.Entites.Tags;
using Journalism.Services.News.Contracts;
using Journalism.Services.News.Contracts.Dtos;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.News;
using Journalism.Test.Tools.NewsPapers;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Unit.Tests.Author.News;

public class AddNewsUnitTest : BusinessIntegrationTest
{
    private readonly AuthorNewsService _sut;

    public AddNewsUnitTest()
    {
        _sut = NewsServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task Add_add_a_news_properly()
    {
        var newspaper = new NewsPaperBuilder()
            .WithTitle("karim").WithWeight(100).Build();
        DbContext.Save(newspaper);
        var category = new CategoryBuilder().Build();
        DbContext.Save(category);

        var dto = new AddNewsDto()
        {
            Title ="karim",
            Author = "karim",
            Text = "miobio",
            Tags = new List<Tag?>(),
            Views = 1,
            Weight = 10,
            NewsPaperId = newspaper.Id,
            CategoryId = category.Id
        };

        await _sut.Add(dto);

        var act = ReadContext.News.Single();
        var act2 = ReadContext.NewsPapers.Include(newsPaper => newsPaper.NewsList).Single();

        act.Title.Should().Be("karim");
        act2.NewsList.Single().Title.Should().Be("karim");

    }

    [Fact]
    public async Task Add_throws_exception_when_newsPaperId_does_not_exist()
    {
        var newspaper = new NewsPaperBuilder().Build();
        DbContext.Save(newspaper);
        var dto = new AddNewsDto()
        {
            Title = "کریم",
            Author = "karim",
            Text = "miobio",
            Views = 1,
            Weight = 10,
            NewsPaperId = 11
        };
        var act = () => _sut.Add(dto);
        await act.Should().ThrowExactlyAsync<NewsPaperIdDoesNotExistException>();
    }
}