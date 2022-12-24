using Cura.Task.DAL.Dtos.User;
using Cura.Task.DAL.Repositories.UserRepository;
using Cura.Task.Service.Helper;
using Cura.Task.Sheard.BaseServices;
using Cura.Task.Sheard.BaseServices.Models;
using Cura.Task.Sheard.EntityFramework;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Service.UserService
{
    public class UserService : ServiceResponse, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _token;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenHelper token, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<IUserRepository>();
            _token = token;
            _config = config;
        }

        public async Task<ServiceOutput> ChangePassword(string email, string newPass)
        {
            var userExist = await _userRepository.GetAsync(a => a.Email == email);
            if (userExist != null)
            {
                userExist.Password = newPass;
                await _unitOfWork.CommitAsync();
                return GetResponse(true);
            }
            return GetResponse(false);
        }

        public async Task<ServiceOutput> IsExist(string email)
        {
            var userExist = await _userRepository.GetAsync(a => a.Email == email);
            if (userExist != null)
            {
                return GetResponse(true);
            }
            return GetResponse(false);
        }

        public async Task<ServiceOutput> Login(RegisterDto dto)
        {
            var userExist = await _userRepository.GetAsync(a => a.Email == dto.Email);
            if (userExist != null)
            {
                var user = await _userRepository.GetAsync(a => a.Email == dto.Email && a.Password == dto.Password);
                if (user == null)
                {
                    return GetResponse("Invalid Username or password", "400", false);
                }
                List<Claim> claims = new List<Claim> { new Claim("UserName", dto.Email) };
                var token = await _token.GenerateToken(claims);
                return GetResponse(token, "200", true);
            }
            return GetResponse("Invalid Data", "400", false);
        }

        public async Task<ServiceOutput> RecoverPassword(string email)
        {
            var user = await _userRepository.GetAsync(a => a.Email == email);
            if (user == null)
            {
                return GetResponse("User not found");
            }
            //need to return link to change password or resend old one to ??

            return GetResponse("", "200", true);
        }

        public async Task<ServiceOutput> Register(RegisterDto dto)
        {
            var user = await _userRepository.AddAsync(new Entities.User { Email = dto.Email, Id = new Guid(), Password = dto.Password, Username = dto.Email });
            if (user != null)
            {
                await _unitOfWork.CommitAsync();
                return GetResponse(true, "200", true);
            }
            return GetResponse(false, "400", false);
        }
    }
}
