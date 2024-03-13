using Journalism.Entites.Tags;
using Journalism.Services.Categories.Contracts.Dtos;

namespace Journalism.Test.Tools.Categories.Dtos;

public static class AddCategoryDtoFactory
{
    public static AddCategoryDto Create()
    {
        return new AddCategoryDto()
        {
         Title = "کریم",
         Weight = 100
        };
    }
    
}