using Journalism.Entites.Users;

namespace Journalism.Services.Users.Contracts;

public interface UserRepository
{
    void Add(User user);
}