CREATE VIEW [Maintenance].[ViewWorkOrderScheduledMaintenance]
AS
	SELECT	CONCAT(mpam.Id, '-', mpp.Id, '-', 2) AS Id,
			mpam.Id AS ProgramingId,
			CASE mpp.ProgramType
				WHEN 1 THEN mpam.ObservationMaintenance
				WHEN 2 THEN mpam.ObservationsMetrology
			END Observation,
			mpp.Id AS MaintenancePlanProgramatedId,
			CASE mpp.ProgramType
				WHEN 1 THEN 'Mantenimiento' 
				WHEN 2 THEN 'Metrología'
			END AS MaintenanceType,
			mpp.State ProgramState,
			CASE mpp.State
				WHEN 1 THEN 'Programado' 
				WHEN 2 THEN 'Con Orden de Trabajo'
				WHEN 3 THEN 'Anulado'
			END AS ProgramStateName,
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
			ISNULL(wo.RequestDate, mpp.DateProgramated) AS RequestDate,
			ISNULL(wo.ProgramDate, mpp.DateProgramated) AS ProgramDate,
			ISNULL(wo.State, 1) WorkOrderState,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id AS MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(tp.Nit, ' - ', tp.Name) AS ResponsibleCodeName,
			mr.ResponsibleRole
	FROM Maintenance.MaintenancePlanAndMetrology mpam WITH (NOLOCK)
	JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON mpam.Id = mpp.MaintenancePlanAndMetrologyId
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON mpp.FixedAssetPhysicalId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON IIF(mpp.ProgramType = 1, mpam.ProtocolMaintenanceId, mpam.ProtocolMetrologyId) = mp.Id
	JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON IIF(mpp.ProgramType = 1, mpam.ResponsibleMaintenanceId, mpam.ResponsibleMetrologyId) = mr.Id
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON 'MaintenancePlanProgramated' = wo.EntityName AND mpp.Id = wo.EntityId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los mantenimientos y metrologías programados para activos fijos, integrando el plan de mantenimiento, el activo físico (placa, serie, modelo), el ítem y su tipo, la ubicación y sede, la marca, el protocolo aplicado y el responsable. Para cada programación indica si es de tipo Mantenimiento o Metrología, el estado del programa (Programado, Con Orden de Trabajo, Anulado) y vincula la orden de trabajo generada si existe, mostrando fechas de solicitud y programación. Sirve como fuente principal para reportes y consultas de órdenes de trabajo de mantenimiento preventivo programado, permitiendo rastrear qué equipo o bien requiere atención, en qué sede y unidad funcional se encuentra, quién es el responsable y bajo qué protocolo se ejecuta el mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrderScheduledMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrderScheduledMaintenance';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de programaciones de mantenimiento y metrología sobre activos fijos físicos, mostrando datos del activo, ubicación, sucursal, protocolo, responsable y la orden de trabajo asociada (si existe).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada MaintenancePlanProgramated debe tener un MaintenancePlanAndMetrology padre, un activo físico, un ítem, tipo de ítem, ubicación, unidad funcional, sucursal y marca válidos (todas son JOIN internos).; El protocolo y responsable seleccionados dependen del ProgramType: si es 1 se usan los de mantenimiento, si es 2 los de metrología; ambos deben existir en sus tablas.; El tercero (ThirdParty) del responsable de mantenimiento debe existir.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de fila siempre lleva sufijo ''-2'', diferenciando esta vista de otras variantes (p.ej. histórica o ejecutada).; Solo se incluyen programaciones cuyo activo físico tiene ítem, tipo de ítem, ubicación con unidad funcional y sucursal, marca, protocolo y responsable existentes (los JOINs son INNER).; ProgramType solo se interpreta para los valores 1 (Mantenimiento) y 2 (Metrología); cualquier otro valor produce campos derivados nulos.; La relación con WorkOrder se establece exclusivamente por la pareja (EntityName=''MaintenancePlanProgramated'', EntityId=mpp.Id).; Cuando no hay orden de trabajo, se asume estado 1 (''Programado'') por defecto.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mantenimiento preventivo/correctivo; Metrología; Programación de mantenimiento; Activo fijo físico; Protocolo de mantenimiento; Responsable de mantenimiento; Orden de trabajo; Sucursal / unidad funcional; Marca y placa de activo', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewWorkOrderScheduledMaintenance: Devuelve una fila por combinación plan-programación, con Id sintético CONCAT(mpam.Id,''-'',mpp.Id,''-'',2).; [RETURN_RESULT] Maintenance.ViewWorkOrderScheduledMaintenance: Si no existe WorkOrder asociada (LEFT JOIN sobre EntityName=''MaintenancePlanProgramated'' y EntityId=mpp.Id), RequestDate y ProgramDate caen a mpp.DateProgramated y WorkOrderState a 1.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mpp.ProgramType = 1 → Observation = mpam.ObservationMaintenance; MaintenanceType = ''Mantenimiento''; protocolo = mpam.ProtocolMaintenanceId; responsable = mpam.ResponsibleMaintenanceId. else ProgramType = 2: Observation = mpam.ObservationsMetrology; MaintenanceType = ''Metrología''; protocolo = mpam.ProtocolMetrologyId; responsable = mpam.ResponsibleMetrologyId.; si mpp.State = 1 / 2 / 3 → ProgramStateName se etiqueta como ''Programado'' / ''Con Orden de Trabajo'' / ''Anulado'' respectivamente.; si Existe WorkOrder con EntityName=''MaintenancePlanProgramated'' y EntityId=mpp.Id → Se exponen su Id, Consecutive, RequestDate, ProgramDate y State. else WorkOrderId/WorkOrderCode quedan nulos; fechas se reemplazan por mpp.DateProgramated y WorkOrderState por 1.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.MaintenancePlanAndMetrology; Maintenance.MaintenancePlanProgramated; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetLocation; Payroll.FunctionalUnit; Payroll.BranchOffice; FixedAsset.FixedAssetTrademark; Maintenance.MaintenanceProtocol; Maintenance.MaintenanceResponsible; Common.ThirdParty; Maintenance.WorkOrder', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderScheduledMaintenance';
GO
