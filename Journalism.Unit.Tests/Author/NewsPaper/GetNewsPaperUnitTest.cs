using FluentAssertions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;

namespace Journalism.Unit.Tests.Author.NewsPaper;

public class GetNewsPaperUnitTest : BusinessIntegrationTest
{
    private readonly AuthorNewsPapersService _sut;

    public GetNewsPaperUnitTest()
    {
        _sut = AuthorNewsPaperServiceFactory.Create(SetupContext);
    }

    [Fact]
    public void Get_get_all_newsPapers_properly()
    {
        var newspaper1 = new NewsPaperBuilder().WithTitle("karim").Build();
        DbContext.Save(newspaper1);
        var newspaper2 = new NewsPaperBuilder().WithTitle("hashem").Build();
        DbContext.Save(newspaper2);

        var act = _sut.GetAll();

        act.First().Title.Should().Be("karim");
        act.Last().Title.Should().Be("hashem");
    }

    [Fact]
    public void Get_get_one_newspaper_properly()
    {
        var newspaper = new NewsPaperBuilder().WithTitle("karim").Build();
        DbContext.Save(newspaper);

        var act = _sut.GetOne(newspaper.Id);

        act.First().Title.Should().Be("karim");
    }

    [Fact]
    public async Task Get_getOne_throws_exception_when_NewspaperId_does_not_exist()
    {
        var act = () => _sut.GetOne(199);
        act.Should().ThrowExactly<NewsPaperIdDoesNotExistException>();
    }
}