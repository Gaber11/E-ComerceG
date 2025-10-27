using Shared.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class PaymentsController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            var result = await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(result);
        }

        [HttpPost("WebHook")]
        public async Task<ActionResult> webHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeHeaders = Request.Headers["Stripe-Signature"];
           await serviceManager.PaymentService.UpdatePaymentStatus(json, stripeHeaders);
            return new EmptyResult();

        }

    }
}
