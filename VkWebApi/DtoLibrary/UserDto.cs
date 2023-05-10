using System.ComponentModel.DataAnnotations;

namespace DtoLibrary;

public class UserDto
{
    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public Role Role { get; set; }
}

public enum Role
{
    User,
    Admin
}