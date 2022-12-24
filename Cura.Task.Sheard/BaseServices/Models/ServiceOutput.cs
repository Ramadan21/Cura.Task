using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Sheard.BaseServices.Models
{
    public class ServiceOutput
    {
        public string MessageCode { get; set; }
        public bool Success { get; set; }
        public Result Result { get; set; }
    }
}
