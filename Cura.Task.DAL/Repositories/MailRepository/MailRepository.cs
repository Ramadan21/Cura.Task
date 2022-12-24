using Cura.Task.Entities;
using Cura.Task.Sheard.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.DAL.Repositories.MailRepository
{
    public class MailRepository : Repository<Mail>, IMailRepository
    {
        public MailRepository(DbContext context) : base(context)
        {

        }
    }
}
