using MercadoPago.Resource.Payment;
using Ticket_App.Dto;
using Ticket_App.Model;

namespace Ticket_App.Service.Interface
{
    public interface ITicketsService
    {
        public Task<Guid> UserBuyedTicketEvent(Guid userId, Guid tickedId);
        public Task<(Payment, string)> CreatePayment(PaymentDto paymentDto);
        public Task<Tickets?> FindTicketById(Guid ticketId);

    }
}
