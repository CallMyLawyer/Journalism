using Journalism.Entites.Categories;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.Tags;

namespace Journalism.Services.PublishedNewsPapers.Contracts.Dtos;

public class GetPublishedNewspapersDto
{
    public int Id{ get; set; }
    public NewsPaper NewsPaper{ get; set; }
    public bool Published{ get; set; }
    public List<Category> Categories{ get; set; }
}