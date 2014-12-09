using System.Configuration;
using Messages;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Client
{
    public class ConfigurationSource : IConfigurationSource
    {
        private readonly string endpointName;

        public ConfigurationSource(string endpointName)
        {
            this.endpointName = endpointName;
        }

        public T GetConfiguration<T>() where T : class, new()
        {
            if (typeof( T) == typeof(MessageForwardingInCaseOfFaultConfig)) return DefaultMessageForwardingInCaseOfFaultConfig() as T;
            if (typeof(T) == typeof(UnicastBusConfig)) return DefaultUnicastBusConfig() as T;

            return ConfigurationManager.GetSection(typeof(T).Name) as T;
        }

        private MessageForwardingInCaseOfFaultConfig DefaultMessageForwardingInCaseOfFaultConfig()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = endpointName + ".ErrorQueue"
            };
        }

        private UnicastBusConfig DefaultUnicastBusConfig()
        {
            return new UnicastBusConfig
            {
                MessageEndpointMappings = new MessageEndpointMappingCollection()
                {
                    new MessageEndpointMapping()
                    {
                        AssemblyName = typeof(Command).Assembly.GetName().Name,
                        TypeFullName = typeof(Command).FullName,
                        Endpoint = endpointName
                    }   
                }
            };
        }
    }
}