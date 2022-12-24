using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cura.Task.Sheard.BaseServices.Models;

namespace Cura.Task.Sheard.BaseServices
{
    public class ServiceResponse
    {
        protected ServiceOutput GetResponse(object data, string messageCode = null, bool success = true)
        {
            return new ServiceOutput { Result = new Result { Data = data }, MessageCode = messageCode, Success = success };
        }

       
    }
}
