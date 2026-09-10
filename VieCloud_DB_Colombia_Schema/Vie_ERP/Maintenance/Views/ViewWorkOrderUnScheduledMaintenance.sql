CREATE VIEW [Maintenance].[ViewWorkOrderUnScheduledMaintenance]
AS
	SELECT	CONCAT(mfr.Id, '-', mfrd.Id, '-', 2) AS Id,
			mfr.Id AS FailureRequestId,
			mfr.Code AS FailureRequestCode,
			ISNULL(mfrd.Description, mfr.Observation) Observation,
			CASE mfr.TypeRequest
				WHEN 1 THEN 'Falla General' 
				WHEN 2 THEN 'Revisión'
				WHEN 3 THEN 'Otro'
			END AS MaintenanceType,
			CASE 
				WHEN mfr.Status = 1 THEN 0
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 1
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 2
				WHEN mfr.Status = 3 THEN 3
			END ProgramState,
			CASE 
				WHEN mfr.Status = 1 THEN 'Registrado'
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 'Confirmado'
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 'Con Orden de Trabajo'
				WHEN mfr.Status = 3 THEN 'Anulado'
			END AS ProgramStateName,
			mfrd.Id AS FailureRequestDetailId,
			'' PartCodeDescription,
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fai.Id AS ItemId,
			fai.ItemTypeId,
			CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
			fait.InventoryTypeId,
			fal.Id AS LocationId,
			CONCAT(fal.code, ' - ', fal.Name) AS LocationCodeName,
			bo.Id AS BranchOfficeId,
			CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
			CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
			wo.Id AS WorkOrderId,
			wo.Consecutive AS WorkOrderCode,
			ISNULL(wo.RequestDate, mfr.DateFailure) AS RequestDate,
			ISNULL(wo.ProgramDate, mfr.DateFailure) AS ProgramDate,
			ISNULL(wo.State, 1) WorkOrderState,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(tp.Nit, ' - ', tp.Name) AS ResponsibleCodeName,
			mr.ResponsibleRole
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON mfrd.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON 'MaintenanceFailureRequest' = wo.EntityName AND mfrd.Id = wo.EntityId
	LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfrd.TransactionClass = 1
UNION ALL
	SELECT	CONCAT(mfr.Id, '-', mfrd.Id, '-', 2) AS Id,
			mfr.Id AS FailureRequestId,
			mfr.Code AS FailureRequestCode,
			ISNULL(mfrd.Description, mfr.Observation) Observation,
			CASE mfr.TypeRequest
				WHEN 1 THEN 'Falla General' 
				WHEN 2 THEN 'Revisión'
				WHEN 3 THEN 'Otro'
			END AS MaintenanceType,
			CASE 
				WHEN mfr.Status = 1 THEN 0
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 1
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 2
				WHEN mfr.Status = 3 THEN 3
			END ProgramState,
			CASE 
				WHEN mfr.Status = 1 THEN 'Registrado'
				WHEN mfr.Status = 2 AND wo.Id IS NULL THEN 'Confirmado'
				WHEN mfr.Status = 2 AND wo.Id IS NOT NULL THEN 'Con Orden de Trabajo'
				WHEN mfr.Status = 3 THEN 'Anulado'
			END AS ProgramStateName,
			mfrd.Id AS FailureRequestDetailId,
			CONCAT(fapac.Code, ' - ', fapac.Name) PartCodeDescription,
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fai.Id AS ItemId,
			fai.ItemTypeId,
			CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
			fait.InventoryTypeId,
			fal.Id AS LocationId,
			CONCAT(fal.code, ' - ', fal.Name) AS LocationCodeName,
			bo.Id AS BranchOfficeId,
			CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
			CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
			wo.Id AS WorkOrderId,
			wo.Consecutive AS WorkOrderCode,
			ISNULL(wo.RequestDate, mfr.DateFailure) AS RequestDate,
			ISNULL(wo.ProgramDate, mfr.DateFailure) AS ProgramDate,
			ISNULL(wo.State, 1) WorkOrderState,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(tp.Nit, ' - ', tp.Name) AS ResponsibleCodeName,
			mr.ResponsibleRole
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
	JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON fapap.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON 'MaintenanceFailureRequest' = wo.EntityName AND mfrd.Id = wo.EntityId
	LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfrd.TransactionClass = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las órdenes de trabajo de mantenimiento no programado (correctivo), originadas a partir de solicitudes de falla o avería reportadas sobre activos fijos. Integra la solicitud de falla (con su tipo: Falla General, Revisión u Otro) y su detalle —ya sea sobre el activo físico completo o sobre una parte, accesorio o consumible específico— con información del bien (placa, serie, modelo, ítem, tipo de ítem, marca), su ubicación física, unidad funcional y sede. Muestra el estado del proceso (Registrado, Confirmado, Con Orden de Trabajo, Anulado), el código de la orden de trabajo asociada si existe, las fechas de solicitud y programación, el protocolo de mantenimiento aplicado y el responsable de la ejecución con su NIT. Es la fuente principal para reportes y seguimiento del mantenimiento correctivo no planificado de activos fijos en la organización.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrderUnScheduledMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrderUnScheduledMaintenance';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las solicitudes de falla de mantenimiento (correctivo/no programado) con su activo físico o parte afectada, ubicación, sucursal y la orden de trabajo asociada (si existe), exponiendo el estado de programación.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de solicitud (MaintenanceFailureRequestDetail) debe tener TransactionClass = 1 (activo físico) o 2 (parte/accesorio/consumible) para ser incluido.; Para TransactionClass=1 debe existir el activo físico referenciado en FixedAssetPhysicalAsset; para TransactionClass=2 debe existir la parte en FixedAssetPhysicalAssetParts y su catálogo en FixedAssetPartsAccesoriesConsumables.; El activo físico debe tener ítem, tipo de ítem, ubicación, unidad funcional, sucursal y marca registrados (joins INNER obligatorios).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La orden de trabajo se enlaza únicamente cuando wo.EntityName = ''MaintenanceFailureRequest'' y wo.EntityId = mfrd.Id (vínculo polimórfico al detalle, no al encabezado).; El identificador de fila siempre termina en ''-2'' (sufijo fijo) independientemente de la TransactionClass.; La observación mostrada prioriza el detalle (mfrd.Description) y, si es nula, usa la observación del encabezado (mfr.Observation).; Sólo se exponen solicitudes de mantenimiento NO programado (correctivo por falla); el universo está limitado a registros de MaintenanceFailureRequest.; La sucursal del activo se deriva siempre vía Location → FunctionalUnit → BranchOffice (no directamente del activo).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de falla de mantenimiento; Mantenimiento correctivo / no programado; Orden de trabajo; Activo físico; Partes, accesorios y consumibles; Protocolo de mantenimiento; Responsable de mantenimiento; Tercero; Sucursal y unidad funcional; Estado de programación (Registrado / Confirmado / Con Orden de Trabajo / Anulado); Tipo de solicitud (Falla General / Revisión / Otro)', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewWorkOrderUnScheduledMaintenance: Devuelve filas con Id sintético CONCAT(mfr.Id,''-'',mfrd.Id,''-'',2); la rama 1 (TransactionClass=1) deja PartCodeDescription vacío y la rama 2 (TransactionClass=2) lo arma con código y nombre de la parte/accesorio/consumible.; [RETURN_RESULT] Maintenance.ViewWorkOrderUnScheduledMaintenance: Cuando la solicitud no tiene WorkOrder asociada, RequestDate y ProgramDate se sustituyen por mfr.DateFailure y WorkOrderState toma el valor 1 por defecto.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mfrd.TransactionClass = 1 → Une el detalle directamente con FixedAssetPhysicalAsset (activo físico completo) y deja PartCodeDescription vacío.; si mfrd.TransactionClass = 2 → Une el detalle con FixedAssetPhysicalAssetParts y FixedAssetPartsAccesoriesConsumables para identificar la parte/accesorio/consumible afectado y poblar PartCodeDescription.; si mfr.TypeRequest = 1 / 2 / 3 → Etiqueta MaintenanceType como ''Falla General'', ''Revisión'' u ''Otro'' respectivamente.; si mfr.Status = 1 → ProgramState=0, ProgramStateName=''Registrado''.; si mfr.Status = 2 AND wo.Id IS NULL → ProgramState=1, ProgramStateName=''Confirmado'' (solicitud confirmada sin orden de trabajo).; si mfr.Status = 2 AND wo.Id IS NOT NULL → ProgramState=2, ProgramStateName=''Con Orden de Trabajo''.; si mfr.Status = 3 → ProgramState=3, ProgramStateName=''Anulado''.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.MaintenanceFailureRequest; Maintenance.MaintenanceFailureRequestDetail; Maintenance.WorkOrder; Maintenance.MaintenanceProtocol; Maintenance.MaintenanceResponsible; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetParts; FixedAsset.FixedAssetPartsAccesoriesConsumables; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetTrademark; Payroll.FunctionalUnit; Payroll.BranchOffice; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderUnScheduledMaintenance';
GO
