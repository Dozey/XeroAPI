using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XeroAPI.Linq
{
    class XeroObjectAttribute : Attribute
    {
        public XeroObjectAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}
