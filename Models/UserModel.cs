

namespace warehouse_management.Models;
public class LoginUser
{
    public string Username { get; set; } = "";
    public string UserId { get; set; } = "";
    public string Password { get; set; } = "";
    public string Token { get; set; } = "";
    public bool Login { get; set; } = false;
    public List<string> UserAccesses { get; set; } = new List<string>();
}