using Cura.Task.DAL.Dtos;
using Cura.Task.DAL.Dtos.Mail;
using Cura.Task.Sheard.BaseServices.Models;
using Cura.Task.Sheard.Helpers.EmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Service.MailService
{
    public interface IMailService
    {
        Task<ServiceOutput> SendMail(Message message);
        Task<ServiceOutput> TrashMail(Guid id);
        Task<ServiceOutput> StarMail(Guid id);
        Task<ServiceOutput> MakeRead(Guid id);
        Task<(List<MailResponseDto>, int, int)> GetInbox(int pageNumber, int pageSize, string email);
        Task<(List<MailResponseDto>, int, int)> GetSentItems(int pageNumber, int pageSize,string email);
        Task<(List<MailResponseDto>, int, int)> GetStaredItems(int pageNumber, int pageSize,string email);
        Task<(List<MailResponseDto>, int, int)> GetTrachItems(int pageNumber, int pageSize, string email);

    }
}
