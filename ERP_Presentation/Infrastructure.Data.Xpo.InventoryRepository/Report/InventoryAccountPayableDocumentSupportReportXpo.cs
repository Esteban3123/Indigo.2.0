
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Payments.AccountPayableDocumentSupport")]
    public class InventoryAccountPayableDocumentSupportReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }   
        string fInvoicePrefix;
        [Size(6)]
        public string InvoicePrefix
        {
            get { return fInvoicePrefix; }
            set { SetPropertyValue<string>("InvoicePrefix", ref fInvoicePrefix, value); }
        }

        long fConsecutive;
        public long Consecutive
        {
            get { return fConsecutive; }
            set { SetPropertyValue<long>("Consecutive", ref fConsecutive, value); }
        }

        InventoryPaymentAccountPayableReport fAccountPayableId;
        [Association(@"PaymentsAccountPayableDocumentSupport_References_PaymentsAccountPayable")]
        public InventoryPaymentAccountPayableReport AccountPayableId
        {
            get { return fAccountPayableId; }
            set { SetPropertyValue<InventoryPaymentAccountPayableReport>("AccountPayableId", ref fAccountPayableId, value); }
        }
        InventoryDocumentSupportReport fDocumentSupportId;
        [Association(@"InventoryDocumentSupportReportXpoReferencesInventoryPaymentAccountPayableReport")]
        public InventoryDocumentSupportReport DocumentSupportId
        {
            get { return fDocumentSupportId; }
            set { SetPropertyValue<InventoryDocumentSupportReport>("DocumentSupportId", ref fDocumentSupportId, value); }
        }
        public InventoryAccountPayableDocumentSupportReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
