using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class InvoicePaymentCollection : Collection<InvoicePayment>
    {
        public InvoicePaymentCollection()
            : base()
        {
        }

        public InvoicePaymentCollection(XElement element)
            : this()
        {
            if(element != null)
                foreach (XElement paymentElement in element.Elements())
                    Add(new InvoicePayment(paymentElement));
        }
    }
}
