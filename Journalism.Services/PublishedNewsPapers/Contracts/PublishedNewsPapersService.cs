using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.PublishedNewsPapers.Contracts;

public interface PublishedNewsPapersService : Service
{
   Task Add(AddPublishedNewsPaperDto dto);
   List<GetPublishedNewspapersDto> GetAll();
}