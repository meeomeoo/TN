using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EWallet.Web.Models
{
    public class ApiResponseModel
    {
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public static ApiResponseModel GetSuccessModel(string message)
        {
            return new ApiResponseModel { Success = true, StatusCode = HttpStatusCode.OK, Message = message };
        }

        public static ApiResponseModel GetFailureModel(string message)
        {
            return new ApiResponseModel { Success = false, StatusCode = HttpStatusCode.OK, Message = message };
        }

        public static ApiResponseModel GetErrorModel(string message)
        {
            return new ApiResponseModel { Success = false, StatusCode = HttpStatusCode.BadGateway, Message = message };
        }
    }

}
