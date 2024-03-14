using Journalism.Entites.Categories;
using Journalism.Entites.News;
using Journalism.Entites.NewsPapers;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.NewsPapers.Contracts.Dtos;
using Journalism.Services.NewsPapers.Contracts.Exceptions;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.NewsPapers;

public class AuthorNewsPapersAppService : AuthorNewsPapersService
{
    private readonly AuthorNewsPapersRepository _newsPapersRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly AuthorCategoryRepository _categoryRepository;
    

    public AuthorNewsPapersAppService(
        AuthorNewsPapersRepository newsPapersRepository
        , UnitOfWork unitOfWork,
        AuthorCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _newsPapersRepository = newsPapersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddNewsPaperDto dto)
    {
        if (_newsPapersRepository.DuplicateTitle(dto.Title))
        {
            throw new ThisTitleAlreadyExistsException();
        }
        var newsPaper = new NewsPaper()
        {
         Title = dto.Title,
         Weight = 0,
         Views = 0,
         PublishedAt = null,
         Categories = new List<Category>() ,
         NewsWeight = 0,
         NewsList = new List<Entites.News.News?>()
        };
        var weights = 0;
        foreach (var category in newsPaper.Categories)
        {
            weights = weights + category.Weight;
        }

        newsPaper.Weight = weights;
        
        _newsPapersRepository.Add(newsPaper);
        await _unitOfWork.Complete();
    }

    public List<GetNewsPapersDto> GetAll()
    {
        return _newsPapersRepository.GetAll();
    }

    public async Task AddCategoryToNewspaper(int categoryId, int newspaperId)
    {
        _newsPapersRepository.AddCategoryToNewsPaper(newspaperId , categoryId);
        await _unitOfWork.Complete();
    }

    public IQueryable<GetNewsPapersDto> GetOne(int id)
    {
        if (_newsPapersRepository.IsExistNewsPaperId(id))
        {
            throw new NewsPaperIdDoesNotExistException();
        }
        return _newsPapersRepository.GetOne(id);
    }
}