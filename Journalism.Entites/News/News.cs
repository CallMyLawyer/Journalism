using Journalism.Entites.Categories;
using Journalism.Entites.Tags;

namespace Journalism.Entites.News;

public class News
{
    public int Id{ get; set; }
    public string Author{ get; set; }
    public string Title{ get; set; }
    public string Text{ get; set; }
    public int Weight{ get; set; }
    public int Views{ get; set; }
    public int NewsPaperId{ get; set; }
    public List<Tag?>? Tags{ get; set; }
}