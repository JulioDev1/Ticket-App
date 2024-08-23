using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stripe;
using Stripe.Checkout;
using Stripe.FinancialConnections;
using System.Security.Claims;
using Ticket_App.Service.Interface;

namespace Ticket_App.Controllers
{

    [Route("api/[controller]")]

    public class StripeController : Controller
    {

        private readonly IUserService userService;

        public StripeController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost("payment-stripe")]
        [Authorize]
        public async Task <string> CheckOutOrder()
        {
            var domain = "http://localhost:5173/";
            
            var id = (User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var guid = Guid.Parse(id);

            var ticket = await userService.ListUserTickets(guid);

            var TicketMapped = ticket.Select(t => new
            {
                Name = t.Name ?? "No Name",
                Event = t.Event != null && t.Event.Description != null ? t.Event.Description : "No Description",
            }).ToList();

            if (ticket is null || !ticket.Any())
            {
                new Exception("user not found ");
            }

            var options = new Stripe.Checkout.SessionCreateOptions
            {

                LineItems = new List<SessionLineItemOptions> {
                    new()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = await CalculateOrderAmount(),
                            Currency = "BRL",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                              Name= string.Join(",", TicketMapped.Select(t=> t.Name )),
                              Description = string.Join(" | ", TicketMapped.Select(t=> t.Event))

                            },
                        },
                        Quantity = 1,
                    }
                   
                },
               

                Mode = "payment",
                SuccessUrl = domain + "?sucess=true",
                CancelUrl = domain +"?canceled=true"
                
            };
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = await service.CreateAsync(options);

            return session.Id;
        }

        [HttpGet("session-status")]
        public ActionResult SessionStatus([FromQuery] string session_id)
        {
            var sessionService = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = sessionService.Get(session_id);

            // Verifica se "customer_details" é um JObject e tenta acessar o email de forma segura
            var customerDetails = session.RawJObject["customer_details"] as JObject;
            string customerEmail = customerDetails?["email"]?.ToString() ?? "Email not available";

            return Ok(new
            {
                status = session.RawJObject["status"]?.ToString() ?? "Status not available",
                customer_email = customerEmail
            });
        }



        private async Task<long> CalculateOrderAmount()
        {
            var id = (User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var guid = Guid.Parse(id);

            var ticket = await userService.ListUserTickets(guid);

            if (ticket is null)
            {
                new Exception("user not found ");
            }

            var price = ticket!.Sum(p => p.Price);
            var amountInCents = (long)(price * 100);

            return amountInCents;

        }
    }
}
