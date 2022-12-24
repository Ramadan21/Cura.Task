using Cura.Task.App.ViewModel;

namespace Cura.Task.App.Services.UserService
{
    public interface IUserService
    {
        Task<ChangePasswordResult> ChangePassword(RegisterDto dto);
        Task<ChangePasswordResult> Register(RegisterDto dto);
        Task<ChangePasswordResult> IsExist(string email);
        Task<LoginResponse> Login(RegisterDto dto);
        Task<LoginResponse> RecoverPassword(string email);
    }
}
