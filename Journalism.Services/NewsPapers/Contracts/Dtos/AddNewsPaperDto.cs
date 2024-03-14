using System.ComponentModel.DataAnnotations;
using Journalism.Entites.News;

namespace Journalism.Services.NewsPapers.Contracts.Dtos;

public class AddNewsPaperDto
{
    [Required]
    public string Title{ get; set; }
}