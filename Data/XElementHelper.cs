using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    internal static class XElementHelper
    {
        public static T ValueOrDefault<T>(XElement element)
        {
            if (element == null)
                return default(T);

            object value = TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(element.Value);

            if (value == null)
                return default(T);

            return (T)value;
        }
    }
}
