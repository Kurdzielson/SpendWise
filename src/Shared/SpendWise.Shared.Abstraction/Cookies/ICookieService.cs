using Microsoft.AspNetCore.Http;

namespace SpendWise.Shared.Abstraction.Cookies;

public interface ICookieService
{
    public void Set(string key, string value, CookieOptions options);
    public string Get(string key);
    public void Remove(string key, CookieOptions options);
}