using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Sheard.Helpers.EmailSender
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Message message);

    }
}
