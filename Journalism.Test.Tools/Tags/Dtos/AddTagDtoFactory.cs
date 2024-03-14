using Journalism.Services.Categories.Contracts.Dtos;
using Journalism.Services.Tags.Contracts.Dtos;

namespace Journalism.Test.Tools.Tags.Dtos;

public static class AddTagDtoFactory
{
    public static AddTagDto Create()
    {
        return new AddTagDto()
        {
            Title = "کریم",
            CategoryId = 1,
        };
    }
}