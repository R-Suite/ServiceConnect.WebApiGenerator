using System;
using System.Linq;
using System.Reflection;
using Common.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceConnect.Interfaces;

namespace ServiceConnect.WebApiGenerator
{
    /// <summary>
    /// Generic web api controller.
    /// </summary>
    public class ServiceConnectController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ServiceConnectController));

        /// <summary>
        /// Generic action. Recieves all web api requests and forwards each request to an appropriate ServiceConnect handler.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fullTypeName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/handlers/{handler}")]
        public virtual IActionResult HandleMessage([FromBody]object message, [FromHeader(Name = "msg-full-type-name")] string fullTypeName, string handler)
        {
            Logger.InfoFormat("ServiceConnectController handling {0}.", fullTypeName);

            Type messageType;
            try
            {
                messageType = Type.GetType(fullTypeName);
            }
            catch (Exception e)
            {
                Logger.Error("Error getting [message] type. ", e);
                throw;
            }

            // Cast
            MethodInfo cast = GetType().GetMethods().First(m => m.Name == "Cast");
            MethodInfo genericCast = cast.MakeGenericMethod(messageType);
            var msgObj = genericCast.Invoke(this, new object[] { message });

            // Send
            MethodInfo send = GetType().GetMethods().First(m => m.Name == "Send");
            MethodInfo genericSend = send.MakeGenericMethod(messageType);
            genericSend.Invoke(this, new object[] { msgObj });

            return StatusCode(200);
        }

        public T Cast<T>(object input)
        {
            T retval;

            try
            {
                retval = JsonConvert.DeserializeObject<T>(input.ToString());
            }
            catch (Exception e)
            {
                Logger.Error("Error desirialising object. ", e);
                throw;
            }

            return retval;
        }

        public void Send<T>(T msg) where T : Message
        {
            IBus bus = Generator.Bus;
            bus.Send<T>(bus.Configuration.TransportSettings.QueueName, msg);
        }
    }
}
