using Cura.Task.App.Services.Mail;
using Cura.Task.App.Services.UserService;
using Cura.Task.App.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cura.Task.App.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly  IUserService _service ;
        

        public RegisterModel(IUserService service)
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
                //  _service.re
                await _service.Register(dto);

            }

            return RedirectToPage("../Index");
        }

    }
}
