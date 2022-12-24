using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Entities
{
    public class MailAttachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }
        public Mail Mail { get; set; }
    }
}
