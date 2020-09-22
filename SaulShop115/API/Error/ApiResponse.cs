using System;

namespace API.Error
{
    public class ApiResponse
    {

        public ApiResponse(int StatusCode,string Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        public int StatusCode { get; set; }

        public string  Message { get; set; }

          private string GetDefaultMessageForStatusCode(int StatusCode)
        {
           return StatusCode switch
           {
               400 => "A bad request, you have made",
               401 => "Authorized, you are not",
               404 => "Resource found, it was not",
               500 => "Errors are the path tothe dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
               _ => null
           };
        }

    }
}