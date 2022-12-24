using Cura.Task.App.ViewModel;

namespace Cura.Task.App.Services.Mail
{
    public interface IMailService
    {
        Task<MailPost> SendMailAsync(Message message);
        Task<MailPost> MakeRead(Guid Id);
        Task<MailPost> TrashMail(Guid Id);
        Task<MailPost> StarMail(Guid Id);
        Task<MailList> GetInbox();
        Task<MailList> GetStared();
        Task<MailList> GetSentItems();
        Task<MailList> GetTrachItems();
    }
}
