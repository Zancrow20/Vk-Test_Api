using System.ComponentModel.DataAnnotations;

namespace DtoLibrary;

public class LoginDto
{
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Login { get; set; }
}