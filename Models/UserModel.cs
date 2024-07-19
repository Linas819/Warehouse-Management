using warehouse_management.UsersDB;

namespace warehouse_management.Models;
public class LoginUser
{
    public LoginUser DbUserToLoginUser(User dbUser, LoginUser loginUser)
    {
        loginUser.Username = dbUser.Username;
        loginUser.UserId = dbUser.UserId;
        loginUser.Password = dbUser.Password;
        loginUser.Token = loginUser.Token;
        loginUser.Login = true;
        UserAccesses = new List<string>();
        return loginUser;
    }
    public string Username { get; set; } = "";
    public string UserId { get; set; } = "";
    public string Password { get; set; } = "";
    public string Token { get; set; } = "";
    public bool Login { get; set; } = false;
    public List<string> UserAccesses { get; set; } = new List<string>();
}