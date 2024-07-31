using System;
using System.Threading.Tasks;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Ticket_App.Dto;

namespace Ticket_App.PaymentMethod
{

    public class PaymentConfiguration
    {
        public PaymentConfiguration() {
            MercadoPagoConfig.AccessToken = "TEST-4001436950340394-073017-57f573a54a687d75967288bb1cf132a7-555970033";
        }

        async Task<Payment> CreatePayment(TicketsDto ticketsDto, string email, string token)
        {
            if(ticketsDto is null)
            {
                new Exception("is null");
            }

            decimal priceConvert = (decimal)ticketsDto!.Price;

            var request = new PaymentCreateRequest
            {
                TransactionAmount = priceConvert,
                Token = "CARD_TOKEN",
                Description = "ticket buyed",
                Installments = 1,
                PaymentMethodId = "visa",
                Payer = new PaymentPayerRequest
                      {
                        Email = email,
                      }
            };

               var client = new PaymentClient();
               Payment payment = await client.CreateAsync(request);
               
               return payment;
        }
    }
    
}
