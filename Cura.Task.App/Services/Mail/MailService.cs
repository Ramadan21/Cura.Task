using Cura.Task.App.Services.ExternalHttpClient;
using Cura.Task.App.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cura.Task.App.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly IExternalHttp _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _basicUrl;
        public MailService(IExternalHttp httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _basicUrl = _configuration["IntegerationLinks:BasicUrl"];
        }


        public async Task<MailList> GetInbox()
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:GetInbox"]}?pageNumber=1&pageSize=10";
            var result = await _httpClient.GetAsync<MailList>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<MailList> GetSentItems()
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:GetSentItems"]}?pageNumber=1&pageSize=10";
            var result = await _httpClient.GetAsync<MailList>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<MailList> GetStared()
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:GetStared"]}?pageNumber=1&pageSize=10";
            var result = await _httpClient.GetAsync<MailList>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<MailList> GetTrachItems()
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:GetTrachItems"]}?pageNumber=1&pageSize=10";
            var result = await _httpClient.GetAsync<MailList>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<MailPost> MakeRead(Guid Id)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:MakeRead"]}?id={Id}";
            var result = await _httpClient.PostWithoutBodyAsync<MailPost>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<MailPost> SendMailAsync(Message message)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:SendEmail"]}";
            var reuslt = await _httpClient.PostAsync<MailPost, Message>(url, message);
            if (reuslt != null)
            {
                return reuslt;
            }
            return null;
        }

        public async Task<MailPost> StarMail(Guid Id)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:StarMail"]}?id={Id}";
            var result = await _httpClient.PostWithoutBodyAsync<MailPost>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<MailPost> TrashMail(Guid Id)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:TrashMail"]}?id={Id}";
            var result = await _httpClient.PostWithoutBodyAsync<MailPost>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}

