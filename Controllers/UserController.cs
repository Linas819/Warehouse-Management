using Microsoft.AspNetCore.Mvc;
using warehouse_management.Services;
using warehouse_management.Models;

namespace warehouse_management.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private UserService userService;
    public UserController(UserService userService)
    {
        this.userService = userService;
    }
    [HttpPost]
    public IActionResult LogIn([FromBody] User user)
    {
        return(Ok(new{
            Token = user.Token
        }));
    }
}