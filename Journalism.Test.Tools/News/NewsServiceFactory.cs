using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.News;
using Journalism.Persistence.EF.NewsPapers;
using Journalism.Services.News;
using Journalism.Services.News.Contracts;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

namespace Journalism.Test.Tools.News;

public class NewsServiceFactory 
{
    public static AuthorNewsService Create(EFDataContext context)
    {
       return new AuthorNewsAppService(new EFCategoryRepository(context),
            new EFNewsRepository(context),
            new EFUnitOfWork(context),
            new EFNewsPaperRepository(context));
    }
}