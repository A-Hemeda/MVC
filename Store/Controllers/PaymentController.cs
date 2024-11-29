using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Services.Contract;
using Stripe;

namespace Store.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            if (basketId is null) return BadRequest("the basket Id is null");
            var basket = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);
            if (basket is null) return BadRequest();

            return Ok(basket);
        }

        //const string endpointSecret = "sk_test_51QBiKcGsroYntw23RTrt8cHmDgpgH0NsIh0pob4X6Q80yNwDDKgt6hVM4BlP1vS8zdpboW0XtIMEoUrJgWuGrywV00DZwz8e8H";

        //[HttpPost("webhook")] // Post : baseurl/api/payment/webhook http://localhost:5148/api/payment/webhook

        //public async Task<IActionResult> StripeWebHook()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        //    try
        //    {
        //        var stripeEvent = EventUtility.ConstructEvent(
        //            json,
        //            Request.Headers["Stripe-Signature"],
        //            StripeWebhookSecret
        //        );
        //        // TODO in Events...
        //        if (stripeEvent.Type == Events.PaymentIntentSucceeded)
        //        {
        //            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

        //            // Handle successful payment intent
        //            // e.g., Update order status in your database
        //            Console.WriteLine($"PaymentIntent was successful: {paymentIntent.Id}");

        //            // Perform additional logic here (e.g., send confirmation email)

        //            return Ok(); // Respond with a 200 OK status
        //        }
        //        else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
        //        {
        //            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

        //            // Handle failed payment intent
        //            // e.g., Notify the user or log the failure
        //            Console.WriteLine($"PaymentIntent failed: {paymentIntent.Id}");

        //            // Perform additional logic here (e.g., notify the user)

        //            return Ok(); // Respond with a 200 OK status
        //        }
        //        else
        //        {
        //            // Handle unrecognized event types
        //            Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
        //            return BadRequest(); // Respond with a 400 Bad Request for unhandled event types
        //        }
        //        return Ok();
        //    }
        //    catch (StripeException e)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}

    

