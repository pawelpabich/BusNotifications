using System;
using NServiceBus;

namespace Messages
{
    public class Command : ICommand
    {
        public Command()
        {
            Timestamp = DateTimeOffset.Now;
        }

        public DateTimeOffset Timestamp { get; set; }
    }
}