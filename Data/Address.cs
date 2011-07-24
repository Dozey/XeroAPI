using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class Address
    {
        public Address(XElement element)
        {
            AddressType = XElementHelper.ValueOrDefault<string>(element.Element("AddressType"));
            AttentionTo = XElementHelper.ValueOrDefault<string>(element.Element("AttentionTo"));
            AddressLine1 = XElementHelper.ValueOrDefault<string>(element.Element("AddressLine1"));
            AddressLine2 = XElementHelper.ValueOrDefault<string>(element.Element("AddressLine2"));
            AddressLine3 = XElementHelper.ValueOrDefault<string>(element.Element("AddressLine3"));
            AddressLine4 = XElementHelper.ValueOrDefault<string>(element.Element("AddressLine4"));
            City = XElementHelper.ValueOrDefault<string>(element.Element("City"));
            Region = XElementHelper.ValueOrDefault<string>(element.Element("Region"));
            PostalCode = XElementHelper.ValueOrDefault<string>(element.Element("PostalCode"));
            Country = XElementHelper.ValueOrDefault<string>(element.Element("Country"));
        }

        public string AddressType { get; private set; }
        public string AttentionTo { get; private set; }
        public string AddressLine1 { get; private set;}
        public string AddressLine2 { get; private set;}
        public string AddressLine3 { get; private set;}
        public string AddressLine4 { get; private set;}
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
    }
}
