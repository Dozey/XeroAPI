using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using XeroAPI.Linq;

namespace XeroAPI.Data
{
    [XeroObject("Invoice")]
    public class Invoice
    {
        public Invoice(XElement element)
        {
            Type = XElementHelper.ValueOrDefault<string>(element.Element("Type"));
            Contact = new Contact(element.Element("Contact"));
            Date = XElementHelper.ValueOrDefault<DateTime>(element.Element("Date"));
            DueDate = XElementHelper.ValueOrDefault<DateTime>(element.Element("DueDate"));
            InvoiceId = XElementHelper.ValueOrDefault<Guid>(element.Element("InvoiceID"));
            InvoiceNumber = XElementHelper.ValueOrDefault<string>(element.Element("InvoiceNumber"));
            BrandingThemeId = XElementHelper.ValueOrDefault<Guid>(element.Element("BrandingThemeID"));
            Url = XElementHelper.ValueOrDefault<Uri>(element.Element("Url"));
            CurrencyCode = XElementHelper.ValueOrDefault<string>(element.Element("CurrencyCode"));
            Status = XElementHelper.ValueOrDefault<string>(element.Element("Status"));
            LineAmountTypes = XElementHelper.ValueOrDefault<string>(element.Element("LineAmountTypes"));
            SubTotal = XElementHelper.ValueOrDefault<decimal>(element.Element("SubTotal"));
            TotalTax = XElementHelper.ValueOrDefault<decimal>(element.Element("TotalTax"));
            Total = XElementHelper.ValueOrDefault<decimal>(element.Element("Total"));
            AmountDue = XElementHelper.ValueOrDefault<decimal>(element.Element("AmountDue"));
            AmountPaid = XElementHelper.ValueOrDefault<decimal>(element.Element("AmountPaid"));
            AmountCredited = XElementHelper.ValueOrDefault<decimal>(element.Element("AmountCredited"));
            LineItems = new LineItemCollection(element.Element("LineItems"));
            UpdatedDateUTC = XElementHelper.ValueOrDefault<DateTime>(element.Element("UpdatedDateUTC"));
            Payments = new InvoicePaymentCollection(element.Element("Payments"));
        }

        public string Type { get; private set; }
        public Contact Contact { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime DueDate { get; private set; }
        public Guid InvoiceId { get; private set; }
        public string InvoiceNumber { get; private set; }
        public Guid BrandingThemeId { get; private set; }
        public Uri Url { get; private set; }
        public string CurrencyCode { get; private set; }
        public string Status { get; private set; }
        public string LineAmountTypes { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal TotalTax { get; private set; }
        public decimal Total { get; private set; }
        public decimal AmountDue { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal AmountCredited { get; private set; }
        public DateTime UpdatedDateUTC { get; private set; }
        public LineItemCollection LineItems { get; private set; }
        public InvoicePaymentCollection Payments { get; private set; }

    }
}
