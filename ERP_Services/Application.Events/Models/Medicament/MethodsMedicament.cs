using Application.Events.Models;
using Application.Events.Repository;
using System;
using System.Data;
using System.Linq;

namespace Application.Events.Serializers
{
    public class MethodsMedicament
    {

        /// <summary>
        /// Forma Farmaceutica
        /// </summary>
        /// <param name="IdPharmaceuticalForm"></param>
        /// <returns></returns>
        public PharmaceuticalForm GeneratePharmaceuticalForm(int? IdPharmaceuticalForm = 0)
        {
            PharmaceuticalForm pharmaceuticalForm = new PharmaceuticalForm();
            ExecuteCommand execute = new ExecuteCommand();
            if (IdPharmaceuticalForm ==null) { IdPharmaceuticalForm = 000; };
            String query = String.Format("SELECT TOP 1 Code,Name,CrystalMedicalForm,Status,RequireStability FROM Inventory.PharmaceuticalForm WHERE Id = @IdPharmaceuticalForm");
            DataTable dtPharmaceuticalForm = execute.GeneralExecuteQuerySqlCommand(query, "IdPharmaceuticalForm", IdPharmaceuticalForm);
            if (dtPharmaceuticalForm == null || dtPharmaceuticalForm.Rows.Count == 0) { return pharmaceuticalForm; }             
            pharmaceuticalForm.Code = Convert.ToString(dtPharmaceuticalForm.Rows[0]["Code"]);
            pharmaceuticalForm.Name = Convert.ToString(dtPharmaceuticalForm.Rows[0]["Name"]);
            pharmaceuticalForm.CrystalMedicalForm = Convert.ToString(dtPharmaceuticalForm.Rows[0]["CrystalMedicalForm"]);
            pharmaceuticalForm.Status = Convert.ToInt16(dtPharmaceuticalForm.Rows[0]["Status"]);
            pharmaceuticalForm.RequireStability = Convert.ToInt16(dtPharmaceuticalForm.Rows[0]["RequireStability"]);            
            return pharmaceuticalForm;
        }

        public string DCICode(int? DCIId)
        {
            string code = "";
            ExecuteCommand execute = new ExecuteCommand();
            if (DCIId == null){  DCIId = 0; }
            String query = String.Format("SELECT TOP 1 Code FROM Inventory.DCI D WHERE d.Id = @DCIId");
            DataTable dtDci = execute.GeneralExecuteQuerySqlCommand(query, "DCIId", DCIId);
            if (dtDci == null || dtDci.Rows.Count == 0) { return code; }
            code = Convert.ToString(dtDci.Rows[0]["Code"]);            
            return code;
        }

        public string ATCEntityCode(int? ATCEntityId)
        {
            string ATCEntityCode = "";
            ExecuteCommand execute = new ExecuteCommand();
            if (ATCEntityId == null) { ATCEntityId = 0; };
            String query = String.Format("SELECT TOP 1 Code FROM Inventory.ATCEntity ae WHERE ae.Id = @ATCEntityId");
            DataTable dtDci = execute.GeneralExecuteQuerySqlCommand (query, "ATCEntityId", ATCEntityId);
            if (dtDci == null || dtDci.Rows.Count == 0) { return ATCEntityCode; }            
            ATCEntityCode = Convert.ToString(dtDci.Rows[0]["Code"]);            
            return ATCEntityCode;
        }

        /// <summary>
        /// Via de administracion
        /// </summary>
        /// <param name="AdministrationRouteId"></param>
        /// <returns></returns>
        public MAdministrationRoute GenerateAdministrationRoute(Domain.Entities.ATC atc)
        {
            MAdministrationRoute AdministrationRoute = new MAdministrationRoute();
            ExecuteCommand execute = new ExecuteCommand();
            if (atc == null) { return AdministrationRoute; };
            String query = String.Format("SELECT TOP 1 PharmaceuticalFormId,Code,Name,Status FROM Inventory.AdministrationRoute AR WHERE AR.Id = @AdministrationRouteId");
            DataTable dtAdministrationRoute = execute.GeneralExecuteQuerySqlCommand(query, "AdministrationRouteId", atc.ATCAdministrationRoute.FirstOrDefault().AdministrationRouteId);
            if (dtAdministrationRoute == null || dtAdministrationRoute.Rows.Count == 0) { return AdministrationRoute; }
            AdministrationRoute.PharmaceuticalForm = GeneratePharmaceuticalForm(Convert.ToInt16(dtAdministrationRoute.Rows[0]["PharmaceuticalFormId"] == DBNull.Value ? 0 : dtAdministrationRoute.Rows[0]["PharmaceuticalFormId"]));
            AdministrationRoute.Code = Convert.ToString(dtAdministrationRoute.Rows[0]["Code"]);
            AdministrationRoute.Name = Convert.ToString(dtAdministrationRoute.Rows[0]["Name"]);
            AdministrationRoute.Status = Convert.ToInt16(dtAdministrationRoute.Rows[0]["Status"]);
            return AdministrationRoute;
        }

        public MATCAdministrationRoute[] GenerateATCAdministrationRoute(Domain.Entities.ATC atc)
        {
            MATCAdministrationRoute[] ArraymATCAdministrationRoute = new MATCAdministrationRoute[atc.ATCAdministrationRoute.Count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0 ;
            foreach (var item in atc.ATCAdministrationRoute)
            {
               String query = String.Format("SELECT TOP 1 AR.Id AS AdministrationRouteId, ar.Code, AR.Name, ar.Status, ar.PharmaceuticalFormId FROM Inventory.AdministrationRoute AR WHERE ar.Id = @AdministrationRouteId");
                DataTable dtATCAdministrationRoute = execute.GeneralExecuteQuerySqlCommand(query, "AdministrationRouteId", item.AdministrationRouteId);
                if (dtATCAdministrationRoute == null || dtATCAdministrationRoute.Rows.Count == 0) { return ArraymATCAdministrationRoute; }                             
                MATCAdministrationRoute mATCAdministrationRoute = new MATCAdministrationRoute();
                mATCAdministrationRoute.PharmaceuticalForm = new PharmaceuticalForm();
                mATCAdministrationRoute.PharmaceuticalForm = GeneratePharmaceuticalForm(Convert.ToInt16(dtATCAdministrationRoute.Rows[0]["PharmaceuticalFormId"] == DBNull.Value ? 0 : dtATCAdministrationRoute.Rows[0]["PharmaceuticalFormId"]));
                mATCAdministrationRoute.Code = Convert.ToString(dtATCAdministrationRoute.Rows[0]["Code"]);
                mATCAdministrationRoute.Name = Convert.ToString(dtATCAdministrationRoute.Rows[0]["Name"]);
                mATCAdministrationRoute.Status = Convert.ToInt16(dtATCAdministrationRoute.Rows[0]["Status"]);            
                mATCAdministrationRoute.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;                    
                ArraymATCAdministrationRoute[loop] = mATCAdministrationRoute;
                loop++;                            
            }
            return ArraymATCAdministrationRoute;
        }

        /// <summary>
        /// Grupo farmacologico
        /// </summary>
        /// <param name="AdministrationRouteId"></param>
        /// <returns></returns>
        public PharmacologicalGroup GeneratePharmacologicalGroup(int? PharmacologicalGroupId )
        {
            PharmacologicalGroup PharmacologicalGroup = new PharmacologicalGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (PharmacologicalGroupId == null){PharmacologicalGroupId = 0;}
            String query = String.Format("SELECT TOP 1 PF.Code,PF.Name,Status,PF.CrystalPharmacologicalGroup FROM Inventory.PharmacologicalGroup PF WHERE PF.Id = @PharmacologicalGroupId");
            DataTable dtPharmacologicalGroup = execute.GeneralExecuteQuerySqlCommand(query, "PharmacologicalGroupId", PharmacologicalGroupId);
            if (dtPharmacologicalGroup == null || dtPharmacologicalGroup.Rows.Count == 0) { return PharmacologicalGroup; }
            PharmacologicalGroup.Code = Convert.ToString(dtPharmacologicalGroup.Rows[0]["Code"]);
            PharmacologicalGroup.Name = Convert.ToString(dtPharmacologicalGroup.Rows[0]["Name"]);
            PharmacologicalGroup.Status = Convert.ToInt16(dtPharmacologicalGroup.Rows[0]["Status"]);
            PharmacologicalGroup.CrystalPharmacologicalGroup = Convert.ToString(dtPharmacologicalGroup.Rows[0]["CrystalPharmacologicalGroup"]);
            return PharmacologicalGroup;
        }

        /// <summary>
        /// Codigo Unidades UPR
        /// </summary>
        /// <param name="UPRUnitsId"></param>
        /// <returns></returns>
       
        public string UPRUnitsCode(int? UPRUnitsId)
        {
            string code = "";
            ExecuteCommand execute = new ExecuteCommand();
            if (UPRUnitsId == null) { UPRUnitsId = 0; }
            String query = String.Format("SELECT TOP 1 Code FROM Inventory.UPRUnits ui WHERE ui.Id = @UPRUnitsId");
            DataTable dtUPRUnits = execute.GeneralExecuteQuerySqlCommand(query, "UPRUnitsId", UPRUnitsId);
            if (dtUPRUnits == null || dtUPRUnits.Rows.Count == 0) { return code; }
            code = Convert.ToString(dtUPRUnits.Rows[0]["Code"]);
            return code;
        }

        /// <summary>
        /// Código CMU
        /// </summary>
        /// <param name="cmuId"></param>
        /// <returns></returns>
        public string ConcentrationMeasureUnitCode(int? cmuId)
        {
            string code = "";
            ExecuteCommand execute = new ExecuteCommand();
            if (cmuId == null) { cmuId = 0; }
            String query = String.Format("SELECT TOP 1 Code FROM Inventory.InventoryMeasurementUnit imu WHERE imu.Id = @cmuId");
            DataTable dtcmu = execute.GeneralExecuteQuerySqlCommand(query, "cmuId", cmuId);
            if (dtcmu == null || dtcmu.Rows.Count == 0) { return code; }
            code = Convert.ToString(dtcmu.Rows[0]["Code"]);
            return code;
        }

        /// <summary>
        /// Nivel de riesgo
        /// </summary>
        /// <param name="InventoryRiskLevel"></param>
        /// <returns></returns>
        public InventoryRiskLevel GetInventoryRiskLevel(int? InventoryRiskLevel)
        {
            InventoryRiskLevel inventoryRiskLevel = new InventoryRiskLevel();
            ExecuteCommand execute = new ExecuteCommand();
            if (InventoryRiskLevel == null) { InventoryRiskLevel = 0; }
            String query = String.Format("SELECT TOP 1 ir.Code,Name,Status FROM Inventory.InventoryRiskLevel ir WHERE ir.Id = @InventoryRiskLevel");
            DataTable dtInventoryRiskLevel = execute.GeneralExecuteQuerySqlCommand(query, "InventoryRiskLevel", InventoryRiskLevel);
            if (dtInventoryRiskLevel == null || dtInventoryRiskLevel.Rows.Count == 0) { return inventoryRiskLevel; }      
            inventoryRiskLevel.Code = Convert.ToString(dtInventoryRiskLevel.Rows[0]["Code"]);
            inventoryRiskLevel.Name = Convert.ToString(dtInventoryRiskLevel.Rows[0]["Name"]);
            inventoryRiskLevel.Status = Convert.ToInt16(dtInventoryRiskLevel.Rows[0]["Status"]);      
            return inventoryRiskLevel;
        }

        /// <summary>
        /// Unidad de medida del peso, solo se llena si el tipo de formulacion es peso o peso - Volumen.
        /// </summary>
        public WeightMeasureUnit GenerateWeightMeasureUnit(int? WeightMeasureUnitId )
        {
            WeightMeasureUnit weightMeasureUnit = new WeightMeasureUnit();
            ExecuteCommand execute = new ExecuteCommand();
            if (WeightMeasureUnitId == null){WeightMeasureUnitId = 0;}
            String query = String.Format("SELECT TOP 1 IMU.Code,IMU.Name,IMU.Abbreviation,IMU.UnitType,	IMU.CrystalMeasurementUnit,IMU.AllowEditCostValue,IMU.CostValue,imu.Status FROM Inventory.InventoryMeasurementUnit IMU WHERE IMU.Id = @WeightMeasureUnitId");            
            DataTable dtWeightMeasureUnit = execute.GeneralExecuteQuerySqlCommand(query, "WeightMeasureUnitId", WeightMeasureUnitId);
            if (dtWeightMeasureUnit == null || dtWeightMeasureUnit.Rows.Count == 0) { return weightMeasureUnit; }
            weightMeasureUnit.Code = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Code"]);
            weightMeasureUnit.Name = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Name"]);
            weightMeasureUnit.Abbreviation = Convert.ToString(dtWeightMeasureUnit.Rows[0]["Abbreviation"]);
            weightMeasureUnit.FormulationType = Convert.ToInt16(dtWeightMeasureUnit.Rows[0]["UnitType"]);
            weightMeasureUnit.Status = Convert.ToInt16(dtWeightMeasureUnit.Rows[0]["Status"]);
            weightMeasureUnit.CrystalMeasurementUnit = Convert.ToString(dtWeightMeasureUnit.Rows[0]["CrystalMeasurementUnit"]);
            weightMeasureUnit.AllowEditCostValue = Convert.ToInt16(dtWeightMeasureUnit.Rows[0]["AllowEditCostValue"]);
            weightMeasureUnit.CostValue = Convert.ToDecimal(dtWeightMeasureUnit.Rows[0]["CostValue"]);        
            return weightMeasureUnit;
        }

        /// <summary>
        /// Grupo de facturación no pos.
        /// </summary>
        public BillingGroupNoPos GenerateBillingGroupNoPos(int? BillingGroupNoPosId)
        {
            BillingGroupNoPos billingGroupNoPos = new BillingGroupNoPos();
            ExecuteCommand execute = new ExecuteCommand();
            if(BillingGroupNoPosId == null) { BillingGroupNoPosId = 0; }
            String query = String.Format("SELECT TOP 1 bg.Code,bg.Name,bg.Status FROM Billing.BillingGroup bg WHERE bg.Id = @BillingGroupNoPosId");
            DataTable dtbillingGroupNoPos = execute.GeneralExecuteQuerySqlCommand(query, "BillingGroupNoPosId", BillingGroupNoPosId);
            if (dtbillingGroupNoPos == null || dtbillingGroupNoPos.Rows.Count == 0) { return billingGroupNoPos; }
            billingGroupNoPos.Code = Convert.ToString(dtbillingGroupNoPos.Rows[0]["Code"]);
            billingGroupNoPos.Name = Convert.ToString(dtbillingGroupNoPos.Rows[0]["Name"]);
            billingGroupNoPos.Status = Convert.ToInt16(dtbillingGroupNoPos.Rows[0]["Status"]);
            return billingGroupNoPos;
        }

        /// <summary>
        /// Lista de insumos y medicamentos asociados
        /// </summary>
        /// <param name="ATCId"></param>
        /// <returns></returns>
        public MRelatedSupplieMedicine[] GenerateRelatedSupplieMedicine(Domain.Entities.ATC atc)
        {
            MRelatedSupplieMedicine[] ArraymRelatedSupplieMedicine= new MRelatedSupplieMedicine[atc.RelatedSupplieMedicine.Count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;           
            foreach (var item in atc.RelatedSupplieMedicine)
            {                
                MRelatedSupplieMedicine mRelatedSupplieMedicine = new MRelatedSupplieMedicine();
                mRelatedSupplieMedicine.ATCCode = atc.Code;
                mRelatedSupplieMedicine.ItemType = item.ItemType;
                String Query = "SELECT TOP 1 Code FROM Inventory.InventorySupplie ins WHERE ins.Id = @SourceId"; //Insumo                                                                                                               
                if (item.ItemType == 2)  { Query = "SELECT TOP 1 Code FROM Inventory.ATC atc WHERE atc.Id = @SourceId"; }  //Medicamento
                DataTable dtRelatedSupplieMedicine = execute.GeneralExecuteQuerySqlCommand(Query, "SourceId", item.SourceId);
                if (dtRelatedSupplieMedicine ==null && dtRelatedSupplieMedicine.Rows.Count ==0) { return ArraymRelatedSupplieMedicine; }
                mRelatedSupplieMedicine.SourceCode = Convert.ToString(dtRelatedSupplieMedicine.Rows[0]["Code"]);
                mRelatedSupplieMedicine.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                ArraymRelatedSupplieMedicine[loop] = mRelatedSupplieMedicine;
                loop++;
            }
                       
            return ArraymRelatedSupplieMedicine;
        }


    }
}