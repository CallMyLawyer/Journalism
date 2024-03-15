using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.NewsPapers;
using Journalism.Persistence.EF.PublishedNewsPapers;
using Journalism.Services.Categories;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers;

namespace Journalism.Test.Tools.Categories;

public static class CategoryServiceFactory
{
    public static AuthorCategoryService Create(EFDataContext context)
    {
        return new AuthorCategoryAppService(
            new EFCategoryRepository(context),
            new EFUnitOfWork(context),
            new EFNewsPaperRepository(context),
            new EFPublishedNewsPapersRepository(context));
    }
}