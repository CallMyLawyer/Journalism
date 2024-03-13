using Journalism.Entites.Categories;

namespace Journalism.Services.NewsPapers.Contracts.Dtos;

public class GetNewsPapersDto
{
    public int Id{ get; set; }
    public string Title{ get; set; }
    public int Weight{ get; set; }
    public int Views{ get; set; }
    public DateTime? PublishedAt{ get; set; }
    public List<Entites.News.News?>? NewsList{ get; set; }
    public Category Category{ get; set; }
}