using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Journalism.Entites.NewsPapers;

namespace Journalism.Services.PublishedNewsPapers.Contracts.Dtos;

public class AddPublishedNewsPaperDto
{
    [Required]
    public int NewsPaperId{ get; set; }
    [DefaultValue(false)]
    public bool Published { get; set; } = false;
}