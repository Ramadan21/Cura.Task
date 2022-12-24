using Cura.Task.App.Helper;
using Cura.Task.App.Services.UserService;
using Cura.Task.App.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cura.Task.App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IUserService _service;


        public IndexModel(IUserService service)
        {
            _service = service;
        }


        [BindProperty]
        public RegisterDto? dto { get; set; }
        public void OnGet()
        {

        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (dto != null)
            {
                var result = await _service.Login(dto);

                if (result.Success)
                {
                    HttpContext.Session.SetString("loginSession", result.Result.data);
                    SessionData.SessionName = HttpContext.Session.GetString("loginSession");
                    SessionData.UserEmail = dto.Email;
                }
            }
            return RedirectToPage("Privacy");
        }
    }
}