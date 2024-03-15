using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Journalism.RestApi.Controllers.Author;
[Route("PublishNewsPaper")]
public class AuthorPublishNewsPaperControllerController : Controller
{
    private readonly PublishedNewsPapersService _service;

    public AuthorPublishNewsPaperControllerController(
        PublishedNewsPapersService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task PublishNewsPaper([FromBody] AddPublishedNewsPaperDto dto)
    {
        await _service.Add(dto);
    }

    [HttpGet]
    public List<GetPublishedNewspapersDto> GetAll()
    {
        return _service.GetAll();
    }

}