using System.Security.Principal;

namespace Journalism.Services.News.Contracts.Dtos;

public class AddNewsDto
{
    public string Author{ get; set; }
    public string Title{ get; set; }
    public string Text{ get; set; }
    
    public int Views{ get; set; }
    public int Weight{ get; set; }
    public int NewsPaperId { get; set; }
}