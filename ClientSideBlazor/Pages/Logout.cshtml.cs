using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSideBlazor.Pages;

public class LogoutModel : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogoutModel(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult OnGet()
    {
        // Clear the user status
        _httpContextAccessor.HttpContext.Items["UserState"] = null;

        // Remove the "UserState" cookie
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserState");

        // Perform the sign out action
        HttpContext.SignOutAsync();

        return RedirectToPage("/Index");  // Redirect the user to the home page or another desired location
    }
}