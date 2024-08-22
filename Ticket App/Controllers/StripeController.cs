using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Forwarding;
using System.Security.Claims;
using Ticket_App.Dto;
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
        public async Task <ActionResult> CheckOutOrder()
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = await CalculateOrderAmount(),
                Currency = "brl",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
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
