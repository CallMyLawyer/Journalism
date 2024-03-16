using Journalism.Entites.PublishedNewsPaper;
using Journalism.Entites.Users;
using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.Users.Contracts.Dtos;

namespace Journalism.Services.Users.Contracts;

public interface UserRepository
{
    List<GetPublishedNewspapersDto> GetAll();
    GetPublishedNewspapersDto GetOne(FilterByIdDto title);
    
    
}