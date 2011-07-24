using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Channels;
using System.Xml.Linq;
using System.Web;
using XeroAPI.Client.ServiceModel;
using XeroAPI.Client.ServiceModel.OAuth;
using System.Net;

namespace XeroAPI.Client
{
    class XeroClient : ClientBase<IXeroClient>, IXeroClient
    {
        public XeroClient(EndpointAddress remoteAddress, OAuthClientCredentials credentials)
            : base(new XeroBinding(), remoteAddress)
        {
            Endpoint.Behaviors.Remove((IEndpointBehavior)Endpoint.Behaviors.OfType<SecurityCredentialsManager>().FirstOrDefault());
            Endpoint.Behaviors.Add(credentials);
            Endpoint.Behaviors.Add(new OAuthSecurityBehavior());
            Endpoint.Behaviors.Add(new WebHttpBehavior());
        }

        public XElement GetOrganisation()
        {
            return Channel.GetOrganisation();
        }

        public XElement GetContacts()
        {
            throw new NotImplementedException();
        }

        public XElement GetInvoiceByNumber(string invoiceNumber)
        {
            return Channel.GetInvoiceByNumber(invoiceNumber);
        }

        public XElement GetInvoices(string filter)
        {
            return Channel.GetInvoices(filter);
        }
    }
}
