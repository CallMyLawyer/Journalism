using Journalism.Services.News.Contracts.Dtos;

namespace Journalism.Services.News.Contracts;

public interface AuthorNewsService
{
   Task Add(AddNewsDto dto);
}