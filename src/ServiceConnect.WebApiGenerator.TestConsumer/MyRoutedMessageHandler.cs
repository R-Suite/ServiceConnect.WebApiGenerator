using System;
using ServiceConnect.Core;
using ServiceConnect.Interfaces;
using ServiceConnect.WebApiGenerator.TestMessages;

namespace ServiceConnect.WebApiGenerator.TestConsumer
{
    [RoutingKey("Route1")]
    public class MyRoutedMessageHandler : IMessageHandler<MyMessage>
    {
        public void Execute(MyMessage message)
        {
            Console.WriteLine("MyRoutedMessageHandler - Recieved message. message.Name = {0}", message.Name);
        }

        public IConsumeContext Context { get; set; }
    }
}
