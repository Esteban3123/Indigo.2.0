

CREATE view [MixingStation].[ViewCampaignKardex]
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
		SUM(crm.ExpendQuantity) Quantity
	FROM MixingStation.CampaignRawMaterial crm
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.Id = crm.RequestPackageDetailStatusId
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
	WHERE rmsd.Status <> 3
		AND rpds.Status <> 6
	GROUP BY rmsd.CampaignDetailId, crm.ProductValidationId
),
KardexSummary AS
(
	SELECT
		ck.CampaignDetailId,
		ip.Id ProductId,
		NULL CodeId,
		CONCAT(ip.Code,' - ',ip.Name) ProductCodeName,
		NULL MovementDate,
		'' MovementTypeName,
		imu.Name UnitMeasurement,
		imu.Id UnitMeasurementId,
		SUM(IIF(ck.MovementType = 1 AND ck.EntityName NOT IN ('Harnessed','IndirectMPQuantity'), ck.Quantity, 0)) QuantityUnitMeasurement,
		SUM(IIF(ck.MovementType = 2 AND ck.EntityName NOT IN ('RawMaterialDevolution','IndirectMPQuantity','CampaignRawMaterial'), ck.Quantity, 0)) OtherQuantityUsed,
		SUM(IIF(ck.MovementType = 2 AND ck.EntityName = 'RawMaterialDevolution', ck.Quantity, 0)) QuantityDevolutionUnitMeasurement,
		ROUND(SUM(IIF(ck.MovementType = 1, ck.Quantity, 0) / cf.ConversionFactor), 3) DeliveredQuantity,
		ROUND(SUM(IIF(ck.MovementType = 2 AND ck.EntityName = 'RawMaterialDevolution', ck.Quantity, 0) / cf.ConversionFactor), 3) QuantityDevolution,
		SUM(IIF(ck.MovementType = 1 AND ck.EntityName = 'Harnessed', ck.Quantity, 0)) QuantityHarnessed,
		SUM(IIF(ck.EntityName = 'CampaignDetailValidation', (ck.Quantity * IIF(ck.MovementType = 2, -1, 1)), 0)) DirectValidationQuantity,
		SUM(IIF(ck.EntityName = 'IndirectMPQuantity', (ck.Quantity * IIF(ck.MovementType = 2, -1, 1)), 0)) IndirectMPQuantityOperation,
		SUM(IIF(ck.EntityName = 'IndirectMPQuantity', ck.Quantity, 0)) IndirectMPQuantity,
		SUM(IIF(ck.EntityName = 'QuantityRemaining', (ck.Quantity * IIF(ck.MovementType = 2, -1, 1)), 0)) QuantityRemaining,
		cf.ConversionFactor
	FROM MixingStation.CampaignKardex ck
	JOIN Inventory.InventoryProduct ip ON ck.ProductId = ip.Id
	JOIN Inventory.ProductType pt ON ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ck.MeasurementUnitId = imu.Id
	LEFT JOIN CTE_Msclass ON ck.CampaignDetailId = CTE_Msclass.CampaignDetailId
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
	GROUP BY ip.Name, ip.Code, imu.Name, ck.CampaignDetailId, ip.Id, imu.Id, cf.ConversionFactor
),
Summary AS
(
	SELECT
		ks.CampaignDetailId,
		ks.ProductId,
		ks.CodeId,
		ks.ProductCodeName,
		ks.MovementDate,
		ks.MovementTypeName,
		ks.UnitMeasurement,
		ks.UnitMeasurementId,
		ks.QuantityUnitMeasurement,
		ks.OtherQuantityUsed + ISNULL(arm.Quantity, 0) QuantityUsed,
		ks.QuantityDevolutionUnitMeasurement,
		ks.DeliveredQuantity,
		CEILING((ks.OtherQuantityUsed + ISNULL(arm.Quantity, 0)) / ks.ConversionFactor) QuantityUsedCampaign,
		ROUND(ks.QuantityDevolutionUnitMeasurement / ks.ConversionFactor, 3) QuantityDevolution,
		ks.QuantityHarnessed,
		ks.DirectValidationQuantity - ISNULL(arm.Quantity, 0) DirectMPQuantity,
		ks.IndirectMPQuantityOperation,
		ks.IndirectMPQuantity,
		ks.QuantityRemaining
	FROM KardexSummary ks
	LEFT JOIN ActiveRawMaterial arm ON arm.CampaignDetailId = ks.CampaignDetailId
		AND arm.ProductId = ks.ProductId
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
		(c.QuantityUnitMeasurement + c.QuantityHarnessed - c.QuantityUsed - c.QuantityDevolutionUnitMeasurement + c.IndirectMPQuantityOperation) as CurrentQuantity,
		c.DeliveredQuantity,
		c.QuantityUsedCampaign,
		c.QuantityDevolution,
		(c.DeliveredQuantity - c.QuantityUsedCampaign - c.QuantityDevolution) as CampaignBalance,
		c.QuantityHarnessed,
		c.QuantityDevolutionUnitMeasurement,
		c.DirectMPQuantity,
		c.IndirectMPQuantity,
		c.QuantityRemaining,
		((c.QuantityUnitMeasurement + c.QuantityHarnessed) - (c.QuantityDevolutionUnitMeasurement + c.QuantityUsed) + c.IndirectMPQuantityOperation) RawMaterialQuantityBalance,
		'' UserMovementDescription,
		0 CKQuantity
	FROM Summary as c

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
FROM MixingStation.CampaignKardex ck
join Inventory.InventoryProduct ip on ck.ProductId = ip.Id
join inventory.ProductType pt on ip.ProductTypeId=pt.Id
LEFT join Inventory.ATC atc on ip.ATCId =atc.Id
left join Inventory.InventoryMeasurementUnit imu on ck.MeasurementUnitId	= imu.Id
GROUP by ip.Name,ip.Code,imu.Name,ck.CampaignDetailId,ck.MovementDate,ck.MovementType,ip.Id,ck.Description,ck.CreationUser,imu.Id

	) K

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Kardex consolidado de movimientos de materias primas e insumos por campaña de preparación en la estación de mezclas. Integra el catálogo de productos del inventario, sus tipos, clasificación ATC, unidades de medida, clase de dosis y materia prima preparada para mostrar, por cada detalle de campaña y producto, los totales de entradas, salidas, devoluciones, cantidades aprovechadas, materia prima directa e indirecta, saldos y balance de campaña. Excluye del consumo preparado las solicitudes y productos anulados, y para campañas NPT calcula la conversión usando volumen. Presenta dos niveles de información: un resumen acumulado por producto y el detalle cronológico de cada movimiento individual con su descripción y usuario responsable.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignKardex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignKardex';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el kardex de campañas de mezcla mostrando, por cada producto y detalle de campaña, los totales acumulados (entradas, salidas, devoluciones, aprovechamientos, MP directa/indirecta, remanentes) y el detalle cronológico de cada movimiento.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen movimientos en MixingStation.CampaignKardex con ProductId válido en Inventory.InventoryProduct.; Cada producto tiene ProductType asociado; los productos con ATC se enlazan opcionalmente a Inventory.ATC.; La unidad de medida del movimiento (MeasurementUnitId) puede ser nula (LEFT JOIN).; Si existe materia prima preparada, CampaignRawMaterial se relaciona con RequestPackageDetailStatus y RequestMixingStationDetail para identificar solicitudes/productos anulados.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'CurrentQuantity = QuantityUnitMeasurement + QuantityHarnessed - QuantityUsed - QuantityDevolutionUnitMeasurement + IndirectMPQuantityOperation.; CampaignBalance = DeliveredQuantity - QuantityUsedCampaign - QuantityDevolution.; RawMaterialQuantityBalance = QuantityUnitMeasurement + QuantityHarnessed - QuantityDevolutionUnitMeasurement - QuantityUsed + IndirectMPQuantityOperation.; QuantityUsed incluye salidas de kardex distintas a devoluciones/indirectas/CampaignRawMaterial más la materia prima preparada activa.; En NPT (MSClass=2), el factor de conversión siempre es ATC.Volume.; QuantityUsedCampaign redondea hacia arriba con CEILING para consumir unidades completas de campaña cuando hay uso parcial de una unidad física.; En filas de resumen, ProductId está informado (con ISNULL→1) y CodeId es NULL; en filas de detalle por movimiento, ProductId es NULL y CodeId contiene el Id del producto.; Las filas de detalle por movimiento siempre tienen agregados en cero salvo CKQuantity y la descripción concatenada con el usuario creador.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de campaña de mezcla; Materia prima directa e indirecta; Devolución de materia prima (RawMaterialDevolution); Aprovechamiento (Harnessed); Remanente (QuantityRemaining); Clasificación ATC; Tipo de formulación (peso/volumen); Unidad de medida de inventario; Entrada/Salida de campaña; Validación de detalle de campaña', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve dos bloques unidos por UNION ALL: (1) fila resumen agregada por CampaignDetailId+Producto+UM con saldos calculados y (2) filas por movimiento individual con descripción y usuario.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ck.MovementType = 1 y EntityName NOT IN (''Harnessed'',''IndirectMPQuantity'') → La cantidad suma a QuantityUnitMeasurement.; si ck.MovementType = 2 y EntityName NOT IN (''RawMaterialDevolution'',''IndirectMPQuantity'',''CampaignRawMaterial'') → La cantidad suma a OtherQuantityUsed.; si CampaignRawMaterial pertenece a una solicitud con RequestMixingStationDetail.Status <> 3 y RequestPackageDetailStatus.Status <> 6 → Su ExpendQuantity suma a QuantityUsed.; si CampaignRawMaterial pertenece a solicitud/producto anulado → No suma al consumo de resumen.; si ck.MovementType = 2 y EntityName = ''RawMaterialDevolution'' → La cantidad suma a QuantityDevolutionUnitMeasurement y QuantityDevolution.; si ProductType.Class = 2 y MSClass = 2 → Usa ATC.Volume como divisor de conversión.; si ProductType.Class = 2 y MSClass <> 2 → Usa ConcentrationQuantity para FormulationType=4, Volume para FormulationType=2 y Weight para los demás casos.; si ProductType.Class <> 2 → Usa factor 1.; QuantityUsedCampaign = CEILING((OtherQuantityUsed + materia prima preparada activa) / factor de conversión).; si ck.EntityName = ''Harnessed'' y MovementType=1 → La cantidad suma a QuantityHarnessed.; si ck.EntityName = ''CampaignDetailValidation'' → Suma a DirectMPQuantity con signo según MovementType.; si ck.EntityName = ''IndirectMPQuantity'' → Suma a IndirectMPQuantityOperation con signo según MovementType y a IndirectMPQuantity en valor absoluto.; si ck.EntityName = ''QuantityRemaining'' → Suma a QuantityRemaining con signo según MovementType.; si ck.MovementType = 1 (detalle) → MovementTypeName se muestra como ''Entrada Campaña''. else Si MovementType <> 1 se muestra como ''Salida Campaña''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignKardex; MixingStation.CampaignRawMaterial; MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.UnitDoseType; Inventory.InventoryProduct; Inventory.ProductType; Inventory.ATC; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignKardex';
GO
