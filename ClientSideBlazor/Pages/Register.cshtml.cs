using System.Text;
using BlazorWasm.Services;
using ClientSideBlazor.Service;
using ClientSideBlazor.Service.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ClientSideBlazor.Pages
{
    public partial class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;
        

        public RegisterModel(IAuthService _authSer)
        {
            _authService = _authSer;
        }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string RepeatPassword { get; set; }
        
        [BindProperty]
        public string ErrorMessage2 { get; set; }

        [BindProperty]
        public bool IsSuccess { get; set; }

        public void OnGet()
        {
            // Initialize properties
            ErrorMessage2 = null;
            IsSuccess = false;
        }

        public async Task<IActionResult> OnPostAsync(string username, string password, string repeat)
        {
            
                try
                {
                    if (repeat == username)
                    {
                        var isSuccess = await _authService.RegisterUserAsync(username, password);
                        if (isSuccess)
                        {
                            IsSuccess = true;
                        }
                        else
                        {
                            ErrorMessage2 = "Failed to register user.";
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    ErrorMessage2 = "An error occurred: " + ex.Message;
                }
                
                return Page();
            
        }
        public bool IsPasswordsMatching()
        {
            
            return Password == RepeatPassword;
        }


        }
    }
