using warehouse_management.UsersDB;
using warehouse_management.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace warehouse_management.Services;
public class UserService
{
    private UsersContext usersContext;
    private readonly IConfiguration configuration;
    public UserService(UsersContext usersContext, IConfiguration configuration)
    {
        this.usersContext = usersContext;
        this.configuration = configuration;
    }
    public LoginUser Login(LoginUser user)
    {
        User dbUser = usersContext.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault()!;
        if(dbUser != null)
        {
            user = user.DbUserToLoginUser(dbUser, user);
            user.Token = GetToken(user.UserId);
            user.UserAccesses = GetUsersAccesses(user.UserId);
        }
        return user;
    }
    public LoginUser LoginToken(string userId)
    {
        LoginUser loginUser = new LoginUser();
        User dbUser = usersContext.Users.Where(x => x.UserId == userId).FirstOrDefault()!;
        if(dbUser != null)
        {
            loginUser.DbUserToLoginUser(dbUser, loginUser);
            loginUser.Token = GetToken(loginUser.UserId);
            loginUser.UserAccesses = GetUsersAccesses(userId);
        }
        return loginUser;
    }
    public string GetToken (string userId)
    {
        string issuer = configuration["Jwt:Issuer"]!;
        string audience = configuration["Jwt:Audience"]!;
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.SerialNumber, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
    public List<string> GetUsersAccesses (string userId)
    {
        List<string> usersAccesses = new List<string>();
        usersAccesses = usersContext.UsersAccesses.Where(x => x.UserId == userId).Select(x => x.AccessId).ToList();
        return usersAccesses;
    }
}