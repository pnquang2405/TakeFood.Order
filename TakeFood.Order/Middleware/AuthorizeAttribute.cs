using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TakeFood.StoreService.Model.Entities;

namespace Order.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly String _role;

    public AuthorizeAttribute(params Roles[] roles)
    {
        string role = "";
        var rs = roles.FirstOrDefault();
        switch (rs)
        {
            case Roles.Admin:
                role = "Admin";
                break;
            case Roles.User:
                role = "User";
                break;
            case Roles.ShopeOwner:
                role = "ShopeOwner";
                break;
            default:
                role = "User";
                break;
        }
        _role = role;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            var check = context.HttpContext.Items["TokenExp"];
            if (Convert.ToBoolean(check) == true)
            {
                context.Result = new JsonResult(new { message = "Unauthorized, Token Exp" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            var role = (List<string>)context.HttpContext.Items["Role"]!;
            if (_role.Any() && !role.Contains(_role))
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        catch (Exception)
        {
            context.Result = new JsonResult(new { message = "Unauthorized, Not Sign Up" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
