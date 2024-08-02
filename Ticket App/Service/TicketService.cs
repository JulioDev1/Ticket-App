using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Repositories.interfaces;
using Ticket_App.Service.Interface;

namespace Ticket_App.Service
{
    public class TicketService : ITicketsService
    {

        private readonly ITicketRepositories ticketRepositories;
        public TicketService(ITicketRepositories ticketRepositories)
        {
            this.ticketRepositories = ticketRepositories;
        }

        public async Task<(Payment, string)> CreatePayment(PaymentDto paymentDto)
        {

            MercadoPagoConfig.AccessToken = "APP_USR-1468252611278931-042916-be1ae241375285de1a6a3c906b5f5864-1792306976";

            if (paymentDto is null)
            {
                throw new Exception("is invalid");
            }

            var request = new PaymentCreateRequest
            {
                TransactionAmount = paymentDto.Amount,
                Description = "ticket buyed",
                Installments = 1,
                PaymentMethodId = "visa",
                Payer = new PaymentPayerRequest
                {
                    Email = paymentDto.Email,
                }
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(request);
            

            if (payment.Status == "approved")
            {
                return (payment, "pagamento aprovado");
            }
            else
            {
                return (payment, "falha ao pagar");
            }

        }

        public async Task<Tickets?> FindTicketById(Guid ticketId)
        {
            var ticket = await ticketRepositories.FindTicketById(ticketId);
            if(ticket is null)
            {
                return null;
            }
            return ticket;
        }

        public async Task<Guid> UserBuyedTicketEvent(Guid userId, Guid tickedId)
        {
            var ticketsExists = await ticketRepositories.FindTicketById(tickedId);
            if (ticketsExists is null) {
                throw new Exception("ticket not found");
            }

            var guid = await ticketRepositories.UserBuyedTicketEvent(userId, ticketsExists.Id);
            return guid;
        }
    }
}
