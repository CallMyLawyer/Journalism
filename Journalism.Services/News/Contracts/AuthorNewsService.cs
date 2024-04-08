using Journalism.Entites.Tags;
using Journalism.Services.News.Contracts.Dtos;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.News.Contracts;

public interface AuthorNewsService : Service
{
   Task Add(AddNewsDto dto);
}