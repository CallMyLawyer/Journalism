using Journalism.Entites.Tags;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Services.News.Contracts;
using Journalism.Services.News.Contracts.Dtos;
using Journalism.Services.News.Contracts.Exceptions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.News;

public class AuthorNewsAppService : AuthorNewsService
{
    private readonly AuthorNewsRepository _authorNewsRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly AuthorCategoryRepository _authorCategoryRepository;
    private readonly AuthorNewsPapersRepository _authorNewsPapersRepository;

    public AuthorNewsAppService(
        AuthorCategoryRepository authorCategoryRepository,
        AuthorNewsRepository authorNewsRepository
        , UnitOfWork unitOfWork ,
        AuthorNewsPapersRepository authorNewsPapersRepository)
    {
        _authorNewsPapersRepository = authorNewsPapersRepository;
        _authorCategoryRepository = authorCategoryRepository;
        _authorNewsRepository = authorNewsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddNewsDto dto)
    {
        if (_authorNewsPapersRepository.IsExistNewsPaperId(dto.NewsPaperId))
        {
            throw new NewsPaperIdDoesNotExistException();
        }

        if (_authorNewsRepository.DuplicateTitle(dto.Title))
        {
            throw new NewsTitleAlreadyExistsInNewsListException();
        }

        var newsPaper = _authorNewsPapersRepository.FindNewsPaper(dto.NewsPaperId);
        if (_authorNewsPapersRepository.IsWeight100OrNot(dto.NewsPaperId))
        {
            throw new NewsPaperWeightMustBe100ForAddNewsException();
        }

        var news = new Entites.News.News()
        {
            Title = dto.Title,
            Author = dto.Author,
            NewsPaperId = dto.NewsPaperId,
            Tags = new List<Tag?>(),
            Text = dto.Text,
            Views = 1,
            Weight = dto.Weight
        };
        if (dto.CategoryId != null)
        {
            var category = _authorCategoryRepository.FindCategory((int)dto.CategoryId);
            category.Weight = category.Weight - news.Weight;
            if (category.Weight < 0)
            {
                throw new CategoryWeightIsFullAndIItCantGetNewsAnyMoreException();
            }

            var newspaper = _authorNewsPapersRepository.FindNewsPaper(dto.NewsPaperId);
            newspaper.NewsWeight = newspaper.NewsWeight + news.Weight;
            newspaper.NewsList?.Add(news);
            _authorNewsRepository.Add(news);
            await _unitOfWork.Complete();
        }
    }

}