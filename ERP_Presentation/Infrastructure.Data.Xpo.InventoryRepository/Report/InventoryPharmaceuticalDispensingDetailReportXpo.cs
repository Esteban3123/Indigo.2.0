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
    [Persistent(@"Inventory.PharmaceuticalDispensingDetail")]
    public class InventoryPharmaceuticalDispensingDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        InventoryPharmaceuticalDispensingReportXpo fPharmaceuticalDispensingId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_PharmaceuticalDispensing")]
        public InventoryPharmaceuticalDispensingReportXpo PharmaceuticalDispensingId
        {
            get { return fPharmaceuticalDispensingId; }
            set { SetPropertyValue<InventoryPharmaceuticalDispensingReportXpo>("PharmaceuticalDispensingId", ref fPharmaceuticalDispensingId, value); }
        }

        ContractCareGroupReportXpo fCareGroupId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesContract_CareGroup")]
        public ContractCareGroupReportXpo CareGroupId
        {
            get { return fCareGroupId; }
            set { SetPropertyValue<ContractCareGroupReportXpo>("CareGroupId", ref fCareGroupId, value); }
        }

        ContractHealthAdministratorReportXpo fHealthAdministratorId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesContract_HealthAdministrator")]
        public ContractHealthAdministratorReportXpo HealthAdministratorId
        {
            get { return fHealthAdministratorId; }
            set { SetPropertyValue<ContractHealthAdministratorReportXpo>("HealthAdministratorId", ref fHealthAdministratorId, value); }
        }

        InventoryCommonThirdPartyXpo fThirdPartyId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesCommon_ThirdParty")]
        public InventoryCommonThirdPartyXpo ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<InventoryCommonThirdPartyXpo>("ThirdPartyId", ref fThirdPartyId, value); }
        }

        InventoryProductReportXpo fProductId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_InventoryProduct")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
     
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_Warehouse")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        int fReturnedQuantity;
        public int ReturnedQuantity
        {
            get { return fReturnedQuantity; }
            set { SetPropertyValue<int>("ReturnedQuantity", ref fReturnedQuantity, value); }
        }

        DateTime fServiceDate;
        public DateTime ServiceDate
        {
            get { return fServiceDate; }
            set { SetPropertyValue<DateTime>("ServiceDate", ref fServiceDate, value); }
        }

        InventoryPayrollFunctionalUnitXpo fFunctionalUnitId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesPayroll_FunctionalUnit")]
        public InventoryPayrollFunctionalUnitXpo FunctionalUnitId
        {
            get { return fFunctionalUnitId; }
            set { SetPropertyValue<InventoryPayrollFunctionalUnitXpo>("FunctionalUnitId", ref fFunctionalUnitId, value); }
        }

        string fOrderedHealthProfessionalCode;
        [Size(20)]
        public string OrderedHealthProfessionalCode
        {
            get { return fOrderedHealthProfessionalCode; }
            set { SetPropertyValue<string>("OrderedHealthProfessionalCode", ref fOrderedHealthProfessionalCode, value); }
        }

        string fOrderedProfessionalSpecialty;
        [Size(3)]
        public string OrderedProfessionalSpecialty
        {
            get { return fOrderedProfessionalSpecialty; }
            set { SetPropertyValue<string>("OrderedProfessionalSpecialty", ref fOrderedProfessionalSpecialty, value); }
        }

        InventoryCommonThirdPartyXpo fOrderedHealthProfessionalThirdPartyId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesCommon_OrderedHealthProfessionalThirdPartyId")]
        public InventoryCommonThirdPartyXpo OrderedHealthProfessionalThirdPartyId
        {
            get { return fOrderedHealthProfessionalThirdPartyId; }
            set { SetPropertyValue<InventoryCommonThirdPartyXpo>("OrderedHealthProfessionalThirdPartyId", ref fOrderedHealthProfessionalThirdPartyId, value); }
        }
        
        string fAuthorizationNumber;
        [Size(20)]
        public string AuthorizationNumber
        {
            get { return fAuthorizationNumber; }
            set { SetPropertyValue<string>("AuthorizationNumber", ref fAuthorizationNumber, value); }
        }

        byte fLiquidationType;
        public byte LiquidationType
        {
            get { return fLiquidationType; }
            set { SetPropertyValue<byte>("LiquidationType", ref fLiquidationType, value); }
        }

        int fCupsEntityId;
        public int CupsEntityId
        {
            get { return fCupsEntityId; }
            set { SetPropertyValue<int>("CupsEntityId", ref fCupsEntityId, value); }
        }

        bool fSurchargeApply;
        public bool SurchargeApply
        {
            get { return fSurchargeApply; }
            set { SetPropertyValue<bool>("SurchargeApply", ref fSurchargeApply, value); }
        }

        decimal fSalePrice;
        public decimal SalePrice
        {
            get { return fSalePrice; }
            set { SetPropertyValue<decimal>("SalePrice", ref fSalePrice, value); }
        }

        decimal fAverageCost;
        public decimal AverageCost
        {
            get { return fAverageCost; }
            set { SetPropertyValue<decimal>("AverageCost", ref fAverageCost, value); }
        }

        decimal fDiscountPercentage;
        public decimal DiscountPercentage
        {
            get { return fDiscountPercentage; }
            set { SetPropertyValue<decimal>("DiscountPercentage", ref fDiscountPercentage, value); }
        }

        decimal fDiscountValue;
        public decimal DiscountValue
        {
            get { return fDiscountValue; }
            set { SetPropertyValue<decimal>("DiscountValue", ref fDiscountValue, value); }
        }

        decimal fTotalSalesPrice;
        public decimal TotalSalesPrice
        {
            get { return fTotalSalesPrice; }
            set { SetPropertyValue<decimal>("TotalSalesPrice", ref fTotalSalesPrice, value); }
        }

        decimal fGrandTotalSalesPrice;
        public decimal GrandTotalSalesPrice
        {
            get { return fGrandTotalSalesPrice; }
            set { SetPropertyValue<decimal>("GrandTotalSalesPrice", ref fGrandTotalSalesPrice, value); }
        }

        [Association(@"InventoryPharmaceuticalDispensingDetailBatchSerialReportXpoReferencesInventoryPharmaceuticalDispensingDetailReportXpo", typeof(InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo> InventoryPharmaceuticalDispensingDetailBatchSerialReportXpoo { get { return GetCollection<InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo>("InventoryPharmaceuticalDispensingDetailBatchSerialReportXpoo"); } }

        public InventoryPharmaceuticalDispensingDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
