using System.ServiceModel;
using System.ServiceModel.Channels;
using XeroAPI.Client.ServiceModel.OAuth;

namespace XeroAPI.Client.ServiceModel
{
    class XeroBinding : WebHttpBinding
    {
        private XeroMessageEncodingBindingElement _encoding = new XeroMessageEncodingBindingElement(new WebMessageEncodingBindingElement());
        private HttpsTransportBindingElement _transport = new HttpsTransportBindingElement();


        public XeroBinding()
        {
            _transport.MaxReceivedMessageSize = 1048576;
            _transport.ManualAddressing = true;
        }

        public override BindingElementCollection CreateBindingElements()
        {
            return new BindingElementCollection(new BindingElement[] { _encoding, _transport });
        }

        public override string Scheme
        {
            get { return "https"; }
        }
    }
}
