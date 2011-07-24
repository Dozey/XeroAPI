using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace XeroAPI.Client.ServiceModel.OAuth
{
    class OAuthSecurityBehavior : IEndpointBehavior
    {
        private OAuthClientCredentials _credentials;

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            _credentials = endpoint.Behaviors.OfType<OAuthClientCredentials>().First();

            clientRuntime.MessageInspectors.Add(new OAuthMessageInspector(_credentials));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
