using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class PhoneCollection : Collection<Phone>
    {
        public PhoneCollection()
            : base()
        {
        }

        public PhoneCollection(XElement element)
        {
            if(element != null)
                foreach (XElement phoneElement in element.Elements())
                    Add(new Phone(phoneElement));
        }
    }
}
