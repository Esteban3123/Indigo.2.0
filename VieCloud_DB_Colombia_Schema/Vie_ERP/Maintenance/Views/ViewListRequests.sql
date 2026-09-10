CREATE VIEW [Maintenance].[ViewListRequests]
AS
	SELECT	mfrd.Id,
			mfr.OperatingUnitId,
			mfr.Code,
			mfr.DateFailure,
			mfr.BranchOfficeId,
			'Activo' TypeDescription,
			fapa.Id PhysicalAssetId,
			fapa.Plate,
			fai.ItemCatalogId,
			CONCAT(faic.Code, ' - ', faic.Description) ItemCatalogCodeDescription,
			CONCAT(fai.Code, ' - ', fai.Description) ItemCodeDescription,
			CONCAT(fal.Code, ' - ', fal.Name) LocationCodeName,
			NULL PartCodeDescription,
			CONCAT(p.Identification, ' - ', p.Fullname) RequestUser,
			mr.Id MaintenanceResponsibleId,
			mr.ResponsibleRole,
			CONCAT(tp.Nit, ' - ', tp.Name) AssignUser,
			wo.Id WorkOrderId,
			wo.Consecutive WorkOrderCode,
			ISNULL(wo.ProgramDate, Common.GETDATE()) ProgramDate,
			COALESCE(wo.Description, mfrd.Description, mfr.Observation) Description,
			wo.State WorkOrderStatus
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON mfrd.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK) ON fai.ItemCatalogId = faic.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Security.[User] u ON mfr.CreationUser = u.UserCode
	LEFT JOIN Security.Person p ON u.IdPerson = p.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON mfrd.Id = wo.EntityId AND 'MaintenanceFailureRequest' = wo.EntityName
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfr.Status = 2 AND mfrd.TransactionClass = 1
UNION ALL
	SELECT	mfrd.Id,
			mfr.OperatingUnitId,
			mfr.Code,
			mfr.DateFailure,
			mfr.BranchOfficeId,
			'Parte de Activo' TypeDescription,
			fapa.Id PhysicalAssetId,
			fapa.Plate,
			fai.ItemCatalogId,
			CONCAT(faic.Code, ' - ', faic.Description) ItemCatalogCodeDescription,
			CONCAT(fai.Code, ' - ', fai.Description) ItemCodeDescription,
			CONCAT(fal.Code, ' - ', fal.Name) LocationCodeName,
			CONCAT(fapac.Code, ' - ', fapac.Name) PartCodeDescription,
			CONCAT(p.Identification, ' - ', p.Fullname) RequestUser,
			mr.Id MaintenanceResponsibleId,
			mr.ResponsibleRole,
			CONCAT(tp.Nit, ' - ', tp.Name) AssignUser,
			wo.Id WorkOrderId,
			wo.Consecutive WorkOrderCode,
			ISNULL(wo.ProgramDate, Common.GETDATE()) ProgramDate,
			COALESCE(wo.Description, mfrd.Description, mfr.Observation) Description,
			wo.State WorkOrderStatus
	FROM Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK)
	JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON mfr.Id = mfrd.MaintenanceFailureRequestId
	JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON fapap.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemCatalog faic WITH (NOLOCK) ON fai.ItemCatalogId = faic.Id
	JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Security.[User] u ON mfr.CreationUser = u.UserCode
	LEFT JOIN Security.Person p ON u.IdPerson = p.Id
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON mfrd.Id = wo.EntityId AND 'MaintenanceFailureRequest' = wo.EntityName
	LEFT JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
	WHERE mfr.Status = 2 AND mfrd.TransactionClass = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de solicitudes de fallas de mantenimiento pendientes o en proceso, que incluye tanto activos físicos completos como partes o componentes de activos. Integra información del activo (placa, catálogo, ítem y ubicación), los datos del usuario que generó la solicitud, el responsable de mantenimiento asignado (con su empresa o tercero contratista) y la orden de trabajo vinculada (código consecutivo, fecha programada, estado y descripción). Sirve como base para el seguimiento operativo del área de mantenimiento, permitiendo visualizar en un único resultado qué activos o partes presentaron fallas, quién las reportó y cuál es el avance de la orden de trabajo asociada.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewListRequests';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewListRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las solicitudes de falla de mantenimiento aprobadas, distinguiendo si la falla aplica al activo físico completo o a una de sus partes, y enriquece cada registro con datos de ubicación, ítem, solicitante, responsable asignado y orden de trabajo asociada.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La solicitud de falla (MaintenanceFailureRequest) debe estar en Status = 2 para ser visible.; Cada detalle debe tener TransactionClass = 1 (activo) o TransactionClass = 2 (parte de activo); otros valores quedan excluidos.; Para detalles tipo ''Parte de Activo'' debe existir el vínculo PhysicalAssetPartsId apuntando a FixedAssetPhysicalAssetParts.; Para detalles tipo ''Activo'' debe existir el vínculo PhysicalAssetId apuntando a FixedAssetPhysicalAsset.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen solicitudes de falla con Status = 2 (estado aprobado/válido para gestión).; Cada fila de la vista corresponde a un único detalle (mfrd.Id) de solicitud de falla, no a la solicitud completa.; El vínculo entre detalle y orden de trabajo siempre se realiza con EntityName fijo = ''MaintenanceFailureRequest''.; El usuario solicitante (RequestUser) se deriva siempre de mfr.CreationUser resolviendo User → Person.; Las dos ramas son mutuamente excluyentes por TransactionClass, por lo que un mismo detalle no aparece duplicado entre ''Activo'' y ''Parte de Activo''.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de falla de mantenimiento; Mantenimiento correctivo; Activo físico; Parte/accesorio/consumible de activo; Orden de trabajo; Responsable de mantenimiento; Ubicación de activo; Catálogo de ítems / categorías de activo fijo; Tercero asignado; Sucursal / unidad operativa', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewListRequests: Devuelve un registro por cada detalle de solicitud de falla con Status=2; etiqueta TypeDescription=''Activo'' cuando TransactionClass=1 y ''Parte de Activo'' cuando TransactionClass=2.; [RETURN_RESULT] Maintenance.ViewListRequests: ProgramDate se expone como wo.ProgramDate y, si no existe orden de trabajo o fecha programada, se sustituye por Common.GETDATE() (fecha actual).; [RETURN_RESULT] Maintenance.ViewListRequests: Description se resuelve por COALESCE: primero la descripción de la WorkOrder, si no existe la del detalle (mfrd.Description) y como último recurso la observación de la solicitud (mfr.Observation).; [RETURN_RESULT] Maintenance.ViewListRequests: PartCodeDescription es NULL para registros tipo ''Activo'' y contiene CONCAT(fapac.Code,'' - '',fapac.Name) para registros tipo ''Parte de Activo''.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mfrd.TransactionClass = 1 (rama UNION superior) → El detalle se interpreta como falla sobre el activo físico completo: se enlaza directo a FixedAssetPhysicalAsset por PhysicalAssetId y PartCodeDescription se fija en NULL.; si mfrd.TransactionClass = 2 (rama UNION inferior) → El detalle se interpreta como falla sobre una parte/accesorio/consumible: se enlaza vía FixedAssetPhysicalAssetParts y se obtiene el código y nombre desde FixedAssetPartsAccesoriesConsumables.; si Existe WorkOrder con EntityId = mfrd.Id y EntityName = ''MaintenanceFailureRequest'' → Se exponen WorkOrderId, WorkOrderCode, ProgramDate y WorkOrderStatus de la orden de trabajo y datos del responsable y tercero asignado. else Estos campos quedan en NULL (excepto ProgramDate que se sustituye por la fecha actual) y no se muestra responsable ni tercero asignado.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.MaintenanceFailureRequest; Maintenance.MaintenanceFailureRequestDetail; Maintenance.WorkOrder; Maintenance.MaintenanceResponsible; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetParts; FixedAsset.FixedAssetPartsAccesoriesConsumables; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; Security.User; Security.Person; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
