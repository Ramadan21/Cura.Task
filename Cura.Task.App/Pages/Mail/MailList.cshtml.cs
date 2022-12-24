using Cura.Task.App.Services.Mail;
using Cura.Task.App.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cura.Task.App.Pages.Mail
{
    public class MailListModel : PageModel
    {

        private readonly IMailService _service;


        public MailListModel(IMailService service)
        {
            _service = service;
        }

        [BindProperty]
        public List<GetMails> lstmessages { get; set; }

        //public void OnGet()
        //{
        //}

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _service.GetInbox();
            if (result.Data.Count > 0)
                lstmessages = result.Data;

            return Page();
        }
    }
}


