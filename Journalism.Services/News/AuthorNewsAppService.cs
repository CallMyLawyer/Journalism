using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Services.News.Contracts;
using Journalism.Services.News.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.News;

public class AuthorNewsAppService : AuthorNewsService
{
    private readonly AuthorNewsRepository _authorNewsRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly AuthorCategoryRepository _authorCategoryRepository;

    public AuthorNewsAppService(
        AuthorCategoryRepository authorCategoryRepository,
        AuthorNewsRepository authorNewsRepository
        , UnitOfWork unitOfWork)
    {
        _authorCategoryRepository = authorCategoryRepository;
        _authorNewsRepository = authorNewsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddNewsDto dto)
    {
        var news = new Entites.News.News()
        {
            Title = dto.Title,
            Author = dto.Author,
            NewsPaperId = dto.NewsPaperId,
            Text = dto.Text,
            Views = dto.Views,
            Weight = dto.Weight

        };
        _authorNewsRepository.Add(news);
        await _unitOfWork.Complete();
    }
}