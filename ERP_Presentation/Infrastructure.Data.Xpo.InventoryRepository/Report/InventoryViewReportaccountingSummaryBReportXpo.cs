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
    [Persistent("Inventory.ViewReportaccountingSummaryB")]
    public class InventoryViewReportaccountingSummaryBReportXpo : XPLiteObject
    {
        long fRow;
        [Key(true)]
        public long Row
        {
            get { return fRow; }
            set { SetPropertyValue<long>("Row", ref fRow, value); }
        }
        decimal ftotal;
        public decimal total
        {
            get { return ftotal; }
            set { SetPropertyValue<decimal>("total", ref ftotal, value); }
        }
        string fentityname;
        [Size(250)]
        public string entityname
        {
            get { return fentityname; }
            set { SetPropertyValue<string>("entityname", ref fentityname, value); }
        }
        byte fMovementType;
        public byte MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<byte>("MovementType", ref fMovementType, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        public InventoryViewReportaccountingSummaryBReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
