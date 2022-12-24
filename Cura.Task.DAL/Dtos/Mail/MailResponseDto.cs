using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.DAL.Dtos.Mail
{
    public class MailResponseDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }
        public DateTime ReciveTime { get; set; }
        public bool HasAttachment { get; set; }
        public bool IsStared { get; set; }
    }
}
