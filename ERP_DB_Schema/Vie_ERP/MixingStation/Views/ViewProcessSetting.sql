





CREATE view [MixingStation].[ViewProcessSetting] 
as 

WITH CTE_Msclass AS
(
	 SELECT udt.MSClass, rmsd.CampaignDetailId
			FROM MixingStation.UnitDoseType udt
			 JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.UnitDoseTypeId = udt.Id	
			 GROUP BY  udt.MSClass,rmsd.CampaignDetailId
			)

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id,
		K.CampaignDetailId
		,ISNULL(K.ProductId,1) ProductId
		,K.CodeId
		,K.ProductCodeName
		,K.MovementDate
		,k.MovementTypeName
		,K.UnitMeasurement 
		,k.UnitMeasurementId
		,K.QuantityUnitMeasurement 
		,K.QuantityUsed
		,K.CurrentQuantity
		,K.DeliveredQuantity
		,K.QuantityUsedCampaign
		,K.QuantityDevolution
		,K.CampaignBalance
		,k.QuantityHarnessed
		,k.QuantityDevolutionUnitMeasurement
		,k.DirectMPQuantity
		,k.IndirectMPQuantity
		,k.QuantityRemaining
		,k.RawMaterialQuantityBalance 
		,k.UserMovementDescription
		,k.CKQuantity  
		, k.BatchSerialId
		, k.BatchCode		
		, CAST(ISNULL((
			SELECT TOP 1 1 
			FROM MixingStation.RequestMixingStationDetail rmsd
			JOIN MixingStation.PackageDetail ppdm ON rmsd.PackageId = ppdm.PackageId
			WHERE rmsd.CampaignDetailId = k.CampaignDetailId
				AND ppdm.AtcId = k.ATCId AND ppdm.MainMedicine = 1
		   ), 0) AS BIT) IsMain
		,CAST(ISNULL((
			SELECT TOP 1 1 
			FROM MixingStation.ViewListMedicinesProduction vlmp
			WHERE vlmp.ATCId = k.ATCId and vlmp.AllowRemant = 'Si'
		   ), 0) AS BIT) AllowsRemanent
        , CAST(ISNULL((
			SELECT TOP 1 cdv.ItemType 
			FROM MixingStation.CampaignDetailValidation cdv
			WHERE cdv.CampaignDetailId = k.CampaignDetailId
				AND cdv.ProductId = k.ProductId AND cdv.BatchSerialId = k.BatchSerialId
		   ), 0) AS TINYINT) ItemType,
		   CASE (CAST(ISNULL((
			SELECT TOP 1 cdv.ItemType 
			FROM MixingStation.CampaignDetailValidation cdv
			WHERE cdv.CampaignDetailId = k.CampaignDetailId
				AND cdv.ProductId = k.ProductId AND ISNULL(cdv.BatchSerialId,0) = ISNULL(k.BatchSerialId,0)
		   ), 0) AS TINYINT))
			   WHEN 1 THEN 'Principal'
			   WHEN 2 THEN 'Canasta'
			   WHEN 3 THEN 'Otro'
			   WHEN 4 THEN 'Solicitudes Manuales'
		   END ItemTypeName
FROM(
	SELECT	c.CampaignDetailId,
		c.ProductId,
		c.CodeId,
		c.ProductCodeName,
		c.MovementDate,
		c.MovementTypeName,
		c.UnitMeasurement,
		c.UnitMeasurementId,
		c.QuantityUnitMeasurement,
		c.QuantityUsed,
		(c.QuantityUnitMeasurement + c.QuantityHarnessed - c.QuantityUsed -c.QuantityDevolutionUnitMeasurement + c.IndirectMPQuantityOperation ) CurrentQuantity,
		c.DeliveredQuantity,
		c.QuantityUsedCampaign,
		c.QuantityDevolution,
		(c.DeliveredQuantity-c.QuantityUsedCampaign-c.QuantityDevolution) CampaignBalance,
		c.QuantityHarnessed,
		c.QuantityDevolutionUnitMeasurement,
		c.DirectMPQuantity,
		c.IndirectMPQuantity,
		c.QuantityRemaining,
		((c.QuantityUnitMeasurement+c.QuantityHarnessed)-(c.QuantityDevolutionUnitMeasurement+c.QuantityUsed )+c.IndirectMPQuantityOperation) RawMaterialQuantityBalance,
		'' UserMovementDescription,
		0 CKQuantity,
		c.BatchSerialId,
		c.BatchCode,
		c.ATCId			
FROM (	
		select
			ck.CampaignDetailId,
			ip.Id ProductId,
			NULL CodeId,
			CONCAT(ip.Code,' - ',ip.Name) ProductCodeName,
			null MovementDate,
			'' MovementTypeName,
			IIF(ip.SupplieId IS NOT NULL,imu.Name ,IIF(CTE_Msclass.Msclass=5,atc.Presentations,imu.Name)) UnitMeasurement,
			imu.Id UnitMeasurementId,
			IIF(ip.SupplieId IS NOT NULL, 
				SUM(IIF(ck.MovementType =1 and  ck.EntityName NOT IN ('Harnessed','IndirectMPQuantity'),ck.Quantity,0)), 
				SUM([MixingStation].[CalculateQuantityTypeMSclass]((IIF(ck.MovementType =1 and ck.EntityName NOT IN ('Harnessed','IndirectMPQuantity'),ck.Quantity,0)),CTE_Msclass.MSClass,atc.Weight))) QuantityUnitMeasurement,
			IIF(ip.SupplieId IS NOT NULL, 
				SUM(IIF(ck.MovementType =2 and ck.EntityName NOT IN('RawMaterialDevolution','IndirectMPQuantity'),ck.Quantity,0)),
				SUM([MixingStation].[CalculateQuantityTypeMSclass]((IIF(ck.MovementType =2 and ck.EntityName NOT IN('RawMaterialDevolution','IndirectMPQuantity'),ck.Quantity,0)),CTE_Msclass.MSClass,atc.Weight))) QuantityUsed,
			SUM(IIF(ck.MovementType =2 and ck.EntityName = 'RawMaterialDevolution',ck.Quantity,0)) QuantityDevolutionUnitMeasurement,
			SUM(IIF(ck.MovementType =1,ck.Quantity,0)/IIF(pt.code ='01',iif(atc.FormulationType = 4, atc.ConcentrationQuantity,iif( atc.FormulationType=2,atc.Volume,atc.Weight)),1)) DeliveredQuantity,
			SUM(IIF(ck.MovementType =2 and ck.EntityName <> 'RawMaterialDevolution',ck.Quantity,0)/IIF(pt.code ='01',iif(atc.FormulationType = 4, atc.ConcentrationQuantity,iif( atc.FormulationType=2,atc.Volume,atc.Weight)),1)) QuantityUsedCampaign,
			SUM(IIF(ck.MovementType =2 and ck.EntityName = 'RawMaterialDevolution',ck.Quantity,0)/IIF(pt.code ='01',iif(atc.FormulationType = 4, atc.ConcentrationQuantity,iif( atc.FormulationType=2,atc.Volume,atc.Weight)),1)) QuantityDevolution,
			SUM(IIF(ck.MovementType =1 and ck.EntityName = 'Harnessed',ck.Quantity,0)) QuantityHarnessed,
			IIF(ip.SupplieId IS NOT NULL, 
				SUM(IIF(ck.MovementType=2 and ck.EntityName='CampaignRawMaterial',ck.Quantity,0)),
				SUM([MixingStation].[CalculateQuantityTypeMSclass]((IIF(ck.MovementType=2 and ck.EntityName='CampaignRawMaterial',ck.Quantity,0)),CTE_Msclass.MSClass,atc.Weight))) DirectMPQuantity,
			SUM(IIF(ck.EntityName = 'IndirectMPQuantity',(ck.Quantity*IIF(ck.MovementType=2,-1,1)),0)) IndirectMPQuantityOperation,
			SUM(IIF(ck.EntityName = 'IndirectMPQuantity',ck.Quantity,0)) IndirectMPQuantity,
			SUM(IIF(ck.EntityName = 'QuantityRemaining',(ck.Quantity*IIF(ck.MovementType =2,-1,1)),0)) QuantityRemaining,
			ck.BatchSerialId,
			b.BatchCode,
			atc.Id ATCId
		FROM  MixingStation.CampaignKardex ck			
		JOIN Inventory.InventoryProduct ip on ck.ProductId = ip.Id
		JOIN inventory.ProductType pt on ip.ProductTypeId=pt.Id
		JOIN CTE_Msclass ON CTE_Msclass.CampaignDetailId = ck.CampaignDetailId
		LEFT JOIN Inventory.BatchSerial b on ck.BatchSerialId=b.Id
		LEFT JOIN Inventory.ATC atc on ip.ATCId =atc.Id
		LEFT JOIN  Inventory.InventoryMeasurementUnit imu on ck.MeasurementUnitId	= imu.Id
		GROUP by ip.Name,ip.Code, ip.SupplieId,imu.Name,ck.CampaignDetailId,ip.id,imu.Id,ck.BatchSerialId,b.BatchCode,atc.Id, atc.Presentations, CTE_Msclass.MSClass) c

UNION All

SELECT 
		ck.CampaignDetailId,
		null ProductId,
		ip.Id CodeId,
		CONCAT(ip.Code,' - ',ip.Name) ProductCodeName,
		ck.MovementDate,
		IIF(ck.MovementType = 1,'Entrada Campaña','Salida Campaña') MovementTypeName,
		IIF(ip.SupplieId IS NOT NULL,imu.Name ,IIF(CTE_Msclass.Msclass=5,atc.Presentations,imu.Name)) UnitMeasurement,
		imu.id UnitMeasurementId,
		0 QuantityUnitMeasurement,
		0 QuantityUsed,
		null CurrentQuantity,
		0 DeliveredQuantity,
		0 QuantityUsedCampaign,
		0 QuantityDevolution,
		0 CampaignBalance,
		0 QuantityHarnessed,
		0 QuantityDevolutionUnitMeasurement,
		0 DirectMPQuantity,
		0 IndirectMPQuantity,
		0 QuantityRemaining,
		0 RawMaterialQuantityBalance,
		CONCAT(ck.Description,' - Usuario: ',ck.CreationUser) UserMovementDescription,
		IIF(ip.SupplieId IS NOT NULL, 
			SUM(ck.Quantity), 
			SUM([MixingStation].[CalculateQuantityTypeMSclass](ck.Quantity,CTE_Msclass.MSClass,atc.Weight))) CKQuantity,
		ck.BatchSerialId,
		null BatchCode,
		atc.Id ATCId			 
FROM  MixingStation.CampaignKardex ck 
JOIN Inventory.InventoryProduct ip  on ck.ProductId = ip.Id
JOIN inventory.ProductType pt on ip.ProductTypeId=pt.Id
JOIN CTE_Msclass WITH(NOLOCK ) ON CTE_Msclass.CampaignDetailId = ck.CampaignDetailId
LEFT JOIN Inventory.ATC atc on ip.ATCId =atc.Id
LEFT JOIN  Inventory.InventoryMeasurementUnit imu on ck.MeasurementUnitId	= imu.Id
GROUP by ip.SupplieId, ip.Name,ip.Code,imu.Name,ck.CampaignDetailId,ck.MovementDate,ck.MovementType,ip.Id,ck.Description,ck.CreationUser,imu.Id, ck.BatchSerialId, atc.Id, CTE_Msclass.MSClass, atc.Presentations) K

GO