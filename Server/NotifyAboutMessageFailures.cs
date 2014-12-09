using System;
using NServiceBus.Faults;

namespace Server
{
    internal class NotifyAboutMessageFailures : IObserver<FirstLevelRetry>
    {
        public void OnNext(FirstLevelRetry value)
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