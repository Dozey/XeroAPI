using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Linq;

namespace XeroAPI.Client
{
    [ServiceContract(Namespace = "")]
    interface IXeroClient
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Organisation", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        XElement GetOrganisation();

        [OperationContract]
        [WebGet(UriTemplate = "/Contacts", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        XElement GetContacts();

        [OperationContract]
        [WebGet(UriTemplate = "/Invoices/{invoiceNumber}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        XElement GetInvoiceByNumber(string invoiceNumber);

        [OperationContract]
        [WebGet(UriTemplate = "/Invoices?where={filter}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        XElement GetInvoices(string filter);
    }
}
