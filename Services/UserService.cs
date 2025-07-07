using warehouse_management.UsersDB;
using warehouse_management.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Org.BouncyCastle.Crypto.Generators;

namespace warehouse_management.Services;
public class UserService
{
    Dictionary<string, string> userAccessRightsNames = new Dictionary<string, string>()
    {
        {"Inventory", "Inventory Check"},
        {"Orders", "Order Creation"},
        {"Register", "Register User"}
    };
    private UsersContext usersContext;
    private readonly IConfiguration configuration;
    public UserService(UsersContext usersContext, IConfiguration configuration)
    {
        this.usersContext = usersContext;
        this.configuration = configuration;
    }
    public LoginUser Login(LoginUser user)
    {
        User dbUser = usersContext.Users.Where(x => x.Username == user.Username).FirstOrDefault()!;
        if(dbUser != null && BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
        {
            user = user.DbUserToLoginUser(dbUser, user);
            user.Token = GetToken(user.UserId);
            user.UserAccesses = GetUsersAccesses(user.UserId);
        }
        return user;
    }
    public DatabaseUpdateResponse Register(RegisterUser user)
    {
        DatabaseUpdateResponse responce = new DatabaseUpdateResponse();
        User existingUser = usersContext.Users.Where(x => x.Username == user.Username).FirstOrDefault()!;
        if (existingUser != null)
        {
            responce.Success = false;
            responce.Message = "Username already exists";
            return responce;
        }
        User registerUser = user.RegisterUserToDbUser(user);
        usersContext.Users.Add(registerUser);
        PropertyInfo[] properties = user.userRights.GetType().GetProperties();
        foreach(PropertyInfo property in properties)
        {
            bool hasAccessRight = (bool)property.GetValue(user.userRights)!;
            if(hasAccessRight)
            {
                string accessId = usersContext.AccessFunctions.Where(x => x.AccessName == userAccessRightsNames[property.Name]).Select(x => x.AccessId).First();
                UsersAccess usersAccess = new UsersAccess{
                    AccessId = accessId,
                    UserId = registerUser.UserId
                };
                usersContext.UsersAccesses.Add(usersAccess);
            }
        }
        responce = SaveUserDatabaseChanges();
        return responce;
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
    public DatabaseUpdateResponse SaveUserDatabaseChanges()
    {
        DatabaseUpdateResponse responseModel = new DatabaseUpdateResponse();
        try{
            usersContext.SaveChanges();
        }catch(Exception e){
            responseModel.Success = false;
            responseModel.Message = e.Message;
        }
        return responseModel;
    }
}