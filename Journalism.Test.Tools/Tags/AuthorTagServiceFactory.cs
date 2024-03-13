using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.Tags;
using Journalism.Services.Categories;
using Journalism.Services.Tags;
using Journalism.Services.Tags.Contracts;

namespace Journalism.Test.Tools.Tags;

public class AuthorTagServiceFactory
{
    public static AuthorTagService Create(EFDataContext context)
    {
        return new AuthorTagAppService(
            new EFTagRepository(context) ,
            new EFUnitOfWork(context),
            new EFCategoryRepository(context));
    }
}