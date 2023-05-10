namespace VkWebApi.DataAccess.Models;

public class UserState
{
    public int Id { get; set; }
    public Status State { get; set; }
    public string Description { get; set; }

}

public enum Status
{
    Active,
    Blocked
}