using Journalism.Services.PublishedNewsPapers.Contracts.Dtos;

namespace Journalism.Services.PublishedNewsPapers.Contracts;

public interface PublishedNewsPapersService
{
   Task Add(AddPublishedNewsPaperDto dto);
}