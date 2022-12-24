using AutoMapper;
using Cura.Task.DAL.Dtos;
using Cura.Task.DAL.Dtos.Mail;
using Cura.Task.DAL.Repositories.MailAttachmentRepository;
using Cura.Task.DAL.Repositories.MailRepository;
using Cura.Task.Entities;
using Cura.Task.Sheard.BaseServices;
using Cura.Task.Sheard.BaseServices.Models;
using Cura.Task.Sheard.EntityFramework;
using Cura.Task.Sheard.Helpers.EmailSender;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Service.MailService
{
    public class MailService : ServiceResponse, IMailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IMailRepository _mailRepository;
        private readonly IMailAttachmentRepository _attachmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public MailService(IEmailSender emailSender, IMailRepository mailRepository, IUnitOfWork unitOfWork, IConfiguration config, IMailAttachmentRepository attachmentRepository, IMapper mapper)
        {
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _mailRepository = _unitOfWork.GetRepository<IMailRepository>();
            _config = config;
            _attachmentRepository = _unitOfWork.GetRepository<IMailAttachmentRepository>();
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task<(List<MailResponseDto>, int, int)> GetInbox(int pageNumber,int pageSize, string email)
        {
            var mails = await _mailRepository.GetAllAsync(a => a.To == email);
            return GetMailList(pageNumber,pageSize, mails);
        }

        private (List<MailResponseDto>, int, int) GetMailList(int pageNumber,int pageSize, IEnumerable<Mail> mails)
        {
            if (mails.Count() > 0)
            {
                var mappedData = _mapper.Map<List<MailResponseDto>>(mails);
                var totalCount = mappedData.Count;
                var resultData = mappedData.OrderByDescending(x => x.ReciveTime).ToList();
                var pagesCount = (int)(totalCount < pageSize ? 1 : decimal.Ceiling((decimal)totalCount / (decimal)pageSize));
                int? skip = (pageNumber - 1) * pageSize;
                resultData = resultData.Skip(skip.Value).Take(pageSize).ToList();

                return (data: resultData, totalCount: totalCount, pagesCount: pagesCount);
            }
            return (data: null, totalCount: 0, pagesCount: 0);
        }

        public async Task<(List<MailResponseDto>, int, int)> GetSentItems(int pageNumber, int pageSize, string email)
        {
            var mails = await _mailRepository.GetAllAsync(a => a.From == email);
            return GetMailList(pageNumber, pageSize, mails);
        }

        public async Task<(List<MailResponseDto>, int, int)> GetStaredItems(int pageNumber, int pageSize, string email)
        {
            var mails = await _mailRepository.GetAllAsync(a => a.To == email && a.IsStared);
            return GetMailList(pageNumber, pageSize, mails);
        }

        public async Task<(List<MailResponseDto>, int, int)> GetTrachItems(int pageNumber, int pageSize, string email)
        {
            var mails = await _mailRepository.GetAllAsync(a => a.IsTrash && (a.To == email || a.From == email));
            return GetMailList(pageNumber, pageSize, mails);
        }

        public async Task<ServiceOutput> SendMail(Message message)
        {
            var sentResult = await _emailSender.SendEmail(message);
            if (sentResult)
            {
                await AddNewMail(message);
                return GetResponse(true, "200", true);
            }

            return GetResponse(false, "400", false);
        }

        private async System.Threading.Tasks.Task AddNewMail(Message message)
        {
            var mail = await _mailRepository.AddAsync(new Entities.Mail
            {
                Id = new Guid(),
                Body = message.Content,
                IsIn = false,
                IsRead = true,
                IsStared = false,
                IsTrash = false,
                SentDate = DateTime.UtcNow,
                Subject = message.Subject,
                To = message.To
            });
            foreach (var item in message.Attachments)
            {
                var attach = await _attachmentRepository.AddAsync(new Entities.MailAttachment
                {
                    Id = new Guid(),
                    FileContent = item.ContentDisposition,
                    FileName = item.FileName
                });
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<ServiceOutput> StarMail(Guid id)
        {
            var mail = await _mailRepository.GetAsync(a => a.Id == id);
            if (mail != null)
            {
                mail.IsStared = true;
                await _unitOfWork.CommitAsync();
                return GetResponse(true, "200", true);
            }
            return GetResponse("Data not found", "404", false);

        }

        public async Task<ServiceOutput> TrashMail(Guid id)
        {
            var mail = await _mailRepository.GetAsync(a => a.Id == id);
            if (mail != null)
            {
                mail.IsTrash = true;
                await _unitOfWork.CommitAsync();
                return GetResponse(true, "200", true);
            }
            return GetResponse("Data not found", "404", false);
        }
        public async Task<ServiceOutput> MakeRead(Guid id)
        {
            var mail = await _mailRepository.GetAsync(a => a.Id == id);
            if (mail != null)
            {
                mail.IsRead = true;
                await _unitOfWork.CommitAsync();
                return GetResponse(true, "200", true);
            }
            return GetResponse("Data not found", "404", false);
        }
    }
}
