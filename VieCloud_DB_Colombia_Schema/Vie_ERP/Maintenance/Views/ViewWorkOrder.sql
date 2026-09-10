CREATE VIEW [Maintenance].[ViewWorkOrder]
AS
	SELECT	wo.Id,
			wo.Consecutive AS WorkOrderCode,
			wo.RequestDate AS RequestDate,
			wo.ProgramDate AS ProgramDate,
			wo.State WorkOrderState,
			CASE wo.State
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
				WHEN 4 THEN 'Aprobado'
				WHEN 5 THEN 'Rechazado'
			END AS WorkOrderStateName,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id AS MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(mtp.Nit, ' - ', mtp.Name) AS MaintenanceResponsibleCodeName,
			mr.ResponsibleRole,
			-------------------------------------------------------------------
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fai.Id AS ItemId,
			fai.ItemTypeId,
			CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
			CONCAT(fapac.Code, ' - ', fapac.Name) PartCodeDescription,
			fait.InventoryTypeId,
			fal.Id AS LocationId,
			CONCAT(fal.code, ' - ', fal.Name) AS LocationCodeName,
			bo.Id AS BranchOfficeId,
			CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
			CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
			far.Id AS FixedAssetResponsibleId,
			CONCAT(ftp.Nit, ' - ', ftp.Name) AS FixedAssetResponsibleCodeName,
			u.UserCode FixedAssetUserCode
	FROM Maintenance.WorkOrder wo WITH (NOLOCK)
	JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id
	JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	JOIN Common.ThirdParty mtp WITH (NOLOCK) ON mr.ThirdPartyId = mtp.Id
	---------------------------------------------------------------------------
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON wo.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	JOIN FixedAsset.FixedAssetResponsible far WITH (NOLOCK) ON fapa.ResponsibleId = far.Id
	JOIN Common.ThirdParty ftp WITH (NOLOCK) ON far.ThirdPartyId = ftp.Id
	---------------------------------------------------------------------------
	LEFT JOIN Security.Person sp ON ftp.Nit = sp.Identification
	LEFT JOIN Security.[User] u ON sp.Id = u.IdPerson
	---------------------------------------------------------------------------
	LEFT JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON wo.EntityId = mfrd.Id AND wo.EntityName = 'MaintenanceFailureRequest'
	LEFT JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
	LEFT JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista consolidada de órdenes de trabajo de mantenimiento. Integra en un único resultado cada orden de trabajo con su protocolo de mantenimiento aplicado, el responsable de mantenimiento (persona o tercero contratista), y el activo físico intervenido junto con su ítem de inventario, tipo de activo, marca, ubicación física, sede y unidad funcional. Permite consultar el estado de la orden (Registrado, Confirmado, Aprobado, Rechazado, Anulado), la pieza o accesorio asociado cuando la orden proviene de una solicitud de falla, y el usuario responsable del activo. Sirve de base para reportes operativos de mantenimiento preventivo y correctivo, seguimiento de equipos, gestión de activos fijos y control de intervenciones por sede o área.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrder';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de órdenes de trabajo de mantenimiento que combina datos del activo físico intervenido (ítem, ubicación, sucursal, marca, responsable), protocolo, responsable de mantenimiento y, opcionalmente, la parte afectada cuando la OT proviene de una solicitud de falla.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada WorkOrder debe tener ProtocolId válido en MaintenanceProtocol, MaintenanceResponsibleId válido en MaintenanceResponsible y PhysicalAssetId válido en FixedAssetPhysicalAsset (de lo contrario la fila se excluye por los INNER JOIN).; El responsable de mantenimiento debe tener un tercero asociado (ThirdPartyId) y el responsable del activo fijo también debe tener tercero.; El activo físico debe tener ítem, tipo de ítem, ubicación, marca y responsable definidos.; La ubicación del activo debe estar vinculada a una unidad funcional y ésta a una sucursal.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda orden de trabajo expuesta debe tener protocolo de mantenimiento, responsable de mantenimiento (con tercero asociado), activo físico, ítem, tipo de ítem, ubicación, unidad funcional, sucursal, marca y responsable del activo fijo (con tercero) — todos vía INNER JOIN.; El estado de la orden de trabajo se traduce a un único nombre de estado entre: Registrado, Confirmado, Anulado, Aprobado o Rechazado; otros valores de State quedan sin nombre.; La sucursal del activo fijo se deriva siempre a través de la unidad funcional de la ubicación del activo (Location → FunctionalUnit → BranchOffice).; El usuario asociado al responsable del activo fijo se obtiene cruzando el NIT del tercero contra la identificación de la persona de seguridad; si no coincide, FixedAssetUserCode queda nulo.; La información de parte/accesorio/consumible solo aparece cuando la orden de trabajo proviene de una solicitud de falla de mantenimiento (EntityName = ''MaintenanceFailureRequest'').', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de trabajo de mantenimiento; Protocolo de mantenimiento; Responsable de mantenimiento; Solicitud de falla de mantenimiento; Activo fijo físico; Partes, accesorios y consumibles de activo fijo; Marca de activo fijo; Ubicación de activo fijo; Unidad funcional; Sucursal/Sede; Tercero; Responsable de activo fijo; Usuario del sistema', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewWorkOrder: Devuelve una fila por cada orden de trabajo con sus datos descriptivos (códigos concatenados con nombre/descripción), el nombre del estado traducido vía CASE y, cuando aplica, la parte del activo físico asociada al detalle de solicitud de falla.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si WorkOrder.State = 1 → Se etiqueta como ''Registrado''; si WorkOrder.State = 2 → Se etiqueta como ''Confirmado''; si WorkOrder.State = 3 → Se etiqueta como ''Anulado''; si WorkOrder.State = 4 → Se etiqueta como ''Aprobado''; si WorkOrder.State = 5 → Se etiqueta como ''Rechazado''; si WorkOrder.EntityName = ''MaintenanceFailureRequest'' y WorkOrder.EntityId coincide con MaintenanceFailureRequestDetail.Id → Se enlaza la parte/accesorio/consumible del activo físico asociada al detalle de la solicitud de falla else No se reporta información de parte (PartCodeDescription queda nulo)', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.WorkOrder; Maintenance.MaintenanceProtocol; Maintenance.MaintenanceResponsible; Common.ThirdParty; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetLocation; Payroll.FunctionalUnit; Payroll.BranchOffice; FixedAsset.FixedAssetTrademark; FixedAsset.FixedAssetResponsible; Security.Person; Security.User; Maintenance.MaintenanceFailureRequestDetail; FixedAsset.FixedAssetPhysicalAssetParts; FixedAsset.FixedAssetPartsAccesoriesConsumables', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrder';
GO
