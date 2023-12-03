using Microsoft.AspNetCore.Http;
using SpendWise.Shared.Abstraction.Cookies;

namespace SpendWise.Shared.Infrastructure.Cookies;

internal class CookieService(IHttpContextAccessor httpContextAccessor) : ICookieService
{
    public void Set(string key, string value, CookieOptions options)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);
    }

    public string Get(string key)
    {
        httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(key, out var value);
        return value;
    }

    public void Remove(string key, CookieOptions options)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(key, options);
    }
}