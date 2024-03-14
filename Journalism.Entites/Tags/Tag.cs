namespace Journalism.Entites.Tags;

public class Tag
{
    public int Id{ get; set; }
    public string Title{ get; set; }
    public int CategoryId{ get; set; }
    public int? NewsId{ get; set; }
}