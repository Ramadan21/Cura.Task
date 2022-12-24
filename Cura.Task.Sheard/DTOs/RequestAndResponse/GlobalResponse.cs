using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cura.Task.Sheard.Dtos.RequestAndResponse
{
    public class GlobalResponse : ActionResult, IActionResult
    {
        public GlobalResponse()
        {
            Success = false;
        }

        public string Message { get; set; }
        public bool? Success { get; set; }
        public string ErrorCode { get; set; }
        public object Result { get; set; }
        public bool IsActive { get; set; } = true;

        public GlobalResponse SuccessResult()
        {
            var result = new GlobalResponse { Success = true, Result = new object() };
            return result;
        }

        public GlobalResponse SuccessResult(object data, string successMessage)
        {
            var result = new GlobalResponse { Success = true, Result = data, Message = successMessage };
            return result;
        }

        public GlobalResponse ErrorResult(string errorMessage, string errorCode)
        {
            var result = new GlobalResponse { Success = false, Result = null, ErrorCode = errorCode, Message = errorMessage };
            return result;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
