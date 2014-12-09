using System;
using NServiceBus.Faults;

namespace Server
{
    public class NotifyAboutToErrorQueue : IObserver<FailedMessage>
    {
        public void OnNext(FailedMessage value)
        {
            Console.WriteLine("OnNext:" + value.Exception.ToString());
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("OnError:" + error.ToString());
        }

        public void OnCompleted()
        {
            Console.WriteLine("OnCompleted:");
        }
    }
}