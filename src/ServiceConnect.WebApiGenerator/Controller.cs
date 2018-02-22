using System;
using Microsoft.AspNetCore.Mvc;

namespace ServiceConnect.WebApiGenerator
{
    public class ServiceConnectController : Controller
    {
        [HttpPost]
        [Route("/v2/myswagger/{handler}")]
        public virtual IActionResult HandleMessage([FromBody]object message, string handler)
        {
            Console.WriteLine("In the generic controller.");

            return StatusCode(200);
        }
    }
}
