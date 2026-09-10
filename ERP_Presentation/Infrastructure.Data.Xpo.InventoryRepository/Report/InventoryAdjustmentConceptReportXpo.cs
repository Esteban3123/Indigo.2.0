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
    [Persistent(@"Inventory.AdjustmentConcept")]
    public class InventoryAdjustmentConceptReportXpo : XPLiteObject
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
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        byte fMovementClass;
        public byte MovementClass
        {
            get { return fMovementClass; }
            set { SetPropertyValue<byte>("MovementClass", ref fMovementClass, value); }
        }
        byte fConceptType;
        public byte ConceptType
        {
            get { return fConceptType; }
            set { SetPropertyValue<byte>("ConceptType", ref fConceptType, value); }
        }
        bool fAffectsAverageCost;
        public bool AffectsAverageCost
        {
            get { return fAffectsAverageCost; }
            set { SetPropertyValue<bool>("AffectsAverageCost", ref fAffectsAverageCost, value); }
        }
        bool fShowExpiredProduct;
        public bool ShowExpiredProduct
        {
            get { return fShowExpiredProduct; }
            set { SetPropertyValue<bool>("ShowExpiredProduct", ref fShowExpiredProduct, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fAdjustmentAccountId;
        [Association(@"InventoryAdjustmentConceptReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo")]
        public InventoryGeneralLedgerMainAccountsReportXpo AdjustmentAccountId
        {
            get { return fAdjustmentAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("AdjustmentAccountId", ref fAdjustmentAccountId, value); }
        }
        InventoryPayrollCostCenterReportXpo fCostCenterId;
        [Association(@"InventoryAdjustmentConceptReportXpoReferencesInventoryPayrollCostCenterReportXpo")]
        public InventoryPayrollCostCenterReportXpo CostCenterId
        {
            get { return fCostCenterId; }
            set { SetPropertyValue<InventoryPayrollCostCenterReportXpo>("CostCenterId", ref fCostCenterId, value); }
        }
        bool fIvaAffects;
        public bool IvaAffects
        {
            get { return fIvaAffects; }
            set { SetPropertyValue<bool>("IvaAffects", ref fIvaAffects, value); }
        }
        int fIvaAccountId;
        public int IvaAccountId
        {
            get { return fIvaAccountId; }
            set { SetPropertyValue<int>("IvaAccountId", ref fIvaAccountId, value); }
        }
        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
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
        string fModificationUser;
        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }
        DateTime fModificationDate;
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }

        [Association(@"InventoryAdjustmentReportXpoReferencesInventoryAdjustmentConceptReportXpo", typeof(InventoryAdjustmentReportXpo))]
        public XPCollection<InventoryAdjustmentReportXpo> InventoryAdjustmentReportXpo { get { return GetCollection<InventoryAdjustmentReportXpo>("InventoryAdjustmentReportXpo"); } }

        [Association(@"InventoryAdjustmentDetailReportXpo_References_InventoryAdjustmentConceptReportXpo", typeof(InventoryAdjustmentDetailReportXpo))]
        public XPCollection<InventoryAdjustmentDetailReportXpo> InventoryAdjustmentDetailReportXpo { get { return GetCollection<InventoryAdjustmentDetailReportXpo>("InventoryAdjustmentDetailReportXpo"); } }

        [Association(@"InventoryTransferOrderReportXpoReferencesInventoryAdjustmentConceptReportXpo", typeof(InventoryTransferOrderReportXpo))]
        public XPCollection<InventoryTransferOrderReportXpo> InventoryTransferOrderAdjustmentReportXpo { get { return GetCollection<InventoryTransferOrderReportXpo>("InventoryTransferOrderAdjustmentReportXpo"); } }

        public InventoryAdjustmentConceptReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
