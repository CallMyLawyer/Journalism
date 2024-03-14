using Journalism.Entites.Categories;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.Tags;

namespace Journalism.Services.News.Contracts;

public interface AuthorNewsRepository
{
    void Add(Entites.News.News news);
    List<Entites.News.News> FindNews(int id);
}