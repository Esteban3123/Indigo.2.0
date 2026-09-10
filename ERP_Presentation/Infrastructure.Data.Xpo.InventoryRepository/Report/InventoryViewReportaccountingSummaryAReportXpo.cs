using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewReportaccountingSummaryA")]
    public class InventoryViewReportaccountingSummaryAReportXpo : XPLiteObject
    {
        long fRow;
        [Key(true)]
        public long Row
        {
            get { return fRow; }
            set { SetPropertyValue<long>("Row", ref fRow, value); }
        }
        decimal fDebito;
        public decimal Debito
        {
            get { return fDebito; }
            set { SetPropertyValue<decimal>("Debito", ref fDebito, value); }
        }
        decimal fCredito;
        public decimal Credito
        {
            get { return fCredito; }
            set { SetPropertyValue<decimal>("Credito", ref fCredito, value); }
        }
        string fEntityName;
        [Size(250)]
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }
        DateTime fVoucherDate;
        public DateTime VoucherDate
        {
            get { return fVoucherDate; }
            set { SetPropertyValue<DateTime>("VoucherDate", ref fVoucherDate, value); }
        }


        public InventoryViewReportaccountingSummaryAReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
