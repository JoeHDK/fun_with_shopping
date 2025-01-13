using Microsoft.AspNetCore.Mvc;

namespace WebshopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessionController : ControllerBase
{
    [HttpGet("init-session")]
    public IActionResult InitializeSession()
    {
        // Generate a unique session ID
        var sessionId = Guid.NewGuid().ToString();

        // Set the session ID in a cookie with a valid SameSite attribute
        Response.Cookies.Append("sessionId", sessionId, new CookieOptions
        {
            HttpOnly = false,
            Secure = true, // Change to true in production (requires HTTPS)
            SameSite = SameSiteMode.None // Use Lax for most cases
        });

        return Ok(new { sessionId });
    }
}