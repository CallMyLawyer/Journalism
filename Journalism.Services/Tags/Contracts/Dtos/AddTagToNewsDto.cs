namespace Journalism.Services.Tags.Contracts.Dtos;

public class AddTagToNewsDto
{
    public string Title{ get; set; }
    public int CategoryId{ get; set; }
    public int? NewsId{ get; set; }
}