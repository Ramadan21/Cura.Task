using Cura.Task.App.Services.ExternalHttpClient;
using Cura.Task.App.ViewModel;
using Newtonsoft.Json;

namespace Cura.Task.App.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IExternalHttp _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _basicUrl;
        public UserService(IExternalHttp httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _basicUrl = _configuration["IntegerationLinks:BasicUrl"];
        }

        public async Task<ChangePasswordResult> ChangePassword(RegisterDto dto)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:ChangePassword"]}";
            var result = await _httpClient.PostAsync<ChangePasswordResult, RegisterDto>(url, dto);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<ChangePasswordResult> IsExist(string email)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:IfExist"]}";
            var result = await _httpClient.GetAsync<ChangePasswordResult>(url);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<LoginResponse> Login(RegisterDto dto)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:Login"]}";
            var result = await _httpClient.PostAsync<LoginResponse, RegisterDto>(url, dto);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public Task<LoginResponse> RecoverPassword(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<ChangePasswordResult> Register(RegisterDto dto)
        {
            var url = $"{_basicUrl}{_configuration["IntegerationLinks:Register"]}";
            var result = await _httpClient.PostAsync<ChangePasswordResult, RegisterDto>(url, dto);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
