using Cura.Task.Entities;
using Cura.Task.Sheard.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.DAL.Repositories.MailAttachmentRepository
{
    public class MailAttachmentRepository:Repository<MailAttachment>, IMailAttachmentRepository
    {
        public MailAttachmentRepository(DbContext context):base(context) { }
    }
}
