using Journalism.Entites.Categories;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.Tags;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.Services.News.Contracts;

public interface AuthorNewsRepository : Repository
{
    void Add(Entites.News.News news);
    List<Entites.News.News> FindNews(int id);
    bool DuplicateTitle(string title);
}