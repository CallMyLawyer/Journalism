using FluentAssertions;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Test.Tools.Categories;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Journalism.Test.Tools.NewsPapers;
using Xunit;

namespace Journalism.Spec.Tests.Author.Categories.AddCategorySpecTests;
[Scenario(" اضافه شدن یک دسته بندی جدید")]
[Story("",
    AsA = "نویسنده خبر",
    IWantTo = "دسته بندی جدیدی را به فهرست دسته بندی ها اضافه کنم",
    InOrderTo = "بتوانم در خبر های جدید از انها استفاده کنم."
)]
public class AddCategorySpecTest : BusinessIntegrationTest

{
    private readonly AuthorCategoryService _sut;
    public AddCategorySpecTest()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }

    [Given("فهرست دسته بندی ها خالی است")]
    private void Given()
    {
        DbContext.RemoveRange();
    }

    [When("یک دسته بندی با نام" +
          " \"کریم\" و وزن \"30\" و تعداد بازدید \"0\" و لیست خالی از تگ به فهرست دسته بندی ها اضافه میکنم.")]
    private async Task When()
    {
        var newsPaper = new NewsPaperBuilder().WithWeight(70).Build();
        DbContext.Save(newsPaper);
        var dto = new AddCategoryDto()
        {
            Title = "کریم",
            Weight = 30,
            NewsPaperId = newsPaper.Id
        };

        await _sut.Add(dto);
    }

    [Then("در فهرست دسته بندی ها باید یک دسته بندی" +
          " با نام \"کریم\" و وزن \"30\" و تعداد بازدید کننده \"0\" و لیست خالی از تگ وجود داشته باشد.")]
    private void Then()
    {
        var act = DbContext.Categories.Single();

        act.Title.Should().Be("کریم");
        act.Views.Should().Be(0);
        act.Weight.Should().Be(30);
    }

    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => When().Wait(),
            _ => Then());
    }
}