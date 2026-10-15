


CREATE view [MixingStation].[ViewCampaignKardex] 
as 

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
		(c.QuantityUnitMeasurement + c.QuantityHarnessed - c.QuantityUsed -c.QuantityDevolutionUnitMeasurement + c.IndirectMPQuantityOperation ) as CurrentQuantity,
		c.DeliveredQuantity,
		c.QuantityUsedCampaign,
		c.QuantityDevolution,
		(c.DeliveredQuantity-c.QuantityUsedCampaign-c.QuantityDevolution) as CampaignBalance,
		c.QuantityHarnessed,
		c.QuantityDevolutionUnitMeasurement,
		c.DirectMPQuantity,
		c.IndirectMPQuantity,
		c.QuantityRemaining,
		(c.QuantityUnitMeasurement+c.QuantityHarnessed-c.QuantityDevolutionUnitMeasurement+DirectMPQuantity + c.IndirectMPQuantityOperation + c.QuantityRemaining) RawMaterialQuantityBalance,
		'' UserMovementDescription,
		0 CKQuantity
FROM (	
		select
			ck.CampaignDetailId,
			ip.Id ProductId,
			NULL CodeId,
			CONCAT(ip.Code,' - ',ip.Name) ProductCodeName,
			null MovementDate,
			'' MovementTypeName,
			imu.Name UnitMeasurement,
			imu.Id UnitMeasurementId,
			sum(iif(ck.MovementType =1 and  ck.EntityName NOT IN ('Harnessed','IndirectMPQuantity'),ck.Quantity,0)) QuantityUnitMeasurement,
			sum(iif(ck.MovementType =2 and ck.EntityName NOT IN('RawMaterialDevolution','IndirectMPQuantity'),ck.Quantity,0)) QuantityUsed,
			sum(iif(ck.MovementType =2 and ck.EntityName = 'RawMaterialDevolution',ck.Quantity,0)) QuantityDevolutionUnitMeasurement,
			round(sum(iif(ck.MovementType =1,ck.Quantity,0)/iif(pt.code ='01',iif(atc.FormulationType=2,atc.Volume,atc.Weight),1)), 3) DeliveredQuantity,
			round(sum(iif(ck.MovementType =2 and ck.EntityName <> 'RawMaterialDevolution',ck.Quantity,0)/iif(pt.code ='01',iif(atc.FormulationType=2,atc.Volume,atc.Weight),1)), 3) QuantityUsedCampaign,
			round(sum(iif(ck.MovementType =2 and ck.EntityName = 'RawMaterialDevolution',ck.Quantity,0)/iif(pt.code ='01',iif(atc.FormulationType=2,atc.Volume,atc.Weight),1)), 3) QuantityDevolution,
			sum(iif(ck.MovementType =1 and ck.EntityName = 'Harnessed',ck.Quantity,0)) QuantityHarnessed,
			sum(iif(ck.EntityName IN('CampaignRawMaterial','CampaignDetailValidation'), (ck.Quantity*iif(ck.MovementType=2,-1,1)),0)) DirectMPQuantity,
			sum(iif(ck.EntityName = 'IndirectMPQuantity',(ck.Quantity*iif(ck.MovementType=2,-1,1)),0)) IndirectMPQuantityOperation,
			sum(iif(ck.EntityName = 'IndirectMPQuantity',ck.Quantity,0)) IndirectMPQuantity,
			sum(iif(ck.EntityName = 'QuantityRemaining',(ck.Quantity*iif(ck.MovementType =2,-1,1)),0)) QuantityRemaining
		FROM  MixingStation.CampaignKardex ck 
		join Inventory.InventoryProduct ip on ck.ProductId = ip.Id
		join inventory.ProductType pt on ip.ProductTypeId=pt.Id
		LEFT join Inventory.ATC atc on ip.ATCId =atc.Id
		left join  Inventory.InventoryMeasurementUnit imu on ck.MeasurementUnitId	= imu.Id
		GROUP by ip.Name,ip.Code,imu.Name,ck.CampaignDetailId,ip.id,imu.Id) as c

UNION All

SELECT 
		ck.CampaignDetailId,
		null ProductId,
		ip.Id CodeId,
		CONCAT(ip.Code,' - ',ip.Name) ProductCodeName,
		ck.MovementDate,
		iif(ck.MovementType = 1,'Entrada Campaña','Salida Campaña') MovementTypeName,
		imu.Name UnitMeasurement,
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
		sum(ck.Quantity) CKQuantity
FROM  MixingStation.CampaignKardex ck 
join Inventory.InventoryProduct ip on ck.ProductId = ip.Id
join inventory.ProductType pt on ip.ProductTypeId=pt.Id
LEFT join Inventory.ATC atc on ip.ATCId =atc.Id
left join  Inventory.InventoryMeasurementUnit imu on ck.MeasurementUnitId	= imu.Id
GROUP by ip.Name,ip.Code,imu.Name,ck.CampaignDetailId,ck.MovementDate,ck.MovementType,ip.Id,ck.Description,ck.CreationUser,imu.Id

	) K