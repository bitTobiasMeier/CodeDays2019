using System;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly AzureStorageVolumeUtility volumeUtility;

        public DeliveryController(AzureStorageVolumeUtility volumeUtility)
        {
            this.volumeUtility = volumeUtility;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return this.volumeUtility.Read();
        }

        // POST api/Delivery
        [HttpPost]
        public void Post([FromBody] Order order)
        {
            var msg = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Delivery: " + order.Quantity + "*" + order.Item + " zu " + order.Price;
            System.Diagnostics.Debug.WriteLine(msg);
            this.volumeUtility.Write(msg);
        }
    }

    public class Order
    {
        public string Item { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}