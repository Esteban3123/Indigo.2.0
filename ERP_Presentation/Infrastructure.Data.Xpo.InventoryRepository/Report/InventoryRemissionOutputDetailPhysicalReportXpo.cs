using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.RemissionOutputDetailPhysical")]
    public class InventoryRemissionOutputDetailPhysicalReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRemissionOutputDetailReportXpo fRemissionOutputDetailId;
        [Association(@"InventoryRemissionOutputDetailPhysicalReportXpoReferencesInventoryRemissionOutputDetailReportXpo")]
        public InventoryRemissionOutputDetailReportXpo RemissionOutputDetailId
        {
            get { return fRemissionOutputDetailId; }
            set { SetPropertyValue<InventoryRemissionOutputDetailReportXpo>("RemissionOutputDetailId", ref fRemissionOutputDetailId, value); }
        }
        InventoryPhysicalInventoryReportXpo fPhysicalInventoryId;
        [Association(@"InventoryRemissionOutputDetailPhysicalReportXpoReferencesInventoryPhysicalInventoryReportXpo")]
        public InventoryPhysicalInventoryReportXpo PhysicalInventoryId
        {
            get { return fPhysicalInventoryId; }
            set { SetPropertyValue<InventoryPhysicalInventoryReportXpo>("PhysicalInventoryId", ref fPhysicalInventoryId, value); }
        }

        [PersistentAlias("concat(RemissionOutputDetailId.RemissionOutputId.CustomerId.Nit, ' - ',RemissionOutputDetailId.RemissionOutputId.CustomerId.Name)")]
        public string NitNameCustomer
        {
            get { return Convert.ToString(EvaluateAlias("NitNameCustomer")); }
        }

        [PersistentAlias("concat(RemissionOutputDetailId.ProductId.Code, ' - ',RemissionOutputDetailId.ProductId.Name)")]
        public string CodeNameProduct
        {
            get { return Convert.ToString(EvaluateAlias("CodeNameProduct")); }
        }


        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        bool fActivated;
        [NonPersistent()]
        public bool Activated
        {
            get { return fActivated; }
            set { this.fActivated = value; }
        }

        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }

        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventory_RemissionOutputDetailPhysical", typeof(InventoryRemissionDevolutionDetailReportXpo))]
        public XPCollection<InventoryRemissionDevolutionDetailReportXpo> Inventory_RemissionDevolutionDetailReportXpo { get { return GetCollection<InventoryRemissionDevolutionDetailReportXpo>("Inventory_RemissionDevolutionDetailReportXpo"); } }

        public InventoryRemissionOutputDetailPhysicalReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
