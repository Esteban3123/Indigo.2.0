CREATE VIEW [Contract].[ViewDashboardAlertsPGPServices]
AS
WITH temp_CareGroup AS (
	SELECT cg.Id CareGroupId, cg.TechnicalNoteId
	FROM Contract.CareGroup cg WITH (NOLOCK)
	WHERE cg.LiquidationType = 5
),
temp_Groupers AS (
	SELECT	cg.CareGroupId, gc.CUPSEntityId, gc.CUPSEntityContractDescriptionId,
			CONCAT(g.Code, ' - ', g.Description) GrouperCodeName, 
			g.UserMin, g.UserMax, g.UserNumber,
			g.ProjectCME, g.TotalContract,
			CONCAT(g.Frequence, ' ', CASE g.MeasurementUnit
									WHEN 1 THEN 'Diario'
									WHEN 2 THEN 'Semanal'
									WHEN 3 THEN 'Mensual'
									WHEN 4 THEN 'Bimensual'
									WHEN 5 THEN 'Trimestral'
									WHEN 6 THEN 'Semestral'
									WHEN 7 THEN 'Anual'
								END) FrequenceDescription
	FROM temp_CareGroup cg WITH (NOLOCK) 
	JOIN Contract.TechnicalNote tn WITH (NOLOCK) ON cg.TechnicalNoteId = tn.Id
	JOIN Contract.TechnicalNoteDetail tnd WITH (NOLOCK) ON tn.Id = tnd.TechnicalNoteId
	JOIN Contract.Groupers g ON tnd.GrouperId = g.Id
	JOIN Contract.GroupersCups gc ON g.Id = gc.GrouperId
),
temp_CUPSEntity AS (
	SELECT	ce.Id, ce.Code, ce.Description, 
			cecd.Id CUPSEntityContractDescriptionId,
			cd.Id ContractDescriptionId, cd.Code ContractDescriptionCode, cd.Name ContractDescriptionName
	FROM Contract.CUPSEntity ce WITH (NOLOCK) 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON ce.Id = cecd.CUPSEntityId AND cecd.IsDelete = 0
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	WHERE ce.Status = 1
),
temp_Services AS (
	SELECT	so.AdmissionNumber, so.PatientCode, sod.PerformsFunctionalUnitId FunctionalUnitId, sod.CareGroupId,
			sod.Id EntityId, so.Code ServiceOrderCode, so.OrderDate RequestDate, sod.PerformsHealthProfessionalCode ProfessionalCode, '' Observations,
			ce.Id CUPSEntityId, ce.CUPSEntityContractDescriptionId,
			CONCAT(ce.Code, ' - ', ce.Description) CUPSEntityCodeName, CONCAT(ce.ContractDescriptionCode, ' - ', ce.ContractDescriptionName) DescriptionCodeName,
			sod.InvoicedQuantity, GrandTotalSalesPrice TotalServiceValue
	FROM Billing.ServiceOrder so WITH (NOLOCK)
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON so.Id = sod.ServiceOrderId
	JOIN temp_CUPSEntity ce WITH (NOLOCK) ON sod.CUPSEntityId = ce.Id AND ISNULL(ce.CUPSEntityContractDescriptionId,0) = ISNULL(sod.CUPSEntityContractDescriptionId,0)
),
temp_Invoice AS (
	SELECT i.CareGroupId, i.InvoiceNumber, i.CapitationInitialDate, i.CapitationEndDate
	FROM temp_CareGroup cg WITH (NOLOCK)
	JOIN Billing.Invoice i WITH (NOLOCK) ON cg.CareGroupId = i.CareGroupId
	WHERE i.Status = 1 AND i.DocumentType = 4
)

-----------------------------------------------------------------------------------------------------------------------

SELECT	CONCAT(v.EntityName, '-', v.EntityId, '-', v.GrouperCodeName, '-', v.CUPSEntityCodeName, '-', v.DescriptionCodeName) RowId,
		v.CareGroupId,
		CASE
			WHEN v.GrouperCodeName IS NULL THEN 'SIN AGRUPADOR'
			ELSE CONCAT(v.GrouperCodeName, ' (Usuarios: ', v.UserNumber, ' - Min: ', v.UserMin, ' - Max: ', v.UserMax, ' - Total Contratado: ', FORMAT(v.TotalContract, 'C0'), ' - Frecuencia: ', v.FrequenceDescription, ')')
		END GroupDescription,
		v.UserMin, v.UserMax, v.TotalContract,
		v.RequestDate,
		v.Patient,
		v.AdmissionNumber,
		v.CareCenter,
		v.FunctionalUnit,
		v.Professional,
		v.DiagnosticCode Diagnostic,
		v.Observations,
		v.CUPSEntityCodeName,
		v.DescriptionCodeName,
		v.Quantity,
		v.CMEValue,
		v.TotalCME,
		v.TotalServiceValue,
		v.ServiceOrderCode,
		v.ServiceControlNumber,
		v.InvoiceNumber,
		CASE
			WHEN v.InvoiceNumber IS NOT NULL THEN 'Con Factura'
			WHEN v.ServiceControlNumber IS NOT NULL THEN 'Con Control de Servicio'
			WHEN v.ServiceOrderCode IS NOT NULL THEN 'Con Orden de Servicio'
			ELSE 'Solicitado'
		END StatusName
FROM
(	
	-- Ordenes de servico
	SELECT	sod.AdmissionNumber, CONCAT(ISNULL(tp.Nit, sod.PatientCode), ' - ', tp.Name) Patient, ca.NOMCENATE CareCenter, CONCAT(fu.Code, ' - ', fu.Name) FunctionalUnit, ISNULL(sc.CareGroupId, sod.CareGroupId) CareGroupId,
			'ServiceOrder' EntityName, sod.EntityId, ISNULL(sc.InvoiceDate, sod.RequestDate) RequestDate, CONCAT(sod.ProfessionalCode, ' - ', p.NOMMEDICO) Professional, diag.CODDIAGNO DiagnosticCode, sod.Observations,
			sod.CUPSEntityCodeName, sod.DescriptionCodeName, sod.InvoicedQuantity Quantity, ISNULL(g.ProjectCME, 0) CMEValue,
			sod.InvoicedQuantity * ISNULL(g.ProjectCME, 0) TotalCME, sod.TotalServiceValue,
			g.GrouperCodeName, ISNULL(g.UserMin, 0) UserMin, ISNULL(g.UserMax, 0) UserMax, g.UserNumber, ISNULL(g.TotalContract, 0) TotalContract, g.FrequenceDescription,
			sod.ServiceOrderCode, sc.InvoiceNumber ServiceControlNumber, 
			STUFF
			(
				(
					SELECT	', ' + i.InvoiceNumber
					FROM temp_Invoice i WITH (NOLOCK)
					WHERE sc.CareGroupId = i.CareGroupId AND CAST(sc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
					FOR XML PATH ('')
				), 1, 1, ''
			) InvoiceNumber
	FROM temp_Services sod WITH (NOLOCK)
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON sod.FunctionalUnitId = fu.Id
	JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON sod.EntityId = sodd.ServiceOrderDetailId
	LEFT JOIN Billing.Invoice sc WITH (NOLOCK) ON sodd.RevenueControlDetailId = sc.RevenueControlDetailId AND sc.Status = 1 AND sc.DocumentType = 5
	JOIN temp_CareGroup cg WITH (NOLOCK) ON ISNULL(sc.CareGroupId, sod.CareGroupId) = cg.CareGroupId
	LEFT JOIN temp_Groupers g WITH (NOLOCK) ON ISNULL(sc.CareGroupId, sod.CareGroupId) = g.CareGroupId AND sod.CUPSEntityId = g.CUPSEntityId AND ISNULL(sod.CUPSEntityContractDescriptionId,0) = ISNULL(g.CUPSEntityContractDescriptionId,0)
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON sod.PatientCode = tp.Nit
	LEFT JOIN dbo.ADINGRESO a WITH (NOLOCK) ON sod.AdmissionNumber = a.NUMINGRES
	LEFT JOIN dbo.ADCENATEN ca WITH (NOLOCK) ON a.CODCENATE = CA.CODCENATE
	LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = a.NUMINGRES AND DIAG.CODDIAGNO = a.CODDIAEGR AND DIAG.CODDIAPRI = 1
	LEFT JOIN dbo.INPROFSAL p WITH (NOLOCK) ON sod.ProfessionalCode = p.CODPROSAL
) v