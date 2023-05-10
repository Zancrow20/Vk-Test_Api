namespace VkWebApi.DataAccess.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime Created_Date { get; set; }
    
    public int UserStateId { get; set; }
    
    public int UserGroupId { get; set; }

    public virtual UserGroup UserGroup { get; set; }
    public virtual UserState UserState { get; set; }
}