
CREATE VIEW [MixingStation].[ViewWarehouseMaquila] 
as 
	SELECT	
			rmsd.Id Id,
			cd.Id	CampaignDetailId,
			cec.ManagesMaquila,
			rmsd.Source,
			cec.WarehouseId,
			CONCAT(w.Code,' - ',w.Name) WarehouseMaquilaCodeName,
			cec.Status,
			ecc.Code ExternalCareCenterCode,
			cec.CustomerId,
			CONCAT(c.Nit,' - ',c.Name) CustomerNitName
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id=rmsd.CampaignDetailId
	JOIN MixingStation.ExternalCareCenter ecc ON rmsd.CareCenterCode=ecc.Code
	JOIN MixingStation.ContractExternalClients cec ON ecc.ContractExternalClientsId=cec.Id
	JOIN Inventory.Warehouse w ON cec.WarehouseId = w.Id
	JOIN Common.Customer c ON cec.CustomerId=c.Id
	WHERE cec.ManagesMaquila =1  and rmsd.Source in (2,3) and cec.Status=1