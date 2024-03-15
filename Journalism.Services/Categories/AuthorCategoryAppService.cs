using Journalism.Entites.Categories;
using Journalism.Entites.Tags;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Categories;

public class AuthorCategoryAppService : AuthorCategoryService
{
    private readonly AuthorCategoryRepository _repository;
    private readonly UnitOfWork _unitOfWork;
    private readonly AuthorNewsPapersRepository _authorNewsPapersRepository;
    private readonly PublishedNewsPapersRepository _publishedNewsPapersRepository;

    public AuthorCategoryAppService(AuthorCategoryRepository repository
    , UnitOfWork unitOfWork , AuthorNewsPapersRepository authorNewsPapersRepository
    , PublishedNewsPapersRepository publishedNewsPapersRepository)
    {
        _publishedNewsPapersRepository = publishedNewsPapersRepository;
        _authorNewsPapersRepository = authorNewsPapersRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Add(AddCategoryDto dto)
    {
        if (_repository.IsExistTitle(dto.Title))
        {
            throw new ThisCategoryNameAlreadyExistsInCategoriesException();
        }

        if (dto.Weight<=0)
        {
            throw new TheWeightCanNotBeLessThanZeroException();
        }
        var category = new Category()
        {
         Title = dto.Title,
         Weight = dto.Weight,
         DefaultWeight = dto.Weight,
         NewsPaperId = dto.NewsPaperId,
         Tags = new List<Tag?>(3),
         Views = 0
        };
        var newsPaper = _authorNewsPapersRepository.FindNewsPaper((int)dto.NewsPaperId);
        newsPaper.Weight = newsPaper.Weight + category.Weight;
        if (_repository.WeightLessThan100(newsPaper.Id))
        {
            throw new NewsPaperWeightCanNotBeMoreThan100Exception();
        }
        newsPaper.Categories?.Add(category);


        _repository.Add(category);
        await _unitOfWork.Complete();

    }

    public List<GetCategoryDto> GetAll()
    {
       return _repository.GetAll();
    }

    public IEnumerable<GetCategoryDto> GetOne(int id)
    {
        return _repository.GetOne(id);
    }

    public async Task Delete(int id)
    {
        if (_repository.IsExistId(id))
        {
            throw new CategoryIdDoesNotExistException();
        }

        if (_repository.TagExistInCategory(id))
        {
            throw new CategoryHasTagException();
        }

        _repository.Delete(id);
        await _unitOfWork.Complete();
    }
}