using System;
using ServiceConnect.Interfaces;
using ServiceConnect.WebApiGenerator.TestMessages;

namespace ServiceConnect.WebApiGenerator.TestConsumer
{
    public class MyMessageHandler : IMessageHandler<MyMessage>
    {
        public void Execute(MyMessage message)
        {
            throw new NotImplementedException();
        }

        public IConsumeContext Context { get; set; }
    }
}
