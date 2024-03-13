using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Journalism.Entites.Tags;

namespace Journalism.Services.Categories.Contracts.Dtos;

public class AddCategoryDto
{
        [Required , MaxLength(50)]
        public string Title{ get; set; }
        [Required]
        public int Weight{ get; set; }

}