using Cura.Task.DAL.Dtos;
using Cura.Task.Service.MailService;
using Cura.Task.Sheard.Helpers.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cura.Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class MailController : ControllerBase
    {
        private readonly IMailService _service;
        public MailController(IMailService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail([FromForm] Message message)
        {
            var result = await _service.SendMail(message);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        [Route("MakeRead")]
        public async Task<IActionResult> MakeRead(Guid id)
        {
            var result = await _service.MakeRead(id);
            if (result.Success)
                return Ok();
            return BadRequest(result);

        }
        [HttpPost]
        [Route("TrashMail")]
        public async Task<IActionResult> TrashMail(Guid id)
        {
            var result = await _service.TrashMail(id);
            if (result.Success)
                return Ok();
            return BadRequest(result);

        }
        [HttpPost]
        [Route("StarMail")]
        public async Task<IActionResult> StarMail(Guid id)
        {
            var result = await _service.StarMail(id);
            if (result.Success)
                return Ok();
            return BadRequest(result);

        }
        [HttpGet]
        [Route("GetInbox")]
        public async Task<IActionResult> GetInbox(int pageNumber,int pageSize)
        {
            var userEmail = Request.Headers.FirstOrDefault(a=>a.Key=="UserEmail").Value;//User.Identities.First().Claims.ElementAt(1);
            var result = await _service.GetInbox(pageNumber,pageSize, userEmail.ToString());
            if (result.Item1 != null)
            {
                var filterd = new { Data = result.Item1, TotalCount = result.Item2, PagesCount = result.Item3 };
                return Ok(filterd);
            }
            return BadRequest(result);
        }
        [HttpGet]
        [Route("GetStared")]
        public async Task<IActionResult> GetStared(int pageNumber, int pageSize)
        {
            var userEmail = Request.HttpContext.User.Identities.First().Claims.ElementAt(0);
            var result = await _service.GetStaredItems(pageNumber,pageSize, userEmail.ToString());
            if (result.Item1 != null)
            {
                var filterd = new { Data = result.Item1, TotalCount = result.Item2, PagesCount = result.Item3 };
                return Ok(filterd);
            }
            return BadRequest(result);
        }
        [HttpGet]
        [Route("GetSentItems")]
        public async Task<IActionResult> GetSentItems(int pageNumber, int pageSize)
        {
            var userEmail = Request.HttpContext.User.Identities.First().Claims.ElementAt(0);
            var result = await _service.GetSentItems(pageNumber, pageSize, userEmail.ToString());
            if (result.Item1 != null)
            {
                var filterd = new { Data = result.Item1, TotalCount = result.Item2, PagesCount = result.Item3 };
                return Ok(filterd);
            }
            return BadRequest(result);
        }
        [HttpGet]
        [Route("GetTrachItems")]
        public async Task<IActionResult> GetTrachItems(int pageNumber, int pageSize)
        {
            var userEmail = Request.HttpContext.User.Identities.First().Claims.ElementAt(0);
            var result = await _service.GetTrachItems(pageNumber, pageSize, userEmail.ToString());
            if (result.Item1 != null)
            {
                var filterd = new { Data = result.Item1, TotalCount = result.Item2, PagesCount = result.Item3 };
                return Ok(filterd);
            }
            return BadRequest(result);
        }
    }
}
