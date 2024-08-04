using MercadoPago.Client;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Http;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Repositories;
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
            var ticketsExists = await FindTicketById(tickedId);
            var userExists = await ticketRepositories.GetUserById(userId);  

            if (ticketsExists is null) {
                throw new Exception("ticket not found");
            }
            if (userExists is null)
            {
                throw new Exception("user not found");
            }
            var guid = await ticketRepositories.UserBuyedTicketEvent(userExists, ticketsExists);
            return guid;
        }
    }
}
