using Microsoft.AspNetCore.Mvc;
using MyEducationCenter.Core;
using MyEducationCenter.LogicLayer;



namespace MyEducationCenter.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthenticationController(
        IUserService userService,
        IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserForRegistrationDto dto)
    {
        try
        {
            await _userService.RegisterUser(dto);
            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while registering the user./n{ex}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate(UserForAuthenticationDto user)
    {
        var token = await _userService.Login(user);
        return Ok(token);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeLanguage(int languageId)
    {
        try
        {
            await _userService.ChangeLanguageAsync(languageId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
