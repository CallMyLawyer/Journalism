using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.Users.Contracts.Dtos;

namespace Journalism.Services.Users.Contracts;

public interface UserService
{
   List<GetPublishedNewspapersDto> GetAll();
   GetPublishedNewspapersDto GetOne(FilterByIdDto title);
}