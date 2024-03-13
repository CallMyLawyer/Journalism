using Journalism.Entites.Categories;

namespace Journalism.Services.News.Contracts;

public interface AuthorNewsRepository
{
    void Add(Entites.News.News news);
    Category FindCategory(int id);
}