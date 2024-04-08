using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.Services.Users.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.Users.Contracts;

public interface UserService : Service
{
   List<GetPublishedNewspapersDto> GetAll();
   GetPublishedNewspapersDto GetOne(FilterByIdDto title);
}