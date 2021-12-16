using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;

namespace OPublisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    { 
        private readonly IBus _bus;
        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                if (ticket != null)
                {
                    for (double i = 1; i < 200; i++)
                    {
                        ticket.Booked = DateTime.Now.AddDays(i);
                        ticket.UserName = i.ToString();
                        Uri uri = new Uri("rabbitMQ://localhost/orderTicketQueue");
                        var endPoint = await _bus.GetSendEndpoint(uri);
                        await endPoint.Send(ticket);
                    }
                  
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
