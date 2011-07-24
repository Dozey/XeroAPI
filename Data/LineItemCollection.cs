using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class LineItemCollection : Collection<LineItem>
    {
        public LineItemCollection()
            : base()
        {
        }

        public LineItemCollection(XElement element)
            : this()
        {
            if(element != null)
                foreach (XElement lineItemElement in element.Elements())
                    Add(new LineItem(lineItemElement));
        }
    }
}
