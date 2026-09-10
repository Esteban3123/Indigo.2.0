using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.EntranceVoucherDevolution")]
    public class InventoryEntranceVouhcerDevolutionReportXpo :XPLiteObject
    {

        #region Properties

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }

        InventoryEntranceVoucherReportXpo fEntranceVoucherId;
        [Association(@"Inventory_EntranceVoucherDevolutionReferencesInventory_EntranceVoucher")]
        public InventoryEntranceVoucherReportXpo EntranceVoucherId
        {
            get { return fEntranceVoucherId; }
            set { SetPropertyValue<InventoryEntranceVoucherReportXpo>("EntranceVoucherId", ref fEntranceVoucherId, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        decimal fFreightValue;
        public decimal FreightValue
        {
            get { return fFreightValue; }
            set { SetPropertyValue<decimal>("FreightValue", ref fFreightValue, value); }
        }

        decimal fFreightIVAPercentage;
        public decimal FreightIVAPercentage
        {
            get { return fFreightIVAPercentage; }
            set { SetPropertyValue<decimal>("FreightIVAPercentage", ref fFreightIVAPercentage, value); }
        }

        decimal fFreightIVAValue;
        public decimal FreightIVAValue
        {
            get { return fFreightIVAValue; }
            set { SetPropertyValue<decimal>("FreightIVAValue", ref fFreightIVAValue, value); }
        }

        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }

        decimal fValueDiscount;
        public decimal ValueDiscount
        {
            get { return fValueDiscount; }
            set { SetPropertyValue<decimal>("ValueDiscount", ref fValueDiscount, value); }
        }

        decimal fValueTax;
        public decimal ValueTax
        {
            get { return fValueTax; }
            set { SetPropertyValue<decimal>("ValueTax", ref fValueTax, value); }
        }

        decimal fWithholdingTax;
        public decimal WithholdingTax
        {
            get { return fWithholdingTax; }
            set { SetPropertyValue<decimal>("WithholdingTax", ref fWithholdingTax, value); }
        }

        decimal fWithholdingICA;
        public decimal WithholdingICA
        {
            get { return fWithholdingICA; }
            set { SetPropertyValue<decimal>("WithholdingICA", ref fWithholdingICA, value); }
        }

        decimal fRetentionSource;
        public decimal RetentionSource
        {
            get { return fRetentionSource; }
            set { SetPropertyValue<decimal>("RetentionSource", ref fRetentionSource, value); }
        }

        decimal fRetentionOther;
        public decimal RetentionOther
        {
            get { return fRetentionOther; }
            set { SetPropertyValue<decimal>("RetentionOther", ref fRetentionOther, value); }
        }

        decimal fDeductionOther;
        public decimal DeductionOther
        {
            get { return fDeductionOther; }
            set { SetPropertyValue<decimal>("DeductionOther", ref fDeductionOther, value); }
        }

        decimal fDistrictTax;
        public decimal DistrictTax
        {
            get { return fDistrictTax; }
            set { SetPropertyValue<decimal>("DistrictTax", ref fDistrictTax, value); }
        }

        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
        }

        int fPaymentNoteId;
        public int PaymentNoteId
        {
            get { return fPaymentNoteId; }
            set { SetPropertyValue<int>("PaymentNoteId", ref fPaymentNoteId, value); }
        }

        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        string fCreationUser;
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

        string fConfirmationUser;
        public string ConfirmationUser
        {
            get { return fConfirmationUser; }
            set { SetPropertyValue<string>("ConfirmationUser", ref fConfirmationUser, value); }
        }

        DateTime fConfirmationDate;
        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }

        string fAnnulmentUser;
        public string AnnulmentUser
        {
            get { return fAnnulmentUser; }
            set { SetPropertyValue<string>("AnnulmentUser", ref fAnnulmentUser, value); }
        }

        DateTime fAnnulmentDate;
        public DateTime AnnulmentDate
        {
            get { return fAnnulmentDate; }
            set { SetPropertyValue<DateTime>("AnnulmentDate", ref fAnnulmentDate, value); }
        }

        #endregion

        #region Custom Members

        [PersistentAlias("iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', 'N/A')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        #endregion

        #region Navigation Properties

        [Association(@"Inventory_EntranceVoucherDevolutionDetailReferencesInventory_EntranceVoucherDevolution", typeof(InventoryEntranceVoucherDevolutionDetailReportXpo))]
        public XPCollection<InventoryEntranceVoucherDevolutionDetailReportXpo> Inventory_EntranceVoucherDevolutionDetails { get { return GetCollection<InventoryEntranceVoucherDevolutionDetailReportXpo>("Inventory_EntranceVoucherDevolutionDetails"); } }

        [Association(@"Inventory_EntranceVoucherDevolutionOtherDeductionReferencesInventory_EntranceVoucherDevolution", typeof(InventoryEntranceVoucherDevolutionOtherDeductionReportXpo))]
        public XPCollection<InventoryEntranceVoucherDevolutionOtherDeductionReportXpo> Inventory_EntranceVoucherDevolutionOtherDeductions { get { return GetCollection<InventoryEntranceVoucherDevolutionOtherDeductionReportXpo>("Inventory_EntranceVoucherDevolutionOtherDeductions"); } }

        #endregion

        #region Builders

        public InventoryEntranceVouhcerDevolutionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion

    }
}
