using Core.Model;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Service;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        // Check eksisterende brugernavn
        var existing = await _userService.GetUserByUsername(user.Username);
        if (existing != null)
            return BadRequest("Username already exists");

        // Validate phone number (8 cifre)
        if (string.IsNullOrEmpty(user.PhoneNumber) || user.PhoneNumber.Length != 8 || !user.PhoneNumber.All(char.IsDigit))
            return BadRequest("Phone number must be exactly 8 digits");

        // Validate email
        if (string.IsNullOrEmpty(user.Email) || 
            !user.Email.Contains("@") || 
            !(user.Email.EndsWith(".dk") || user.Email.EndsWith(".com")))
        {
            return BadRequest("Email must contain '@' and end with .dk or .com");
        }

        // Hash password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        await _userService.CreateUser(user);
        return Ok("User created");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User login)
    {
        var user = await _userService.GetUserByUsername(login.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        return Ok(new { user.Id, user.Username });
    }
}