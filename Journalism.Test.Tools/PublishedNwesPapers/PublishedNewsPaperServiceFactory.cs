using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.NewsPapers;
using Journalism.Persistence.EF.PublishedNewsPapers;
using Journalism.Services.PublishedNewsPapers;
using Journalism.Services.PublishedNewsPapers.Contracts;

namespace Journalism.Test.Tools.PublishedNwesPapers;

public class PublishedNewsPaperServiceFactory
{
    public static PublishedNewsPapersService Create(EFDataContext context)
    {
        return new PublishedNewsPapersAppService(
            new EFUnitOfWork(context),
            new EFCategoryRepository(context),
            new EFPublishedNewsPapersRepository(context),
            new EFNewsPaperRepository(context));
    }
}