using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class InvoicePayment
    {
        public InvoicePayment(XElement element)
        {
            Date = XElementHelper.ValueOrDefault<DateTime>(element.Element("Date"));
            Amount = XElementHelper.ValueOrDefault<decimal>(element.Element("Amount"));
            PaymentId = XElementHelper.ValueOrDefault<Guid>(element.Element("PaymentID"));
        }

        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public Guid PaymentId { get; private set; }
    }
}
