﻿using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Managment.Contracts.Dtos;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Managment.Contracts;

public interface ManagerRepository : Repository
{
    GetPublishedNewspapersDto GetTheNewsPaperWithMustViews();
    GetCategoryDto2 GetTheCategoryWithMustViews();
    GetNewsDto GetTheNewsWithMustViews();
}