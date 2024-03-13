using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.NewsPapers;
using Journalism.Persistence.EF.Tags;
using Journalism.Services.NewsPapers;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.Tags;

namespace Journalism.Test.Tools.NewsPapers;

public class AuthorNewsPaperServiceFactory
{
    public static AuthorNewsPapersService Create(EFDataContext context)
    {
        return new AuthorNewsPapersAppService(
            new EFNewsPaperRepository(context),
            new EFUnitOfWork(context),
            new EFCategoryRepository(context));
    }
}