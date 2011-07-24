using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class Organisation
    {
        public Organisation(XElement element)
        {
            Name = XElementHelper.ValueOrDefault<string>(element.Element("Name"));
            LegalName = XElementHelper.ValueOrDefault<string>(element.Element("LegalName"));
            PaysTax = XElementHelper.ValueOrDefault<bool>(element.Element("PaysTax"));
            Version = XElementHelper.ValueOrDefault<string>(element.Element("Version"));
            BaseCurrency = XElementHelper.ValueOrDefault<string>(element.Element("BaseCurrency"));
        }

        public string Name { get; private set; }
        public string LegalName { get; private set; }
        public bool PaysTax { get; private set; }
        public string Version { get; private set; }
        public string BaseCurrency { get; private set;}
    }
}
