using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Payroll.BranchOffice")]
    public class InventoryPayrollBranchOfficeReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        [Size(50)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fAddress;
        [Size(80)]
        public string Address
        {
            get { return fAddress; }
            set { SetPropertyValue<string>("Address", ref fAddress, value); }
        }
        string fTelephone;
        [Size(15)]
        public string Telephone
        {
            get { return fTelephone; }
            set { SetPropertyValue<string>("Telephone", ref fTelephone, value); }
        }
        [Association(@"InventoryDocumentInvoiceProductSalesReportXpoReferencesInventoryPayrollBranchOfficeReportXpo", typeof(InventoryDocumentInvoiceProductSalesReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesReportXpo> InventoryDocumentInvoiceProductSalesReportXpo { get { return GetCollection<InventoryDocumentInvoiceProductSalesReportXpo>("InventoryDocumentInvoiceProductSalesReportXpo"); } }

        public InventoryPayrollBranchOfficeReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
