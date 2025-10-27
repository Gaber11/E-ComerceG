using Domain.Exeptions;
using Shared.ErrorModel;
using System.Net;
using System.Text.Json.Serialization;

namespace E_ComerceG.MiddleWares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next , ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
          
        }
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
                if (httpcontext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundApiAsync(httpcontext);
            }
            catch (Exception ex)
            {
                //log Exeption
                _logger.LogError($"SomeThing is wrong : {ex}");
                //Handle Exeption
                await HandleExeptionAsync(httpcontext, ex);

            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext httpcontext)
        {
            httpcontext.Response.ContentType = "application/json";
            var response = new ErrorDetails  //C# object
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The End Point{httpcontext.Request.Path} not Found"
            }.ToString();
            await httpcontext.Response.WriteAsync(response);

        }

        private async Task HandleExeptionAsync(HttpContext httpcontext, Exception ex)
        {
            //Set content type [application/json]
            //Set Status Code to 500
            // return standard response
            httpcontext.Response.ContentType = "application/json";
            httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
            var response = new ErrorDetails  //C# object
            {
                StatusCode = httpcontext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            httpcontext.Response.StatusCode = ex switch
            {
              NotFoundExeption => (int)HttpStatusCode.NotFound, //404
                UnauthorizedException => (int)HttpStatusCode.Unauthorized, //401
               ValidationExeption validationExeption => HandleValidationExeption(validationExeption, response),
                _ => (int)HttpStatusCode.InternalServerError //500

            };
            response.StatusCode = httpcontext.Response.StatusCode;

            await httpcontext.Response.WriteAsync(response.ToString());

        }

        private int HandleValidationExeption(ValidationExeption validationExeption, ErrorDetails response)
        {
            response.Errors = validationExeption.Errors;
            return (int)HttpStatusCode.BadRequest; //400


        }
    }
}
