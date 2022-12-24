using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Sheard.Dtos.RequestAndResponse
{
    public class GenericGlobalResponse<T> : GlobalResponse
    {
        public new T Data { get; set; }
    }
}
