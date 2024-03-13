using Journalism.Entites.News;

namespace Journalism.Services.NewsPapers.Contracts.Dtos;

public class AddNewsPaperDto
{
    public string Title{ get; set; }
    public int CategoryId{ get; set; }
}