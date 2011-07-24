using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class Phone
    {
        public Phone(XElement element)
        {
            PhoneType = XElementHelper.ValueOrDefault<string>(element.Element("PhoneType"));
            PhoneNumber = XElementHelper.ValueOrDefault<string>(element.Element("PhoneNumber"));
            PhoneAreaCode = XElementHelper.ValueOrDefault<string>(element.Element("PhoneAreaCode"));
            PhoneCountryCode = XElementHelper.ValueOrDefault<string>(element.Element("PhoneCountryCode"));
        }

        public string PhoneType { get; private set; }
        public string PhoneNumber { get; private set; }
        public string PhoneAreaCode { get; private set; }
        public string PhoneCountryCode { get; private set; }
    }
}
