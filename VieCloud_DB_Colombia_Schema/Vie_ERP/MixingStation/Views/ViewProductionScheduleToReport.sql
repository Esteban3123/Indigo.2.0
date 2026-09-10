CREATE view [MixingStation].[ViewProductionScheduleToReport]
As

with CampaignValidation as (
	select cdv.Id
		, pr.ATCId
		, pr.SupplieId
		, cdv.CampaignDetailId
		, cdv.DeliveredQuantity
		, cdv.DevolutionQuantity
		, bs.BatchCode
	from MixingStation.CampaignDetailValidation cdv with(nolock)
	inner join Inventory.InventoryProduct pr with(nolock) on cdv.ProductId = pr.Id
	inner join Inventory.BatchSerial bs with(nolock) on cdv.BatchSerialId = bs.Id
)
SELECT 
	cdi.Id
	, cd.Id as CampaignDetailId
	, ISNULL([ip].ATCId, cdi.AtcId) as AtcId
	, ISNULL([ip].SupplieId, cdi.SupplyId) as SupplyId
	, cdi.ProductId as ProductId--_Eliminar
	, cdi.ItemType
	, atc.Concentration
	,	case 
			when cdi.AtcId is not null then concat(atc.Code, ' - ', atc.Name)
			when cdi.SupplyId is not null then concat(su.Code, ' - ', su.SupplieName)

			when patc.Id is not null then concat(patc.Code, ' - ', patc.Name)
			when psu.Id is not null then concat(psu.Code, ' - ', psu.SupplieName)

			else concat([ip].Code, ' - ', [ip].Name)
		end as ItemCodeName
	,	STUFF((
			SELECT ', ' + [BatchCode]
			FROM CampaignValidation
			WHERE CampaignDetailId = cd.Id And 
				(ATCId = cdi.AtcId or ATCId = ip.ATCId or SupplieId = cdi.SupplyId or SupplieId = ip.SupplieId)
			FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
		  ,1,2,'') AS BatchCodes
	, Count(cv.BatchCode) as CantidadLotes -- 
	, cdi.RequestQuantity as RequestQuantities
	, Sum(cv.DeliveredQuantity) as DispensingQuantities
	, 0 as IsCold
	, 1 as IsEnvironment
	, 0 as AditionalQuantities
	, sum(rmdd.Quantity) as DevolutionQuantities
	, ps.CreationDate as ProductionDate
	, ps.Code as ProductionScheduleCode
	, udt.Description as UnitDoseTypeName
	, ps.CreationUser as UserCode
	--, ' ----------- ' as Separator
	--, cdi.* 
FROM MixingStation.ProductionSchedule ps with(nolock)
inner join MixingStation.ProductionScheduleDetail psd with(nolock) on psd.ProductionScheduleId = ps.Id
inner join MixingStation.CampaignDetail cd with(nolock) on psd.CampaignDetailId = cd.Id
inner join MixingStation.CampaignDetailItems cdi with(nolock) on cdi.CampaignDetailId = cd.Id
inner join MixingStation.UnitDoseType udt with(nolock) on cd.UnitDoseTypeId = udt.Id
left join Inventory.ATC atc with(nolock) on cdi.AtcId = atc.Id
left join Inventory.InventorySupplie su with(nolock) on cdi.SupplyId = su.Id
left join Inventory.InventoryProduct [ip] with(nolock) on cdi.ProductId = [ip].Id

left join Inventory.ATC patc with(nolock) on ip.ATCId = patc.Id
left join Inventory.InventorySupplie psu with(nolock) on ip.SupplieId = psu.Id

left join CampaignValidation cv on cv.CampaignDetailId = cd.Id And 
	(cv.ATCId = cdi.AtcId or cv.ATCId = ip.ATCId or cv.SupplieId = cdi.SupplyId or cv.SupplieId = ip.SupplieId)
left join MixingStation.RawMaterialDevolutionDetail rmdd on rmdd.CampaignDetailValidationId = cv.Id
where psd.CampaignDetailId = 241
group by cdi.Id
	, cd.Id
	, ip.ATCId
	, cdi.AtcId
	, ip.SupplieId
	, cdi.SupplyId
	, cdi.ProductId
	, cdi.ItemType
	, atc.Concentration
	, atc.Code, atc.Name
	, su.Code, su.SupplieName
	, ip.Code, ip.Name
	, ps.CreationUser
	, ps.Code
	, udt.Description
	, ps.CreationDate
	, cdi.RequestQuantity
	, patc.Code
	, patc.Name
	, psu.Code
	, psu.SupplieName
	, psu.Id
	, patc.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte para el cronograma de producción de la estación de mezclas farmacéuticas. Consolida, para cada ítem de una campaña de preparación, la información del producto dispensado (medicamento ATC o insumo), su concentración, los lotes utilizados, las cantidades solicitadas, dispensadas y devueltas, junto con la fecha y código del cronograma de producción y el tipo de dosis unitaria. Integra el detalle de validación de campaña (CampaignDetailValidation) con los lotes de inventario (BatchSerial), el catálogo de productos (InventoryProduct, ATC), el detalle de ítems de campaña (CampaignDetailItems) y las devoluciones de materia prima, permitiendo generar reportes operativos y de trazabilidad del proceso de preparación y dispensación en la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewProductionScheduleToReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewProductionScheduleToReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida, para un detalle de campaña específico, la información de programación de producción de la estación de mezclas con sus ítems, lotes entregados, cantidades solicitadas, dispensadas y devueltas, para reportería.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un ProductionSchedule con su ProductionScheduleDetail relacionado al CampaignDetail filtrado.; El CampaignDetail debe tener al menos un CampaignDetailItems y un UnitDoseType definido.; Las validaciones de entrega (CampaignDetailValidation) deben tener ProductId con InventoryProduct y BatchSerialId con BatchSerial existentes para ser incluidas en CampaignValidation.; El filtro fijo psd.CampaignDetailId = 241 debe corresponder a un detalle de campaña existente para devolver filas (la vista está cableada a ese Id).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ítems vinculados a un cronograma de producción real (JOIN obligatorio con ProductionSchedule y ProductionScheduleDetail).; Los lotes (BatchCodes) y validaciones consideradas pertenecen al mismo CampaignDetail y deben coincidir por ATC o por insumo entre el ítem de campaña y el producto de inventario.; Las banderas de almacenamiento se entregan fijas: IsCold=0, IsEnvironment=1 y AditionalQuantities=0 (no se calculan a partir de datos).; El AtcId/SupplyId del resultado prioriza siempre el del InventoryProduct sobre el declarado en el ítem de campaña (ISNULL(ip.x, cdi.x)).; Las cantidades dispensadas y devueltas se agregan (SUM) por ítem de detalle de campaña.; BatchCodes se materializa como una lista separada por comas mediante STUFF + FOR XML PATH.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'campaña de preparación; estación de mezclas; programación de producción; detalle de campaña; validación de entrega; devolución de materia prima; lote (BatchCode); ATC; insumo; producto de inventario; dosis unitaria; cantidad solicitada; cantidad dispensada; cantidad devuelta', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewProductionScheduleToReport: Devuelve una fila por ítem de campaña (cdi.Id) agrupando lotes en BatchCodes y sumando DeliveredQuantity y Quantity de devoluciones, restringido por ''where psd.CampaignDetailId = 241''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cdi.AtcId IS NOT NULL → ItemCodeName se arma con código y nombre del ATC (atc.Code - atc.Name); si cdi.SupplyId IS NOT NULL → ItemCodeName se arma con código y nombre del insumo (su.Code - su.SupplieName); si el ítem es un producto cuyo InventoryProduct tiene ATCId asociado (patc.Id IS NOT NULL) → ItemCodeName se resuelve usando el ATC del producto (patc.Code - patc.Name); si el InventoryProduct tiene SupplieId asociado (psu.Id IS NOT NULL) → ItemCodeName se resuelve usando el insumo del producto (psu.Code - psu.SupplieName) else Se usa código y nombre del propio producto de inventario (ip.Code - ip.Name); si ISNULL([ip].ATCId, cdi.AtcId) / ISNULL([ip].SupplieId, cdi.SupplyId) → El AtcId/SupplyId reportado prioriza el del producto de inventario; si el ítem no es producto, usa el del propio detalle de campaña', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetailValidation; Inventory.InventoryProduct; Inventory.BatchSerial; MixingStation.ProductionSchedule; MixingStation.ProductionScheduleDetail; MixingStation.CampaignDetail; MixingStation.CampaignDetailItems; MixingStation.UnitDoseType; Inventory.ATC; Inventory.InventorySupplie; MixingStation.RawMaterialDevolutionDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewProductionScheduleToReport';
GO
