using System;
using Messages;
using NServiceBus;

namespace Server
{
    public class CommandHandler : IHandleMessages<Command>
    {
        public void Handle(Command message)
        {
            Console.WriteLine("Message delivered");
            throw new NotImplementedException();
        }
    }
}