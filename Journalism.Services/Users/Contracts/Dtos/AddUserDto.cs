using System.ComponentModel.DataAnnotations;

namespace Journalism.Services.Users.Contracts.Dtos;

public class AddUserDto
{
    [Required]
    public string FullName{ get; set; }
}