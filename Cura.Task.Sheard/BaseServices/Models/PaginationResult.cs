using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cura.Task.Sheard.BaseServices.Models;

namespace Cura.Task.Sheard.BaseServices.Models
{
    public class PaginationResult : Result
    {
        public long TotalCount { get; set; }

        public long PagesCount { get; set; }
    }
}
