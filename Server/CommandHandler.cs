using System;
using System.Linq;
using System.Reflection;
using Messages;
using NServiceBus;
using NServiceBus.Faults;

namespace Server
{
    public class CommandHandler : IHandleMessages<Command>
    {
        private readonly IManageMessageFailures messageFailures;

        public CommandHandler(IManageMessageFailures messageFailures)
        {
            this.messageFailures = messageFailures;
            var field = messageFailures.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Single(f => f.Name == "busNotifications");
            var notifications = (BusNotifications) field.GetValue(messageFailures);
        }

        public void Handle(Command message)
        {
            Console.WriteLine("Message delivered");
            throw new NotImplementedException();
        }
    }
}