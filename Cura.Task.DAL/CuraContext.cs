using Cura.Task.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.DAL
{
    public class CuraContext :DbContext
    {
        public CuraContext(DbContextOptions<CuraContext> options):base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailAttachment> MailAttachments { get; set; }
    }
}
