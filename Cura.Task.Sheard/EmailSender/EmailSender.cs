using System.Net.Mail;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net;

namespace Cura.Task.Sheard.Helpers.EmailSender
{
    public class EmailSender : IEmailSender
    {
        // private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;
        //  private readonly EmailConfiguration _emailConfig;
        private readonly string from;
        private readonly int port;
        private readonly string smtp;
        private readonly string username;
        private readonly string password;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            from = _configuration["EmailConfiguration:From"];
            port =int.Parse( _configuration["EmailConfiguration:Port"]);
            smtp = _configuration["EmailConfiguration:SmtpServer"];
            username = _configuration["EmailConfiguration:UserName"];
            password = _configuration["EmailConfiguration:Password"];
        }

        public async Task<bool> SendEmail(Message message)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage(from, message.To))
                {
                    mailMessage.Body = message.Content;
                    mailMessage.Subject = message.Subject;
                    mailMessage.IsBodyHtml = false;
                    foreach (var item in message.Attachments)
                    {
                        if (item.Length > 0)
                        {
                            var fileName = Path.GetFileName(item.FileName);
                            mailMessage.Attachments.Add(new Attachment(item.OpenReadStream(), fileName));
                        } 
                    }
                    using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
                    {
                        client.Port = port;
                        client.EnableSsl = true;
                        client.Host = smtp;client.UseDefaultCredentials = false;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        NetworkCredential credential = new NetworkCredential(username, password);
                        
                        client.Credentials = credential;
                        if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) == false)
                        {
                            ServicePointManager.SecurityProtocol =
                                SecurityProtocolType.Tls |
                                SecurityProtocolType.Tls11 |
                                SecurityProtocolType.Tls12;
                        }
                        client.Send(mailMessage);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        //public async Task<bool> SendEmail(Message message)
        //{
        //    var emailMessage = CreateEmailMessage(message);
        //    Send(emailMessage);
        //    return true;
        //}
        //private MimeMessage CreateEmailMessage(Message message)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress(from, from));
        //    emailMessage.To.Add(MimeKit.InternetAddress{ } message.To);
        //    emailMessage.Subject = message.Subject;

        //    var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

        //    if (message.Attachments != null && message.Attachments.Any())
        //    {
        //        byte[] fileBytes;
        //        foreach (var attachment in message.Attachments)
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                attachment.CopyTo(ms);
        //                fileBytes = ms.ToArray();
        //            }

        //            bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
        //        }
        //    }

        //    emailMessage.Body = bodyBuilder.ToMessageBody();
        //    return emailMessage;
        //}

        //private void Send(MimeMessage mailMessage)
        //{
        //    using (var client = new MailKit.Net.Smtp.SmtpClient())
        //    {
        //        try
        //        {
        //            client.Connect(smtp, port, true);
        //            client.AuthenticationMechanisms.Remove("XOAUTH2");
        //            client.Authenticate(username, password);

        //            client.Send(mailMessage);
        //        }
        //        catch
        //        {
        //            //log an error message or throw an exception or both.
        //            throw;
        //        }
        //        finally
        //        {
        //            client.Disconnect(true);
        //            client.Dispose();
        //        }
        //    }
        //}

    }
}
