using Journalism.Entites.Categories;

namespace Journalism.Entites.NewsPapers;

public class NewsPaper
{
    public int Id{ get; set; }
    public string Title{ get; set; }
    public int Weight{ get; set; }
    public int Views{ get; set; }
    public DateTime? PublishedAt{ get; set; }
    public List<News.News?>? NewsList{ get; set; }
    public Category Category{ get; set; }
    public int CategoryId{ get; set; }
}