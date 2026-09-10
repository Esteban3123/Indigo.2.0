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
Author: Oscar astudillo reyes
Work Item : Bug #37053
Sprint : ERP_Services\Sprint Week 22 - 23 (2026)
==============================================================================================================================*/
-- NoveltyLineType (7 tipos de línea)
SET IDENTITY_INSERT [Payroll].[NoveltyLineType] ON;
INSERT INTO [Payroll].[NoveltyLineType] ([Id],[Code],[Name],[State],[CreationUser],[CreationDate]) VALUES
(1,'SALARIO','Salario',                                        1,'SYSTEM',GETDATE()),
(2,'SLN',    'Suspensión/Licencia No Remunerada/Sanción',      1,'SYSTEM',GETDATE()),
(3,'IGE',    'Incapacidad General por Enfermedad',             1,'SYSTEM',GETDATE()),
(4,'LMA',    'Licencia de Maternidad o Paternidad',            1,'SYSTEM',GETDATE()),
(5,'VAC_LR', 'Vacaciones o Licencia Remunerada',               1,'SYSTEM',GETDATE()),
(6,'VCT',    'Variación de Centro de Trabajo',                 1,'SYSTEM',GETDATE()),
(7,'IRL',    'Incapacidad por Riesgo Laboral',                 1,'SYSTEM',GETDATE());
SET IDENTITY_INSERT [Payroll].[NoveltyLineType] OFF;

-- ConceptClass (72 clases)
SET IDENTITY_INSERT [Payroll].[ConceptClass] ON;
INSERT INTO [Payroll].[ConceptClass] (Id,Code,Description,State,CreationUser,CreationDate) VALUES
( 1,'001','Hora Extra Ordinaria Diurna',                        1,'SYSTEM',GETDATE()),
( 2,'002','Primas de Servicios',                                1,'SYSTEM',GETDATE()),
( 3,'003','Otras Primas',                                       1,'SYSTEM',GETDATE()),
( 4,'004','Bonificación por Servicios',                         1,'SYSTEM',GETDATE()),
( 5,'005','Sueldo',                                             1,'SYSTEM',GETDATE()),
( 6,'006','Auxilio de Transporte',                              1,'SYSTEM',GETDATE()),
( 7,'007','Indemnizaciones',                                    1,'SYSTEM',GETDATE()),
( 8,'008','Provisión Cesantías',                                1,'SYSTEM',GETDATE()),
( 9,'009','Aporte Riesgos Profesionales',                       1,'SYSTEM',GETDATE()),
(10,'010','Otros Devengados',                                   1,'SYSTEM',GETDATE()),
(11,'011','Otros Deducidos',                                    1,'SYSTEM',GETDATE()),
(12,'012','Hora Extra Ordinaria Nocturna',                      1,'SYSTEM',GETDATE()),
(13,'013','Hora Extra Dominical Diurna',                        1,'SYSTEM',GETDATE()),
(14,'014','Pensión Empleado',                                   1,'SYSTEM',GETDATE()),
(15,'015','Pensión Patrono',                                    1,'SYSTEM',GETDATE()),
(16,'016','Pensión Voluntaria',                                 1,'SYSTEM',GETDATE()),
(17,'017','Salud Empleado',                                     1,'SYSTEM',GETDATE()),
(18,'018','Salud Patrono',                                      1,'SYSTEM',GETDATE()),
(19,'019','Salud Voluntaria',                                   1,'SYSTEM',GETDATE()),
(20,'020','Retención',                                          1,'SYSTEM',GETDATE()),
(21,'021','Incapacidad Ambulatoria',                            1,'SYSTEM',GETDATE()),
(22,'022','Incapacidad Hospitalaria',                           1,'SYSTEM',GETDATE()),
(23,'023','Maternidad',                                         1,'SYSTEM',GETDATE()),
(24,'024','Licencias',                                          1,'SYSTEM',GETDATE()),
(25,'025','Licencia No Remunerada',                             1,'SYSTEM',GETDATE()),
(26,'026','Sanción',                                            1,'SYSTEM',GETDATE()),
(27,'027','Incapacidad Riesgos Profesionales',                  1,'SYSTEM',GETDATE()),
(28,'028','Permisos',                                           1,'SYSTEM',GETDATE()),
(29,'030','Vacaciones',                                         1,'SYSTEM',GETDATE()),
(30,'031','Provisión Vacaciones',                               1,'SYSTEM',GETDATE()),
(31,'033','Provisión Primas',                                   1,'SYSTEM',GETDATE()),
(32,'034','Provisión Interes de Cesantías',                     1,'SYSTEM',GETDATE()),
(33,'035','Parafiscal Sena',                                    1,'SYSTEM',GETDATE()),
(34,'036','Parafiscal Caja',                                    1,'SYSTEM',GETDATE()),
(35,'037','Parafiscal ICBF',                                    1,'SYSTEM',GETDATE()),
(36,'038','Aporte Fondo Seguridad Pensional',                   1,'SYSTEM',GETDATE()),
(37,'041','Convenios',                                          1,'SYSTEM',GETDATE()),
(38,'042','Recargo Nocturno Normal',                            1,'SYSTEM',GETDATE()),
(39,'043','Recargo Nocturno Festivo',                           1,'SYSTEM',GETDATE()),
(40,'044','Sindicato',                                          1,'SYSTEM',GETDATE()),
(41,'045','Cuentas AFC',                                        1,'SYSTEM',GETDATE()),
(42,'046','Bonificacion por Año de Servicio',                   1,'SYSTEM',GETDATE()),
(43,'047','Gastos de Representación',                           1,'SYSTEM',GETDATE()),
(44,'048','Ajuste Retención en la Fuente',                      1,'SYSTEM',GETDATE()),
(45,'049','Prima de Vacaciones',                                1,'SYSTEM',GETDATE()),
(46,'050','Hora Extra Dominical Nocturna',                      1,'SYSTEM',GETDATE()),
(47,'051','Recargo Diurno Festivo',                             1,'SYSTEM',GETDATE()),
(48,'052','Recargo Diurno Dominical',                           1,'SYSTEM',GETDATE()),
(49,'053','Embargos',                                           1,'SYSTEM',GETDATE()),
(50,'054','Viaticos',                                           1,'SYSTEM',GETDATE()),
(51,'055','Bonificación Salarial',                              1,'SYSTEM',GETDATE()),
(52,'056','Provision Bonificacion Año Servicio',                1,'SYSTEM',GETDATE()),
(53,'057','Provision Incremento Vacacional',                    1,'SYSTEM',GETDATE()),
(54,'058','Provision Bonificacion Especial Recreacion',         1,'SYSTEM',GETDATE()),
(55,'059','Provision Prima Vacaciones',                         1,'SYSTEM',GETDATE()),
(56,'060','Provisión Prima Navidad',                            1,'SYSTEM',GETDATE()),
(57,'061','Retención de Indemnización',                         1,'SYSTEM',GETDATE()),
(58,'062','Auxilio de Alimentos',                               1,'SYSTEM',GETDATE()),
(59,'063','Bonificación Especial para Recreación',              1,'SYSTEM',GETDATE()),
(60,'064','Incremento Vacacional',                              1,'SYSTEM',GETDATE()),
(61,'065','Bonificacion No Salarial',                           1,'SYSTEM',GETDATE()),
(62,'066','Apoyo a Sostenimiento',                              1,'SYSTEM',GETDATE()),
(63,'067','Incapacidad Ambulatoria Patrono',                    1,'SYSTEM',GETDATE()),
(64,'068','Incapacidad Ambulatoria ERP',                        1,'SYSTEM',GETDATE()),
(65,'069','Incapacidad Hospitalaria Patrono',                   1,'SYSTEM',GETDATE()),
(66,'070','Incapacidad Hospitalaria ERP',                       1,'SYSTEM',GETDATE()),
(67,'071','Calamidad domestica',                                1,'SYSTEM',GETDATE()),
(68,'072','Licencia Remunerada',                                1,'SYSTEM',GETDATE()),
(69,'073','Vacaciones en Dinero',                               1,'SYSTEM',GETDATE()),
(70,'074','Licencia por Luto',                                  1,'SYSTEM',GETDATE()),
(71,'075','Incapacidad Riesgos Profesionales - Patrono',        1,'SYSTEM',GETDATE()),
(72,'076','Incapacidad Riesgos Profesionales - ARL',            1,'SYSTEM',GETDATE());
SET IDENTITY_INSERT [Payroll].[ConceptClass] OFF;

-- ContributorType (44 tipos — reemplaza datos anteriores)
DELETE FROM [Payroll].[ContributorType];
SET IDENTITY_INSERT [Payroll].[ContributorType] ON;
INSERT INTO [Payroll].[ContributorType] ([Id],[Code],[Name],[State],[CreationUser],[CreationDate]) VALUES
( 1,'01','Dependiente',                                                                          1,'SYSTEM',GETDATE()),
( 2,'02','Servicio doméstico',                                                                   1,'SYSTEM',GETDATE()),
( 3,'03','Independiente',                                                                        1,'SYSTEM',GETDATE()),
( 4,'04','Madre sustituta',                                                                      1,'SYSTEM',GETDATE()),
( 5,'12','Aprendices en etapa lectiva',                                                          1,'SYSTEM',GETDATE()),
( 6,'16','Independiente agremiado o asociado',                                                   1,'SYSTEM',GETDATE()),
( 7,'18','Funcionarios públicos sin tope máximo en el IBC',                                      1,'SYSTEM',GETDATE()),
( 8,'19','Aprendices en etapa productiva',                                                       1,'SYSTEM',GETDATE()),
( 9,'20','Estudiantes (Régimen especial - Ley 789/2002)',                                        1,'SYSTEM',GETDATE()),
(10,'21','Estudiantes de posgrado en salud y residentes',                                        1,'SYSTEM',GETDATE()),
(11,'22','Profesor de establecimiento particular',                                               1,'SYSTEM',GETDATE()),
(12,'23','Estudiantes aporte solo riesgos laborales',                                            1,'SYSTEM',GETDATE()),
(13,'30','Dependiente entidades o universidades públicas regímenes especial y de excepción',     1,'SYSTEM',GETDATE()),
(14,'31','Cooperados o precooperativas de trabajo asociado',                                     1,'SYSTEM',GETDATE()),
(15,'32','Cotizante miembro carrera diplomática o consular / organismo multilateral',            1,'SYSTEM',GETDATE()),
(16,'33','Beneficiario del Fondo de Solidaridad Pensional',                                     1,'SYSTEM',GETDATE()),
(17,'34','Concejal o edil JAL Bogotá amparado por póliza de salud',                             1,'SYSTEM',GETDATE()),
(18,'35','Concejal municipal o distrital',                                                      1,'SYSTEM',GETDATE()),
(19,'36','Edil JAL beneficiario del Fondo de Solidaridad Pensional',                            1,'SYSTEM',GETDATE()),
(20,'40','Beneficiario UPC Adicional',                                                           1,'SYSTEM',GETDATE()),
(21,'42','Cotizante independiente pago solo salud',                                              1,'SYSTEM',GETDATE()),
(22,'43','Cotizante a pensiones con pago por tercero',                                           1,'SYSTEM',GETDATE()),
(23,'44','Cotizante dependiente empleo de emergencia duración >= 1 mes',                         1,'SYSTEM',GETDATE()),
(24,'45','Cotizante dependiente empleo de emergencia duración < 1 mes',                          1,'SYSTEM',GETDATE()),
(25,'47','Trabajador dependiente entidad beneficiaria SGP — Aportes Patronales',                 1,'SYSTEM',GETDATE()),
(26,'51','Trabajador de tiempo parcial',                                                         1,'SYSTEM',GETDATE()),
(27,'52','Beneficiario del mecanismo de protección al cesante',                                  1,'SYSTEM',GETDATE()),
(28,'53','Afiliado partícipe',                                                                   1,'SYSTEM',GETDATE()),
(29,'54','Prepensionado de entidad en liquidación',                                              1,'SYSTEM',GETDATE()),
(30,'55','Afiliado partícipe — Dependiente',                                                     1,'SYSTEM',GETDATE()),
(31,'56','Prepensionado con aporte voluntario a salud',                                          1,'SYSTEM',GETDATE()),
(32,'57','Independiente voluntario al Sistema General de Riesgos Laborales',                     1,'SYSTEM',GETDATE()),
(33,'58','Estudiantes de prácticas laborales en el sector público',                              1,'SYSTEM',GETDATE()),
(34,'59','Independiente con contrato de prestación de servicios superior a 1 mes',               1,'SYSTEM',GETDATE()),
(35,'60','Edil JAL no beneficiario del Fondo de Solidaridad Pensional',                          1,'SYSTEM',GETDATE()),
(36,'61','Beneficiario programa de reincorporación',                                             1,'SYSTEM',GETDATE()),
(37,'62','Personal del Magisterio',                                                              1,'SYSTEM',GETDATE()),
(38,'63','Beneficiario de Prestación humanitaria',                                               1,'SYSTEM',GETDATE()),
(39,'64','Trabajador penitenciario',                                                             1,'SYSTEM',GETDATE()),
(40,'65','Dependiente vinculado al Piso de Protección Social',                                   1,'SYSTEM',GETDATE()),
(41,'66','Independiente vinculado al Piso de Protección Social',                                 1,'SYSTEM',GETDATE()),
(42,'67','Voluntario en Primera Respuesta aporte solo al Sistema de Riesgos Laborales',          1,'SYSTEM',GETDATE()),
(43,'68','Dependiente Veterano de la Fuerza Pública',                                            1,'SYSTEM',GETDATE()),
(44,'69','Contribuyente solidario',                                                              1,'SYSTEM',GETDATE());
SET IDENTITY_INSERT [Payroll].[ContributorType] OFF;

-- ContributorSubtype (11 subtipos — reemplaza datos anteriores)
DELETE FROM [Payroll].[ContributorSubType];
SET IDENTITY_INSERT [Payroll].[ContributorSubType] ON;
INSERT INTO [Payroll].[ContributorSubType] (Id,Code,Name,CreationUser,CreationDate) VALUES
( 1,'00','No aplica subtipo de cotizante',                                  'SYSTEM',GETDATE()),
( 2,'01','Dependiente pensionado activo',                                   'SYSTEM',GETDATE()),
( 3,'02','Independiente pensionado activo',                                 'SYSTEM',GETDATE()),
( 4,'03','No obligado a cotizar a pensiones por edad',                      'SYSTEM',GETDATE()),
( 5,'04','Requisitos cumplidos para pensión mínima o indemnización',        'SYSTEM',GETDATE()),
( 6,'05','Indemnización sustitutiva o devolución de saldos reconocida',     'SYSTEM',GETDATE()),
( 7,'06','Perteneciente a régimen exceptuado de pensiones',                 'SYSTEM',GETDATE()),
( 8,'09','Pensionado con mesada igual o superior a 25 SMLMV',               'SYSTEM',GETDATE()),
( 9,'10','Residente en exterior afiliado voluntario a pensiones',           'SYSTEM',GETDATE()),
(10,'11','Conductor de taxi servicio público',                              'SYSTEM',GETDATE()),
(11,'12','Conductor de taxi - no obligado a cotizar pensión',               'SYSTEM',GETDATE());
SET IDENTITY_INSERT [Payroll].[ContributorSubType] OFF;

-- PilaNoveltyType (16 tipos AT2)
SET IDENTITY_INSERT [Payroll].[PilaNoveltyType] ON;
INSERT INTO [Payroll].[PilaNoveltyType] (Id,Code,Description,PilaField,State,CreationUser,CreationDate) VALUES
( 1,'ING',   'Ingreso al cotizante',                       15,1,'SYSTEM',GETDATE()),
( 2,'RET',   'Retiro del cotizante',                       16,1,'SYSTEM',GETDATE()),
( 3,'TDE',   'Traslado desde EPS (entrada)',               17,1,'SYSTEM',GETDATE()),
( 4,'TAE',   'Traslado a EPS (salida)',                    18,1,'SYSTEM',GETDATE()),
( 5,'TDP',   'Traslado desde AFP (entrada)',               19,1,'SYSTEM',GETDATE()),
( 6,'TAP',   'Traslado a AFP (salida)',                    20,1,'SYSTEM',GETDATE()),
( 7,'VSP',   'Variación permanente de salario',            21,1,'SYSTEM',GETDATE()),
( 8,'Corr',  'Correcciones',                               22,1,'SYSTEM',GETDATE()),
( 9,'VST',   'Variación transitoria de salario',           23,1,'SYSTEM',GETDATE()),
(10,'SLN',   'Suspensión/Licencia No Remunerada/Sanción',  24,1,'SYSTEM',GETDATE()),
(11,'IGE',   'Incapacidad General por Enfermedad',         25,1,'SYSTEM',GETDATE()),
(12,'LMA',   'Licencia de Maternidad o Paternidad',        26,1,'SYSTEM',GETDATE()),
(13,'VAC-LR','Vacaciones o Licencia Remunerada',           27,1,'SYSTEM',GETDATE()),
(14,'AVP',   'Aporte Voluntario a Pensión',                28,1,'SYSTEM',GETDATE()),
(15,'VCT',   'Variación de Centro de Trabajo',             29,1,'SYSTEM',GETDATE()),
(16,'IRL',   'Incapacidad por Riesgo Laboral (días)',       30,1,'SYSTEM',GETDATE());
SET IDENTITY_INSERT [Payroll].[PilaNoveltyType] OFF;

-- ConceptClassNoveltyLineType — Salary / roles base (SALARIO)
INSERT INTO [Payroll].[ConceptClassNoveltyLineType]
    (ConceptClassId, NoveltyLineTypeId, Role, State, CreationUser, CreationDate)
SELECT cc.Id, nlt.Id, v.Role, 1, 'SYSTEM', GETDATE()
FROM (VALUES
    ('005','Salary'),('014','Pension'),('015','Pension'),
    ('017','Health'),('018','Health'),('009','OccupationalRisk'),
    ('036','CompensationFund'),('035','Sena'),('037','Icbf'),('038','Fsp'),
    ('001','TransitorySalary'),('004','TransitorySalary'),('010','TransitorySalary'),
    ('011','TransitorySalary'),('012','TransitorySalary'),('013','TransitorySalary'),
    ('042','TransitorySalary'),('043','TransitorySalary'),('046','TransitorySalary'),
    ('047','TransitorySalary'),('050','TransitorySalary'),('051','TransitorySalary'),
    ('052','TransitorySalary'),('054','TransitorySalary'),('055','TransitorySalary')
) AS v(ClassCode, Role)
INNER JOIN [Payroll].[ConceptClass]    cc  ON cc.Code  = v.ClassCode
INNER JOIN [Payroll].[NoveltyLineType] nlt ON nlt.Code = 'SALARIO';

-- ConceptClassNoveltyLineType — clase 066 / Salary / SALARIO
IF NOT EXISTS (
    SELECT 1 FROM Payroll.ConceptClassNoveltyLineType
    WHERE ConceptClassId = 62 AND NoveltyLineTypeId = 1 AND Role = 'Salary'
)
    INSERT INTO Payroll.ConceptClassNoveltyLineType
        (ConceptClassId, NoveltyLineTypeId, Role, State, CreationUser, CreationDate)
    VALUES (62, 1, 'Salary', 1, 'PILA_FIX_2026-05-27', GETDATE());

-- ConceptClassNoveltyLineType — ContractDetail (SLN / IGE / LMA / VAC_LR / IRL)
INSERT INTO [Payroll].[ConceptClassNoveltyLineType]
    (ConceptClassId, NoveltyLineTypeId, Role, State, CreationUser, CreationDate)
SELECT cc.Id, nlt.Id, 'ContractDetail', 1, 'SYSTEM', GETDATE()
FROM (VALUES ('005'),('001'),('004'),('010'),('011'),('012'),('013'),
             ('042'),('043'),('046'),('047'),('050'),('051'),('052'),('054'),('055')
) AS c(ClassCode)
INNER JOIN [Payroll].[ConceptClass] cc ON cc.Code = c.ClassCode
CROSS JOIN (VALUES ('SLN'),('IGE'),('LMA'),('VAC_LR'),('IRL')) AS l(LineTypeCode)
INNER JOIN [Payroll].[NoveltyLineType] nlt ON nlt.Code = l.LineTypeCode;

-- ConceptClassNoveltyLineType — VacationSalary (IBC CCF/SENA/ICBF en VAC_LR)
INSERT INTO [Payroll].[ConceptClassNoveltyLineType]
    (ConceptClassId, NoveltyLineTypeId, Role, State, CreationUser, CreationDate)
SELECT cc.Id, nlt.Id, 'VacationSalary', 1, 'SYSTEM', GETDATE()
FROM (VALUES ('030'),('071'),('072'),('073'),('074')) AS v(ClassCode)
INNER JOIN [Payroll].[ConceptClass]    cc  ON cc.Code  = v.ClassCode
INNER JOIN [Payroll].[NoveltyLineType] nlt ON nlt.Code = 'VAC_LR';

-- ConceptClassNoveltyLineType — VacationX (Campo 27 = X)
INSERT INTO [Payroll].[ConceptClassNoveltyLineType]
    (ConceptClassId, NoveltyLineTypeId, Role, State, CreationUser, CreationDate)
SELECT cc.Id, nlt.Id, 'VacationX', 1, 'SYSTEM', GETDATE()
FROM (VALUES ('030'),('073')) AS v(ClassCode)
INNER JOIN [Payroll].[ConceptClass]    cc  ON cc.Code  = v.ClassCode
INNER JOIN [Payroll].[NoveltyLineType] nlt ON nlt.Code = 'VAC_LR';

-- ConceptClassNoveltyLineType — AvpContribution (clase 016, Campo 28)
INSERT INTO [Payroll].[ConceptClassNoveltyLineType]
    (ConceptClassId, NoveltyLineTypeId, Role, State, CreationUser, CreationDate)
SELECT cc.Id, nlt.Id, 'AvpContribution', 1, 'SYSTEM', GETDATE()
FROM [Payroll].[ConceptClass]          cc
INNER JOIN [Payroll].[NoveltyLineType] nlt ON nlt.Code = 'SALARIO'
WHERE cc.Code = '016';

-- ContributorTypePilaNovelty (Tabla 16 AT2)
INSERT INTO [Payroll].[ContributorTypePilaNovelty]
    (ContributorTypeId, PilaNoveltyTypeId, State, CreationUser, CreationDate)
SELECT ct.Id, pnt.Id, 1, 'SYSTEM', GETDATE()
FROM (VALUES
    ('01','ING'),('01','RET'),('01','TDE'),('01','TAE'),('01','TDP'),('01','TAP'),('01','VSP'),('01','VST'),('01','SLN'),('01','IGE'),('01','LMA'),('01','VAC-LR'),('01','AVP'),('01','VCT'),('01','IRL'),
    ('02','ING'),('02','RET'),('02','SLN'),('02','IGE'),('02','LMA'),('02','VAC-LR'),('02','TAE'),('02','VSP'),('02','VST'),('02','AVP'),('02','VCT'),('02','TDE'),('02','TAP'),('02','TDP'),
    ('03','ING'),('03','RET'),('03','SLN'),('03','IGE'),('03','VSP'),('03','AVP'),('03','TDE'),('03','TAP'),('03','TDP'),('03','VST'),('03','VAC-LR'),
    ('04','ING'),('04','RET'),('04','SLN'),('04','IGE'),('04','LMA'),('04','VSP'),('04','VST'),('04','AVP'),('04','TDE'),
    ('12','ING'),('12','RET'),('12','SLN'),('12','IGE'),('12','TAE'),('12','VSP'),('12','TDE'),
    ('16','ING'),('16','RET'),('16','SLN'),('16','IGE'),('16','LMA'),('16','VSP'),('16','VST'),('16','AVP'),('16','TDE'),('16','TAP'),('16','TDP'),('16','VAC-LR'),('16','TAE'),
    ('18','ING'),('18','RET'),('18','TDE'),('18','TAE'),('18','TDP'),('18','TAP'),('18','VSP'),('18','VST'),('18','SLN'),('18','IGE'),('18','LMA'),('18','VAC-LR'),('18','AVP'),('18','VCT'),('18','IRL'),
    ('19','ING'),('19','RET'),('19','SLN'),('19','IGE'),('19','TAE'),('19','VSP'),('19','VST'),('19','TDE'),('19','VAC-LR'),
    ('20','ING'),('20','RET'),('20','SLN'),('20','IGE'),('20','LMA'),('20','VAC-LR'),('20','TAE'),('20','VSP'),('20','VST'),('20','IRL'),('20','AVP'),('20','VCT'),('20','TDE'),('20','TAP'),
    ('21','ING'),('21','RET'),('21','SLN'),('21','IGE'),('21','TAE'),('21','VSP'),('21','IRL'),('21','TDE'),('21','TAP'),('21','TDP'),
    ('22','ING'),('22','RET'),('22','TDE'),('22','TAE'),('22','TDP'),('22','TAP'),('22','VSP'),('22','VST'),('22','SLN'),('22','IGE'),('22','LMA'),('22','VAC-LR'),('22','AVP'),('22','VCT'),('22','IRL'),
    ('23','ING'),('23','RET'),('23','IRL'),('23','VSP'),('23','VCT'),
    ('30','ING'),('30','RET'),('30','SLN'),('30','IGE'),('30','LMA'),('30','VAC-LR'),('30','TAE'),('30','VSP'),('30','VST'),('30','IRL'),('30','AVP'),('30','VCT'),('30','TDE'),('30','TAP'),
    ('31','ING'),('31','RET'),('31','SLN'),('31','IGE'),('31','LMA'),('31','VAC-LR'),('31','TAE'),('31','VSP'),('31','VST'),('31','IRL'),('31','AVP'),('31','VCT'),('31','TDE'),('31','TAP'),('31','TDP'),
    ('32','ING'),('32','RET'),('32','TDE'),('32','TAE'),('32','TDP'),('32','TAP'),('32','VSP'),('32','VST'),('32','SLN'),('32','IGE'),('32','LMA'),('32','VAC-LR'),('32','AVP'),('32','VCT'),('32','IRL'),
    ('33','ING'),('33','RET'),('33','SLN'),('33','TAE'),('33','VSP'),('33','TDE'),('33','TAP'),
    ('34','ING'),('34','RET'),('34','SLN'),('34','TAE'),('34','VSP'),('34','TDE'),('34','TAP'),('34','TDP'),
    ('35','ING'),('35','RET'),('35','SLN'),('35','IGE'),('35','LMA'),('35','VAC-LR'),('35','TAE'),('35','VSP'),('35','VST'),('35','IRL'),('35','AVP'),('35','TDE'),('35','TAP'),
    ('36','ING'),('36','RET'),('36','SLN'),('36','TAE'),('36','VSP'),('36','TDE'),('36','TAP'),('36','TDP'),('36','VAC-LR'),
    ('40','ING'),('40','RET'),
    ('42','ING'),('42','RET'),('42','SLN'),('42','IGE'),('42','TAE'),('42','VSP'),('42','TDE'),
    ('43','ING'),('43','RET'),('43','SLN'),('43','IGE'),('43','LMA'),('43','VAC-LR'),('43','TAE'),('43','VSP'),('43','AVP'),('43','TDE'),('43','TAP'),
    ('44','ING'),('44','RET'),('44','SLN'),('44','IGE'),('44','LMA'),('44','TAE'),('44','VSP'),('44','IRL'),('44','TDE'),('44','TAP'),('44','VCT'),
    ('45','ING'),('45','RET'),('45','SLN'),('45','IGE'),('45','TAE'),
    ('47','ING'),('47','RET'),('47','TDE'),('47','TAE'),('47','TDP'),('47','TAP'),('47','VSP'),('47','VST'),('47','SLN'),('47','IGE'),('47','LMA'),('47','VAC-LR'),('47','AVP'),('47','VCT'),('47','IRL'),
    ('51','ING'),('51','RET'),('51','SLN'),('51','IGE'),('51','LMA'),('51','VAC-LR'),('51','VSP'),('51','VCT'),('51','TDE'),
    ('52','ING'),('52','RET'),
    ('53','ING'),('53','RET'),('53','SLN'),('53','IGE'),('53','LMA'),('53','VAC-LR'),('53','TAE'),('53','VSP'),('53','VST'),('53','IRL'),('53','AVP'),('53','TDE'),('53','TAP'),
    ('54','ING'),('54','RET'),('54','SLN'),('54','IGE'),('54','VSP'),('54','VAC-LR'),
    ('55','ING'),('55','RET'),('55','TDE'),('55','TAE'),('55','TDP'),('55','TAP'),('55','VSP'),('55','VST'),('55','SLN'),('55','IGE'),('55','LMA'),('55','VAC-LR'),('55','AVP'),('55','VCT'),('55','IRL'),
    ('56','ING'),('56','RET'),('56','SLN'),('56','IGE'),('56','LMA'),('56','TAE'),('56','VSP'),
    ('57','ING'),('57','RET'),('57','SLN'),('57','IGE'),('57','LMA'),('57','VAC-LR'),('57','TAE'),('57','VSP'),('57','VST'),('57','IRL'),('57','TDE'),('57','TAP'),('57','TDP'),
    ('58','ING'),('58','RET'),('58','SLN'),('58','IGE'),('58','LMA'),('58','TAE'),('58','VSP'),('58','IRL'),('58','TDE'),('58','TAP'),('58','VCT'),
    ('59','ING'),('59','RET'),('59','SLN'),('59','IGE'),('59','LMA'),('59','TAE'),('59','VSP'),('59','VST'),('59','IRL'),('59','AVP'),('59','TDE'),('59','TAP'),('59','TDP'),
    ('60','ING'),('60','RET'),('60','SLN'),('60','IGE'),('60','LMA'),('60','VAC-LR'),('60','TAE'),('60','VSP'),('60','VCT'),
    ('61','ING'),('61','RET'),('61','SLN'),('61','VAC-LR'),
    ('62','ING'),('62','RET'),('62','SLN'),('62','IGE'),('62','LMA'),('62','VAC-LR'),('62','TAE'),('62','VSP'),('62','IRL'),('62','VCT'),
    ('63','ING'),('63','RET'),('63','SLN'),
    ('64','ING'),('64','RET'),('64','SLN'),('64','IGE'),('64','LMA'),('64','VAC-LR'),
    ('67','ING'),('67','RET'),('67','IRL'),('67','VSP'),('67','VCT'),
    ('68','ING'),('68','RET'),('68','TDE'),('68','TAE'),('68','TDP'),('68','TAP'),('68','VSP'),('68','VST'),('68','SLN'),('68','IGE'),('68','LMA'),('68','VAC-LR'),('68','AVP'),('68','VCT'),('68','IRL')
) AS c(TypeCode, NoveltyCode)
INNER JOIN [Payroll].[ContributorType] ct  ON ct.Code  = c.TypeCode    AND ct.State = 1
INNER JOIN [Payroll].[PilaNoveltyType] pnt ON pnt.Code = c.NoveltyCode AND pnt.State = 1;

-- ContributorTypeSubtype (Tabla 15 AT2)
INSERT INTO [Payroll].[ContributorTypeSubtype]
    (ContributorTypeId, ContributorSubtypeId, State, CreationUser, CreationDate)
SELECT ct.Id, cs.Id, 1, 'SYSTEM', GETDATE()
FROM (VALUES
    ('01','00'),('01','01'),('01','02'),('01','03'),('01','04'),('01','05'),('01','06'),('01','09'),('01','10'),
    ('02','00'),('02','01'),('02','03'),('02','04'),('02','09'),
    ('03','00'),('03','02'),('03','03'),('03','04'),('03','05'),('03','09'),('03','10'),
    ('04','00'),
    ('12','00'),('19','00'),('20','00'),('21','00'),
    ('22','00'),('22','01'),('22','02'),('22','03'),('22','04'),('22','05'),('22','09'),
    ('23','00'),('33','00'),('40','00'),('42','00'),('43','00'),
    ('16','00'),('16','02'),('16','03'),('16','04'),('16','05'),('16','09'),
    ('18','00'),('18','01'),('18','03'),('18','04'),('18','05'),('18','06'),
    ('30','00'),('30','01'),('30','03'),('30','04'),('30','05'),('30','06'),('30','09'),
    ('31','00'),('31','01'),('31','03'),('31','04'),('31','05'),('31','06'),
    ('32','00'),('32','01'),('32','03'),('32','04'),('32','05'),('32','09'),
    ('34','00'),('34','01'),('34','03'),('34','04'),('34','05'),('34','06'),
    ('35','00'),('35','01'),('35','03'),('35','04'),('35','05'),('35','09'),
    ('36','00'),
    ('44','00'),('44','01'),('44','03'),('44','04'),('44','09'),
    ('45','00'),
    ('47','00'),('47','01'),('47','03'),('47','04'),('47','05'),('47','09'),
    ('51','00'),('51','01'),('51','03'),('51','04'),('51','09'),
    ('52','00'),('52','01'),('52','03'),('52','09'),
    ('53','00'),('53','01'),('53','03'),('53','04'),('53','05'),('53','06'),('53','09'),
    ('54','00'),
    ('55','00'),('55','01'),('55','03'),('55','04'),('55','05'),('55','09'),
    ('56','00'),
    ('57','00'),('57','01'),('57','02'),('57','03'),('57','04'),('57','05'),('57','06'),('57','09'),
    ('58','00'),
    ('59','00'),('59','02'),('59','03'),('59','04'),('59','05'),('59','09'),('59','10'),
    ('60','00'),('61','00'),('62','00'),('63','00'),('64','00'),
    ('65','00'),('66','00'),('67','00'),('69','00'),
    ('68','00'),('68','01'),('68','02'),('68','03'),('68','04'),('68','05'),('68','06'),('68','09'),('68','10')
) AS c(TypeCode, SubCode)
INNER JOIN [Payroll].[ContributorType]    ct ON ct.Code = c.TypeCode AND ct.State = 1
INNER JOIN [Payroll].[ContributorSubtype] cs ON cs.Code = c.SubCode;

/*==============================================================================================================================
Author: Oscar astudillo reyes
Work Item : Bug #37193
Sprint : ERP_Services\Sprint Week 22 - 23 (2026)
==============================================================================================================================*/
INSERT INTO Payroll.ElectronicPayrollConcepts 
    (Code, Name, ConceptType, InternalCode, State, CreationUser, CreationDate)
SELECT Code, Name, ConceptType, InternalCode, 1, '999', GETDATE()
FROM (VALUES
    -- ========== ESPECIALES ==========
    (0,  '000', 'No Aplica',                                        3),
    -- ========== DEVENGADOS ==========
    (1,  '001', 'Salario Básico',                                   1),
    (2,  '002', 'Auxilio de Transporte',                            1),
    (3,  '003', 'Viático Manutención y Alojamiento Salarial',       1),
    (4,  '004', 'Viático Manutención y Alojamiento No Salarial',    1),
    (5,  '005', 'Hora Extra Diurna',                                1),
    (6,  '006', 'Hora Extra Nocturna',                              1),
    (7,  '007', 'Hora Recargo Nocturno',                            1),
    (8,  '008', 'Hora Extra Diurna Dominical/Festivo',              1),
    (9,  '009', 'Hora Recargo Diurno Dominical/Festivo',            1),
    (10, '010', 'Hora Extra Nocturna Dominical/Festivo',            1),
    (11, '011', 'Hora Recargo Nocturno Dominical/Festivo',          1),
    (12, '012', 'Vacaciones Comunes',                               1),
    (13, '013', 'Vacaciones Compensadas',                           1),
    (14, '014', 'Primas',                                           1),
    (15, '015', 'Cesantías',                                        1),
    (16, '016', 'Intereses de Cesantías',                           1),
    (17, '017', 'Incapacidades',                                    1),
    (18, '018', 'Licencia de Maternidad o Paternidad',              1),
    (19, '019', 'Licencia Remunerada',                              1),
    (20, '020', 'Licencia No Remunerada',                           1),
    (21, '021', 'Bonificación Salarial',                            1),
    (22, '022', 'Bonificación No Salarial',                         1),
    (23, '023', 'Auxilio Salarial',                                 1),
    (24, '024', 'Auxilio No Salarial',                              1),
    (25, '025', 'Huelga Legal',                                     1),
    (26, '026', 'Otro Concepto Devengado',                          1),
    (27, '027', 'Compensación Ordinaria',                           1),
    (28, '028', 'Compensación Extraordinaria',                      1),
    (29, '029', 'Bono EPCTV',                                       1),
    (30, '030', 'Comisiones',                                       1),
    (31, '031', 'Pago a Terceros Devengado',                        1),
    (32, '032', 'Anticipos Devengado',                              1),
    (33, '033', 'Dotación',                                         1),
    (34, '034', 'Apoyo Sostenimiento',                              1),
    (35, '035', 'Teletrabajo',                                      1),
    (36, '036', 'Bonificación por Retiro',                          1),
    (37, '037', 'Indemnización',                                    1),
    (38, '038', 'Reintegro Devengado',                              1),
    -- ========== DEDUCCIONES ==========
    (39, '039', 'Aporte a Salud',                                   2),
    (40, '040', 'Aporte a Fondo de Pensión',                        2),
    (41, '041', 'Fondo de Solidaridad Pensional',                   2),
    (42, '042', 'Fondo de Solidaridad Pensional - Subsistencia',    2),
    (43, '043', 'Sindicatos',                                       2),
    (44, '044', 'Sanciones',                                        2),
    (45, '045', 'Libranza',                                         2),
    (46, '046', 'Pago a Terceros Deducción',                        2),
    (47, '047', 'Anticipos Deducción',                              2),
    (48, '048', 'Otras Deducciones',                                2),
    (49, '049', 'Aportes Voluntarios a Pensión',                    2),
    (50, '050', 'Retención en la Fuente',                           2),
    (51, '051', 'Aportes a Fondos AFC',                             2),
    (52, '052', 'Cooperativa',                                      2),
    (53, '053', 'Embargo Fiscal',                                   2),
    (54, '054', 'Planes Complementarios',                           2),
    (55, '055', 'Educación',                                        2),
    (56, '056', 'Reintegro Deducción',                              2),
    (57, '057', 'Deuda',                                            2)
) AS Conceptos(InternalCode, Code, Name, ConceptType)
WHERE NOT EXISTS (
    SELECT 1 
    FROM Payroll.ElectronicPayrollConcepts epc 
    WHERE epc.InternalCode = Conceptos.InternalCode
)

/*==============================================================================================================================
Author: Oscar astudillo reyes
Work Item : Bug #38551
Sprint : ERP_Services\Sprint Week 26 - 27 (2026)
==============================================================================================================================*/

-- 1. PRIMAS (Type 14)
DECLARE @IdPrimas INT = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 14);
INSERT INTO [Payroll].[ElectronicPayrollConceptSubtype]
    ([IdElectronicPayrollConcepts], [Name], [InternalSubCode], [State])
SELECT v.IdElectronicPayrollConcepts, v.Name, v.InternalSubCode, v.State
FROM (VALUES
    (@IdPrimas, 'Prima Salarial', 0, 1),
    (@IdPrimas, 'Prima No Salarial', 1, 1)
) AS v(IdElectronicPayrollConcepts, Name, InternalSubCode, State)
WHERE NOT EXISTS (
    SELECT 1 FROM [Payroll].[ElectronicPayrollConceptSubtype] s
    WHERE s.IdElectronicPayrollConcepts = v.IdElectronicPayrollConcepts AND s.InternalSubCode = v.InternalSubCode
);

-- 2. INCAPACIDADES (Type 17)
DECLARE @IdIncapacidades INT = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 17);
INSERT INTO [Payroll].[ElectronicPayrollConceptSubtype]
    ([IdElectronicPayrollConcepts], [Name], [InternalSubCode], [State])
SELECT v.IdElectronicPayrollConcepts, v.Name, v.InternalSubCode, v.State
FROM (VALUES
    (@IdIncapacidades, 'Incapacidad Comun', 1, 1),
    (@IdIncapacidades, 'Incapacidad Profesional', 2, 1),
    (@IdIncapacidades, 'Incapacidad Laboral', 3, 1)
) AS v(IdElectronicPayrollConcepts, Name, InternalSubCode, State)
WHERE NOT EXISTS (
    SELECT 1 FROM [Payroll].[ElectronicPayrollConceptSubtype] s
    WHERE s.IdElectronicPayrollConcepts = v.IdElectronicPayrollConcepts AND s.InternalSubCode = v.InternalSubCode
);

-- 3. OTROS CONCEPTOS DEVENGADOS (Type 26)
DECLARE @IdOtrosConceptos INT = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 26);
INSERT INTO [Payroll].[ElectronicPayrollConceptSubtype]
    ([IdElectronicPayrollConcepts], [Name], [InternalSubCode], [State])
SELECT v.IdElectronicPayrollConcepts, v.Name, v.InternalSubCode, v.State
FROM (VALUES
    (@IdOtrosConceptos, 'Otro Concepto Salarial', 0, 1),
    (@IdOtrosConceptos, 'Otro Concepto No Salarial', 1, 1)
) AS v(IdElectronicPayrollConcepts, Name, InternalSubCode, State)
WHERE NOT EXISTS (
    SELECT 1 FROM [Payroll].[ElectronicPayrollConceptSubtype] s
    WHERE s.IdElectronicPayrollConcepts = v.IdElectronicPayrollConcepts AND s.InternalSubCode = v.InternalSubCode
);


-- 4. BONOS EPCTV (Type 29)
DECLARE @IdBonos INT = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 29);
INSERT INTO [Payroll].[ElectronicPayrollConceptSubtype]
    ([IdElectronicPayrollConcepts], [Name], [InternalSubCode], [State])
SELECT v.IdElectronicPayrollConcepts, v.Name, v.InternalSubCode, v.State
FROM (VALUES
    (@IdBonos, 'Bono EPCTV Salarial', 0, 1),
    (@IdBonos, 'Bono EPCTV No Salarial', 1, 1),
    (@IdBonos, 'Bono EPCTV Alimentacion Salarial', 2, 1),
    (@IdBonos, 'Bono EPCTV Alimentacion No Salarial', 3, 1)
) AS v(IdElectronicPayrollConcepts, Name, InternalSubCode, State)
WHERE NOT EXISTS (
    SELECT 1 FROM [Payroll].[ElectronicPayrollConceptSubtype] s
    WHERE s.IdElectronicPayrollConcepts = v.IdElectronicPayrollConcepts AND s.InternalSubCode = v.InternalSubCode
);


-- 5. SANCIONES (Type 44)
DECLARE @IdSanciones INT = (SELECT Id FROM Payroll.ElectronicPayrollConcepts WHERE InternalCode = 44);
INSERT INTO [Payroll].[ElectronicPayrollConceptSubtype]
    ([IdElectronicPayrollConcepts], [Name], [InternalSubCode], [State])
SELECT v.IdElectronicPayrollConcepts, v.Name, v.InternalSubCode, v.State
FROM (VALUES
    (@IdSanciones, 'Sancion Publica', 0, 1),
    (@IdSanciones, 'Sancion Privada', 1, 1)
) AS v(IdElectronicPayrollConcepts, Name, InternalSubCode, State)
WHERE NOT EXISTS (
    SELECT 1 FROM [Payroll].[ElectronicPayrollConceptSubtype] s
    WHERE s.IdElectronicPayrollConcepts = v.IdElectronicPayrollConcepts AND s.InternalSubCode = v.InternalSubCode
);