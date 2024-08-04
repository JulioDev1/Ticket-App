using MercadoPago.Resource.Payment;
using Ticket_App.Dto;

namespace Ticket_App.Service.Interface
{
    public interface IPaymentService
    {
        public Task<(Payment, string)> CreatePayment(PaymentDto paymentDto);

    }
}
