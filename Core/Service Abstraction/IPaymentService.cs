using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Abstraction
{
    public interface IPaymentService
    {
        //Create or Update PaymentIntent ==> string (string basketId)
    public Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);

        public Task UpdatePaymentStatus(string request, string stripeHeaders);


    }
}
