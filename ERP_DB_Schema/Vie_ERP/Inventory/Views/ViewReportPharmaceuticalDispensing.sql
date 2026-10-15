
CREATE VIEW [Inventory].[ViewReportPharmaceuticalDispensing]
AS
SELECT  pdd.Row,
		pd.Id, 
		pd.Code as DispensacionCode, 
		pd.DocumentDate, 
		pdd.ServiceDate, 
		pd.AdmissionNumber, 
		pac.IPCODPACI as Codepacient, 
		pac.IPNOMCOMP as NamePacient,
		pdd.FunctionalUnitName as UnidadFuncional, 
		ISNULL(bed.NUMCAMHOS, '') AS Bed,
		ISNULL(pdd.ProfessionalName,'Medicamento de custodia')as Profesional, 
		pdd.WarehouseCodeName,
		pdd.ProductCode as ProductCode, 
		SUBSTRING(pdd.ProductName,1,45) as ProductName,
		pdd.BatchCode,
		pdd.ExpirationDate,
		pdd.Quantity, 
		pd.CreationUser 
FROM [Inventory].[PharmaceuticalDispensing] pd WITH (NOLOCK)
JOIN dbo.ADINGRESO ing ON pd.AdmissionNumber = ing.NUMINGRES
JOIN [dbo].INPACIENT pac WITH (NOLOCK) ON ing.IPCODPACI = pac.IPCODPACI
JOIN
(
	SELECT	CONCAT(MIN(pdd.Id), '-', MIN(pddbs.Id)) Row,
			pdd.PharmaceuticalDispensingId,
			pdd.ServiceDate,
			fu.Name FunctionalUnitName,
			tp.Name ProfessionalName,
			CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName,
			ip.Code ProductCode,
			ip.Name ProductName,
			bs.BatchCode, 
			bs.ExpirationDate, 
			SUM(pddbs.Quantity) Quantity
	FROM Inventory.Warehouse w WITH (NOLOCK)
	JOIN [Inventory].[PharmaceuticalDispensingDetail] pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
	JOIN [Inventory].[InventoryProduct] ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
	JOIN [Payroll].[FunctionalUnit] fu WITH (NOLOCK) ON pdd.FunctionalUnitId = fu.Id
	LEFT JOIN [Common].[ThirdParty] tp WITH (NOLOCK) ON pdd.OrderedHealthProfessionalThirdPartyId = tp.Id
	LEFT JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
	LEFT JOIN Inventory.PhysicalInventory phi WITH (NOLOCK) ON pddbs.PhysicalInventoryId = phi.Id
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phi.BatchSerialId = bs.Id
	GROUP BY pdd.PharmaceuticalDispensingId, pdd.ServiceDate, fu.Name, tp.Name, w.Code, w.Name, ip.Code, ip.Name, bs.BatchCode, bs.ExpirationDate
) pdd ON pd.Id = pdd.PharmaceuticalDispensingId
LEFT JOIN dbo.CHCAMASHO bed ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = bed.CODICAMAS