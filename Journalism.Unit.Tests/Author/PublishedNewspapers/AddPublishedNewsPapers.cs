using FluentAssertions;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Exceptions;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Unit.Tests.Author.PublishedNewspapers;

public class AddPublishedNewsPapers : BusinessIntegrationTest
{
    private readonly PublishedNewsPapersService _sut;

    public AddPublishedNewsPapers()
    {
        _sut = PublishedNewsPaperServiceFactory.Create(SetupContext);
    }

    [Fact]
    public void Add_publish_a_newspaper_and_add_to_publishedNewspapers_properly()
    {
        var newspaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).Build();
        DbContext.Save(newspaper);

        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = newspaper.Id,
            Published = false
        };

        _sut.Add(dto);

        var act = ReadContext.PublishedNewsPapers.Include(publishedNewsPaper => publishedNewsPaper.NewsPaper).Single();

        act.NewsPaper.Id.Should().Be(newspaper.Id);
    }

    [Fact]
    public void Add_throws_exception_when_newspaperId_Does_not_exists()
    {
        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = 10,
            Published = false
        };
        var act =()=>_sut.Add(dto);
    }
}