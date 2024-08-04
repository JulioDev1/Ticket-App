using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Mvc;
using Ticket_App.Dto;
using Ticket_App.Service.Interface;

namespace Ticket_App.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        [HttpPost("payment")]
        public async Task <ActionResult<(Payment, string)>> CreatePayment(PaymentDto paymentDto)
        {
            return await paymentService.CreatePayment(paymentDto);   
        }
    }
}
