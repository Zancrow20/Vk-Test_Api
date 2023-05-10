using System.ComponentModel.DataAnnotations;

namespace DtoLibrary;

public class GetUserByIdDto
{
    [Required]
    public int Id { get; set; }
}