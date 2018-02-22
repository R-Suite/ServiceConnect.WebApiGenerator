using System;
using ServiceConnect.Interfaces;

namespace ServiceConnect.WebApiGenerator.TestMessages
{
    public class MyMessage : Message
    {
        public MyMessage(Guid correlationId) : base(correlationId)
        {
        }

        public string Name { get; set; }
    }
}
