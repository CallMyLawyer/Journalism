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
         Weight = 100,
         Views = 0,
         Category = _categoryRepository.FindCategory(dto.CategoryId) ,
         NewsList = new List<Entites.News.News?>()
        };
        _newsPapersRepository.Add(newsPaper);
        await _unitOfWork.Complete();
    }

    public List<GetNewsPapersDto> GetAll()
    {
        return _newsPapersRepository.GetAll();
    }
}