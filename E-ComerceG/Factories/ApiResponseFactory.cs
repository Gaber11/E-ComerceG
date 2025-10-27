using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModel;
using System.Net;

namespace E_ComerceG.Factories
{
    public  class ApiResponseFactory
    {
        public static IActionResult CostumValidationErrorResponse(ActionContext context)
        {
            // context ===> ModelState ===> Dectionary <string , ModelStateEntry>
            //String  ===> Key , name of param
            //ModelStatEntry  ==> object ==> errors
            //get all errors in modelstate entry
            // create costum response
            // Return 
            var errors = context.ModelState.Where(error => error.Value.Errors.Any()).Select(error =>
            new ValidationError
            {
                Field = error.Key,
                Errors = error.Value.Errors.Select(e => e.ErrorMessage)
            });
            var response = new ValidationErrorResponse()
            {
                Errors = errors,
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation Failed"

            };
            return new BadRequestObjectResult(response);



        }
    }
}
