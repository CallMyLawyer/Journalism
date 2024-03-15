using System.ComponentModel.DataAnnotations;
using Journalism.Entites.Tags;

namespace Journalism.Services.Categories.Contracts.Dtos;

public class GetCategoryDto
{
    [Key]
    public int Id{ get; set; }
    public string Title{ get; set; }
    public List<Tag?> Tags { get; set; }
    public int Weight{ get; set; }
    public int Views{ get; set; }
}