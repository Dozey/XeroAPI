using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Xml.Linq;
using XeroAPI.Client;
using XeroAPI.Client.ServiceModel.OAuth;
using XeroAPI.Data;
using System.Net;

namespace XeroAPI
{
    class XeroPrivateApplication : XeroApplication
    {
        private XeroClient _client;

        public XeroPrivateApplication(string key, string secret, string pfxFile, string pfxPassword)
        {
            EndpointAddress endpointAddress = new EndpointAddress("https://api.xero.com/api.xro/2.0/");


            IPHostEntry hostEntry = Dns.GetHostEntry(endpointAddress.Uri.DnsSafeHost);

            if (hostEntry.AddressList.Length == 0)
                throw new NotSupportedException("Unable to resolve host");


            OAuthClientCredentials credentials = new OAuthClientCredentials();
            credentials.OAuthKey = key;
            credentials.OAuthToken = key;
            credentials.OAuthSecret = secret;
            credentials.OAuthTokenSecret = secret;
            credentials.OAuthPfxFile = pfxFile;
            credentials.OAuthPfxPassword = pfxPassword;
            credentials.OAuthRealm = hostEntry.AddressList[0].ToString();

            _client = new XeroClient(endpointAddress, credentials);
        }

        public override Organisation Organisation
        {
            get { return new Organisation(_client.GetOrganisation().Element("Organisation")); }
        }

        public override Invoice GetInvoice(string number)
        {
            var result = _client.GetInvoiceByNumber(number);
            //result = result.Element("Invoices");

            return result != null ? new Invoice(result.Element("Invoice")) : null;
        }

        public override Invoice[] GetInvoices(string query)
        {
            var result = _client.GetInvoices(query);

            List<Invoice> invoices = new List<Invoice>();
            
            foreach (XElement invoiceElement in result.Elements())
                invoices.Add(new Invoice(invoiceElement));

            return invoices.ToArray();
        }
    }
}
