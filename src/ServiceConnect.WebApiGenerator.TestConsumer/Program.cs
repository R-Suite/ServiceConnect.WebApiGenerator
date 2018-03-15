using System;
using ServiceConnect.Container.StructureMap;
using StructureMap;

namespace ServiceConnect.WebApiGenerator.TestConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IContainer myContainer = new StructureMap.Container();

            var bus = Bus.Initialize(config =>
            {
                config.SetContainer(myContainer);
                config.ScanForMesssageHandlers = true;
                config.SetHost("localhost");
            });

            //bus.RunWebApiHost(new[] { "http://localhost:5050", "http://localhost:5051" });
            //bus.RunWebApiHost("http://localhost:5050");
            bus.RunWebApiHost();
        }
    }
}
