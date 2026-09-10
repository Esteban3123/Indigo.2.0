using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using Infrastructure.Data.Xpo.PayrollRepository;
using Infrastructure.Data.Xpo.InventoryRepository.Relation;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PurchaseOrder")]
    public class InventoryPurchaseOrderReportXpo : XPLiteObject
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
        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }
        byte fOrderType;
        public byte OrderType
        {
            get { return fOrderType; }
            set { SetPropertyValue<byte>("OrderType", ref fOrderType, value); }
        }
        bool fIsBasedContract;
        public bool IsBasedContract
        {
            get { return fIsBasedContract; }
            set { SetPropertyValue<bool>("IsBasedContract", ref fIsBasedContract, value); }
        }
        InventoryContractReportXpo fContractId;
        [Association(@"InventoryPurchaseOrderReportXpoReferencesInventoryContractReportXpo")]
        public InventoryContractReportXpo ContractId
        {
            get { return fContractId; }
            set { SetPropertyValue<InventoryContractReportXpo>("ContractId", ref fContractId, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }
        DateTime fDeliveredDate;
        public DateTime DeliveredDate
        {
            get { return fDeliveredDate; }
            set { SetPropertyValue<DateTime>("DeliveredDate", ref fDeliveredDate, value); }
        }
        InventoryCommonSupplierXpo fSupplierId;
        [Association(@"Inventory_PurchaseOrderReferencesCommon_Supplier")]
        public InventoryCommonSupplierXpo SupplierId
        {
            get { return fSupplierId; }
            set { SetPropertyValue<InventoryCommonSupplierXpo>("SupplierId", ref fSupplierId, value); }
        }
        int fSupplierDistributionLineId;
        public int SupplierDistributionLineId
        {
            get { return fSupplierDistributionLineId; }
            set { SetPropertyValue<int>("SupplierDistributionLineId", ref fSupplierDistributionLineId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryPurchaseOrderReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }

        string fDescription;
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        string fPaymentMethod;
        [Size(200)]
        public string PaymentMethod
        {
            get { return fPaymentMethod; }
            set { SetPropertyValue<string>("PaymentMethod", ref fPaymentMethod, value); }
        }
        string fDeliveryMethod;
        [Size(200)]
        public string DeliveryMethod
        {
            get { return fDeliveryMethod; }
            set { SetPropertyValue<string>("DeliveryMethod", ref fDeliveryMethod, value); }
        }
        string fDeliveryPlace;
        [Size(200)]
        public string DeliveryPlace
        {
            get { return fDeliveryPlace; }
            set { SetPropertyValue<string>("DeliveryPlace", ref fDeliveryPlace, value); }
        }
        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }
        decimal fDiscountValue;
        public decimal DiscountValue
        {
            get { return fDiscountValue; }
            set { SetPropertyValue<decimal>("DiscountValue", ref fDiscountValue, value); }
        }
        decimal fIvaValue;
        public decimal IvaValue
        {
            get { return fIvaValue; }
            set { SetPropertyValue<decimal>("IvaValue", ref fIvaValue, value); }
        }
        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
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
        string fConfirmationUser;
        [Size(20)]
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
        [Size(20)]
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

        PayrollFunctionalUnitXpo fFunctionalUnitRequestId;
        [Association(@"Inventory_PurchaseOrderReferencesCommon_FunctionalUnitRequest")]
        public PayrollFunctionalUnitXpo FunctionalUnitRequestId
        {
            get { return fFunctionalUnitRequestId; }
            set { SetPropertyValue<PayrollFunctionalUnitXpo>("FunctionalUnitRequestId", ref fFunctionalUnitRequestId, value); }
        }

        int fDaysTerm;
        public int DaysTerm
        {
            get { return fDaysTerm; }
            set { SetPropertyValue<int>("DaysTerm", ref fDaysTerm, value); }
        }

        int fTypePaymentMethod;
        public int TypePaymentMethod
        {
            get { return fTypePaymentMethod; }
            set { SetPropertyValue<int>("TypePaymentMethod", ref fTypePaymentMethod, value); }
        }

        CurrencyXpo fCurrency;
        [Association(@"Currency_References_InventoryPurchaseOrderReportXpo")]
        [Persistent("CurrencyId")]
        public CurrencyXpo Currency
        {
            get { return fCurrency; }
            set { SetPropertyValue<CurrencyXpo>("Currency", ref fCurrency, value); }
        }

        [PersistentAlias("Currency.Id")]
        public int CurrencyId
        {
            get { return Convert.ToInt32(EvaluateAlias("CurrencyId")); }

        }

        [PersistentAlias("Currency.Abbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }


        [PersistentAlias("Iif([Status] = 1, 'Registrado', [Status] = 2, 'Confirmado','Anulado')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        [Association(@"Inventory_PurchaseOrderDetailReferencesInventory_PurchaseOrder", typeof(InventoryPurchaseOrderDetailReportXpo))]
        public XPCollection<InventoryPurchaseOrderDetailReportXpo> Inventory_PurchaseOrderDetails { get { return GetCollection<InventoryPurchaseOrderDetailReportXpo>("Inventory_PurchaseOrderDetails"); } }

        public InventoryPurchaseOrderReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
