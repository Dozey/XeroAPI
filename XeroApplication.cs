using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XeroAPI.Data;
using XeroAPI.Linq;

namespace XeroAPI
{
    public abstract class XeroApplication
    {
        private IQueryable<Invoice> _queryableInvoices;
        private IQueryable<Contact> _queryableContacts;
        private IQueryable<Item> _queryableItems;


        public XeroApplication()
        {
            
        }


        public static XeroApplication CreatePrivateApplication(string key, string secret, string pfxFile, string pfxPassword)
        {
            return new XeroPrivateApplication(key, secret, pfxFile, pfxPassword);

        }


        public abstract Organisation Organisation { get; }
        public abstract Invoice GetInvoice(string number);
        public abstract Invoice[] GetInvoices(string query);

        public IQueryable<Invoice> Invoices
        {
            get
            {
                if (_queryableInvoices == null)
                    _queryableInvoices = new QueryableXeroData<Invoice>(new XeroQueryContext<Invoice>(new XeroQueryDelegate<Invoice>((q) => GetInvoices(q))));

                return _queryableInvoices;
            }
        }
        public IQueryable<Contact> Contacts { get; set; }
        public IQueryable<Item> Items { get; set; }
    }
}
