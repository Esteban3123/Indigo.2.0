CREATE VIEW [Authorization].[ViewWithdrawal]
AS
SELECT	CONCAT('ManagementMedicalOrder-', mmo.Id) Id,
		mmo.EntityName, mmo.EntityId,
		ou.UnitName OperatingUnit,
		ha.Name HealthAdministrator,
		CONCAT(RTRIM(fu.UFUCODIGO), ' - ', fu.UFUDESCRI) FunctionalUnit,
		mmo.PatientCode,
		p.IPNOMCOMP PatientName,
		mmo.AdmissionNumber,
		mmo.Folio,
		ing.CODCAMACT Bed,
		ing.IFECHAING AdmissionDate,
		ing.FECHEGRESO EgressDate,
		mmo.ProfessionalCode Professional,
		mmo.RequestDate,
		mmo.Type,
		mmo.ItemCode,
		IIF(mmo.Type = 1, ce.Description, ip.Name) ItemName,
		mmo.RequestQuantity Quantity,
		mmo.Status
FROM Common.OperatingUnit ou
JOIN [Authorization].ManagementMedicalOrder mmo ON ou.Id = mmo.OperatingUnitId
JOIN dbo.INPACIENT p ON mmo.PatientCode = p.IPCODPACI
JOIN dbo.ADINGRESO ing ON mmo.AdmissionNumber = ing.NUMINGRES
JOIN .INUNIFUNC fu ON mmo.FunctionalUnitCode = fu.UFUCODIGO
JOIN Contract.HealthAdministrator ha ON ing.GENCONENTITY = ha.Id
LEFT JOIN Contract.CUPSEntity ce ON mmo.Type = 1 AND mmo.ItemCode = ce.Code 
LEFT JOIN Inventory.InventoryProduct ip ON mmo.Type = 2 AND mmo.ItemCode = ip.Code