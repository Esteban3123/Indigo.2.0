using Application.Events.Repository;
using Application.Events.Serializers;
using Domain.Entities;
using System;
using System.Data;
using System.Data.SqlTypes;

namespace Application.Events.Models.Product
{
    class MethodsProduct
    {

        public ProductType GetProductType(int ProductTypeId)
        {
            ProductType productType = new ProductType();
            ExecuteCommand execute = new ExecuteCommand();
            if (ProductTypeId == 0) { return productType; }
            string query = "SELECT TOP 1 Code,Name,Class FROM Inventory.ProductType WHERE Id = @ProductTypeId";
            DataTable dtProductType = execute.GeneralExecuteQuerySqlCommand(query, "ProductTypeId", ProductTypeId);
            if (dtProductType == null || dtProductType.Rows.Count == 0) { return productType; }
            productType.Code = Convert.ToString(dtProductType.Rows[0]["Code"]);
            productType.Name = Convert.ToString(dtProductType.Rows[0]["Name"]);
            productType.Class = Convert.ToString(dtProductType.Rows[0]["Class"]);
            return productType;
        }

        public ATC GetATC(int? ATCId)
        {
            ATC ATCEntity = new ATC();
            ExecuteCommand execute = new ExecuteCommand();
            if (ATCId == null ||ATCId == 0) { return ATCEntity; }
            string query = "SELECT TOP 1 Code,Name,PharmacologicalGroupId," +
                "Presentations,Concentration,ConcentrationQuantity,ConcentrationMeasureUnitId,StabilityMinimumHours,StabilityMaximumHours," +
                "FormulationType,Weight,WeightMeasureUnit,Volume,VolumeMeasureUnit,AdministrationUnitId,Warning,Dosage,DiluentProduct,Osmolarity," +
                "Density,Antibiotic, PharmaceuticalFormId, RequireMedicalBoard" +
                " FROM Inventory.ATC WHERE Id = @ATCId";
            DataTable dtATC = execute.GeneralExecuteQuerySqlCommand(query, "ATCId", ATCId);
            if (dtATC == null || dtATC.Rows.Count == 0) { return ATCEntity; }
            ATCEntity.Code = Convert.ToString(dtATC.Rows[0]["Code"]);
            ATCEntity.Name = Convert.ToString(dtATC.Rows[0]["Name"]);
            ATCEntity.Presentations = Convert.ToString(dtATC.Rows[0]["Presentations"]);
            if (dtATC.Rows[0]["PharmaceuticalFormId"] != DBNull.Value) { ATCEntity.PharmaceuticalForm = GeneratePharmaceuticalForm(Convert.ToInt32(dtATC.Rows[0]["PharmaceuticalFormId"])); }
            ATCEntity.Concentration = Convert.ToString(dtATC.Rows[0]["Concentration"]);
            ATCEntity.ConcentrationQuantity = Convert.ToDecimal(dtATC.Rows[0]["ConcentrationQuantity"]);
            if (dtATC.Rows[0]["ConcentrationMeasureUnitId"] != DBNull.Value) { ATCEntity.ConcentrationMeasurementUnit = GenerateConcentrationMeasurementUnit(Convert.ToInt32(dtATC.Rows[0]["ConcentrationMeasureUnitId"])); }
            ATCEntity.StabilityMinimumHours = Convert.ToInt32(dtATC.Rows[0]["StabilityMinimumHours"]);
            ATCEntity.StabilityMaximumHours = Convert.ToDecimal(dtATC.Rows[0]["StabilityMaximumHours"]);
            ATCEntity.FormulationType = Convert.ToUInt16(dtATC.Rows[0]["FormulationType"]);
            if (dtATC.Rows[0]["Weight"] != DBNull.Value) { ATCEntity.Weight = Convert.ToDecimal(dtATC.Rows[0]["Weight"]); }
            if (dtATC.Rows[0]["Volume"] != DBNull.Value) { ATCEntity.Volume = Convert.ToDecimal(dtATC.Rows[0]["Volume"]); }
            if (dtATC.Rows[0]["WeightMeasureUnit"] != DBNull.Value) { ATCEntity.WeightMeasureUnit = GenerateWeightMeasureUnit(Convert.ToInt32(dtATC.Rows[0]["WeightMeasureUnit"])); }
            if (dtATC.Rows[0]["VolumeMeasureUnit"] != DBNull.Value) { ATCEntity.VolumeMeasureUnit = GenerateWeightMeasureUnit(Convert.ToInt32(dtATC.Rows[0]["VolumeMeasureUnit"])); }
            if (dtATC.Rows[0]["AdministrationUnitId"] != DBNull.Value) { ATCEntity.AdministrationUnit = GenerateWeightMeasureUnit(Convert.ToInt32(dtATC.Rows[0]["AdministrationUnitId"])); }
            ATCEntity.Warning = Convert.ToString(dtATC.Rows[0]["Warning"]);
            ATCEntity.Dosage = Convert.ToString(dtATC.Rows[0]["Dosage"]);
            ATCEntity.DiluentProduct = Convert.ToInt32(dtATC.Rows[0]["DiluentProduct"]);
            if (dtATC.Rows[0]["Osmolarity"] != DBNull.Value) { ATCEntity.Osmolarity = Convert.ToDecimal(dtATC.Rows[0]["Osmolarity"]); }
            if (dtATC.Rows[0]["Density"] != DBNull.Value) { ATCEntity.Density = Convert.ToDecimal(dtATC.Rows[0]["Density"]); }
            if (dtATC.Rows[0]["Antibiotic"] != DBNull.Value) { ATCEntity.Antibiotic = Convert.ToInt32(dtATC.Rows[0]["Antibiotic"]); }
            ATCEntity.PharmacologicalGroup = GeneratePharmacologicalGroup(Convert.ToInt32(dtATC.Rows[0]["PharmacologicalGroupId"]));
            ATCEntity.ATCAdministrationRoute = GenerateATCAdministrationRoute(ATCId);
            if (dtATC.Rows[0]["RequireMedicalBoard"] != DBNull.Value) { ATCEntity.RequireMedicalBoard = Convert.ToBoolean(dtATC.Rows[0]["RequireMedicalBoard"]); }
            return ATCEntity;
        }

        /// <summary>
        /// Obtiene la unidad de medida de la concetración.
        /// </summary>
        /// <param name="ConcentrationMeasureUnitId"></param>
        /// <returns></returns>
        public WeightMeasureUnit GenerateConcentrationMeasurementUnit(int? ConcentrationMeasureUnitId)
        {
            WeightMeasureUnit concentrationMeasurementUnit = new WeightMeasureUnit();
            ExecuteCommand execute = new ExecuteCommand();
            if (ConcentrationMeasureUnitId == null) { ConcentrationMeasureUnitId = 0; }
            String query = String.Format("SELECT TOP 1 IMU.Code,IMU.Name FROM Inventory.InventoryMeasurementUnit IMU WHERE IMU.Id = @ConcentrationMeasureUnitId");
            DataTable dtWeightMeasureUnit = execute.GeneralExecuteQuerySqlCommand(query, "ConcentrationMeasureUnitId", ConcentrationMeasureUnitId);
            if (dtWeightMeasureUnit == null || dtWeightMeasureUnit.Rows.Count == 0) { return concentrationMeasurementUnit; }
            concentrationMeasurementUnit.Code = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Code"]);
            concentrationMeasurementUnit.Name = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Name"]);
            return concentrationMeasurementUnit;
        }

        /// <summary>
        /// Obtiene la forma Farmaceutica.
        /// </summary>
        /// <param name="PharmaceuticalFormId"></param>
        /// <returns></returns>
        public PharmaceuticalForm GeneratePharmaceuticalForm(int? PharmaceuticalFormId)
        {
            PharmaceuticalForm pharmaceuticalForm = new PharmaceuticalForm();
            ExecuteCommand execute = new ExecuteCommand();
            if (PharmaceuticalFormId == null) { PharmaceuticalFormId = 0; }
            String query = String.Format("SELECT TOP 1 Code,Name FROM Inventory.PharmaceuticalForm WHERE Id = @PharmaceuticalFormId");
            DataTable dtWeightMeasureUnit = execute.GeneralExecuteQuerySqlCommand(query, "PharmaceuticalFormId", PharmaceuticalFormId);
            if (dtWeightMeasureUnit == null || dtWeightMeasureUnit.Rows.Count == 0) { return pharmaceuticalForm; }
            pharmaceuticalForm.Code = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Code"]);
            pharmaceuticalForm.Name = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Name"]);
            return pharmaceuticalForm;
        }

        /// <summary>
        /// Unidad de medida del peso, solo se llena si el tipo de formulacion es peso o peso - Volumen.
        /// </summary>
        public WeightMeasureUnit GenerateWeightMeasureUnit(int? WeightMeasureUnitId)
        {
            WeightMeasureUnit weightMeasureUnit = new WeightMeasureUnit();
            ExecuteCommand execute = new ExecuteCommand();
            if (WeightMeasureUnitId == null) { WeightMeasureUnitId = 0; }
            String query = String.Format("SELECT TOP 1 IMU.Code,IMU.Name FROM Inventory.InventoryMeasurementUnit IMU WHERE IMU.Id = @WeightMeasureUnitId");
            DataTable dtWeightMeasureUnit = execute.GeneralExecuteQuerySqlCommand(query, "WeightMeasureUnitId", WeightMeasureUnitId);
            if (dtWeightMeasureUnit == null || dtWeightMeasureUnit.Rows.Count == 0) { return weightMeasureUnit; }
            weightMeasureUnit.Code = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Code"]);
            weightMeasureUnit.Name = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Name"]);
            return weightMeasureUnit;
        }

        /// <summary>
        /// Grupo farmacologico
        /// </summary>
        /// <param name="PharmacologicalGroupId"></param>
        /// <returns></returns>
        public PharmacologicalGroup GeneratePharmacologicalGroup(int? PharmacologicalGroupId)
        {
            PharmacologicalGroup PharmacologicalGroup = new PharmacologicalGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (PharmacologicalGroupId == null) { PharmacologicalGroupId = 0; }
            String query = String.Format("SELECT TOP 1 PF.Code,PF.Name FROM Inventory.PharmacologicalGroup PF WHERE PF.Id = @PharmacologicalGroupId");
            DataTable dtPharmacologicalGroup = execute.GeneralExecuteQuerySqlCommand(query, "PharmacologicalGroupId", PharmacologicalGroupId);
            if (dtPharmacologicalGroup == null || dtPharmacologicalGroup.Rows.Count == 0) { return PharmacologicalGroup; }
            PharmacologicalGroup.Code = Convert.ToString(dtPharmacologicalGroup.Rows[0]["Code"]);
            PharmacologicalGroup.Name = Convert.ToString(dtPharmacologicalGroup.Rows[0]["Name"]);
            return PharmacologicalGroup;
        }

        public AdministrationRoute[] GenerateATCAdministrationRoute(int? ATCId)
        {
            ExecuteCommand execute = new ExecuteCommand();

            String queryATCAdministrationRoute = String.Format("SELECT AdministrationRouteId FROM Inventory.ATCAdministrationRoute WHERE ATCId = @ATCId");
            DataTable dtATCAdministrationRoute = execute.GeneralExecuteQuerySqlCommand(queryATCAdministrationRoute, "ATCId", ATCId);

            AdministrationRoute[] ArraymATCAdministrationRoute = new AdministrationRoute[dtATCAdministrationRoute.Rows.Count];
            int loop = 0;
            foreach (var item in dtATCAdministrationRoute.AsEnumerable())
            {
                String queryAdministrationRoute = String.Format("SELECT TOP 1 ar.Code, AR.Name FROM Inventory.AdministrationRoute AR WHERE ar.Id = @AdministrationRouteId");
                DataTable administrationRoute = execute.GeneralExecuteQuerySqlCommand(queryAdministrationRoute, "AdministrationRouteId", Convert.ToInt32(item["AdministrationRouteId"]));
                if (administrationRoute == null || administrationRoute.Rows.Count == 0) { return ArraymATCAdministrationRoute; }
                AdministrationRoute mATCAdministrationRoute = new AdministrationRoute();
                mATCAdministrationRoute.Code = Convert.ToString(administrationRoute.Rows[0]["Code"]);
                mATCAdministrationRoute.Name = Convert.ToString(administrationRoute.Rows[0]["Name"]);
                ArraymATCAdministrationRoute[loop] = mATCAdministrationRoute;
                loop++;
            }
            return ArraymATCAdministrationRoute;
        }

        public ProductGroup GetProductGroup(int ProductGroupId)
        {
            ProductGroup ProductGroupEntity = new ProductGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (ProductGroupId == 0) { return ProductGroupEntity; }
            string query = "SELECT TOP 1 Code,Name FROM Inventory.ProductGroup WHERE Id = @ProductGroupId";
            DataTable dtProductGroup = execute.GeneralExecuteQuerySqlCommand(query, "ProductGroupId", ProductGroupId);
            if (dtProductGroup == null || dtProductGroup.Rows.Count == 0) { return ProductGroupEntity; }
            ProductGroupEntity.Code = Convert.ToString(dtProductGroup.Rows[0]["Code"]);
            ProductGroupEntity.Name = Convert.ToString(dtProductGroup.Rows[0]["Name"]);
            return ProductGroupEntity;
        }

        public ProductSubGroup GetProductSubGroup(int ProductSubGroupId)
        {
            ProductSubGroup ProductSubGroupEntity = new ProductSubGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (ProductSubGroupId == 0) { return ProductSubGroupEntity; }
            string query = "SELECT TOP 1 Code,Name FROM Inventory.ProductSubGroup WHERE Id = @ProductSubGroupId";
            DataTable dtProductSubGroup = execute.GeneralExecuteQuerySqlCommand(query, "ProductSubGroupId", ProductSubGroupId);
            if (dtProductSubGroup == null || dtProductSubGroup.Rows.Count == 0) { return ProductSubGroupEntity; }
            ProductSubGroupEntity.Code = Convert.ToString(dtProductSubGroup.Rows[0]["Code"]);
            ProductSubGroupEntity.Name = Convert.ToString(dtProductSubGroup.Rows[0]["Name"]);
            return ProductSubGroupEntity;
        }

        public MeasurementUnit GetMeasurementUnit(int? MeasurementUnitId)
        {
            MeasurementUnit MeasurementUnitEntity = new MeasurementUnit();
            ExecuteCommand execute = new ExecuteCommand();
            if (MeasurementUnitId == null || MeasurementUnitId == 0) { return MeasurementUnitEntity; }
            string query = "SELECT TOP 1 Code,Name FROM Inventory.InventoryMeasurementUnit WHERE Id = @MeasurementUnitId";
            DataTable dtMeasurementUnit = execute.GeneralExecuteQuerySqlCommand(query, "MeasurementUnitId", MeasurementUnitId);
            if (dtMeasurementUnit == null || dtMeasurementUnit.Rows.Count == 0) { return MeasurementUnitEntity; }
            MeasurementUnitEntity.Code = Convert.ToString(dtMeasurementUnit.Rows[0]["Code"]);
            MeasurementUnitEntity.Name = Convert.ToString(dtMeasurementUnit.Rows[0]["Name"]);
            return MeasurementUnitEntity;
        }

        public PackagingUnit GetPackagingUnit(int PackagingUnitId)
        {
            PackagingUnit PackagingUnitEntity = new PackagingUnit();
            ExecuteCommand execute = new ExecuteCommand();
            if (PackagingUnitId == 0) { return PackagingUnitEntity; }
            string query = "SELECT TOP 1 Code,Name FROM Inventory.PackagingUnit WHERE Id = @PackagingUnitId";
            DataTable dtPackagingUnit = execute.GeneralExecuteQuerySqlCommand(query, "PackagingUnitId", PackagingUnitId);
            if (dtPackagingUnit == null || dtPackagingUnit.Rows.Count == 0) { return PackagingUnitEntity; }
            PackagingUnitEntity.Code = Convert.ToString(dtPackagingUnit.Rows[0]["Code"]);
            PackagingUnitEntity.Name = Convert.ToString(dtPackagingUnit.Rows[0]["Name"]);
            return PackagingUnitEntity;
        }

        public Manufacturer GetManufacturer(int? ManufacturerId)
        {
            Manufacturer ManufacturerEntity = new Manufacturer();
            ExecuteCommand execute = new ExecuteCommand();
            if (ManufacturerId== null || ManufacturerId == 0) { return ManufacturerEntity; }
            string query = "SELECT TOP 1 Code,Name FROM Inventory.Manufacturer WHERE Id = @ManufacturerId";
            DataTable dtManufacturer = execute.GeneralExecuteQuerySqlCommand(query, "ManufacturerId", ManufacturerId);
            if (dtManufacturer == null || dtManufacturer.Rows.Count == 0) { return ManufacturerEntity; }
            ManufacturerEntity.Code = Convert.ToString(dtManufacturer.Rows[0]["Code"]);
            ManufacturerEntity.Name = Convert.ToString(dtManufacturer.Rows[0]["Name"]);
            return ManufacturerEntity;
        }

        public IVA GetIVA(int? IVAId)
        {
            IVA IVAEntity = new IVA();
            ExecuteCommand execute = new ExecuteCommand();
            if (IVAId == null || IVAId == 0) { return IVAEntity; }
            string query = "SELECT TOP 1 Code,Name FROM GeneralLedger.GeneralLedgerIVA WHERE Id = @IVAId";
            DataTable dtIva = execute.GeneralExecuteQuerySqlCommand(query, "IVAId", IVAId);
            if (dtIva == null || dtIva.Rows.Count == 0) { return IVAEntity; }
            IVAEntity.Code = Convert.ToString(dtIva.Rows[0]["Code"]);
            IVAEntity.Name = Convert.ToString(dtIva.Rows[0]["Name"]);
            return IVAEntity;
        }

        public BillingGroup GetBillingGroup(int? BillingGroupId)
        {
            BillingGroup BillingGroup = new BillingGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (BillingGroupId==null || BillingGroupId == 0) { return BillingGroup; }
            string query = "SELECT TOP 1 Code,Name FROM Billing.BillingGroup WHERE Id = @BillingGroupId";
            DataTable dtBillingGroup = execute.GeneralExecuteQuerySqlCommand(query, "BillingGroupId", BillingGroupId);
            if (dtBillingGroup == null || dtBillingGroup.Rows.Count == 0) { return BillingGroup; }
            BillingGroup.Code = Convert.ToString(dtBillingGroup.Rows[0]["Code"]);
            BillingGroup.Name = Convert.ToString(dtBillingGroup.Rows[0]["Name"]);
            return BillingGroup;
        }

        public PBillingGroupNoPos GetPBillingGroupNoPos(int BillingGroupNoPosId)
        {
            PBillingGroupNoPos BillingGroupNoPos = new PBillingGroupNoPos();
            ExecuteCommand execute = new ExecuteCommand();
            if (BillingGroupNoPosId == 0) { return BillingGroupNoPos; }
            string query = "SELECT TOP 1 Code,Name FROM Billing.BillingGroup WHERE Id = @BillingGroupNoPosId";
            DataTable dtBillingGroupNoPos = execute.GeneralExecuteQuerySqlCommand(query, "BillingGroupNoPosId", BillingGroupNoPosId);
            if (dtBillingGroupNoPos == null || dtBillingGroupNoPos.Rows.Count == 0) { return BillingGroupNoPos; }
            BillingGroupNoPos.Code = Convert.ToString(dtBillingGroupNoPos.Rows[0]["Code"]);
            BillingGroupNoPos.Name = Convert.ToString(dtBillingGroupNoPos.Rows[0]["Name"]);
            return BillingGroupNoPos;
        }

        public PInventoryRiskLevel GetInventoryRiskLevel(int InventoryRiskLevelId)
        {
            PInventoryRiskLevel InventoryRiskLevelEntity = new PInventoryRiskLevel();
            ExecuteCommand execute = new ExecuteCommand();
            if (InventoryRiskLevelId == 0) { return InventoryRiskLevelEntity; }
            string query = "SELECT TOP 1 Code,Name FROM Inventory.InventoryRiskLevel WHERE Id = @InventoryRiskLevelId";
            DataTable dtInventoryRiskLevel = execute.GeneralExecuteQuerySqlCommand(query, "InventoryRiskLevelId", InventoryRiskLevelId);
            if (dtInventoryRiskLevel == null || dtInventoryRiskLevel.Rows.Count == 0) { return InventoryRiskLevelEntity; }
            InventoryRiskLevelEntity.Code = Convert.ToString(dtInventoryRiskLevel.Rows[0]["Code"]);
            InventoryRiskLevelEntity.Name = Convert.ToString(dtInventoryRiskLevel.Rows[0]["Name"]);
            return InventoryRiskLevelEntity;
        }

        public Supplie GetSupplie(int SupplieId)
        {
            Supplie SupplieEntity = new Supplie();
            ExecuteCommand execute = new ExecuteCommand();
            if (SupplieId == 0) { return SupplieEntity; }
            string query = "SELECT TOP 1 Code,SupplieName FROM Inventory.InventorySupplie WHERE Id = @SupplieId";
            DataTable dtSupplie = execute.GeneralExecuteQuerySqlCommand(query, "SupplieId", SupplieId);
            if (dtSupplie == null || dtSupplie.Rows.Count == 0) { return SupplieEntity; }
            SupplieEntity.Code = Convert.ToString(dtSupplie.Rows[0]["Code"]);
            SupplieEntity.Name = Convert.ToString(dtSupplie.Rows[0]["SupplieName"]);
            return SupplieEntity;
        }

        public ProductBarcode[] GetBarcodes(Domain.Entities.InventoryProduct InventoryProductEntity)
        {
            int count = 0;
            if (InventoryProductEntity.ProductBarcode == null || InventoryProductEntity.ProductBarcode.Count == 0) {  return new ProductBarcode[count]; }
            count = InventoryProductEntity.ProductBarcode.Count;
            ProductBarcode[] aProductBarcode = new ProductBarcode[count];            
            int loop = 0;
            foreach (var item in InventoryProductEntity.ProductBarcode)
            {
                ProductBarcode ProductBarcodeEntity = new ProductBarcode();                                                                            
                ProductBarcodeEntity.CreationDate = Convert.ToString(item.CreationDate);
                ProductBarcodeEntity.Barcode = Convert.ToString(item.Barcode);
                ProductBarcodeEntity.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;                
                aProductBarcode[loop] = ProductBarcodeEntity;
                loop++;
            }
            return aProductBarcode;
        }


    }
}
