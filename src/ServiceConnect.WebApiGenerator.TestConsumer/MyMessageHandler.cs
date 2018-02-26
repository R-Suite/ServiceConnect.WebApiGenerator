using System;
using ServiceConnect.Interfaces;
using ServiceConnect.WebApiGenerator.TestMessages;

namespace ServiceConnect.WebApiGenerator.TestConsumer
{
    public class MyMessageHandler : IMessageHandler<MyMessage>
    {
        public void Execute(MyMessage message)
        {
            Console.WriteLine("MyMessageHandler - Recieved message. message.Name = {0}", message.Name);
        }

        public IConsumeContext Context { get; set; }
    }
}
