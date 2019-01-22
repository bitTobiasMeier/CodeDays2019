using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // POST api/order
        [HttpPost]
        public async void PostAsync ([FromBody] Order order)
        {
            System.Diagnostics.Debug.WriteLine(order.Quantity + "*" + order.Item + " zu " + order.Price);
            var client = new HttpClient();
            var svcname = Environment.GetEnvironmentVariable("DeliveryServiceName");
            await client.PostAsJsonAsync("http://"+ svcname + "/api/Delivery/", order);
        }

        
    }
}
