using System.ComponentModel.DataAnnotations;

namespace Journalism.Services.Tags.Contracts.Dtos;

public class AddTagDto
{
    [Required]
    public string Title{ get; set; }
    [Required]
    public int CategoryId{ get; set; }
}