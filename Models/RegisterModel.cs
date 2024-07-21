using warehouse_management.UsersDB;

namespace warehouse_management.Models;

public class RegisterUser
{
    public User RegisterUserToDbUser(RegisterUser user)
    {
        User dbUser = new User{
            UserId = user.FirstName.Substring(0,3) + user.Lastname.Substring(0,3) + (int)DateTime.Now.Ticks/100000,
            Username = user.Username,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.Lastname,
            CreatedDate = DateTime.Now
        };
        return dbUser;
    }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string Lastname { get; set; } = "";
    public RegisterUserRights userRights { get; set; } = new RegisterUserRights();
}

public class RegisterUserRights
{
    public bool Inventory { get; set; } = false;
    public bool Orders { get; set; } = false;
    public bool Register { get; set; } = false;
}