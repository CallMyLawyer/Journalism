using Journalism.Services.News.Contracts;
using Journalism.Services.News.Contracts.Dtos;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Journalism.RestApi.Controllers.Author;
[Route("News")]
public class AuthorNewsController : Controller
{
    private readonly AuthorNewsService _service;

    public AuthorNewsController(AuthorNewsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task Add([FromBody]AddNewsDto dto)
    {
        await _service.Add(dto);
    }
}