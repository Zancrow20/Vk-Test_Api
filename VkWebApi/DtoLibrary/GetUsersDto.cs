using System.ComponentModel.DataAnnotations;

namespace DtoLibrary;

public class GetUsersDto
{
    [Required]
    public int Page { get; set; }
    
    [Required]
    public int PageSize { get; set; }
}