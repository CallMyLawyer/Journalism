using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Journalism.RestApi.Controllers.Author;
[Route("Category")]
public class AuthorCategoryController : Controller
{
    private readonly AuthorCategoryService _service;

    public AuthorCategoryController(AuthorCategoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task Add([FromBody]AddCategoryDto dto)
    {
        await _service.Add(dto);
    }

    [HttpGet]
    public List<GetCategoryDto> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public IEnumerable<GetCategoryDto>GetOne([FromRoute] int id)
    {
        return _service.GetOne(id);
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] int id)
    {
        await _service.Delete(id);
    }
}