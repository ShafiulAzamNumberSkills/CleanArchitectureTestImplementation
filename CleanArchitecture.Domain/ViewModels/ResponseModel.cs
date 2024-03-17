using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.ViewModels
{
    public class ResponseModel
    {
        public static ResponseModel ok(object data)
        {
            return new ResponseModel()
            {
                Success = true,
                Message = "Request Succefully Completed",
                Data = data
            };
        }

        public static ResponseModel validationErrors(Dictionary<string, string> validationErrors)
        {
            return new ResponseModel()
            {
                Success = false,
                Message = "Validation Errors!",
                ValidationErrors = validationErrors
            };
        }

        public static ResponseModel customError(string message)
        {
            return new ResponseModel()
            {
                Success = false,
                Message = message,
            };
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> ValidationErrors { get; set; }
        public object Data { get; set; }
    }
}
