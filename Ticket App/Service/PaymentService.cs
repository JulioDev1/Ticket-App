using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Ticket_App.Dto;
using Ticket_App.Service.Interface;

namespace Ticket_App.Service
{
    public class PaymentService : IPaymentService
    {
        public async Task<(Payment, string)> CreatePayment(PaymentDto paymentDto)
        {


            MercadoPagoConfig.AccessToken = "TEST-4001436950340394-073017-57f573a54a687d75967288bb1cf132a7-555970033";

            var requestOptions = new RequestOptions();

            requestOptions.CustomHeaders.Add("x-idempotency-key", Guid.NewGuid().ToString());


            var client = new PaymentClient();
            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = paymentDto.TransactionAmount,
                Description = paymentDto.Description,
                Installments = paymentDto.Installments,
                PaymentMethodId = paymentDto.PaymentMethodId,

                Payer = new PaymentPayerRequest
                {
                    Email = paymentDto.Email,
                    Identification = new IdentificationRequest
                    {
                        Type = paymentDto.Type,
                        Number = paymentDto.Number,
                    },

                },
            };

            Payment payment = await client.CreateAsync(paymentRequest, requestOptions);


            if (payment.Status == "approved")
            {
                return (payment, "pagamento aprovado");
            }
            else
            {
                return (payment, "falha ao pagar");
            }

        }
    }
}
