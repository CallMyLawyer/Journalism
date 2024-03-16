using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Managment.Contracts;
using Journalism.Services.Managment.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Journalism.RestApi.Controllers.Managment;
[Route("Manager")]
public class ManagerController : Controller
{
    private readonly ManagerService _service;

    public ManagerController(ManagerService service)
    {
        _service = service;
    }

    [HttpGet("MustViewsNewsPaper")]
    public GetPublishedNewspapersDto GetTheNewsPaperWithMustViews()
    {
        return _service.GetTheNewsPaperWithMustViews();
    }

    [HttpGet("MustViewsCategory")]
    public GetCategoryDto2 GetTheCategoryWithMustViews()
    {
        return _service.GetTheCategoryWithMustViews();
    }

    [HttpGet("MustViewsNews")]
    public GetNewsDto GetTheNewsWithMustViews()
    {
        return _service.GetTheNewsWithMustViews();
    }
}