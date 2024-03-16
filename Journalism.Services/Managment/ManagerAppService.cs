using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Managment.Contracts;
using Journalism.Services.Managment.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Managment;

public class ManagerAppService : ManagerService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ManagerRepository _repository;

    public ManagerAppService(
        UnitOfWork unitOfWork ,
        ManagerRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public GetPublishedNewspapersDto GetTheNewsPaperWithMustViews()
    {
        return _repository.GetTheNewsPaperWithMustViews();
    }

    public GetCategoryDto2 GetTheCategoryWithMustViews()
    {
        return _repository.GetTheCategoryWithMustViews();
    }

    public GetNewsDto GetTheNewsWithMustViews()
    {
        return _repository.GetTheNewsWithMustViews();
    }

    public GetCategoryDto GetCategoryWithMustNews()
    {
        throw new NotImplementedException();
    }
}