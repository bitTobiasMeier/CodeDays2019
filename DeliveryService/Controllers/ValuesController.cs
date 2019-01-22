using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AzureStorageVolumeUtility volumeUtility;

        public ValuesController(AzureStorageVolumeUtility volumeUtility)
        {
            this.volumeUtility = volumeUtility;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"StoreRoot:" + Environment.GetEnvironmentVariable("STORE_ROOT") ,  "StateFolderName:" +Environment.GetEnvironmentVariable("STATE_FOLDER_NAME") ,
                "FabricId:" + Environment.GetEnvironmentVariable("Fabric_Id") };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return this.volumeUtility.GetStateFolderPath();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
