using FoodDelivery.Models;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Services.Auth;
using FoodDelivery.Models.DominModels.Auth;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApplication5.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("CustomerSignUp")]
    public async Task<IActionResult> CustomerSignUp([FromBody] CustomerSignUpModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.CustomerSignUp(model);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    [HttpPost("ChiefSignUp")]
    public async Task<IActionResult> ChiefSignUp([FromBody] ChiefSignUpModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ChiefSignUp(model);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
    {
        var h = HttpContext;
        var result = await _authService.GetTokenAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        if (!string.IsNullOrEmpty(result.RefreshToken))
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    [HttpPost("addRole")]
    public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.AddRoleAsync(model);

        if (!string.IsNullOrEmpty(result))
            return BadRequest(result);

        return Ok(model);
    }

    [HttpGet("refreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        var result = await _authService.RefreshTokenAsync(refreshToken);

        if (!result.IsAuthenticated)
            return BadRequest(result);

        SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    [HttpPost("revokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
    {
        var token = model.Token ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            return BadRequest("Token is required!");

        var result = await _authService.RevokeTokenAsync(token);

        if (!result)
            return BadRequest("Token is invalid!");

        return Ok();
    }

    private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires.ToLocalTime(),
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
