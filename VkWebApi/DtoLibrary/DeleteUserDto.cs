using System.ComponentModel.DataAnnotations;

namespace DtoLibrary;

public class DeleteUserDto
{
    [Required]
    public int Id { get; set; }
}