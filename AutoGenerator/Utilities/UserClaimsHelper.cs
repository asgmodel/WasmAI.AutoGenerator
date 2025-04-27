using AutoGenerator;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AutoGenerator.Utilities;

public interface IBaseUserClaimsHelper: ITClaimsHelper
{
    string UserId { get; }
    string? UserRole { get; }
    string? Email { get; }
}

public abstract class BaseUserClaimsHelper(IHttpContextAccessor httpContext) : IBaseUserClaimsHelper
{
    private HttpContext? HttpContext => httpContext?.HttpContext;
    public string UserId => (HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier))!;
    public string? UserRole => HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    public string? Email => HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

}

