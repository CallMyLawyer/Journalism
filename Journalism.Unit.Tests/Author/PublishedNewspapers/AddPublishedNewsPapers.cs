using FluentAssertions;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
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
    public async Task Add_throws_exception_when_newspaperId_Does_not_exists()
    {
        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = 10,
            Published = false
        };
        var act =()=>_sut.Add(dto);
        await act.Should().ThrowExactlyAsync<NewsPaperIdDoesNotExistException>();
    }

    [Fact]
    public async Task Add_throws_exception_when_newspaper_weight_is_not_100()
    {
        var newspaper = new NewsPaperBuilder().WithWeight(50).Build();
        DbContext.Save(newspaper);
        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = newspaper.Id,
            Published = false
        };
        var act = () => _sut.Add(dto);

        await act.Should().ThrowExactlyAsync<NewsPaperWeightMustBe100ForPublishException>();
    }

    [Fact]
    public async Task Add_throws_exception_when_newspaper_published_before()
    {
        var newspaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).Build();
        DbContext.Save(newspaper);
        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = newspaper.Id,
            Published = false
        };
        await _sut.Add(dto);
        var act =()=> _sut.Add(dto);

        await act.Should()
            .ThrowExactlyAsync<ThisNewsPaperHasPublishedBeforeAndItCantPublishAgainWriteANewOneException>();
    }

    [Fact]
    public async Task Add_throws_exception_when_2_newspapers_published_in_one_day()
    {
        var newsPaper = new NewsPaperBuilder()
            .WithNewsWeight(100).WithWeight(100).Build();
        DbContext.Save(newsPaper);

        var dto = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = newsPaper.Id,
            Published = false
        };
        await _sut.Add(dto);
        var newsPaper2 = new NewsPaperBuilder()
            .WithNewsWeight(100).WithWeight(100).Build();
        DbContext.Save(newsPaper);
        var dto2 = new AddPublishedNewsPaperDto()
        {
            NewsPaperId = newsPaper2.Id,
            Published = false
        };
        var act = () => _sut.Add(dto2);
        await act.Should().ThrowExactlyAsync<ToDaysNewsPaperHasAlreadyPublishedGetReadyForTomorrowException>();
    }
}