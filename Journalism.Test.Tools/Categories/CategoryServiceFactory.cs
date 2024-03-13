using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Services.Categories;
using Journalism.Services.Categories.Contracts;

namespace Journalism.Test.Tools.Categories;

public static class CategoryServiceFactory
{
    public static AuthorCategoryService Create(EFDataContext context)
    {
        return new AuthorCategoryAppService(
            new EFCategoryRepository(context),
            new EFUnitOfWork(context));
    }
}