﻿using System;
using System.Reflection;

namespace XeroAPI.Linq
{
    internal static class AttributeUtility
    {

        public static T Find<T>(ICustomAttributeProvider attrProvider) where T : Attribute
        {
            T[] attributes = FindAll<T>(attrProvider);

            if (attributes.Length > 0)
                return attributes[0];

            return null;
        }

        public static T[] FindAll<T>(ICustomAttributeProvider attrProvider) where T : Attribute
        {
            return attrProvider.GetCustomAttributes(typeof(T), false) as T[];
        }

        public static bool TryFind<T>(ICustomAttributeProvider attrProvider, out T attribute) where T : Attribute
        {
            attribute = Find<T>(attrProvider);

            if (attribute == null)
                return false;

            return true;
        }

        public static bool TryFindAll<T>(ICustomAttributeProvider attrProvider, out T[] attributes) where T : Attribute
        {
            attributes = FindAll<T>(attrProvider);

            if (attributes == null)
                return false;

            return true;
        }
    }
}
