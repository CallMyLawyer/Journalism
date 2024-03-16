using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Managment.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;

namespace Journalism.Services.Managment.Contracts;

public interface ManagerRepository
{
    GetPublishedNewspapersDto GetTheNewsPaperWithMustViews();
    GetCategoryDto2 GetTheCategoryWithMustViews();
    GetNewsDto GetTheNewsWithMustViews();
}