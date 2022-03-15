using Microsoft.AspNetCore.Mvc;
using Mollie.Api.Client;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Models;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Controllers
{
    public class PaymentController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IPaymentClient paymentClient = new PaymentClient("test_kaN7VyneuvgknQKDjn9kmSJExTDux2");
            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = new Amount(Currency.EUR, 100.00m),
                Description = "Test payment of the example project",
                RedirectUrl = "https://google.com",
                Method = Mollie.Api.Models.Payment.PaymentMethod.Ideal
            };

            PaymentResponse paymentResponse = await paymentClient.CreatePaymentAsync(paymentRequest);
            
            return View(paymentResponse.Links.Checkout);
        }
    }
}
