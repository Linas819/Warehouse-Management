using Microsoft.AspNetCore.Mvc;
using warehouse_management.Services;
using warehouse_management.Models;
using Microsoft.AspNetCore.Authorization;

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
            User = user
        }));
    }
    [HttpGet]
    public IActionResult Test()
    {
        return(Ok(new{
            Success = true
        }));
    }

}