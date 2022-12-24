using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Entities
{
    public class Mail
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsRead { get; set; }
        public bool IsIn { get; set; }
        public bool IsTrash { get; set; }
        public bool IsStared { get; set; }
        public DateTime SentDate { get; set; }
        public virtual List<MailAttachment> MailAttachments { get; set; }

    }
}
