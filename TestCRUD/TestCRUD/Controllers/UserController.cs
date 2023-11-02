using Microsoft.AspNetCore.Mvc;
using TestCRUD.Models;
using TestCRUD.Services;

namespace TestCRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    public UserController(IUserService userService)
    {
        this.userService = userService;
    }
    [HttpGet("id")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await userService.GetUserById(id);
        if (user == null) {
            return NotFound("User not found");
        }
        return Ok(user);
    }
    [HttpGet("username")]
    public async Task<ActionResult<User>> GetUserByUsername(string username)
    {
        var user = await userService.GetUserByUsername(username);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }
    [HttpGet("email")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        var user = await userService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }
    [HttpGet]
    public async Task<ActionResult<User>> GetUsers()
    {
        var user = await userService.GetAllUsers();
        return Ok(user);
    }
    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser(int id, User user)
    {
        user.id = id;
        var existingUser = await userService.GetUserById(id);
        if(existingUser == null)
        {

            return NotFound("User not found");
        }
        var otherUser = await userService.GetUserByUsername(user.username);
        if(otherUser == null || existingUser.username == user.username)
        {
            otherUser = await userService.GetUserByEmail(user.email);
            if(otherUser == null || existingUser.email == user.email)
            {
                await userService.UpdateUser(id, user);
                return Ok(user);
            }
            else
            {
                return BadRequest("There exists user with this email");
            }
        }
        else
        {
            return BadRequest("There exists user with this username");
        }

    }
    [HttpDelete("id")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        var existingUser = await userService.GetUserById(id);
        if (existingUser == null)
        {
            return NotFound("User not found");
        }
        await userService.DeleteUser(id);
        return Ok("Deleted successfully");

    }
    [HttpPost("id")]
    public async Task<ActionResult<User>> AddUser(User user)
    {
        var existingUser = await userService.GetUserByUsername(user.username);
        if (existingUser == null)
        {
            existingUser = await userService.GetUserByEmail(user.email);
            if (existingUser == null)
            {
                await userService.AddUser(user);
                return Ok(user);
            }
            else
            {
                return BadRequest("There exists user with this email");
            }
        }
        else
        {
            return BadRequest("There exists user with this username");
        }

    }
}

