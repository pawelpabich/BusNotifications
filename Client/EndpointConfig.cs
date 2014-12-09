using Client;
using NServiceBus;
using NServiceBus.Features;

namespace NsevicebusCOnfig
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class ClientEndpointConfig : IConfigureThisEndpoint
    {

        public void Customize(BusConfiguration configuration)
        {
            const string endpointName = "BusNotificationsEndpoint";
            configuration.EndpointName(endpointName);
            configuration.AssembliesToScan(AllAssemblies.Matching("Client").And("NServiceBus").And("Messages"));
            configuration.UseTransport<SqlServerTransport>()
                         .ConnectionString("Data Source=.;Initial Catalog=Test;Integrated Security=True");
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.CustomConfigurationSource(new ConfigurationSource(endpointName));
            configuration.LicensePath(@"C:\Code\PortalPOC\Trunk\Source\ServicesPortal.DataWarehouse\bin\Debug\NServiceBusLicense.xml");     
            configuration.DisableFeature<SecondLevelRetries>();
        }
    }
}
