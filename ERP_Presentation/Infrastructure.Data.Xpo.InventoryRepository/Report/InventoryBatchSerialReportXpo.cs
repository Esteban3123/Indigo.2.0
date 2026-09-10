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
    [Persistent(@"Inventory.BatchSerial")]
    public class InventoryBatchSerialReportXpo : XPLiteObject
    {

        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        InventoryProductReportXpo fProductId;
        [Association(@"InventoryBatchSerialReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }

        byte fType;
        public byte Type
        {
            get { return fType; }
            set { SetPropertyValue<byte>("Type", ref fType, value); }
        }

        string fBatchCode;
        [Size(50)]
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }

        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }

        string fBarcode;
        [Size(50)]
        public string Barcode
        {
            get { return fBarcode; }
            set { SetPropertyValue<string>("Barcode", ref fBarcode, value); }
        }

        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }

        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }

        //Propiedad Añadida
        bool fSeleccionado = false;
        [NonPersistent()]
        public bool Seleccionado
        {
            get { return fSeleccionado; }
            set { this.fSeleccionado = value; }
        }

        #endregion

        #region "Navigation"

        [Association(@"InventoryKardexReportXpoReferencesInventoryBatchSerialReportXpo", typeof(InventoryKardexReportXpo))]
        public XPCollection<InventoryKardexReportXpo> InventoryKardexReportXpo { get { return GetCollection<InventoryKardexReportXpo>("InventoryKardexReportXpo"); } }

        [Association(@"Inventory_RemissionEntranceDetailBatchSerialReferencesInventoryBatchSerialReportXpo", typeof(InventoryRemissionEntranceDetailBatchSerialReportXpo))]
        public XPCollection<InventoryRemissionEntranceDetailBatchSerialReportXpo> InventoryRemissionEntranceDetailBatchSerials { get { return GetCollection<InventoryRemissionEntranceDetailBatchSerialReportXpo>("InventoryRemissionEntranceDetailBatchSerials"); } }

        [Association(@"Inventory_ConsignmentInventoryRemissionDetailBatchSerialReferencesInventoryBatchSerialReportXpo", typeof(InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo))]
        public XPCollection<InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo> InventoryConsignmentInventoryRemissionDetailBatchSerials { get { return GetCollection<InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo>("InventoryConsignmentInventoryRemissionDetailBatchSerials"); } }

        [Association(@"Inventory_EntranceVoucherDetailBatchSerialReferencesInventoryBatchSerialReportXpo", typeof(InventoryEntranceVoucherDetailBatchSerialReportXpo))]
        public XPCollection<InventoryEntranceVoucherDetailBatchSerialReportXpo> InventoryEntranceVoucherDetailBatchSerials { get { return GetCollection<InventoryEntranceVoucherDetailBatchSerialReportXpo>("InventoryEntranceVoucherDetailBatchSerials"); } }

        [Association(@"InventoryPhysicalInventoryReportXpoReferencesInventoryBatchSerialReportXpo", typeof(InventoryPhysicalInventoryReportXpo))]
        public XPCollection<InventoryPhysicalInventoryReportXpo> InventoryPhysicalInventoryReportXpo { get { return GetCollection<InventoryPhysicalInventoryReportXpo>("InventoryPhysicalInventoryReportXpo"); } }

        [Association(@"InventoryAdjustmentDetailBatchSerialReportXpoReferencesInventoryBatchSerialReportXpo", typeof(InventoryAdjustmentDetailBatchSerialReportXpo))]
        public XPCollection<InventoryAdjustmentDetailBatchSerialReportXpo> InventoryAdjustmentDetailBatchSerialReportXpo { get { return GetCollection<InventoryAdjustmentDetailBatchSerialReportXpo>("InventoryAdjustmentDetailBatchSerialReportXpo"); } }

        [Association(@"InventoryLoanMerchandiseDetailBatchSerialReportXpoReferencesInventoryBatchSerialReportXpo", typeof(InventoryLoanMerchandiseDetailBatchSerialReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDetailBatchSerialReportXpo> InventoryBatchLoanMerchandiseDetailBatchSerialReportXpo { get { return GetCollection<InventoryLoanMerchandiseDetailBatchSerialReportXpo>("InventoryBatchLoanMerchandiseDetailBatchSerialReportXpo"); } }

        [Association(@"Inventory_InventoryControlDetailBatchSerialReferencesInventory_BatchSerial", typeof(InventoryControlDetailBatchSerialReportXpo))]
        public XPCollection<InventoryControlDetailBatchSerialReportXpo> InventoryControlDetailBatchSerials { get { return GetCollection<InventoryControlDetailBatchSerialReportXpo>("InventoryControlDetailBatchSerials"); } }

        #endregion

        #region "Builders"

        public InventoryBatchSerialReportXpo(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion

    }
}
