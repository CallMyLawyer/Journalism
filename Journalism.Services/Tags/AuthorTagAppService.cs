using Journalism.Entites.Tags;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Categories.Contracts.Exceptions;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Tags.Contracts.Dtos;
using Journalism.Services.Tags.Contracts.Exceptions;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Tags;

public class AuthorTagAppService : AuthorTagService
{
    private readonly AuthorTagRepository _tagRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly AuthorCategoryRepository _categoryRepository;

    public AuthorTagAppService(AuthorTagRepository tagRepository
    , UnitOfWork unitOfWork , AuthorCategoryRepository categoryRepository)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }
    public async Task Add(AddTagDto dto)
    {
        if (_categoryRepository.IsExistId(dto.CategoryId))
        {
            throw new CategoryIdDoesNotExistException();
        }

        if (_tagRepository.DuplicateTitle(dto.Title))
        {
            throw new TitleAlreadyExistsInTagsException();
        }
        var tag = new Tag()
        {
            Title = dto.Title,
            CategoryId = dto.CategoryId,
        };
        
        _tagRepository.Add(tag);
        await _unitOfWork.Complete();
    }

    public List<GetTagDto> GetAll()
    {
        return _tagRepository.GetAll();
    }

    public GetTagDto GetOne(int id)
    {
        if (_tagRepository.IsExistTagId(id))
        {
            throw new TagIdDoesNotExistException();
        }
        return _tagRepository.GetOne(id);
    }

    public async Task Delete(int id)
    {
        if (_tagRepository.IsExistTagId(id))
        {
            throw new TagIdDoesNotExistException();
        }
        _tagRepository.Delete(id);
        await _unitOfWork.Complete();
    }

    public async Task AddTagToNews(AddTagToNewsDto dto)
    {
        var tag = new Tag()
        {
            Title = dto.Title,
            CategoryId = dto.CategoryId,
            NewsId = dto.NewsId
        };
        await _tagRepository.AddTagToNews(tag , tag.NewsId);
        await _unitOfWork.Complete();
    }
}