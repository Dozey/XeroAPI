using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class AddressCollection : Collection<Address>
    {
        public AddressCollection()
            : base()
        {
        }

        public AddressCollection(XElement element)
            : this()
        {
            if(element != null)
                foreach (XElement addressElement in element.Elements())
                    Add(new Address(addressElement));
        }
    }
}
