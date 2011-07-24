using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XeroAPI.Data
{
    public class Contact
    {
        public Contact(XElement element)
        {
            ContactID = XElementHelper.ValueOrDefault<Guid>(element.Element("ContactID"));
            ContactStatus = XElementHelper.ValueOrDefault<string>(element.Element("ContactStatus"));
            Name = XElementHelper.ValueOrDefault<string>(element.Element("Name"));
            FirstName = XElementHelper.ValueOrDefault<string>(element.Element("FirstName"));
            LastName = XElementHelper.ValueOrDefault<string>(element.Element("LastName"));
            EmailAddress = XElementHelper.ValueOrDefault<string>(element.Element("EmailAddress"));
            BankAccountDetails = XElementHelper.ValueOrDefault<string>(element.Element("BankAccountDetails"));
            TaxNumber = XElementHelper.ValueOrDefault<string>(element.Element("TaxNumber"));
            AccountsReceivableTaxType = XElementHelper.ValueOrDefault<string>(element.Element("AccountsReceivableTaxType"));
            AccountsPayableTaxType = XElementHelper.ValueOrDefault<string>(element.Element("AccountsPayableTaxType"));
            Addresses = new AddressCollection(element.Element("Addresses"));
            Phones = new PhoneCollection(element.Element("Phones"));
            UpdatedDateUTC = XElementHelper.ValueOrDefault<DateTime>(element.Element("UpdatedDateUTC"));
            ContactGroups = XElementHelper.ValueOrDefault<string>(element.Element("ContactGroups"));
            IsSupplier = XElementHelper.ValueOrDefault<bool>(element.Element("IsSupplier"));
            IsCustomer = XElementHelper.ValueOrDefault<bool>(element.Element("IsCustomer"));
            DefaultCurrency = XElementHelper.ValueOrDefault<string>(element.Element("DefaultCurrency"));
        }

        public Guid ContactID { get; private set; }
        public string ContactStatus { get; private set; }
        public string Name { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string BankAccountDetails { get; private set; }
        public string TaxNumber { get; private set; }
        public string AccountsReceivableTaxType { get; private set; }
        public string AccountsPayableTaxType { get; private set; }
        public AddressCollection Addresses { get; private set; }
        public PhoneCollection Phones { get; private set; }
        public DateTime UpdatedDateUTC { get; private set; }
        public string ContactGroups { get; private set; }
        public bool IsSupplier { get; private set; }
        public bool IsCustomer { get; private set; }
        public string DefaultCurrency { get; private set; }
    }
}
