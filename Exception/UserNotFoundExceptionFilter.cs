using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace WebAPI.Exception
{
    public class UserNotFoundExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Handle the exception and generate a response
            if (context.Exception is UserNotFoundException ex)
            {
                var response = new { error = ex.Message };
                var payload = JsonConvert.SerializeObject(response);
                context.Result = new ContentResult
                {
                    Content = payload,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
