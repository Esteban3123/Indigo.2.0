#region "Imports"

using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.ViewConsignmentInventoryRemissionWithoutLegalize")]
    public partial class ViewConsignmentInventoryRemissionWithoutLegalizeXpo : XPLiteObject
    {

        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int? fBatchSerialId;
        public int? BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<int?>("BatchSerialId", ref fBatchSerialId, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        int fUsedQuantity;
        public int UsedQuantity
        {
            get { return fUsedQuantity; }
            set { SetPropertyValue<int>("UsedQuantity", ref fUsedQuantity, value); }
        }

        int fLegalizedQuantity;
        public int LegalizedQuantity
        {
            get { return fLegalizedQuantity; }
            set { SetPropertyValue<int>("LegalizedQuantity", ref fLegalizedQuantity, value); }
        }

        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }

        int fProductId;
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
        }

        decimal fUnitValue;
        public decimal UnitValue
        {
            get { return fUnitValue; }
            set { SetPropertyValue<decimal>("UnitValue", ref fUnitValue, value); }
        }

        decimal fIvaValue;
        public decimal IvaValue
        {
            get { return fIvaValue; }
            set { SetPropertyValue<decimal>("IvaValue", ref fIvaValue, value); }
        }

        decimal fLastValue;
        public decimal LastValue
        {
            get { return fLastValue; }
            set { SetPropertyValue<decimal>("LastValue", ref fLastValue, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        DateTime fRemissionDate;
        public DateTime RemissionDate
        {
            get { return fRemissionDate; }
            set { SetPropertyValue<DateTime>("RemissionDate", ref fRemissionDate, value); }
        }

        int fSupplierId;
        public int SupplierId
        {
            get { return fSupplierId; }
            set { SetPropertyValue<int>("SupplierId", ref fSupplierId, value); }
        }

        int fSupplierDistributionLineId;
        public int SupplierDistributionLineId
        {
            get { return fSupplierDistributionLineId; }
            set { SetPropertyValue<int>("SupplierDistributionLineId", ref fSupplierDistributionLineId, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }

        string fGroupCodeName;
        public string GroupCodeName
        {
            get { return fGroupCodeName; }
            set { SetPropertyValue<string>("GroupCodeName", ref fGroupCodeName, value); }
        }

        string fCodeNameProduct;
        public string CodeNameProduct
        {
            get { return fCodeNameProduct; }
            set { SetPropertyValue<string>("CodeNameProduct", ref fCodeNameProduct, value); }
        }

        string fBatchCode;
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }

        int fCurrencyId;
        public int CurrencyId
        {
            get { return fCurrencyId; }
            set { SetPropertyValue<int>("CurrencyId", ref fCurrencyId, value); }
        }

        string fCurrencyAbbreviation;
        public string CurrencyAbbreviation
        {
            get { return fCurrencyAbbreviation; }
            set { SetPropertyValue<string>("CurrencyAbbreviation", ref fCurrencyAbbreviation, value); }
        }

        #endregion

        #region Builders

        public ViewConsignmentInventoryRemissionWithoutLegalizeXpo(Session session) : base(session)
        {
        }

        public ViewConsignmentInventoryRemissionWithoutLegalizeXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }

}
