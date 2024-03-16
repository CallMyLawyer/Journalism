using Journalism.Entites.PublishedNewsPaper;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.Users.Contracts;
using Journalism.Services.Users.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Journalism.RestApi.Controllers.Author;
[Route("User")]
public class UserController : Controller
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet("GetAllNewsPapers")]
    public List<GetPublishedNewspapersDto> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("GetNewsPaperById")]
    public GetPublishedNewspapersDto GetOne( FilterByIdDto title)
    {
        return _service.GetOne(title);
    }
}