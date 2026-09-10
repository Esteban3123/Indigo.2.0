UPDATE p set IdentificationTypeId = atd.ID
		from Common.Person p with(NOLOCK)
		JOIN ADTIPOIDENTIFICA atd WITH(NOLOCK) on (CASE p.IdentificationType
														WHEN 0 THEN 'CC'
														WHEN 1 then 'CE'
														WHEN 2 THEN 'TI'
														WHEN 3 THEN 'RC'
														WHEN 4 THEN 'PA'
														WHEN 5 THEN 'AS'
														WHEN 6 then 'MS'
														WHEN 7 THEN 'NI'
														WHEN 8 THEN 'NU'
														WHEN 9 THEN 'CN'
														WHEN 10 THEN 'CD'
														WHEN 11 THEN 'SC'
														WHEN 12 THEN 'PE'
														WHEN 13 THEN 'PT'
														WHEN 14 THEN 'DE'
														WHEN 15 THEN 'SI'
														ELSE 'OT'
														END
														) = atd.SIGLA

/*==========================================================================================================================
	Author: Cesar Collazos
	BUG : 12221
	Sprint : Sprint_week_36-38
==============================================================================================================================*/
UPDATE Treasury.CashReceiptDetails SET CurrencyId = 1 , TRM = 1, ValueInCurrencyHeader = Value WHERE ValueInCurrencyHeader = 0
UPDATE Treasury.PaymentMethods SET CurrencyId = 1 , TRM = 1, ValueInCurrencyHeader = Value WHERE ValueInCurrencyHeader = 0

/*==========================================================================================================================
	Author: Andrés Steven Rojas
	BUG : 12171
	Sprint : Sprint_week_36-38
==============================================================================================================================*/
update pnd
 set TotalConceptValue=
CASE 
    WHEN pn.EntityName is null THEN 
	CASE 
          WHEN pnd.IdGeneralLedgerIVA is NULL THEN BaseValue
		  WHEN IdGeneralLedgerIVA is NOT NULL THEN BaseValue + IVAValue
        END
    when pn.EntityName is not null  then
        CASE 
           WHEN IdGeneralLedgerIVA is NULL THEN BaseValue
		   WHEN IdGeneralLedgerIVA is NOT NULL THEN IVAValue
        END
 END
from Payments.PaymentNotes pn
join Payments.PaymentsNoteDetails pnd on pnd.IdPaymentsNote = pn.Id
WHERE pnd.TotalConceptValue IS NULL

UPDATE Portfolio.AccountReceivable set DateAccountBecomeZero = [Portfolio].[GetLastMovementAccountReceivable](Id)
WHERE Balance = 0 AND DateAccountBecomeZero IS NULL

UPDATE Portfolio.PortfolioNoteConcept
SET HandleTax = (
	SELECT SalePriceIncludeTax
	FROM GeneralLedger.CompanySettings
	)

-- Se asigna TotalConcept cuando no existía IVA
UPDATE Portfolio.PortfolioNoteDetail SET TotalConcept = [Value]
WHERE IdGeneralLedgerIVA IS NULL AND TotalConcept IS NULL

UPDATE FixedAsset.FixedAssetInitialBalanceItem
SET ValidMinorAmount = IIF(Depreciate = 1, 1, NULL)

UPDATE FixedAsset.FixedAssetItemCatalog
SET HandlesDepreciationbyDistribution = 0
WHERE HandlesDepreciationbyDistribution IS NULL

/*==============================================================================================================================
	Author: Karen Esquivel
	PBI : 10824
	Sprint : ERP_Services\VF_week_16-18 (2024)
==============================================================================================================================*/
IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SurgicalReport'
          AND Object_ID = Object_ID(N'[Contract].[CUPSEntity]'))
BEGIN
	PRINT 'YA EXISTE'
  UPDATE Contract.CUPSEntity
  SET SurgicalReport = 1
  WHERE ServiceType = 5
END
ELSE
BEGIN
	PRINT 'NO EXISTE'
  ALTER TABLE Contract.CUPSEntity
      ADD SurgicalReport BIT NOT NULL CONSTRAINT DF_SurgicalReport DEFAULT (
          CASE WHEN ServiceType = 5 THEN 1 ELSE 0 END
      );
END

/*==============================================================================================================================
	Author: Angi Durán Vargas
	PBI : 16687
	Sprint : ERP_Services\VF_week_18-20 (2024)
==============================================================================================================================*/

UPDATE Payments.AccountPayable SET TaxRegistration = (case DeductibleIva
																when 0 then ISNULL((select top 1 TaxRegistration from GeneralLedger.CompanySettings where TaxRegistration<>2 AND TaxRegistration<>3),4)
																when 1 then 2
															 end)  
										WHERE TaxRegistration is null

/*==============================================================================================================================
	Author: Angi Durán Vargas
	PBI : 16691
	Sprint : ERP_Services\VF_week_18-20 (2024)
==============================================================================================================================*/

UPDATE Cost.CostDistributionDirectCost SET TaxRegistration = (case DeductibleIva
																when 0 then ISNULL((select top 1 TaxRegistration from GeneralLedger.CompanySettings where TaxRegistration<>2 AND TaxRegistration<>3),4)
																when 1 then 2
															 end)  
										WHERE TaxRegistration is null

										UPDATE Portfolio.PortfolioNoteAccountReceivableDetail set BaseValue = Value
/*==============================================================================================================================
	Author: Angi Durán Vargas
	PBI : 16698
	Sprint : ERP_Services\VF_week_18-20 (2024)
==============================================================================================================================*/

alter table Treasury.VoucherTransactionDetails add TaxRegistration tinyint NULL

UPDATE Treasury.VoucherTransactionDetails SET TaxRegistration = (case discountableIVA
																when 0 then ISNULL((select top 1 TaxRegistration from GeneralLedger.CompanySettings where TaxRegistration<>2 AND TaxRegistration<>3),4)
																when 1 then 2
															 end)  
										WHERE TaxRegistration is null

/*==============================================================================================================================*/

/*==============================================================================================================================
	Author: Andrés Steven Rojas
	BUG : 18374
	Sprint : ERP_Services\VF_week_22-24 (2024)
==============================================================================================================================*/

UPDATE 
	Inventory.EntranceVoucherDetail
SET
	NetoValue = SubTotalValue - DiscountValue
WHERE
	NetoValue = 0

/*==========================================================================================================================
	Author: Anthony Ocampo
	PBI : 17769
	Sprint : ERP_Services\SCM_week_22-24 (2024)
==============================================================================================================================*/
UPDATE INVENTORY.DrugInteraction
SET ATCEntityId = IdATCEntity
from Inventory.DCIATCEntity
where DCIId = IdDCI and IdATCEntity is not null

UPDATE INVENTORY.DrugActive
SET ATCEntityId = IdATCEntity
FROM Inventory.DCIATCEntity
WHERE DCIId = IdDCI AND IdATCEntity IS NOT NULL

/*==============================================================================================================================
Author: Cesar Collazos
	PBI : 10184
	Sprint : ERP_Services\SCM_week_24-26 (2024)
==============================================================================================================================*/

UPDATE Inventory.ATC SET TotalSubstanceConcentration = CAST(ATC.concentrationquantity AS VARCHAR(50)) + ' ' + imu.Abbreviation
FROM Inventory.ATC atc
INNER JOIN Inventory.InventoryMeasurementUnit imu ON atc.ConcentrationMeasureUnitId = imu.Id
WHERE atc.Combined = 1

/*==============================================================================================================================
	Author: Andrés Steven Rojas
	BUG : 18543
	Sprint : ERP_Services\VF_week_24-26 (2024)
==============================================================================================================================*/

-- Solo para clientes Colombia (TRM = 1)
UPDATE 
	Portfolio.PortfolioTransferDetail
SET 
	ValueInCurrencyInvoice = Value
WHERE 
	ValueInCurrencyInvoice = 0

UPDATE 
	 Contract.CareGroup
SET 
	LiquidationType = 2
WHERE 
	LiquidationType IN (3,4)	


	/*==============================================================================================================================

	Author: Juan Pablo Daza Medina 
	PBI : 8537
	Sprint : ERP_Services\VF_week_38-40 (2024)
==============================================================================================================================*/

Update Payroll.HumanTalentParameterization set JSONdata = 
('{
  "Tabs": [
    {
      "TabName": "Información de Identificación",
      "Code": "INDlcgPersonalIdInfo"
    },
    {
      "TabName": "Información Personal",
      "Code": "INDlcgPersonalInfo"
    },
    {
      "TabName": "Información General",
      "Code": "INDlcgGeneralInfo"
    },
    {
      "TabName": "Información de Pensionado",
      "Code": "INDlcgPensionaryInfo"
    },
    {
      "TabName": "Información de Deducciones",
      "Code": "INDlcgDeductionInfo"
    },
    {
      "TabName": "Información Discapacidades e Idiomas",
      "Code": "INDlcgDisabilityAndLanguageInfo"
    },
    {
      "TabName": "Información de Estudios y Profesiones",
      "Code": "INDlcgStudyAndProfessionInfo"
    },
    {
      "TabName": "Información del Grupo Familiar",
      "Code": "INDlcgFamilyInfo"
    }
  ],
  "Items": [
    {
      "ItemText": "Número de Identificación",
      "ItemName": "INDlciIdNumber",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.ButtonEdit",
      "Visible": true,
      "Obligatory": true,
      "Mandatory": true,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Típo de Identificación",
      "ItemName": "INDlciIdType",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": true,
      "Mandatory": true,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Fecha de Nacimiento",
      "ItemName": "INDlciBirthDate",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DateEdit",
      "Visible": true,
      "Obligatory": true,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Edad",
      "ItemName": "INDlciAge",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.LabelControl",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Ciudad Expedición Identificación",
      "ItemName": "INDlciIdExpeditionCityId",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Fecha Expedición Identificación",
      "ItemName": "INDlciIdDate",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DateEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Primer Nombre",
      "ItemName": "INDlciFirstName",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
      "Obligatory": true,
      "Mandatory": true,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Segundo Nombre",
      "ItemName": "INDlciSecondName",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Primer Apellido",
      "ItemName": "INDlciFirstLastname",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
      "Obligatory": true,
      "Mandatory": true,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Segundo Apellido",
      "ItemName": "INDlciSecondLastname",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
	{
      "ItemText": "Código Interno",
      "ItemName": "INDlciInternalCode",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Creencias Religiosas",
      "ItemName": "LayoutControlItem7",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Grupos Étnicos",
      "ItemName": "INDlciEthnicGroups",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Firma Digitalizada",
      "ItemName": "LayoutControlItem2",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.PopupContainerEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Ciudad de Nacimiento",
      "ItemName": "INDlciBirthCity",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Género",
      "ItemName": "INDlciGender",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Tipo de Libreta Militar",
      "ItemName": "INDlciMilitaryCardType",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Número de Tarjeta Militar",
      "ItemName": "INDlciMilitaryCardNumber",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Fecha de Fallecimiento",
      "ItemName": "INDlciDeathDate",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DateEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Grupo Sanguíneo",
      "ItemName": "INDlciBloodGroup",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "RH",
      "ItemName": "INDlciRH",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Estado Civil",
      "ItemName": "INDlciMaritalStatus",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": true,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Tipo Vivienda",
      "ItemName": "INDlciHousingType",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Estrato Socioeconómico",
      "ItemName": "INDciSocioEconomicStatus",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "¿Consume Cigarrillo?",
      "ItemName": "INDciCigaretteConsumption",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "¿Practica Deporte?",
      "ItemName": "INDciSportPractice",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Imprimir Certif. Laboral",
      "ItemName": "LayoutControlItem1",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.CheckEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Contacto",
      "ItemName": "INDlciContact",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.PopupContainerEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Uso Tiempo Libre",
      "ItemName": "INDlciFreeTime",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Idiomas",
      "ItemName": "INDlciLanguage",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Nacionalidad",
      "ItemName": "INDlciNationalityPopup",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Reubicado",
      "ItemName": "INDLciRelocation",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Antiguedad en la Empresa",
      "ItemName": "INDlciCompanyTime",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.LabelControl",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Antiguedad en el Cargo",
      "ItemName": "INDlciPositionTime",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.LabelControl",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Unión Sindical",
      "ItemName": "INDlciTradeUnion",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Información de Contacto",
      "ItemName": "INDlciContactInfo",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Cotizante en el Exterior",
      "ItemName": "INDLciContributorAbroad",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "Presentation.Controls.CtrYesNo",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Pensionado",
      "ItemName": "INDlciPensionary",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Pensionado"
    },
    {
      "ItemText": "Ext. Obligado a Cotizar",
      "ItemName": "INDLcForeignobligedQuotePension",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Pensionado"
    },
    {
      "ItemText": "Incapacitado Permanente",
      "ItemName": "INDLcPermanentInability",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Pensionado"
    },
    {
      "ItemText": "Tipo de Retención",
      "ItemName": "INDlciRetentionType",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": true,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    { 
      "ItemText": "Tipo Cotizante", 
      "ItemName": "INDlciContributorType", 
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem", 
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit", 
      "Visible": true, 
      "Obligatory": true, 
      "Mandatory": false, 
      "LcgName": "Información de Deducciones" 
    },
    { 
      "ItemText": "Subtipo de Cotizante", 
      "ItemName": "INDlciContributorSubtype", 
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem", 
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit", 
      "Visible": true, 
      "Obligatory": true, 
      "Mandatory": false, 
      "LcgName": "Información de Deducciones" 
    },
    {
      "ItemText": "Deducción por Vivienda",
      "ItemName": "INDlciHousingDeductionValue",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Deducción por Educación",
      "ItemName": "INDlciEducationDeductionValue",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Promedio Salud Año Ant",
      "ItemName": "INDlciAverageHealthLastYearValue",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Tipo Declarante",
      "ItemName": "INdLciDeclarantType",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Aporte Salud RTF",
      "ItemName": "INDLciHealthContribution",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Pensión Complementaria",
      "ItemName": "INDlciSupplementaryPensionValue",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Discapacidades",
      "ItemName": "INDlciDisability",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Discapacidades e Idiomas"
    },
    {
      "ItemText": "Enfermedad Diagnosticada",
      "ItemName": "LayoutControlItem5",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": false,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Discapacidades e Idiomas"
    },
    {
      "ItemText": "Estudios",
      "ItemName": "INDlciStudy",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Estudios y Profesiones"
    },
    {
      "ItemText": "Profesiones",
      "ItemName": "INDlciProfession",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Estudios y Profesiones"
    },
    {
      "ItemText": "Personas a Cargo",
      "ItemName": "INDlciDependents",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información del Grupo Familiar"
    },
    {
      "ItemText": "Grupo Familiar",
      "ItemName": "INDlciRelationship",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información del Grupo Familiar"
    }
  ]
}')WHERE ID = 1

UPDATE h
SET h.JSONdata = '{
    "Tabs": [
        {
            "TabName": "Información de Identificación",
            "Code": "INDlcgPersonalIdInfo"
        },
        {
            "TabName": "Información Personal",
            "Code": "INDlcgPersonalInfo"
        },
        {
            "TabName": "Información General",
            "Code": "INDlcgGeneralInfo"
        },
        {
            "TabName": "Información de Pensionado",
            "Code": "INDlcgPensionaryInfo"
        },
        {
            "TabName": "Información de Deducciones",
            "Code": "INDlcgDeductionInfo"
        },
        {
            "TabName": "Información Discapacidades e Idiomas",
            "Code": "INDlcgDisabilityAndLanguageInfo"
        },
        {
            "TabName": "Información de Estudios y Profesiones",
            "Code": "INDlcgStudyAndProfessionInfo"
        },
        {
            "TabName": "Información del Grupo Familiar",
            "Code": "INDlcgFamilyInfo"
        }
    ],
    "Items": [
        {
            "ItemText": "Número de Identificación",
            "ItemName": "INDlciIdNumber",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.ButtonEdit",
            "Visible": true,
            "Obligatory": true,
            "Mandatory": true,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Típo de Identificación",
            "ItemName": "INDlciIdType",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": true,
            "Obligatory": true,
            "Mandatory": true,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Fecha de Nacimiento",
            "ItemName": "INDlciBirthDate",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DateEdit",
            "Visible": true,
            "Obligatory": true,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Edad",
            "ItemName": "INDlciAge",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.LabelControl",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Ciudad Expedición Identificación",
            "ItemName": "INDlciIdExpeditionCityId",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Fecha Expedición Identificación",
            "ItemName": "INDlciIdDate",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DateEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Primer Nombre",
            "ItemName": "INDlciFirstName",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": true,
            "Mandatory": true,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Segundo Nombre",
            "ItemName": "INDlciSecondName",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Primer Apellido",
            "ItemName": "INDlciFirstLastname",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": true,
            "Mandatory": true,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Segundo Apellido",
            "ItemName": "INDlciSecondLastname",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Código Interno",
            "ItemName": "INDlciInternalCode",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Creencias Religiosas",
            "ItemName": "LayoutControlItem7",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Grupos Étnicos",
            "ItemName": "INDlciEthnicGroups",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Firma Digitalizada",
            "ItemName": "LayoutControlItem2",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.PopupContainerEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
		{
            "ItemText": "Código Asegurado CCSS",
            "ItemName": "INDlciCodigoCCSS",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Identificación"
        },
        {
            "ItemText": "Ciudad de Nacimiento",
            "ItemName": "INDlciBirthCity",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Género",
            "ItemName": "INDlciGender",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Tipo de Libreta Militar",
            "ItemName": "INDlciMilitaryCardType",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Número de Tarjeta Militar",
            "ItemName": "INDlciMilitaryCardNumber",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Fecha de Fallecimiento",
            "ItemName": "INDlciDeathDate",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DateEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Grupo Sanguíneo",
            "ItemName": "INDlciBloodGroup",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "RH",
            "ItemName": "INDlciRH",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Estado Civil",
            "ItemName": "INDlciMaritalStatus",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": true,
            "Obligatory": true,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Tipo Vivienda",
            "ItemName": "INDlciHousingType",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Estrato Socioeconómico",
            "ItemName": "INDciSocioEconomicStatus",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "¿Consume Cigarrillo?",
            "ItemName": "INDciCigaretteConsumption",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "¿Practica Deporte?",
            "ItemName": "INDciSportPractice",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Imprimir Certif. Laboral",
            "ItemName": "LayoutControlItem1",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.CheckEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Contacto",
            "ItemName": "INDlciContact",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.PopupContainerEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Uso Tiempo Libre",
            "ItemName": "INDlciFreeTime",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Idiomas",
            "ItemName": "INDlciLanguage",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Nacionalidad",
            "ItemName": "INDlciNationalityPopup",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Personal"
        },
        {
            "ItemText": "Reubicado",
            "ItemName": "INDLciRelocation",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información General"
        },
        {
            "ItemText": "Antiguedad en la Empresa",
            "ItemName": "INDlciCompanyTime",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.LabelControl",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información General"
        },
        {
            "ItemText": "Antiguedad en el Cargo",
            "ItemName": "INDlciPositionTime",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.LabelControl",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información General"
        },
        {
            "ItemText": "Unión Sindical",
            "ItemName": "INDlciTradeUnion",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información General"
        },
        {
            "ItemText": "Información de Contacto",
            "ItemName": "INDlciContactInfo",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información General"
        },
        {
            "ItemText": "Cotizante en el Exterior",
            "ItemName": "INDLciContributorAbroad",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "Presentation.Controls.CtrYesNo",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información General"
        },
        {
            "ItemText": "Pensionado",
            "ItemName": "INDlciPensionary",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Pensionado"
        },
        {
            "ItemText": "Ext. Obligado a Cotizar",
            "ItemName": "INDLcForeignobligedQuotePension",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Pensionado"
        },
        {
            "ItemText": "Incapacitado Permanente",
            "ItemName": "INDLcPermanentInability",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Pensionado"
        },
        {
            "ItemText": "Tipo de Retención",
            "ItemName": "INDlciRetentionType",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.GridLookUpEdit",
            "Visible": true,
            "Obligatory": true,
            "Mandatory": false,
            "LcgName": "Información de Deducciones"
        },
        {
            "ItemText": "Deducción por Vivienda",
            "ItemName": "INDlciHousingDeductionValue",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Deducciones"
        },
        {
            "ItemText": "Deducción por Educación",
            "ItemName": "INDlciEducationDeductionValue",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Deducciones"
        },
        {
            "ItemText": "Promedio Salud Año Ant",
            "ItemName": "INDlciAverageHealthLastYearValue",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Deducciones"
        },
        {
            "ItemText": "Tipo Declarante",
            "ItemName": "INdLciDeclarantType",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Deducciones"
        },
        {
            "ItemText": "Aporte Salud RTF",
            "ItemName": "INDLciHealthContribution",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Deducciones"
        },
        {
            "ItemText": "Pensión Complementaria",
            "ItemName": "INDlciSupplementaryPensionValue",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Deducciones"
        },
        {
            "ItemText": "Discapacidades",
            "ItemName": "INDlciDisability",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Discapacidades e Idiomas"
        },
        {
            "ItemText": "Enfermedad Diagnosticada",
            "ItemName": "LayoutControlItem5",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": false,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información Discapacidades e Idiomas"
        },
        {
            "ItemText": "Estudios",
            "ItemName": "INDlciStudy",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Estudios y Profesiones"
        },
        {
            "ItemText": "Profesiones",
            "ItemName": "INDlciProfession",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información de Estudios y Profesiones"
        },
        {
            "ItemText": "Personas a Cargo",
            "ItemName": "INDlciDependents",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.TextEdit",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información del Grupo Familiar"
        },
        {
            "ItemText": "Grupo Familiar",
            "ItemName": "INDlciRelationship",
            "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
            "Control": "DevExpress.XtraEditors.DropDownButton",
            "Visible": true,
            "Obligatory": false,
            "Mandatory": false,
            "LcgName": "Información del Grupo Familiar"
        }
    ]
}'
FROM Payroll.HumanTalentParameterization h
WHERE Id = 1

----------------------------------------------------------------  Sprint Week 18 - 19 (2026)  -----------------------------------------------------------------------
----Author: Mariana Gonzalez
----BUG : Migracion
---------------------------------------------------------------------------------------------------------------------------------------------------------------------

begin tran t123
begin try

    UPDATE Security.Form
    SET Name = 'Líneas de Distribución'
    WHERE Id = 584;

    UPDATE Security.Form
    SET Name = 'Notas Débito / Crédito'
    WHERE Id = 686;

    UPDATE Security.Form
    SET Name = 'Notas Débito / Crédito'
    WHERE Id = 731;
    
    UPDATE Security.Form
    SET Name = 'Notas Débito / Crédito'
    WHERE Id = 2610;
    
    UPDATE Security.Form
    SET Name = 'Indicadores Económicos'
    WHERE Id = 675;

    UPDATE Security.Form
    SET Name = 'Rangos de Provisión'
    WHERE Id = 681;

    UPDATE Security.Form
    SET Name = 'Conceptos de Conciliación'
    WHERE Id = 2211;

    UPDATE Security.Form
    SET Name = 'Revalorización de Cartera'
    WHERE Id = 100;

    UPDATE Security.Form
    SET Name = 'Cuentas de Difícil Recaudo'
    WHERE Id = 1816;

    UPDATE Security.Form
    SET Name = 'Registro Técnico'
    WHERE Id = 576;

    UPDATE Security.Form
    SET Name = 'Función Equipo'
    WHERE Id = 2077;

   UPDATE Security.Form
   SET Name = 'Estadística Mantenimiento de Equipos'
   WHERE Id = 2216;

COMMIT TRAN t123;
end TRY
begin catch 
  IF @@TRANCOUNT > 0   
       ROLLBACK TRAN t123;
end catch  

/*==============================================================================================================================
	Author: Felix Camilo Salazar Roldan
	BUG : 35827 No está generando ordenes de servicio con interfaz stella (INDIGOBOT) a imágenes diagnosticas
	Sprint : ERP_Services\Sprint Week 20 - 21 (2026)
==============================================================================================================================*/
--NOTA: Despues de realizar algun Alter a la vista [dbo].[VServicesProceduresImagesDx]  ejecutar los EXEC
--Refrescar metadatos: sin esto, [Tipo]=NULL en la vista del bot y Stella no procesa imágenes

  EXEC sp_refreshview 'dbo.VServicesProceduresImagesDx';
  EXEC sp_refreshview 'Billing.BotViewServicesProceduresImagesDX';


----------------------------------------------------------------  Sprint Week 22 - 23 (2026)  -----------------------------------------------------------------------
 -- Author: Anthony Ocampo
 -- PBI: Product Backlog Item 35925: Automatización y adaptación proceso de saldos iniciales + notas electronicas
 -- Descripción: Se actualiza la tabla de PortfolioInitialBalanceAccountReceivable para comprender nuevas columnas de guardado de cuentas contables de deterioro.
---------------------------------------------------------------------------------------------------------------------------------------------------------------------

ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable]
    ADD [AccountHardCollectionId]            INT             NULL,
        [DeteriorationBalance]               DECIMAL (18, 2) NOT NULL CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_DeteriorationBalance]              DEFAULT ((0)),
        [DeteriorationBalanceCurrentYear]    DECIMAL (18, 2) NOT NULL CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_DeteriorationBalanceCurrentYear]   DEFAULT ((0)),
        [DeteriorationBalancePreviousYear]   DECIMAL (18, 2) NOT NULL CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_DeteriorationBalancePreviousYear]  DEFAULT ((0)),
        [CurrentDeteriorationYear]           INT             NULL,
        [CUV]                                VARCHAR (200)   NULL;
GO

ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable]
    ADD CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts7]
        FOREIGN KEY ([AccountHardCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]);
GO

ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable]
    NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts7];
GO

ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable]
    ADD CONSTRAINT [CK_PortfolioInitialBalanceAccountReceivable_ValidateDeteriorationBalance]
        CHECK ([DeteriorationBalance] = ([DeteriorationBalanceCurrentYear] + [DeteriorationBalancePreviousYear]));
GO

ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable]
    ADD CONSTRAINT [CK_PortfolioInitialBalanceAccountReceivable_ValidateDeteriorationBalancesNotNegatives]
        CHECK ([DeteriorationBalance]            >= (0)
           AND [DeteriorationBalanceCurrentYear] >= (0)
           AND [DeteriorationBalancePreviousYear] >= (0));
GO


----------------------------------------------------------------  Sprint Week 26 - 27 (2026)  -----------------------------------------------------
/*==============================================================================================================================
Author: Oscar astudillo reyes
Work Item : Bug #38551
==============================================================================================================================*/
-- Tabla temporal con el mapeo ConceptClass 
DECLARE @Mapeo TABLE (
    ConceptClass CHAR(3),
    InternalCode INT,
    Descripcion VARCHAR(200)
)
INSERT INTO @Mapeo (ConceptClass, InternalCode, Descripcion) VALUES
-- ========== DEVENGADOS ==========
('005', 1, 'Sueldo → Salario Básico'),
('006', 2, 'Aux Transporte → Auxilio de Transporte'),
('054', 4, 'Viáticos NS → Viático No Salarial'),
('001', 5, 'HED → Hora Extra Diurna'),
('012', 6, 'HEN → Hora Extra Nocturna'),
('042', 7, 'HRN → Hora Recargo Nocturno'),
('013', 8, 'HEDDF → Hora Extra Diurna Dom/Fest'),
('051', 9, 'HRDDF → Hora Recargo Diurno Dom/Fest'),
('052', 9, 'HRDDF → Hora Recargo Diurno Dom/Fest (Duplicado)'),
('050', 10, 'HENDF → Hora Extra Nocturna Dom/Fest'),
('043', 11, 'HRNDF → Hora Recargo Nocturno Dom/Fest'),
('030', 13, 'Vacaciones Compensadas'),
('031', 13, 'Provisión Vacaciones'),
('002', 14, 'Primas'),
('003', 14, 'Primas (Duplicado)'),
('033', 14, 'Provisión Primas'),
('008', 15, 'Cesantías/Provisión'),
('034', 16, 'Intereses Cesantías'),
('021', 17, 'Incapacidad Común'),
('022', 17, 'Incapacidad Profesional'),
('027', 17, 'Incapacidad Laboral'),
('067', 17, 'Incapacidad Ambulatoria - Patrono'),
('068', 17, 'Incapacidad Ambulatoria - EPS'),
('069', 17, 'Incapacidad Hospitalaria - Patrono'),
('070', 17, 'Incapacidad Hospitalaria - EPS'),
('075', 17, 'Incapacidad Riesgo Profesional - Patrono'),
('076', 17, 'Incapacidad Riesgo Profesional - ERP'),
('023', 18, 'Licencia Maternidad/Paternidad'),
('074', 18, 'Licencia por Luto'),
('024', 19, 'Licencia Remunerada'),
('028', 19, 'Licencia Remunerada (Duplicado)'),
('025', 20, 'Licencia No Remunerada'),
('004', 21, 'Bonificación Salarial'),
('046', 21, 'Bonificación Salarial (Duplicado)'),
('049', 21, 'Prima de Vacaciones'),
('055', 21, 'Bonificación Salarial (Bono Variable)'),
('064', 21, 'Incremento de Vacaciones'),
('063', 22, 'Bonificación Especial Recreación'),
('065', 22, 'Bono NS'),
('047', 23, 'Auxilio Salarial'),
('010', 26, 'Otro Concepto'),
('066', 34, 'Apoyo Sostenimiento'),
('007', 37, 'Indemnización'),

-- ========== DEDUCCIONES ==========
('017', 39, 'Aporte Salud'),
('014', 40, 'Aporte Pensión'),
('038', 41, 'FSP'),
('044', 43, 'Sindicatos'),
('026', 44, 'Sanción'),
('041', 45, 'Libranza'),
('011', 48, 'Otras Deducciones'),
('019', 48, 'Otras Deducciones (Duplicado)'),
('016', 49, 'Aportes Voluntarios Pensión'),
('020', 50, 'Retención en la Fuente'),
('048', 50, 'Retención en la Fuente (Duplicado)'),
('061', 50, 'Retención Indemnización'),
('045', 51, 'Aportes AFC'),
('053', 53, 'Embargo Fiscal'),

-- ========== CONCEPTOS SIN MAPEO EN SP (usan InternalCode 0 = No Aplica) ==========
('009', 0, 'Aporte Riesgos Profesionales → No Aplica (Patronal)'),
('015', 0, 'Aporte Pensión Patrono → No Aplica (Patronal)'),
('018', 0, 'Aporte Salud Patrono → No Aplica (Patronal)'),
('035', 0, 'Aporte Parafiscal SENA → No Aplica (Patronal)'),
('036', 0, 'Aporte Parafiscal Caja → No Aplica (Patronal)'),
('037', 0, 'Aporte Parafiscal ICBF → No Aplica (Patronal)')


-- Ejecutar actualización para conceptos CON homologación (InternalCode > 0)
UPDATE c
SET c.IdElectronicPayrollConcepts = epc.Id
FROM Payroll.Concept c
INNER JOIN @Mapeo m ON c.ConceptClass = m.ConceptClass
INNER JOIN Payroll.ElectronicPayrollConcepts epc ON epc.InternalCode = m.InternalCode
WHERE c.State = 1  -- Solo conceptos activos
  AND m.InternalCode > 0
  AND c.IdElectronicPayrollConcepts IS NULL


-- Incapacidad Comun (Subtype 1, Id = 3)
UPDATE Payroll.Concept
SET IdElectronicPayrollConceptSubtype = (Select id from  [Payroll].[ElectronicPayrollConceptSubtype]  where name =   'Incapacidad Comun')
WHERE ConceptClass IN ('021', '022', '067', '068', '069', '070')
  AND IdElectronicPayrollConcepts = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 17)
  AND IdElectronicPayrollConceptSubtype IS NULL


-- --'Incapacidad Laboral'
UPDATE Payroll.Concept
SET IdElectronicPayrollConceptSubtype = (Select id from  [Payroll].[ElectronicPayrollConceptSubtype]  where name =   'Incapacidad Laboral')
WHERE ConceptClass IN ('027')
  AND IdElectronicPayrollConcepts = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 17)
  AND IdElectronicPayrollConceptSubtype IS NULL


UPDATE Payroll.Concept
SET IdElectronicPayrollConceptSubtype = (Select id from  [Payroll].[ElectronicPayrollConceptSubtype]  where name =   'Sancion Publica')
WHERE ConceptClass = '026'
  AND IdElectronicPayrollConcepts = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 44)
  AND IdElectronicPayrollConceptSubtype IS NULL


----------------------------------------------------------------  Sprint Week 31 (2026)  -----------------------------------------------------
/*==============================================================================================================================
Author: Karen Esquivel
Descripcion: Backfill de sincronización de [Authorization].[BotDashboardAuthorization] contra el estado vivo de
             TraceabilityPaperwork (Status, AssignUserCode, PreviousStatus). Ejecutar UNA sola vez, después de crear el
             trigger TR_TraceabilityPaperwork_SyncBotDashboard (Authorization\Tables\TraceabilityPaperwork.sql), para
             que los registros ya existentes en la caché queden al día antes de que el trigger cubra los cambios futuros.
==============================================================================================================================*/
;WITH LatestTp AS (
    SELECT tp.EntityId, tp.EntityName, tp.ServiceCode, tp.Id, tp.Status, tp.AssignUserCode, tp.PreviousStatus,
           ROW_NUMBER() OVER (PARTITION BY tp.EntityId, tp.EntityName, tp.ServiceCode ORDER BY tp.Id DESC) AS rn
    FROM [Authorization].TraceabilityPaperwork tp
    WHERE tp.EntityId IS NOT NULL AND tp.EntityName IS NOT NULL AND tp.ServiceCode IS NOT NULL
)
UPDATE h
SET h.TraceabilityPaperworkStatus = COALESCE(lt.Status, h.TraceabilityPaperworkStatus),
    h.TraceabilityPaperworkId = COALESCE(lt.Id, h.TraceabilityPaperworkId),
    h.AssignUserCode = COALESCE(lt.AssignUserCode, h.AssignUserCode),
    h.PreviousStatus = COALESCE(lt.PreviousStatus, h.PreviousStatus)
FROM [Authorization].BotDashboardAuthorization h
JOIN LatestTp lt ON h.EntityId = lt.EntityId
                AND h.EntityName = lt.EntityName
                AND h.ItemCodeOriginal = lt.ServiceCode
WHERE lt.rn = 1;


/*==============================================================================================================================
Descripcion: Limpieza de índices de prueba en [Authorization].[BotDashboardAuthorization] creados manualmente
==============================================================================================================================*/
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IDX_BotDashboard_CareCenter_Type_Service' AND object_id = OBJECT_ID('[Authorization].[BotDashboardAuthorization]'))
    DROP INDEX [IDX_BotDashboard_CareCenter_Type_Service] ON [Authorization].[BotDashboardAuthorization];

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IDX_BotDashboardAuthorization_CareCenterCode_Type_ServiceId' AND object_id = OBJECT_ID('[Authorization].[BotDashboardAuthorization]'))
    DROP INDEX [IDX_BotDashboardAuthorization_CareCenterCode_Type_ServiceId] ON [Authorization].[BotDashboardAuthorization];

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_BotDashboardAuthorization_CareCenterCode_Type_ServiceId' AND object_id = OBJECT_ID('[Authorization].[BotDashboardAuthorization]'))
    DROP INDEX [IX_BotDashboardAuthorization_CareCenterCode_Type_ServiceId] ON [Authorization].[BotDashboardAuthorization];

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_BotDashboardAuthorization_Dashboard_InitialQueue' AND object_id = OBJECT_ID('[Authorization].[BotDashboardAuthorization]'))
    DROP INDEX [IX_BotDashboardAuthorization_Dashboard_InitialQueue] ON [Authorization].[BotDashboardAuthorization];

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_BotDashboardAuthorization_CareCenter_Status' AND object_id = OBJECT_ID('[Authorization].[BotDashboardAuthorization]'))
    DROP INDEX [IX_BotDashboardAuthorization_CareCenter_Status] ON [Authorization].[BotDashboardAuthorization];
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TraceabilityPaperwork_Dashboard_Status' AND object_id = OBJECT_ID('[Authorization].[TraceabilityPaperwork]'))
    DROP INDEX [IX_TraceabilityPaperwork_Dashboard_Status] ON [Authorization].[TraceabilityPaperwork];

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TraceabilityPaperwork_Status_CareCenter' AND object_id = OBJECT_ID('[Authorization].[TraceabilityPaperwork]'))
    DROP INDEX [IX_TraceabilityPaperwork_Status_CareCenter] ON [Authorization].[TraceabilityPaperwork];
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE name = 'PatientNameAge' AND object_id = OBJECT_ID('[Authorization].[BotDashboardAuthorization]'))
BEGIN
    ALTER TABLE [Authorization].[BotDashboardAuthorization]
        ADD [PatientNameAge] AS (CONCAT([PatientName], ' - ', [PatientAge])) PERSISTED;
END
GO

UPDATE STATISTICS [Authorization].[BotDashboardAuthorization] WITH FULLSCAN;
GO

/*======================================================Sprint Week 32 - 33 (2026)=============================================
	Author: Jhon Sebastian Hermida
	Work Item: PBI 35988 - Ajuste de formulario Parámetros en el módulo de Glosas, para actualización de notas crédito de vigencias anteriores
    Descripción: Actualización de parámetros de glosas - cuenta PUC y tipo de tercero de vigencias anteriores
==============================================================================================================================*/
IF COL_LENGTH('Glosas.TimeParameters', 'PreviousLifetimesMainAccountId') IS NULL
BEGIN
    ALTER TABLE [Glosas].[TimeParameters]
        ADD [PreviousLifetimesMainAccountId] INT NULL;

    ALTER TABLE [Glosas].[TimeParameters]
        ADD CONSTRAINT [FK_TimeParameters_PreviousLifetimesMainAccount]
            FOREIGN KEY ([PreviousLifetimesMainAccountId])
            REFERENCES [GeneralLedger].[MainAccounts] ([Id]);
END
GO

IF COL_LENGTH('Glosas.TimeParameters', 'PreviousLifetimesThirdPartyType') IS NULL
BEGIN
    ALTER TABLE [Glosas].[TimeParameters]
        ADD [PreviousLifetimesThirdPartyType] TINYINT NULL
            CONSTRAINT [DF_TimeParameters_PreviousLifetimesThirdPartyType] DEFAULT ((1));
END
GO

-- Compatibilidad: si ya había tercero parametrizado, asumir "Tercero diferente de Factura"
IF COL_LENGTH('Glosas.TimeParameters', 'PreviousLifetimesThirdPartyType') IS NOT NULL
BEGIN
    UPDATE [Glosas].[TimeParameters]
    SET [PreviousLifetimesThirdPartyType] = CASE
            WHEN [PreviousLifetimesThirdPartyId] IS NOT NULL THEN CAST(2 AS TINYINT)
            ELSE CAST(1 AS TINYINT)
        END
    WHERE [PreviousLifetimesThirdPartyType] IS NULL;
END
GO

-- Precarga opcional de cuenta PUC desde el concepto legacy (solo si la cuenta aún está vacía)
IF COL_LENGTH('Glosas.TimeParameters', 'PreviousLifetimesMainAccountId') IS NOT NULL
BEGIN
    UPDATE tp
    SET tp.[PreviousLifetimesMainAccountId] = pnc.[IdAccount]
    FROM [Glosas].[TimeParameters] tp
    INNER JOIN [Portfolio].[PortfolioNoteConcept] pnc
        ON pnc.[Id] = tp.[PreviousLifetimesConceptNoteId]
    WHERE tp.[PreviousLifetimesMainAccountId] IS NULL
      AND pnc.[IdAccount] IS NOT NULL;
END
GO

-- Tercero opcional por línea de detalle NoteType=6 (override Type 2 vigencias anteriores / Glosas)
IF COL_LENGTH('Portfolio.PortfolioNoteAccountReceivableDetail', 'ThirdPartyId') IS NULL
BEGIN
    ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail]
        ADD [ThirdPartyId] INT NULL;
END
GO

IF COL_LENGTH('Portfolio.PortfolioNoteAccountReceivableDetail', 'ThirdPartyId') IS NOT NULL
   AND NOT EXISTS (
        SELECT 1
        FROM sys.foreign_keys
        WHERE name = 'FK_PortfolioNoteAccountReceivableDetail_ThirdParty'
          AND parent_object_id = OBJECT_ID('Portfolio.PortfolioNoteAccountReceivableDetail')
   )
BEGIN
    ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail]
        ADD CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_ThirdParty]
            FOREIGN KEY ([ThirdPartyId])
            REFERENCES [Common].[ThirdParty] ([Id]);
END
GO

/*======================================================Sprint Week 34 - 35 (2026)=============================================
	Author: Karen Julieth Esquivel Polo
	Work Item: BUG-42122 - Versión Migración - Dashboard Farmacia Paquetes QX no muestra items
    Descripción: Ajuste campo nuevo en HCFARMEPD para evitar usar tabla AGEPROGQX, con este script quedaria cargada la data anterior para mantener la integridad de la información y no perder el dato de CODSERIPS
==============================================================================================================================*/

BEGIN
--- Update con la tabla renombrada legacy_AGEPROGQX 
UPDATE D
    SET D.CODSERIPS_QX = P.CODSERIPS
FROM dbo.HCFARMEPD AS D
INNER JOIN legacy_AGEPROGQX AS P ON D.IDAGEPROGQX = P.CODAUTONU
WHERE D.IDAGEPROGQX IS NOT NULL AND D.CODSERIPS_QX IS NULL;

--- Update con la tabla anterior AGEPROGQX (por si no se ha renombrado)
--UPDATE D
--    SET D.CODSERIPS_QX = P.CODSERIPS
--FROM dbo.HCFARMEPD AS D
--INNER JOIN dbo.AGEPROGQX AS P ON D.IDAGEPROGQX = P.CODAUTONU
--WHERE D.IDAGEPROGQX IS NOT NULL AND D.CODSERIPS_QX IS NULL;
END
GO

/*======================================================Sprint Week 36 - 37 (2026)=============================================
  Author: Cesar Collazos
  Work Item: PBI-32507
    Descripción: Agrega el parámetro que permite excluir las facturas de un Grupo de Atención EAPB del Mecanismo de Validación FEV RIPS.
==============================================================================================================================*/
IF COL_LENGTH('Contract.CareGroup', 'ExcludeFEVRIPS') IS NULL
BEGIN
    ALTER TABLE [Contract].[CareGroup]
        ADD [ExcludeFEVRIPS] BIT NOT NULL
            CONSTRAINT [DF_CareGroup_ExcludeFEVRIPS] DEFAULT ((0)) WITH VALUES;
END
GO

/*=============================================================================================================================
	Author: Jhon Sebastian Hermida
	Work Item: Bug Migración -Talento Humano
    Descripción: Se ajusta el campo EmployeeTypeId para que se asigne nulo por defecto, ya que el tipo de empleado se asigna cuando se crea el contrato
==============================================================================================================================*/

IF EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_Employee_EmployeeTypeId')
    ALTER TABLE Payroll.Employee DROP CONSTRAINT DF_Employee_EmployeeTypeId;

ALTER TABLE Payroll.Employee ADD CONSTRAINT DF_Employee_EmployeeTypeId
    DEFAULT (NULL) FOR [EmployeeTypeId];