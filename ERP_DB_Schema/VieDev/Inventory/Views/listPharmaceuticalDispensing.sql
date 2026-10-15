



CREATE VIEW [Inventory].[listPharmaceuticalDispensing]
AS

with cte_WareHouse as ( SELECT c.PharmaceuticalDispensingId,
								STRING_AGG(CONCAT(w.code,' - ', w.name),', ') Warehouse
						FROM (SELECT	
									phd.PharmaceuticalDispensingId,
									phd.WarehouseId
							FROM Inventory.PharmaceuticalDispensingDetail phd WITH(NOLOCK)
							GROUP by  phd.PharmaceuticalDispensingId,phd.WarehouseId) c
						JOIN Inventory.Warehouse w WITH(NOLOCK) ON c.WarehouseId =w.Id
						GROUP by c.PharmaceuticalDispensingId	
						)

SELECT
	ph.Id,
	ph.Code,
	ph.AdmissionNumber,
	RTRIM( pac.IPCODPACI) AS CodePatient,
	pac.IPNOMCOMP AS Patient,
	ph.DocumentDate, 
	ph.Status,
	wh.Warehouse,
	CONCAT(ph.CreationUser,' - ', ps.Fullname) AS CreationUser

	FROM Inventory.PharmaceuticalDispensing AS ph WITH (NOLOCK)
	LEFT JOIN cte_WareHouse AS wh ON ph.Id = wh.PharmaceuticalDispensingId
	LEFT JOIN Security.[UserInt] AS us ON us.UserCode = ph.CreationUser
	LEFT JOIN Security.PersonInt AS ps ON ps.id = us.IdPerson
	LEFT JOIN dbo.ADINGRESO AS ad WITH (NOLOCK) ON ph.AdmissionNumber = ad.NUMINGRES 
	LEFT JOIN dbo.INPACIENT AS pac WITH (NOLOCK) ON ad.IPCODPACI = pac.IPCODPACI