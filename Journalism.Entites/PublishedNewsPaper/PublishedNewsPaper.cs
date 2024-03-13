using Journalism.Entites.NewsPapers;

namespace Journalism.Entites.PublishedNewsPaper;

public class PublishedNewsPaper
{
    public int Id{ get; set; }
    public NewsPaper NewsPaper{ get; set; }
    public bool Published{ get; set; }
}