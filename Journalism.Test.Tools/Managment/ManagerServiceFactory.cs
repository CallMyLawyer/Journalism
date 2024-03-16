using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Managment;
using Journalism.Services.Managment;
using Journalism.Services.Managment.Contracts;

namespace Journalism.Test.Tools.Managment;

public class ManagerServiceFactory
{
    public static ManagerService Create(EFDataContext context)
    {
        return new ManagerAppService(
            new EFUnitOfWork(context),
            new EFManagerRepository(context));
    }
}