//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.InventoryRepository
//' Author           : Hector Rodriguez Rubiano
//' Created          : 08-01-2020
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.InventorySupplie")]
    public class InventorySupplieXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        [Size(20)]
        [Persistent("Code")]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fSupplieName;
        [Size(100)]
        [Persistent("SupplieName")]
        public string SupplieName
        {
            get { return fSupplieName; }
            set { SetPropertyValue<string>("SupplieName", ref fSupplieName, value); }
        }

        bool fPBSProduct;
        [Persistent("PBSProduct")]
        public bool PBSProduct
        {
            get { return fPBSProduct; }
            set { SetPropertyValue<bool>("PBSProduct", ref fPBSProduct, value); }
        }

        bool fSupplieStatus;
        [Persistent("SupplieStatus")]
        public bool SupplieStatus
        {
            get { return fSupplieStatus; }
            set { SetPropertyValue<bool>("SupplieStatus", ref fSupplieStatus, value); }
        }

        bool fJustificationOfInputs;
        [Persistent("JustificationOfInputs")]
        public bool JustificationOfInputs
        {
            get { return fJustificationOfInputs; }
            set { SetPropertyValue<bool>("JustificationOfInputs", ref fJustificationOfInputs, value); }
        }

        bool fOsteosynthesisMaterial;
        [Persistent("OsteosynthesisMaterial")]
        public bool OsteosynthesisMaterial
        {
            get { return fOsteosynthesisMaterial; }
            set { SetPropertyValue<bool>("OsteosynthesisMaterial", ref fOsteosynthesisMaterial, value); }
        }

        bool fConsumption;
        [Persistent("Consumption")]
        public bool Consumption
        {
            get { return fConsumption; }
            set { SetPropertyValue<bool>("Consumption", ref fConsumption, value); }
        }

        [Size(120)]
        [PersistentAlias("concat(concat(Code,' - '),SupplieName)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }
               
        InventoryRiskLevelXpo fRiskLevelId;
        [Association("InventoryRiskLevelReferencesInventorySupplie")]
        public InventoryRiskLevelXpo RiskLevelId
        {
            get { return fRiskLevelId; }
            set { SetPropertyValue<InventoryRiskLevelXpo>("RiskLevelId", ref fRiskLevelId, value); }
        }

        bool fIsParenteralNutritionSupply;
        [Persistent("IsParenteralNutritionSupply")]
        public bool IsParenteralNutritionSupply
        {
            get { return fIsParenteralNutritionSupply; }
            set { SetPropertyValue<bool>("IsParenteralNutritionSupply", ref fIsParenteralNutritionSupply, value); }
        }

        #endregion

        #region Navigations

        [Association(@"Inventory_InventoryProductReferencesInventory_Supplie", typeof(InventoryProductXpo))]
        public XPCollection<InventoryProductXpo> Inventory_InventoryProducts { get { return GetCollection<InventoryProductXpo>("Inventory_InventoryProducts"); } }

        [Association("PackageDetail_References_Supply", typeof(PackageDetailXpo))]
        public XPCollection<PackageDetailXpo> PackageDetails { get { return GetCollection<PackageDetailXpo>("PackageDetails"); } }

        [Association("InventoryRequestOtherDetailReportXpo_References_Supplied", typeof(InventoryRequestOtherDetailReportXpo))]
        public XPCollection<InventoryRequestOtherDetailReportXpo> InventoryRequestOtherDetailReportXpo { get { return GetCollection<InventoryRequestOtherDetailReportXpo>("InventoryRequestOtherDetailReportXpo"); } }
        #endregion

        #region Builders

        public InventorySupplieXpo(Session session) : base(session)
        {
        }

        public InventorySupplieXpo()
            : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
