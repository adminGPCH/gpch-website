// D:\GPCH\APP\PROUX_ERP.Server\Controllers\UserController.cs

namespace PROUX_ERP.Server.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using PROUX_ERP.Shared.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // 👇 Forzamos el esquema Cookie para que en APIs devuelva 401 en vez de redirigir a OIDC
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [HttpGet]
    public ActionResult<UserInfo> Get()
    {
        var user = HttpContext.User;

        // Si no hay identidad autenticada, devolvemos 401 limpio
        if (user?.Identity?.IsAuthenticated != true)
            return Unauthorized();

        // Construimos el objeto UserInfo con claims relevantes
        var userInfo = new UserInfo
        {
            Name = user.Identity?.Name,
            Email = user.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
            Claims = user.Claims
                .Select(c => new ClaimItem
                {
                    Type = c.Type,
                    Value = c.Value
                })
                .ToList()
        };

        return Ok(userInfo);
    }
}
