

CREATE view [MixingStation].[ViewProcessSetting] 
as 

WITH CTE_Msclass AS
(
	SELECT
		CASE WHEN SUM(IIF(udt.MSClass = 2, 1, 0)) > 0 THEN 2 ELSE MAX(udt.MSClass) END MSClass,
		rmsd.CampaignDetailId
	FROM MixingStation.UnitDoseType udt
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.UnitDoseTypeId = udt.Id
	WHERE rmsd.CampaignDetailId IS NOT NULL
	GROUP BY rmsd.CampaignDetailId
),
ActiveRawMaterial AS
(
	SELECT
		rmsd.CampaignDetailId,
		crm.ProductValidationId ProductId,
		crm.BatchSerialId,
		SUM(crm.ExpendQuantity) Quantity
	FROM MixingStation.CampaignRawMaterial crm
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.Id = crm.RequestPackageDetailStatusId
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
	WHERE rmsd.Status <> 3
		AND rpds.Status <> 6
	GROUP BY rmsd.CampaignDetailId, crm.ProductValidationId, crm.BatchSerialId
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
				SUM(IIF(ck.MovementType =2 and ck.EntityName NOT IN('RawMaterialDevolution','IndirectMPQuantity','CampaignRawMaterial'),ck.Quantity,0)) + MAX(ISNULL(arm.Quantity, 0)),
				SUM([MixingStation].[CalculateQuantityTypeMSclass]((IIF(ck.MovementType =2 and ck.EntityName NOT IN('RawMaterialDevolution','IndirectMPQuantity','CampaignRawMaterial'),ck.Quantity,0)),CTE_Msclass.MSClass,atc.Weight)) +
					[MixingStation].[CalculateQuantityTypeMSclass](MAX(ISNULL(arm.Quantity, 0)),CTE_Msclass.MSClass,ISNULL(NULLIF(MAX(atc.Weight), 0), 1))) QuantityUsed,
			SUM(IIF(ck.MovementType =2 and ck.EntityName = 'RawMaterialDevolution',ck.Quantity,0)) QuantityDevolutionUnitMeasurement,
			SUM(IIF(ck.MovementType =1,ck.Quantity,0)/cf.ConversionFactor) DeliveredQuantity,
			CEILING((SUM(IIF(ck.MovementType =2 and ck.EntityName NOT IN('RawMaterialDevolution','IndirectMPQuantity','CampaignRawMaterial'),ck.Quantity,0)) + MAX(ISNULL(arm.Quantity, 0)))/cf.ConversionFactor) QuantityUsedCampaign,
			SUM(IIF(ck.MovementType =2 and ck.EntityName = 'RawMaterialDevolution',ck.Quantity,0)/cf.ConversionFactor) QuantityDevolution,
			SUM(IIF(ck.MovementType =1 and ck.EntityName = 'Harnessed',ck.Quantity,0)) QuantityHarnessed,
			IIF(ip.SupplieId IS NOT NULL, 
				MAX(ISNULL(arm.Quantity, 0)),
				[MixingStation].[CalculateQuantityTypeMSclass](MAX(ISNULL(arm.Quantity, 0)),CTE_Msclass.MSClass,ISNULL(NULLIF(MAX(atc.Weight), 0), 1))) DirectMPQuantity,
			SUM(IIF(ck.EntityName = 'IndirectMPQuantity',(ck.Quantity*IIF(ck.MovementType=2,-1,1)),0)) IndirectMPQuantityOperation,
			SUM(IIF(ck.EntityName = 'IndirectMPQuantity',ck.Quantity,0)) IndirectMPQuantity,
			SUM(IIF(ck.EntityName = 'QuantityRemaining',(ck.Quantity*IIF(ck.MovementType =2,-1,1)),0)) QuantityRemaining,
			ck.BatchSerialId,
			b.BatchCode,
			atc.Id ATCId
		FROM  MixingStation.CampaignKardex ck WITH(NOLOCK)			
		JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on ck.ProductId = ip.Id
		JOIN inventory.ProductType pt WITH(NOLOCK) on ip.ProductTypeId=pt.Id
		JOIN CTE_Msclass ON CTE_Msclass.CampaignDetailId = ck.CampaignDetailId
		LEFT JOIN ActiveRawMaterial arm ON arm.CampaignDetailId = ck.CampaignDetailId
			AND arm.ProductId = ck.ProductId
			AND ISNULL(arm.BatchSerialId, 0) = ISNULL(ck.BatchSerialId, 0)
		LEFT JOIN Inventory.BatchSerial b WITH(NOLOCK) on ck.BatchSerialId=b.Id
		LEFT JOIN Inventory.ATC atc WITH(NOLOCK) on ip.ATCId =atc.Id
		LEFT JOIN  Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) on ck.MeasurementUnitId	= imu.Id
		CROSS APPLY (
			SELECT CAST(
				IIF(pt.Class = 2,
					IIF(CTE_Msclass.MSClass = 2,
						ISNULL(NULLIF(atc.Volume, 0), 1),
						IIF(atc.FormulationType = 4,
							ISNULL(NULLIF(atc.ConcentrationQuantity, 0), 1),
							IIF(atc.FormulationType = 2,
								ISNULL(NULLIF(atc.Volume, 0), 1),
								ISNULL(NULLIF(atc.Weight, 0), 1)
							)
						)
					),
					1
				) AS DECIMAL(20, 6)
			) ConversionFactor
		) cf
		GROUP by ip.Name,ip.Code, ip.SupplieId,imu.Name,ck.CampaignDetailId,ip.id,imu.Id,ck.BatchSerialId,b.BatchCode,atc.Id, atc.Presentations, CTE_Msclass.MSClass, cf.ConversionFactor) c

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
JOIN Inventory.InventoryProduct ip WITH(NOLOCK)  on ck.ProductId = ip.Id
JOIN inventory.ProductType pt WITH(NOLOCK) on ip.ProductTypeId=pt.Id
JOIN CTE_Msclass WITH(NOLOCK ) ON CTE_Msclass.CampaignDetailId = ck.CampaignDetailId
LEFT JOIN Inventory.ATC atc WITH(NOLOCK) on ip.ATCId =atc.Id
LEFT JOIN  Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) on ck.MeasurementUnitId	= imu.Id
GROUP by ip.SupplieId, ip.Name,ip.Code,imu.Name,ck.CampaignDetailId,ck.MovementDate,ck.MovementType,ip.Id,ck.Description,ck.CreationUser,imu.Id, ck.BatchSerialId, atc.Id, CTE_Msclass.MSClass, atc.Presentations) K

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de configuración y seguimiento de proceso de una campaña de preparación en la estación de mezclas (mixing station). Integra el kardex de movimientos de la campaña con el catálogo de productos de inventario, tipos de producto, lotes/seriales, clasificación ATC, unidades de medida y materia prima preparada activa para calcular saldos y cantidades por producto. Excluye del consumo de preparación las solicitudes anuladas y productos anulados, evitando duplicar CampaignRawMaterial desde kardex. En campañas NPT utiliza siempre el volumen como factor de conversión. Identifica si cada ítem es el medicamento principal del paquete, si permite remanente, y clasifica cada ítem por tipo (Principal, Canasta, Otro, Solicitudes Manuales).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewProcessSetting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewProcessSetting';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida, por detalle de campaña y producto, los saldos y movimientos del kardex de la estación de mezclas (entradas, salidas, devoluciones, aprovechamientos, MP directa/indirecta y remanentes), clasificando cada ítem como Principal, Canasta, Otro o Solicitudes Manuales.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada CampaignDetailId debe tener al menos un RequestMixingStationDetail asociado a un UnitDoseType para resolver su MSClass (vía CTE_Msclass, JOIN INNER).; Los productos del kardex deben existir en Inventory.InventoryProduct y tener ProductType definido.; La función MixingStation.CalculateQuantityTypeMSclass debe estar disponible para convertir cantidades según MSClass y peso del ATC.; Si existe materia prima preparada, CampaignRawMaterial se relaciona con RequestPackageDetailStatus y RequestMixingStationDetail para excluir solicitudes/productos anulados.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'CurrentQuantity = QuantityUnitMeasurement + QuantityHarnessed - QuantityUsed - QuantityDevolutionUnitMeasurement + IndirectMPQuantityOperation.; CampaignBalance = DeliveredQuantity - QuantityUsedCampaign - QuantityDevolution.; RawMaterialQuantityBalance = (QuantityUnitMeasurement + QuantityHarnessed) - (QuantityDevolutionUnitMeasurement + QuantityUsed) + IndirectMPQuantityOperation.; QuantityUsed combina salidas de kardex distintas a devoluciones/indirectas/CampaignRawMaterial más CampaignRawMaterial activo.; CampaignRawMaterial activo excluye RequestMixingStationDetail.Status=3 y RequestPackageDetailStatus.Status=6.; En NPT (MSClass=2), DeliveredQuantity, QuantityUsedCampaign y QuantityDevolution usan ATC.Volume como factor.; QuantityUsedCampaign redondea con CEILING para representar unidades físicas consumidas cuando hay uso parcial.; ProductId nunca es NULL en la salida: si el origen es NULL, se reemplaza por 1 (ISNULL(K.ProductId,1)).; En filas de detalle los campos de saldo agregados se devuelven en 0/NULL; solo CKQuantity refleja la cantidad del movimiento.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewProcessSetting: Devuelve dos bloques unidos con UNION ALL: (1) un agregado por producto/lote con saldos calculados; (2) un detalle fila-a-fila de cada movimiento del kardex con su descripción y usuario, etiquetado como ''Entrada Campaña'' (MovementType=1) o ''Salida Campaña'' (MovementType=2).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ip.SupplieId IS NOT NULL (el producto es un insumo) → QuantityUnitMeasurement, QuantityUsed, DirectMPQuantity y CKQuantity se expresan sin conversión de MSClass else Se aplica MixingStation.CalculateQuantityTypeMSclass para cantidades operativas.; si ip.SupplieId IS NULL y CTE_Msclass.MSClass = 5 → UnitMeasurement toma atc.Presentations else UnitMeasurement toma imu.Name.; si ProductType.Class = 2 y MSClass = 2 → DeliveredQuantity, QuantityUsedCampaign y QuantityDevolution se dividen por ATC.Volume.; si ProductType.Class = 2 y MSClass <> 2 → Usa ConcentrationQuantity para FormulationType=4, Volume para FormulationType=2 y Weight para los demás casos.; si ProductType.Class <> 2 → Usa factor 1.; si ck.MovementType = 1 AND EntityName NOT IN (''Harnessed'',''IndirectMPQuantity'') → Suma a QuantityUnitMeasurement.; si ck.MovementType = 2 AND EntityName NOT IN (''RawMaterialDevolution'',''IndirectMPQuantity'',''CampaignRawMaterial'') → Suma al consumo de kardex.; si CampaignRawMaterial tiene RequestMixingStationDetail.Status <> 3 y RequestPackageDetailStatus.Status <> 6 → Su ExpendQuantity suma a QuantityUsed y DirectMPQuantity.; si CampaignRawMaterial pertenece a solicitud/producto anulado → No suma al consumo de resumen.; si ck.MovementType = 2 AND EntityName = ''RawMaterialDevolution'' → Suma a QuantityDevolutionUnitMeasurement y QuantityDevolution.; QuantityUsedCampaign = CEILING((consumo kardex + materia prima activa) / factor de conversión).; si ck.EntityName = ''IndirectMPQuantity'' → Suma con signo según MovementType en IndirectMPQuantityOperation y suma absoluta en IndirectMPQuantity.; si ck.EntityName = ''QuantityRemaining'' → Suma con signo según MovementType en QuantityRemaining.; IsMain, AllowsRemanent e ItemType conservan sus reglas de búsqueda por campaña/producto/lote.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'MixingStation.CalculateQuantityTypeMSclass', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignKardex; MixingStation.CampaignRawMaterial; MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.UnitDoseType; MixingStation.PackageDetail; MixingStation.ViewListMedicinesProduction; MixingStation.CampaignDetailValidation; Inventory.InventoryProduct; Inventory.ProductType; Inventory.BatchSerial; Inventory.ATC; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProcessSetting';
GO
