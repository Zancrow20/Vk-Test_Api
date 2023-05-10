using DtoLibrary;

namespace VkWebApi.DataAccess.Models;

public class UserGroup
{
    public int Id { get; set; }
    public Role Role { get; set; } 
    public string Description { get; set; }

}