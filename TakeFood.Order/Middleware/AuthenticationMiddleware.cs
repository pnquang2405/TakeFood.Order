using Microsoft.IdentityModel.Tokens;
using Order.Service;
using System.IdentityModel.Tokens.Jwt;

namespace Order.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    private IUserService userService;
    private IJwtService jwtService;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, IUserService _userService, IJwtService _jwtService)
    {
        try
        {
            userService = _userService;
            jwtService = _jwtService;
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                attachUserToContext(context, token, userService, jwtService);
            await _next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private async void attachUserToContext(HttpContext context, string token, IUserService userService, IJwtService jwtService)
    {
        try
        {
            jwtService.ValidSecurityToken(token);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadToken(token!) as JwtSecurityToken;
            string id = jwtService.GetId(token);
            List<string> roles = jwtService.GetRoles(token);
            context.Items["Role"] = roles;
            context.Items["Id"] = id;
            context.Items["TokenExp"] = false;
        }
        catch (SecurityTokenExpiredException err)
        {
            context.Items["TokenExp"] = true;
            context.Items["Id"] = "0";
            context.Items["Role"] = "UnAuth";
        }
        catch (Exception err)
        {
            context.Items["TokenExp"] = true;
            context.Items["Id"] = "0";
            context.Items["Role"] = "UnAuth";
        }
    }
}
