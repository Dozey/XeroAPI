using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XeroAPI.Data
{
    public static class InvoiceType
    {
        public const string AccountReceivable = "ACCREC";
        public const string AccountPayable = "ACCPAY";
    }

    public static class PhoneType
    {
        public const string Default = "DEFAULT";
        public const string DDI = "DDI";
        public const string Mobile = "MOBILE";
        public const string Fax = "FAX";
    }

    public static class AddressType
    {
        public const string POBox = "POBOX";
        public const string Stree = "STREET";
    }

    public static class ContactStatus
    {
        public const string Active = "ACTIVE";
        public const string Deleted = "DELETED";
    }

    public static class InvoiceStatus
    {
        public const string Draft = "DRAFT";
        public const string Submitted = "SUBMITTED";
        public const string Deleted = "DELETED";
        public const string Authorised = "AUTHORISED";
        public const string Paid = "PAID";
        public const string Voided = "VOIDED";
    }

    public static class LineAmountType
    {
        public const string Exclusive = "EXCLUSIVE";
        public const string Inclusive = "INCLUSIVE";
    }


}
