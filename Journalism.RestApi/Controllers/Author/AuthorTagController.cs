using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Journalism.RestApi.Controllers.Author;
[Route("Tag")]
public class AuthorTagController : Controller
{
  private readonly AuthorTagService _service;

  public AuthorTagController(AuthorTagService service)
  {
    _service = service;
  }

  [HttpPost]
  public async Task Add([FromBody] AddTagDto dto)
  {
    await _service.Add(dto);
  }

  [HttpGet]
  public List<GetTagDto> GetAll()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public GetTagDto GetOne([FromRoute] int id)
  {
    return _service.GetOne(id);
  }

  [HttpDelete("{id}")]
  public async Task Delete([FromRoute] int id)
  {
    await _service.Delete(id);
  }

  [HttpPatch]
  public async Task AddTagToNews([FromBody] AddTagToNewsDto dto)
  {
    await _service.AddTagToNews(dto);
  }
}