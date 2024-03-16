using Journalism.Persistence.EF;
using Journalism.Persistence.EF.NewsPapers;
using Journalism.Persistence.EF.Users;
using Journalism.Services.Users;
using Journalism.Services.Users.Contracts;

namespace Journalism.Test.Tools.Users;

public class UserServiceFactory 
{
    public static UserService Create(EFDataContext context)
    {
        return new UserAppService(
            new EFUserRepository(context),
            new EFUnitOfWork(context),
            new EFNewsPaperRepository(context));
    }
}