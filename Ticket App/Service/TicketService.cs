using MercadoPago.Client;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Http;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Http.HttpResults;
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
        //**

        public async Task<(Payment, string)> CreatePayment(PaymentDto paymentDto)
        {
            
            MercadoPagoConfig.AccessToken = "TEST-4001436950340394-073017-57f573a54a687d75967288bb1cf132a7-555970033";

            var requestOptions = new RequestOptions();

            requestOptions.CustomHeaders.Add("x-idempotency-key", Guid.NewGuid().ToString());


            var client = new PaymentClient();
            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = paymentDto.TransactionAmount,
                Token = paymentDto.Token,
                Description = paymentDto.Description,
                Installments = paymentDto.Installments,
                PaymentMethodId =paymentDto.PaymentMethodId,
                Payer = new PaymentPayerRequest
                {
                    Email =paymentDto.Email,
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
