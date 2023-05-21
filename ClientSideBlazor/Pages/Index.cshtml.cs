using System.Security.Claims;
using BlazorWasm.Services;
using ClientSideBlazor.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Models;

namespace ClientSideBlazor.Pages

{
    
    [AllowAnonymous]
    public partial class IndexModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly UserState _userState;
        

        public IndexModel(IAuthService authService, UserState userState)
        {
            _authService = authService;
            _userState = userState;
        }

        public UserState UserState { get; set; }

        public async Task OnGetAsync()
        {
            UserState = _authService.GetUserState();
        }
    }
}
