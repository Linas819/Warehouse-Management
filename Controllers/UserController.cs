using Microsoft.AspNetCore.Mvc;
using warehouse_management.Services;
using warehouse_management.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace warehouse_management.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private UserService userService;
    public UserController(UserService userService)
    {
        this.userService = userService;
    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult LogIn([FromBody] LoginUser user)
    {
        user = userService.Login(user);
        return(Ok(new{
            Login = user.Login,
            UserId = user.UserId,
            Token = user.Token,
            UserAccess = user.UserAccesses
        }));
    }
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] RegisterUser user)
    {
        DatabaseUpdateResponce responce = userService.Register(user);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpGet]
    public IActionResult GetLoginToken()
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        LoginUser user = userService.LoginToken(userId);
        return(Ok(new{
            Login = user.Login,
            UserId = user.UserId,
            Token = user.Token,
            UserAccess = user.UserAccesses
        }));
    }

}