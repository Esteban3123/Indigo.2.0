

CREATE VIEW [Inventory].[ViewReportPharmaceuticalDispensingDevolution]
AS
SELECT	pddevd.Row,
		pddev.Id PharmaceuticalDispensingDevolutionId,
		pddev.Code as DevolutionCode,
		pddev.DocumentDate,
		pddev.AdmissionNumber,
		pac.IPCODPACI as Codepacient, 
		pac.IPNOMCOMP as NamePacient,
		ISNULL(bed.NUMCAMHOS, '') AS Bed,
		CONCAT(w.Code, ' - ', w.Name) AS WareHouse, 
		pddev.Observation, 
		pddevd.ProductCode,
		SUBSTRING(pddevd.ProductName,1,45) as Name, 
		pddevd.BatchCode,
		pddevd.ExpirationDate,
		pddevd.Quantity,
		pddev.CreationUser
FROM [Inventory].[Warehouse] w WITH (NOLOCK)
JOIN [Inventory].[PharmaceuticalDispensingDevolution] pddev WITH (NOLOCK) ON w.Id = pddev.WarehouseId
JOIN dbo.ADINGRESO ing ON pddev.AdmissionNumber = ing.NUMINGRES
JOIN [dbo].INPACIENT pac WITH (NOLOCK) ON ing.IPCODPACI = pac.IPCODPACI
JOIN
(
	SELECT	CONCAT(MIN(pddevd.Id), '-', MIN(pdd.Id), '-', MIN(pddbs.Id)) Row,
			pddevd.PharmaceuticalDispensingDevolutionId,
			ip.Code ProductCode,
			ip.Name ProductName,
			bs.BatchCode, 
			bs.ExpirationDate, 
			SUM(pddevd.Quantity) Quantity
	FROM [Inventory].[PharmaceuticalDispensingDevolutionDetail] pddevd WITH (NOLOCK)
	JOIN [Inventory].[PharmaceuticalDispensingDetailBatchSerial] pddbs WITH (NOLOCK) ON pddevd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
	JOIN [Inventory].[PharmaceuticalDispensingDetail] pdd WITH (NOLOCK) ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
	JOIN [Inventory].[InventoryProduct] ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
	JOIN Inventory.PhysicalInventory phi WITH (NOLOCK) ON ISNULL(pddbs.PhysicalInventoryId,pddbs.PhysicalInventoryCustodyId) = phi.Id
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phi.BatchSerialId = bs.Id
	GROUP BY pddevd.PharmaceuticalDispensingDevolutionId, pdd.ServiceDate, ip.Code, ip.Name, bs.BatchCode, bs.ExpirationDate
) pddevd ON pddev.Id = pddevd.PharmaceuticalDispensingDevolutionId
LEFT JOIN dbo.CHCAMASHO bed ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = bed.CODICAMAS