using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class LineItem
    {
        public LineItem(XElement element)
        {
            Description = XElementHelper.ValueOrDefault<string>(element.Element("Description"));
            Quantity = XElementHelper.ValueOrDefault<decimal>(element.Element("Quantity"));
            UnitAmount = XElementHelper.ValueOrDefault<decimal>(element.Element("UnitAmount"));
            ItemCode = XElementHelper.ValueOrDefault<string>(element.Element("ItemCode"));
            AccountCode = XElementHelper.ValueOrDefault<string>(element.Element("AccountCode"));
            TaxType = XElementHelper.ValueOrDefault<string>(element.Element("TaxType"));
            TaxAmount = XElementHelper.ValueOrDefault<decimal>(element.Element("TaxAmount"));
            LineAmount = XElementHelper.ValueOrDefault<decimal>(element.Element("LineAmount"));
        }

        public string Description { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal UnitAmount { get; private set; }
        public string ItemCode { get; private set; }
        public string AccountCode { get; private set; }
        public string TaxType { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal LineAmount { get; private set; }
    }
}
