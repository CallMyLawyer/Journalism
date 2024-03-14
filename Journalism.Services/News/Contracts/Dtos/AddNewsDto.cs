using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Journalism.Entites.Tags;

namespace Journalism.Services.News.Contracts.Dtos;

public class AddNewsDto
{
    [Required]
    public string Author{ get; set; }
    [Required]
    public string Title{ get; set; }
    [Required]
    public string Text{ get; set; }
    
    public int Views{ get; set; }
    public int Weight{ get; set; }
    [Required]
    public int NewsPaperId { get; set; }
    public List<Tag?>? Tags{ get; set; }
    public int? CategoryId{ get; set; }
}