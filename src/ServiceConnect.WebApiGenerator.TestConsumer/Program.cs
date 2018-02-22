using System;
using Microsoft.AspNetCore.Hosting;

namespace ServiceConnect.WebApiGenerator.TestConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Generator.BuildWebHost(args).Run();

            Console.ReadLine();
        }
    }
}
