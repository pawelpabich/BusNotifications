using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using NServiceBus;
using NServiceBus.Faults;
using NServiceBus.Features;

namespace Server
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantToRunWhenBusStartsAndStops
    {
        private readonly BusNotifications notifications;
        private readonly IList<IDisposable> streams;

        public EndpointConfig()
        {
            streams = new List<IDisposable>();
            notifications = new BusNotifications();
        }

        public void Customize(BusConfiguration configuration)
        {
            const string endpointName = "BusNotificationsEndpoint";
            configuration.EndpointName(endpointName);
            configuration.AssembliesToScan(AllAssemblies.Matching("Server").And("NServiceBus").And("Messages"));
            configuration.UseTransport<SqlServerTransport>()
                         .ConnectionString("Data Source=.;Initial Catalog=Test;Integrated Security=True");
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.CustomConfigurationSource(new ConfigurationSource(endpointName));
            configuration.DisableFeature<SecondLevelRetries>();
            configuration.LicensePath(@"C:\Code\PortalPOC\Trunk\Source\ServicesPortal.DataWarehouse\bin\Debug\NServiceBusLicense.xml");

            streams.Add(notifications.Errors.MessageHasFailedAFirstLevelRetryAttempt.Subscribe(OnNext));
            streams.Add(notifications.Errors.MessageSentToErrorQueue.Subscribe(OnNext));

            configuration.RegisterComponents(c => c.RegisterSingleton(notifications));
        }

        public void Start()
        {
            //Subscription here did not work....
            //streams.Add(notifications.Errors.MessageHasFailedAFirstLevelRetryAttempt.Subscribe(OnNext));
            //streams.Add(notifications.Errors.MessageSentToErrorQueue.Subscribe(OnNext));
        }

        public void Stop()
        {
            foreach (var stream in streams)
            {
                stream.Dispose();
            }
        }

        public static void OnNext(FirstLevelRetry value)
        {
            Console.WriteLine("OnNext FirstLevelRetry:" + value.Exception.ToString());
        }

        public static void OnNext(FailedMessage value)
        {
            Console.WriteLine("OnNext FailedMessage:" + value.Exception.ToString());
        }
    }
}
