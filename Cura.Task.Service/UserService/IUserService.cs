using Cura.Task.DAL.Dtos.User;
using Cura.Task.Sheard.BaseServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Service.UserService
{
    public interface IUserService
    {
        Task<ServiceOutput> Register(RegisterDto dto);
        Task<ServiceOutput> Login(RegisterDto dto);
        Task<ServiceOutput> RecoverPassword(string email);
        Task<ServiceOutput> IsExist(string email);
        Task<ServiceOutput> ChangePassword(string email,string newPass);
    }
}
