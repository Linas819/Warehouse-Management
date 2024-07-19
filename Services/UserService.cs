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
    public LoginUser GetToken (LoginUser user)
    {
        User dbUser = usersContext.Users.Where(x => x.Username == user.Username && x.Password == user.Password).First();
        if(dbUser != null)
        {
            user.Login = true;
            user.UserId = dbUser.UserId;
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            user.Token = tokenHandler.WriteToken(token);
        }
        return user;
    }
}