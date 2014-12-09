using System;
using Messages;
using NsevicebusCOnfig;
using NServiceBus;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new BusConfiguration();

            new ClientEndpointConfig().Customize(configuration);
            var bus = Bus.CreateSendOnly(configuration);

            bus.Send(new Command());

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
