using FluentAssertions;
using Journalism.Services.Users.Contracts;
using Journalism.Services.Users.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Journalism.Test.Tools.PublishedNwesPapers;
using Journalism.Test.Tools.Users;

namespace Journalism.Unit.Tests.Users;

public class UserUnitTests : BusinessIntegrationTest
{
    private readonly UserService _sut;

    public UserUnitTests()
    {
        _sut = UserServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task User_get_all_published_newspapers_properly()
    {        var category = new CategoryBuilder().WithWeight(10).WithTitle("mio").Build();
        DbContext.Save(category);
        var newspaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).WithTitle("کریم").WithCategoryId(category.Id).Build();
        DbContext.Save(newspaper);
        newspaper.PublishedAt = DateTime.Today;
        var published = new PublishedNewsPaperBuilder()
            .WithNewsPaper(newspaper).WithPublished(true).Build();
        DbContext.Save(published);

        var act = _sut.GetAll();

        act.First().NewsPaper.Title.Should().Be("کریم");
    }

    [Fact]
    public async Task User_get_one_published_newspaper_properly()
    {
        var category = new CategoryBuilder().WithWeight(10).WithTitle("mio").Build();
        DbContext.Save(category);
        var newspaper = new NewsPaperBuilder()
            .WithWeight(100).WithNewsWeight(100).WithTitle("کریم").WithCategoryId(category.Id).Build();
        DbContext.Save(newspaper);
        newspaper.PublishedAt = DateTime.Today;
        var published = new PublishedNewsPaperBuilder()
            .WithNewsPaper(newspaper).WithPublished(true).Build();
        DbContext.Save(published);
        var title = new FilterByIdDto()
        {
            Id = newspaper.Id
        };
        var act = _sut.GetOne(title);

        act.NewsPaper.Title.Should().Be("کریم");
    }
}