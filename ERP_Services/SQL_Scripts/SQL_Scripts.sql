SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:
-- Create date: 06/09/2023
-- Description:	Archivo SQL, Scripts Nuevas Columnas de tablas
-- =============================================

/*==================================================================================================================
	Author: Giovanny Plazas
	PBI : 11804
	Sprint : Sprint_week_34-36
=====================================================================================================================*/
ALTER TABLE Common.Person add IdentificationTypeId INT
ALTER TABLE [Common].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_ADTIPOIDENTIFICA] FOREIGN KEY([IdentificationTypeId])
REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID])

ALTER TABLE [Common].[Person] CHECK CONSTRAINT [FK_Person_ADTIPOIDENTIFICA]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id del tipo de identificacion, de la tabla ADTIPOIDENTIFICA Este es el nuevo campo' , @level0type=N'SCHEMA',@level0name=N'Common', @level1type=N'TABLE',@level1name=N'Person', @level2type=N'COLUMN',@level2name=N'IdentificationTypeId'


		IF EXISTS(SELECT 1  
					from Common.Person p with(NOLOCK)
					LEFT JOIN ADTIPOIDENTIFICA atd WITH(NOLOCK) on (CASE p.IdentificationType
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
						WHERE atd.ID is NULL
						 ) BEGIN

			insert into ADTIPOIDENTIFICA(CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA,MinimunLength,MaximumLength)
			SELECT CONCAT(9,iif(p.IdentificationType in (0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15),p.IdentificationType,20)),
					CASE p.IdentificationType
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
						END,
						CASE p.IdentificationType
						WHEN 0 THEN 'CC - Cédula de Ciudadanía'
						WHEN 1 then 'CE - Cédula de Extranjería'
						WHEN 2 THEN 'TI - Tarjeta de Identidad'
						WHEN 3 THEN 'RC - Registro Civil'
						WHEN 4 THEN 'PA - Pasaporte'
						WHEN 5 THEN 'AS - Adulto Sin Identificación'
						WHEN 6 then 'MS - Menor Sin Identificación'
						WHEN 7 THEN 'NI - Nit'
						WHEN 8 THEN 'NU - Número único de identificación personal'
						WHEN 9 THEN 'CN - Cetrigicado Nacido Vivo'
						WHEN 10 THEN 'CD - Carnet Diplomático'
						WHEN 11 THEN 'SC - Salvoconducto'
						WHEN 12 THEN 'PE - Permiso especial de Permanencia'
						WHEN 13 THEN 'PT - Permiso por Protección Temporal'
						WHEN 14 THEN 'DE - Documento extranjero'
						WHEN 15 THEN 'SI - Sin identificacion'
						ELSE 'Otro'
						END, 1,'999',common.GETDATE(),4,25

			FROM Common.Person p
			LEFT JOIN [dbo].[ADTIPOIDENTIFICA] atd on  (CASE p.IdentificationType
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
			WHERE atd.ID IS null
			GROUP by p.IdentificationType
		END

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
--=====================================================================================================================

/*=====================================================================================================================
	Author: Juan Pablo Daza Medina
	PBI : 11984
	Sprint : Sprint_week_36-38
==========================================================================================================================*/
ALTER TABLE [Inventory].[InventoryProduct] 
ADD [SismedReport] BIT DEFAULT (0);
--==========================================================================================================================

/*==========================================================================================================================
	Author: Cesar Collazos
	BUG : 12221
	Sprint : Sprint_week_36-38
==============================================================================================================================*/
UPDATE Treasury.CashReceiptDetails SET CurrencyId = 1 , TRM = 1, ValueInCurrencyHeader = Value WHERE ValueInCurrencyHeader = 0
UPDATE Treasury.PaymentMethods SET CurrencyId = 1 , TRM = 1, ValueInCurrencyHeader = Value WHERE ValueInCurrencyHeader = 0
--=============================================================================================================================

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

/*==========================================================================================================================
	Author: Angi Duran
	BUG : 12262
	Sprint : Sprint_week_36-38
==============================================================================================================================*/
alter table Portfolio.PortfolioInitialBalanceAccountReceivableAccounting alter column Value numeric(18,2) not null
--=============================================================================================================================

/*==================================================================================================================
	Author: Juan Pablo Daza Medina
	PBI : 9514
	Sprint : Sprint_week_38-40
=====================================================================================================================*/
ALTER TABLE [Payroll].[UnemployedLiquidation] 
ADD UnemployedInterestPaidWithPayroll BIT DEFAULT (0);


/*==================================================================================================================
	Author: Juan Jose Aviles Roa
	PBI : 11347
	Sprint : Sprint_week_30-42
=====================================================================================================================*/
alter table Payroll.BankFile add Process tinyint not null default(1)

/*==================================================================================================================
	Author: Giovanny Plazas
	PBI : 13423
	Sprint : Sprint_week_36-38
=====================================================================================================================*/
ALTER TABLE Billing.SettingsBilling ADD MaxInvoiceItems int not null default(0)

/*==================================================================================================================
	Author: Julieth Cardenas
	PBI : 13700
	Sprint : Sprint_week_46-48(2023)
=====================================================================================================================*/
ALTER TABLE Cost.CostGeneralExpense ADD DistributionType TINYINT not null DEFAULT(1)
/*==================================================================================================================
	Author: Oscar Astudillo 
	PBI : 13503
	Sprint : Sprint_week_46-48(2023)
=====================================================================================================================*/
ALTER TABLE MixingStation.QuantityRemaining
ALTER COLUMN RemnantVolume decimal(18, 4);
/*==========================================================================================================================
	Author: Cesar Collazos
	BUG : 13601
	Sprint : Sprint_week_46-48
==============================================================================================================================*/
ALTER TABLE Portfolio.AccountReceivable ADD DateAccountBecomeZero date NULL

UPDATE Portfolio.AccountReceivable set DateAccountBecomeZero = [Portfolio].[GetLastMovementAccountReceivable](Id)
WHERE Balance = 0 AND DateAccountBecomeZero IS NULL
/*==========================================================================================================================
	Author: Cesar Collazos
	PBI : 13934
	Sprint : Sprint_week_52(2023)-2(2024)
==============================================================================================================================*/
ALTER TABLE Portfolio.PortfolioNoteConcept
ADD HandleTax BIT DEFAULT 0 NOT NULL;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el concepto maneja o no impuesto' , @level0type=N'SCHEMA',@level0name=N'Portfolio', @level1type=N'TABLE',@level1name=N'PortfolioNoteConcept', @level2type=N'COLUMN',@level2name=N'HandleTax'
GO

UPDATE Portfolio.PortfolioNoteConcept
SET HandleTax = (
	SELECT SalePriceIncludeTax
	FROM GeneralLedger.CompanySettings
	)

/*==========================================================================================================================
	Author: Andrés Steven Rojas
	PBI : 14042
	Sprint : Sprint_week_52(2023)-2(2024)
==============================================================================================================================*/
ALTER TABLE Portfolio.PortfolioNoteDetail
ADD [IdGeneralLedgerIVA] [int] NULL,
	[IvaRate] [decimal](18, 2) NULL,
	[TotalConcept] [decimal](18, 2) NULL

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relaciona la tarifa del IVA' , @level0type=N'SCHEMA',@level0name=N'Portfolio', @level1type=N'TABLE',@level1name=N'PortfolioNoteDetail', @level2type=N'COLUMN',@level2name=N'IdGeneralLedgerIVA'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'El valor aplicado al valor base por la tarifa de IVA' , @level0type=N'SCHEMA',@level0name=N'Portfolio', @level1type=N'TABLE',@level1name=N'PortfolioNoteDetail', @level2type=N'COLUMN',@level2name=N'IvaRate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor resultante de la suma del campo Base más valor IVA' , @level0type=N'SCHEMA',@level0name=N'Portfolio', @level1type=N'TABLE',@level1name=N'PortfolioNoteDetail', @level2type=N'COLUMN',@level2name=N'TotalConcept'
GO


-- Se asigna TotalConcept cuando no existía IVA
UPDATE Portfolio.PortfolioNoteDetail SET TotalConcept = [Value]
WHERE IdGeneralLedgerIVA IS NULL AND TotalConcept IS NULL

/*==========================================================================================================================
	Author: Cesar Collazos
	PBI : 8319
	Sprint : HCM_week_4-6 (2024)
==============================================================================================================================*/
INSERT INTO Payroll.HumanTalentParameterization (JSONdata, CreationUser, CreationDate)
VALUES (N'{
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
      "Obligatory": false,
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
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Fecha Expedición Identificación",
      "ItemName": "INDlciIdDate",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DateEdit",
      "Visible": true,
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
      "ItemText": "Creencias Religiosas",
      "ItemName": "LayoutControlItem7",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
	{
      "ItemText": "Grupos Étnicos",
      "ItemName": "INDlciEthnicGroups",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Identificación"
    },
    {
      "ItemText": "Firma Digitalizada",
      "ItemName": "LayoutControlItem2",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.PopupContainerEdit",
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
      "ItemText": "Fecha de Fallecimiento",
      "ItemName": "INDlciDeathDate",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DateEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Grupo Sanguíneo",
      "ItemName": "INDlciBloodGroup",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "RH",
      "ItemName": "INDlciRH",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
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
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
	{
      "ItemText": "Tipo Vivienda",
      "ItemName": "INDlciHousingType",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
	{
      "ItemText": "Estrato Socioeconómico",
      "ItemName": "INDciSocioEconomicStatus",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
	{
      "ItemText": "¿Consume Cigarrillo?",
      "ItemName": "INDciCigaretteConsumption",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
	{
      "ItemText": "¿Practica Deporte?",
      "ItemName": "INDciSportPractice",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.GridLookUpEdit",
      "Visible": true,
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
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Personal"
    },
    {
      "ItemText": "Idiomas",
      "ItemName": "INDlciLanguage",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": true,
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
      "Visible": true,
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
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Información de Contacto",
      "ItemName": "INDlciContactInfo",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información General"
    },
    {
      "ItemText": "Cotizante en el Exterior",
      "ItemName": "INDLciContributorAbroad",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "Presentation.Controls.CtrYesNo",
      "Visible": true,
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
      "Visible": true,
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
      "Obligatory": false,
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
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Tipo Declarante",
      "ItemName": "INdLciDeclarantType",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.SearchLookUpEdit",
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información de Deducciones"
    },
    {
      "ItemText": "Aporte Salud RTF",
      "ItemName": "INDLciHealthContribution",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.TextEdit",
      "Visible": true,
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
      "Visible": true,
      "Obligatory": false,
      "Mandatory": false,
      "LcgName": "Información Discapacidades e Idiomas"
    },
    {
      "ItemText": "Enfermedad Diagnosticada",
      "ItemName": "LayoutControlItem5",
      "LayaoutContolItem": "DevExpress.XtraLayout.LayoutControlItem",
      "Control": "DevExpress.XtraEditors.DropDownButton",
      "Visible": true,
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
}', 999, GETDATE())
/*==============================================================================================================================
	Author: Cesar Collazos
	PBI : 16043
	Sprint : ERP_Services\VF_week_12-14 (2024)
==============================================================================================================================*/

ALTER TABLE FixedAsset.FixedAssetInitialBalanceItem
ADD ValidMinorAmount BIT NULL

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valida menor cuantía (1 - SI, 0 - NO)' , @level0type=N'SCHEMA',@level0name=N'FixedAsset', @level1type=N'TABLE',@level1name=N'FixedAssetInitialBalanceItem', @level2type=N'COLUMN',@level2name=N'ValidMinorAmount'
GO

UPDATE FixedAsset.FixedAssetInitialBalanceItem
SET ValidMinorAmount = IIF(Depreciate = 1, 1, NULL)

/*==============================================================================================================================
	Author: Steven Rojas
	PBI : 15738
	Sprint : ERP_Services\VF_week_14-16 (2024)
==============================================================================================================================*/
-- CABECERA
ALTER TABLE FixedAsset.FixedAssetItemCatalog
ADD HandlesDepreciationbyDistribution BIT NOT NULL

ALTER TABLE [FixedAsset].[FixedAssetItemCatalog] ADD  CONSTRAINT [DF_FixedAssetItemCatalog_HandlesDepreciationbyDistribution]  DEFAULT ((0)) FOR [HandlesDepreciationbyDistribution]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maneja depreciación por Distribución' , @level0type=N'SCHEMA',@level0name=N'FixedAsset', @level1type=N'TABLE',@level1name=N'FixedAssetItemCatalog', @level2type=N'COLUMN',@level2name=N'HandlesDepreciationbyDistribution'
GO	

UPDATE FixedAsset.FixedAssetItemCatalog
SET HandlesDepreciationbyDistribution = 0
WHERE HandlesDepreciationbyDistribution IS NULL

-- DETALLES
ALTER TABLE FixedAsset.FixedAssetItemCatalogDetail
ADD CostCenterId int NULL,
	DistributionPercentage decimal(20, 4) NULL

ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail]  WITH CHECK ADD  CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [Payroll].[CostCenter] ([Id])
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id del Centro de Costo' , @level0type=N'SCHEMA',@level0name=N'FixedAsset', @level1type=N'TABLE',@level1name=N'FixedAssetItemCatalogDetail', @level2type=N'COLUMN',@level2name=N'CostCenterId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Porcentaje de Distribución' , @level0type=N'SCHEMA',@level0name=N'FixedAsset', @level1type=N'TABLE',@level1name=N'FixedAssetItemCatalogDetail', @level2type=N'COLUMN',@level2name=N'DistributionPercentage'
GO

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

  -- Agregar descripción a la columna
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el procedimiento quirúrgico tiene o no informe obligatorio, Si ServiceType = 5; entonces el valor por defecto de esta columna es True' , @level0type=N'SCHEMA',@level0name=N'Contract', @level1type=N'TABLE',@level1name=N'CUPSEntity', @level2type=N'COLUMN',@level2name=N'SurgicalReport'
END

/*==============================================================================================================================
	Author: Cesar Collazos
	PBI : 16299
	Sprint : ERP_Services\VF_week_16-18 (2024)
==============================================================================================================================*/

ALTER TABLE FixedAsset.FixedAssetPhysicalAsset
ADD NetHistoricalValue decimal(18, 2) DEFAULT(0) NOT NULL,
FinancialDiscount decimal(18, 2) DEFAULT(0) NOT NULL

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor histórico neto(valor histórico - valor descuento financiero)' , @level0type=N'SCHEMA',@level0name=N'FixedAsset', @level1type=N'TABLE',@level1name=N'FixedAssetPhysicalAsset', @level2type=N'COLUMN',@level2name=N'NetHistoricalValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descuento financiero' , @level0type=N'SCHEMA',@level0name=N'FixedAsset', @level1type=N'TABLE',@level1name=N'FixedAssetPhysicalAsset', @level2type=N'COLUMN',@level2name=N'FinancialDiscount'
GO

/*==============================================================================================================================
	Author: Cesar Collazos
	BUG : 17115
	Sprint : ERP_Services\VF_week_18-20 (2024)
==============================================================================================================================*/

ALTER TABLE Budget.RecognitionDetail
ALTER COLUMN InitialValue NUMERIC (18,2) NOT NULL

ALTER TABLE Budget.RecognitionDetail
ALTER COLUMN DebitValueModification NUMERIC (18,2) NOT NULL

ALTER TABLE Budget.RecognitionDetail
ALTER COLUMN CreditValueModification NUMERIC (18,2) NOT NULL

ALTER TABLE Budget.RecognitionDetail
ALTER COLUMN TotalRecognition NUMERIC (18,2) NOT NULL

ALTER TABLE Budget.RecognitionDetail
ALTER COLUMN ExecutedValue NUMERIC (18,2) NOT NULL

ALTER TABLE Budget.RecognitionDetail
ALTER COLUMN Balance NUMERIC (18,2) NOT NULL



/*==============================================================================================================================
	Author: Angi Durán Vargas
	PBI : 16687
	Sprint : ERP_Services\VF_week_18-20 (2024)
==============================================================================================================================*/

alter table Payments.AccountPayable add TaxRegistration tinyint NULL


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

alter table Cost.CostDistributionDirectCost add TaxRegistration tinyint NULL

UPDATE Cost.CostDistributionDirectCost SET TaxRegistration = (case DeductibleIva
																when 0 then ISNULL((select top 1 TaxRegistration from GeneralLedger.CompanySettings where TaxRegistration<>2 AND TaxRegistration<>3),4)
																when 1 then 2
															 end)  
										WHERE TaxRegistration is null
		


/*==============================================================================================================================*/

/*==================================================================================================================
	Author: Giovanny Plazas
	PBI : 16991
	Sprint : ERP_Services\VF_week_20-22 (2024)
=====================================================================================================================*/


ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] ADD [BaseValue] NUMERIC(20,2) NOT NULL DEFAULT(0)


ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] ADD [TaxValue] NUMERIC(20,2) NOT NULL DEFAULT(0)

ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] ADD [TaxPercentage] NUMERIC(5,2)

ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] ADD [TaxId] INT

ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail]  WITH CHECK ADD  CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_TaxId] FOREIGN KEY([TaxId])
REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
GO

ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] CHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_TaxId]
GO


EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor base, antes de IVA' , @level0type=N'SCHEMA',@level0name=N'Portfolio', @level1type=N'TABLE',@level1name=N'PortfolioNoteAccountReceivableDetail', @level2type=N'COLUMN',@level2name=N'BaseValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del IVA' , @level0type=N'SCHEMA',@level0name=N'Portfolio', @level1type=N'TABLE',@level1name=N'PortfolioNoteAccountReceivableDetail', @level2type=N'COLUMN',@level2name=N'TaxValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id del IVA Asociado' , @level0type=N'SCHEMA',@level0name=N'Portfolio', @level1type=N'TABLE',@level1name=N'PortfolioNoteAccountReceivableDetail', @level2type=N'COLUMN',@level2name=N'TaxId'
GO


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
		

/*==============================================================================================================================*/

/*==========================================================================================================================
	Author: Anthony Ocampo
	PBI : 17769
	Sprint : ERP_Services\SCM_week_22-24 (2024)
==============================================================================================================================*/

ALTER TABLE [Inventory].[DrugInteraction]
ALTER COLUMN [DCIId] [int] NULL

ALTER TABLE [Inventory].[DrugInteraction]
ALTER COLUMN [ATCEntityId] [int] NULL

UPDATE INVENTORY.DrugInteraction
SET ATCEntityId = IdATCEntity
from Inventory.DCIATCEntity
where DCIId = IdDCI and IdATCEntity is not null

ALTER TABLE [Inventory].[DrugInteraction]
ALTER COLUMN [ATCEntityId] [int] NOT NULL

------------------------------------

ALTER TABLE [Inventory].[DrugActive]
ALTER COLUMN [ATCEntityId] [int] NOT NULL

ALTER TABLE [Inventory].[DrugActive]
ALTER COLUMN [DCIId] [int] NULL

UPDATE INVENTORY.DrugActive
SET ATCEntityId = IdATCEntity
FROM Inventory.DCIATCEntity
WHERE DCIId = IdDCI AND IdATCEntity IS NOT NULL

ALTER TABLE [Inventory].[DrugActive]
ALTER COLUMN [ATCEntityId] [int] NOT NULL

/*==============================================================================================================================
Author: Cesar Collazos
	PBI : 10184
	Sprint : ERP_Services\SCM_week_24-26 (2024)
==============================================================================================================================*/

ALTER TABLE Inventory.ATC ADD TotalSubstanceConcentration VARCHAR(50) NULL

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Totalizado de concentración de sustancias de medicamentos combinados' , @level0type=N'SCHEMA',@level0name=N'Inventory', @level1type=N'TABLE',@level1name=N'ATC', @level2type=N'COLUMN',@level2name=N'TotalSubstanceConcentration'
GO

UPDATE Inventory.ATC SET TotalSubstanceConcentration = CAST(ATC.concentrationquantity AS VARCHAR(50)) + ' ' + imu.Abbreviation
FROM Inventory.ATC atc
INNER JOIN Inventory.InventoryMeasurementUnit imu ON atc.ConcentrationMeasureUnitId = imu.Id
WHERE atc.Combined = 1

/*==============================================================================================================================*/
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
		

/*==============================================================================================================================
	Author: Julieth Cardenas
	BUG : 19106
	Sprint : ERP_Services\VF_week_30-32 (2024)
	==============================================================================================================================*/
CREATE TRIGGER [Treasury].[tgg_VoucherTransaction_ValidateMainAccount]
   ON  [Treasury].[VoucherTransaction]
   AFTER  INSERT,UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	
	IF EXISTS (
	SELECT  1
	FROM INSERTED vt
	JOIN Treasury.EntityBankAccounts eba ON eba.Id = vt.IdEntityBankAccount
	WHERE vt.IdMainAccount <> eba.IdMainAccount
	)
		BEGIN
		THROW 51000, 'Error generado por control desde trigger. La cuenta contable de la cuenta bancaria no es igual a la que esta parametrizada.', 1
	END


	IF EXISTS (
	SELECT  1
	FROM INSERTED vt
	JOIN Treasury.CashRegisters cr ON  cr.id = vt.IdCashRegister
	WHERE vt.IdMainAccount <> cr.IdMainAccount
	)
		BEGIN
		THROW 51000, 'Error generado por control desde trigger. La cuenta contable de la caja no es igual a la que esta parametrizada.', 1
	END



END
GO
/*==============================================================================================================================
	Author: Felix Camilo Salazar Roldan
	BUG : 20128
	Sprint : ERP_Services\VF_week_36-38 (2024)
==============================================================================================================================*/

UPDATE 
	 Contract.CareGroup
SET 
	LiquidationType = 2
WHERE 
	LiquidationType IN (3,4)	

/*==============================================================================================================================
	Author: Juan Pablo Daza Medina 
	BUG : 20128
	Sprint : ERP_Services\VF_week_42-44 (2024)
==============================================================================================================================*/

ALTER TABLE Payroll.Liquidation
ALTER COLUMN BankAccountNumber VARCHAR(30);
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
/*==============================================================================================================================
==============================================================================================================================*/

/*==============================================================================================================================
	Author: Oscar steven Astudillo
	BUG : #20970
	Sprint : SCM_week_40-42 (2024)
==============================================================================================================================*/
CREATE TRIGGER tgg_ValidateEmail
ON [Common].[Email]
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    -- Validar que no se inserten o actualicen valores que contengan espacios
    IF EXISTS (
        SELECT *
        FROM inserted
        WHERE Email LIKE '% %'               -- Espacios en cualquier parte
           OR Email <> LTRIM(RTRIM(Email))   -- Espacios al inicio o al final
    )
    BEGIN
        RAISERROR('El valor de Email no puede contener espacios al inicio, al final o en ninguna parte.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END


    IF EXISTS (SELECT 1 FROM inserted WHERE Id  > 0 AND Id IS NOT NULL )
    BEGIN
        -- Manejar update
        UPDATE e
        SET 
			e.IdPerson = i.IdPerson,
            e.Email = i.Email,
            e.State = i.State,
            e.Synchronized = i.Synchronized,
            e.Type = i.Type
        FROM [Common].[Email] e
        INNER JOIN inserted i ON e.Id = i.Id;
    END
    ELSE
    BEGIN
        -- Manejo de Insert
        INSERT INTO [Common].[Email] (IdPerson, Email, State, Synchronized, Type)
        SELECT IdPerson, Email, State, Synchronized, Type
        FROM inserted WHERE (Id IS NULL OR Id = 0);
    END
END;
/*==============================================================================================================================
	Author: Juan Pablo Daza Medina 
	Pbi : #20932
	Sprint : SCM_week_42-44 (2024)
==============================================================================================================================*/
ALTER TABLE Payroll.Employee
ADD RemainingVacationDays INT NOT NULL DEFAULT 0;


/*==============================================================================================================================
	Author: Julieth Angélica Cárdenas Gómez 
	Pbi : #21015
	Sprint : SCM_week_42-46 (2024)
==============================================================================================================================*/

Drop table Common.ThirdParty2,	
 Cost.CostDistributionBase2,
 Cost.CostDistributionBaseDetail2,		
 Cost.CostGeneralExpense2,		
 Cost.CostOrganizationalStructureOfCosts2,	
 Cost.CostProductionCenter2,	
 Cost.CostProductionCenterCostCenter2,		
 Cost.CostProductionCenterHomologation2,			
 dbo.tmpCUPS_QX_CAC_HOMI,	
 HumanTalent.PVReportMe, 			
 Payments.AccountPayableConcepts2,		
 Payments.PruebaA,		
 Payroll.CostDistribution2,		
 Report.TABLAINVENTARIOS1,		
 Common.PruebaDiscountDetail,		
 Common.PruebaDiscountHeader,		
 Common.RG_Recovery_1_Supplier,		
 dbo.SALDOSGLOSAS_CORREGIRSALDOCARTERA,			
 Payroll.Fondos_Contratos777,		
 Glosas.ConciliationTemp
	
DROP VIEW [Authorization].ViewListRequests_CTE22,		
Billing.ViewListRevenueControl_Daniel,		
GeneralLedger.ViewHomologationAccount2,		
Payroll.VistaLetra2,		
Treasury.VReportTreasuryNewsletterCash2,		
ViewInternal.VistaRadicacionDetallada2,		
dbo.tmpCausationInvoice,		
ViewInternal.FURIPS2	

DROP PROCEDURE Billing.SP_ConfirmBasicBilling_1,			
Billing.SP_GenerateJournalVoucherByBasicBilling_1,			
Billing.SP_GenerateJournalVoucherDetailsPackage_1,			
Billing.SP_GetInvoiceDetailsByInvoiceId1,			
Billing.SP_ListarFacturasPruebas,			
dbo.ESE_SP_Facturacion_FacturacionDetallada_1,			
Glosas.SP_NumberConsecutivo_2,			
Inventory.SP_GeneratePharmaceuticalDispensing_1,			
Inventory.SP_GeneratePharmaceuticalDispensing1,		
Inventory.SP_SavePhysicalInventoryKardexTmp,					
Inventory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission2,			
MixingStation.SP_CMCenterLineUnit1,
MixingStation.SP_ProductListCampaingCapera,
MixingStation.SP_ProductListCampaingOscar

DROP FUNCTION Contract.GetHomologationCups_1, MixingStation.CalculationQuantityMaterialRaw_1

/*==============================================================================================================================
	Author: Juan Pablo Daza Medina
	Bug : #22008
	Sprint : SCM_week_44-46 (2024)
==============================================================================================================================*/
--Se hace estos cambios porque la tabla de ingreso de activos sus campos estaban sin redondeo
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN FreightIVAValue Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN [Value] Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN ValueDiscount Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN ValueTax Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN WithholdingTax Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN WithholdingICA Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN RetentionSource Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN RetentionOther Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN DeductionOther Decimal(20,2);
ALTER TABLE FixedAsset.FixedAssetEntry ALTER COLUMN TotalValue Decimal(20,2);

--SE ELIMINARON MANUAL
--Invetory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission_1
--Invenotry.SP_SavePhysicalInventoryKardex_CRISTIAN,	



/*==============================================================================================================================
	Author: Oscar Astudillo reyes
	Pbi : #21259
	Sprint : ERP_Services\VF_week_42-44 (2024)
==============================================================================================================================*/
ALTER TABLE Common.EconomicActivity
ADD IsIncomeGenerating BIT NOT NULL DEFAULT 0;


/*==============================================================================================================================
	Author: Julieth Angélica Cárdenas Gómez
	Bug : #21488
	Sprint : ERP_Services\SCM_week_42-44 (2024)
==============================================================================================================================*/

ALTER TABLE Inventory.PharmaceuticalDispensingNotes 
DROP CONSTRAINT DF__Pharmaceu__Admis__6B2C1091;


ALTER TABLE Inventory.PharmaceuticalDispensingNotes 
ALTER COLUMN AdmissionNumber  CHAR (10)  NOT NULL

ALTER TABLE [Inventory].[PharmaceuticalDispensingNotes] 
ADD  CONSTRAINT [DF__Pharmaceu__Admis__6B2C1091]  DEFAULT ((1)) FOR [AdmissionNumber]


/*==============================================================================================================================
	Author: Oscar stiven Astudillo reyes
	Bug : #21190
	Sprint : ERP_Services\Sprint_week_42-44 (2024)
==============================================================================================================================*/


ALTER TABLE Security.UserConfiguration
ADD ReportPathType TINYINT NOT NULL DEFAULT  1;

ALTER TABLE Security.UserOperatingUnit
ADD ReportPath VARCHAR(200);

/*==============================================================================================================================
	Author: Ariadna Sophia Cabrera Carrera
	PBI : #22221
	Sprint : ERP_Services\Sprint_week_2-4 (2025)
==============================================================================================================================*/

BEGIN TRAN t123
BEGIN TRY

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

COMMIT TRAN t123

END TRY
BEGIN CATCH

SELECT ERROR_MESSAGE() AS MESSAGE
ROLLBACK TRAN t123

END CATCH

/*==============================================================================================================================
	Author: Julieth Angelica Cardenas Gomez                                                                          
	Bug : #23636
	Sprint : ERP_Services\Sprint_week_2-4 (2025)
==============================================================================================================================*/
--Se ejecuta en el contenedor correspondiente.
alter table  [security].[User] 
alter column Position VARCHAR(150)

/*==============================================================================================================================
	Author: Ariadna Sophia Cabrera Carrera                                                                        
	Bug : #23929
	Sprint : ERP_Services\VF_week_4-6 (2025))
==============================================================================================================================*/

BEGIN TRAN t123
BEGIN TRY


ALTER TABLE [FixedAsset].[SettingFixedAsset]
ALTER COLUMN [IvaCost] BIT NULL

ALTER TABLE [FixedAsset].[SettingFixedAsset]
DROP CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts2]

ALTER TABLE [FixedAsset].[SettingFixedAsset]
ALTER COLUMN [IVAAccountPayableConceptId] INT NULL

ALTER TABLE [FixedAsset].[SettingFixedAsset]
WITH CHECK ADD CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts2] FOREIGN KEY ([IVAAccountPayableConceptId])
REFERENCES [Payments].[AccountPayableConcepts] ([Id])


COMMIT TRAN t123

END TRY
BEGIN CATCH

SELECT ERROR_MESSAGE() AS MESSAGE
ROLLBACK TRAN t123

END CATCH

/*==============================================================================================================================
	Author: Felix Camilo Salazar Roldan
	PBI : #23929
	Sprint : ERP_Services\VF_week_6-8 (2025))
==============================================================================================================================*/

ALTER TABLE Payroll.Liquidation
ADD DependentsSupplementary DECIMAL(18,2) CONSTRAINT DF_Liquidation_DependentsSupplementary DEFAULT(0) null

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Campo que almacena la renta complementaria' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'Liquidation', @level2type=N'COLUMN',@level2name=N'DependentsSupplementary'
GO

/*==============================================================================================================================
	Author: Ariadna Sophia Cabrera Carrera                                                                        
	PBI : #23092
	Sprint : ERP_Services\HCM_week_6-8 (2025)
==============================================================================================================================*/

BEGIN TRAN t123
BEGIN TRY

INSERT INTO Security.FormAction (IdForm, IdAction)
	SELECT f.Id, a.Id
	FROM Security.Form f
	CROSS JOIN Security.Action a
	WHERE f.Name = 'Talento Humano'
	AND (a.Name = 'Visualizar Reporte' OR a.Name = 'Imprimir Reporte')

COMMIT TRAN t123

END TRY
BEGIN CATCH

ROLLBACK TRAN t123

END CATCH

/*==============================================================================================================================
Author: Ariadna Sophia Cabrera Carrera 
	PBI : 23859
	Sprint : ERP_Services\SCM_week_14-16 (2025)
==============================================================================================================================*/

--Modificaciones realizadas para CIMA en la base INDIGOSEC 999

BEGIN TRAN t123
BEGIN TRY

DECLARE @IdForm INT 
SET @IdForm = (SELECT ISNULL(MAX(Id), 0) + 1 FROM Security.Form)

INSERT INTO Security.Form (Id, Name, PrintEvents, HasSequence, IsNativeForm,
		HasForm, ClassName, AssemblyName, HandlesMassiveConfirm, SequenceModule, State) 
	VALUES (@IdForm, 'Informe de Renta', 'SUCA', 0, 1, 1, 
		'FrmReportIncomeTax', 'Presentation.Payroll', 0, 'Payroll', 1)

INSERT INTO Security.ModuleForm (IdModule, IdTitle, IdForm, FormOrder)
	VALUES(53, 4, @IdForm, 1)

SELECT * FROM Security.Form ORDER BY Id DESC
SELECT *FROM Security.ModuleForm ORDER BY Id DESC

COMMIT TRAN t123

END TRY
BEGIN CATCH

SELECT ERROR_MESSAGE() AS MESSAGE
ROLLBACK TRAN t123

END CATCH



BEGIN TRAN t234
BEGIN TRY

DECLARE @NewFormId INT
SET @NewFormId = (SELECT Id FROM Security.Form WHERE Name = 'Informe de Renta');

INSERT INTO Security.FormAction (IdForm, IdAction)
SELECT 
    @NewFormId AS IdForm,
    IdAction
FROM Security.FormAction
WHERE IdForm = 2193;

SELECT * FROM Security.FormAction WHERE IdForm = @NewFormId

COMMIT TRAN t234

END TRY
BEGIN CATCH

SELECT ERROR_MESSAGE() AS MESSAGE
ROLLBACK TRAN t234

END CATCH

/*==============================================================================================================================
Author: Juan Pablo Daza Medina  
	PBI : 7989
	Sprint : ERP_Services\SCM_week_14-16 (2025)
==============================================================================================================================*/

INSERT INTO Security.Form (Id, Name, PrintEvents, HasSequence, IsNativeForm,
		HasForm, ClassName, AssemblyName, HandlesMassiveConfirm, SequenceModule, State) 
	VALUES (@IdForm, 'Reporte de vacaciones', 'SUCA', 0, 1, 1, 
		'FrmReportVacation', 'Presentation.Payroll', 0, 'Payroll', 1)

INSERT INTO Security.ModuleForm (IdModule, IdTitle, IdForm, FormOrder)
	VALUES(53, 4, 2855, 1)

INSERT INTO Security.FormAction(2855, 11)
INSERT INTO Security.FormAction(2855, 22)
INSERT INTO Security.FormAction(2855, 23)
INSERT INTO Security.FormAction(2855, 24)
INSERT INTO Security.FormAction(2855, 25)
INSERT INTO Security.FormAction(2855, 41)
INSERT INTO Security.FormAction(2855, 70)
INSERT INTO Security.FormAction(2855, 114)

/*==============================================================================================================================
Author: Ariadna Sophia Cabrera Carrera  
	PBI : 26378
	Sprint : ERP_Services\Sprint_week_18-20 (2025)
==============================================================================================================================*/

BEGIN TRAN t123
BEGIN TRY


ALTER TABLE Billing.SettingsBilling
ADD ValidateAgeOfMajority BIT NOT NULL DEFAULT 0;

SELECT * FROM Billing.SettingsBilling

COMMIT TRAN t123

END TRY
BEGIN CATCH

SELECT ERROR_MESSAGE() AS MESSAGE
ROLLBACK TRAN t123

END CATCH

/*==============================================================================================================================
Author: Andrea Pahola Coqueco Cuellar  
	Work Item : Bug #30535
	Sprint : ERP_Services\Sprint_week_36-37 (2025)
==============================================================================================================================*/

INSERT INTO [Billing].[InvoiceEntityCapitatedGroupers]
	(InvoiceEntityCapitatedId, GroupersId, Code, Description, UserMin, UserMax, ProjectCME, TotalContract)
SELECT
	iec.Id, g.Id, g.Code, g.Description, g.UserMin, g.UserMax, g.ProjectCME, iec.TotalValue + iec.DiscountValue TotalContract
FROM Billing.InvoiceEntityCapitated iec
JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id AND cg.LiquidationType = 5
CROSS APPLY (
	SELECT TOP 1
		g.Id, g.Code, g.Description, g.UserMin, g.UserMax, g.ProjectCME, g.CreationDate,
		IIF(iec.CreationDate > g.CreationDate, 1, 2) condicion, gcg.DocumentDate
	FROM [Contract].[GroupersCareGroup] gcg
	JOIN [Contract].[Groupers] g ON gcg.GroupersId = g.Id
	WHERE iec.CareGroupId = gcg.CareGroupId
	ORDER BY
		IIF(iec.CreationDate > g.CreationDate, 1, 2),
		g.CreationDate DESC
) g
WHERE iec.Status >= 2 AND iec.Status <> 3

/*==============================================================================================================================
Author: Felix Camilo Salazar Roldan 
	Work Item : Bug #33631
	Sprint : ERP_Services (2026)
==============================================================================================================================*/
ALTER TABLE [Contract].[ContractDetail]
ALTER COLUMN [ContractNumber] VARCHAR(100) NOT NULL;