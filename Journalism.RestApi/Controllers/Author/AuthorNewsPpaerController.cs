﻿using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Journalism.RestApi.Controllers.Author;
[Route("NewsPapers")]
public class AuthorNewsPpaerController : Controller
{ 
    private readonly AuthorNewsPapersService _service;

    public AuthorNewsPpaerController(AuthorNewsPapersService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task Add([FromBody]AddNewsPaperDto dto)
    {
        await _service.Add(dto);
    }

    [HttpGet]
    public List<GetNewsPapersDto> GetAll()
    {
        return _service.GetAll();
    }
}