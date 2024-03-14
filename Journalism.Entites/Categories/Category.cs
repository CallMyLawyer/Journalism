using Journalism.Entites.Tags;

namespace Journalism.Entites.Categories;

public class Category
{
    public int Id{ get; set; }
    public string Title{ get; set; }
    public List<Tag?>? Tags { get; set; }
    public int Weight{ get; set; }
    public int Views{ get; set; }
    public int? NewsPaperId{ get; set; }
}