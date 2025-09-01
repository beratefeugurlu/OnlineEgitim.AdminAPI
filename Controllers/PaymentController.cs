using Microsoft.AspNetCore.Mvc;
using OnlineEgitim.AdminAPI.Models;
using OnlineEgitim.AdminAPI.Services;

namespace OnlineEgitim.AdminAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            var result = await _paymentService.ProcessPaymentAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
