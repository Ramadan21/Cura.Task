using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Sheard.Dtos.RequestAndResponse
{
    public class PaginationResponse : ApiResponse
    {
        public long TotalCount { get; set; }

        public long PagesCount { get; set; }
    }
}
