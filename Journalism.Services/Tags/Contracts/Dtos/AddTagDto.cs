namespace Journalism.Services.Tags.Contracts.Dtos;

public class AddTagDto
{
    public string Title{ get; set; }
    public int Weight{ get; set; }
    public int CategoryId{ get; set; }
}