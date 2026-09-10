CREATE VIEW [Maintenance].[ViewWorkOrderNotification]
AS
	SELECT	CONCAT('MaintenanceFailureRequestDetailNotification','-', mfrdn.Id) Id,
			'MaintenanceFailureRequest' EntityName,
			mfrdn.MaintenanceFailureRequestDetailId EntityId,
			wo.id WorkOrderId,
			'Solicitud de Mantenimiento' TypeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			mfrdn.Email,
			IIF(mfrdn.Status = 0, 'Sin Enviar', 'Enviado') StatusName,
			mfrdn.CreationDate,
			mfrdn.ShippingDate
	FROM Maintenance.MaintenanceFailureRequestDetailNotification mfrdn WITH (NOLOCK)
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON mfrdn.ThirdPartyId = tp.Id
	LEFT JOIN Maintenance.WorkOrder wo WITH (NOLOCK) ON mfrdn.Id = wo.EntityId AND 'MaintenanceFailureRequest' = wo.EntityName
UNION ALL
	SELECT	CONCAT('WorkOrderNotification','-', won.Id) Id,
			wo.EntityName,
			wo.EntityId,
			wo.Id WorkOrderId,
			CASE won.Type
				WHEN 1 THEN 'Orden de Trabajo Asignada'
				WHEN 2 THEN 'Orden de Trabajo Terminada'
				WHEN 3 THEN 'Orden de Trabajo Anulada'
				WHEN 4 THEN 'Orden de Trabajo Aceptada'
				WHEN 5 THEN 'Orden de Trabajo Rechazada'
			END TypeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			won.Email,
			IIF(won.Status = 0, 'Sin Enviar', 'Enviado') StatusName,
			won.CreationDate,
			won.ShippingDate
	FROM Maintenance.WorkOrder wo WITH (NOLOCK)
	JOIN Maintenance.WorkOrderNotification won WITH (NOLOCK) ON wo.Id = won.WorkOrderId
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON won.ThirdPartyId = tp.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida en una sola consulta todas las notificaciones relacionadas con mantenimiento, uniendo dos fuentes: las notificaciones enviadas sobre solicitudes de falla (mantenimiento correctivo) y las notificaciones asociadas a órdenes de trabajo (asignación, terminación, anulación, aceptación o rechazo). Para cada notificación muestra el tercero destinatario (NIT y nombre del proveedor o contratista), el correo electrónico usado, el tipo de notificación, el estado de envío (sin enviar o enviado) y las fechas de creación y despacho. Sirve como panel centralizado de trazabilidad de comunicaciones hacia terceros en los procesos de mantenimiento, útil para auditoría, seguimiento de alertas y reportes de gestión de órdenes de trabajo y solicitudes de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrderNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewWorkOrderNotification';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en un único listado las notificaciones derivadas de solicitudes de falla de mantenimiento y las notificaciones propias de órdenes de trabajo, mostrando tercero destinatario, tipo, estado de envío y fechas.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada notificación debe tener un tercero (ThirdParty) asociado vía ThirdPartyId; de lo contrario no aparece en el resultado por el JOIN obligatorio.; Para las notificaciones de órdenes de trabajo, debe existir el vínculo WorkOrderId en WorkOrderNotification.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de salida (Id) siempre se prefija con el origen para evitar colisiones entre las dos fuentes unidas.; ThirdPartyNitName siempre se compone como ''Nit - Name'' del tercero.; StatusName es binario: solo ''Sin Enviar'' (Status=0) o ''Enviado'' (cualquier otro valor).; Las notificaciones de tipo ''Solicitud de Mantenimiento'' usan EntityName fijo ''MaintenanceFailureRequest''.; Se utiliza WITH (NOLOCK) en todas las lecturas, por lo que pueden incluirse lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de mantenimiento; Orden de trabajo; Notificación a terceros; Tercero (NIT); Estado de envío de notificación; Tipos de notificación de orden de trabajo (asignada, terminada, anulada, aceptada, rechazada)', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.ViewWorkOrderNotification: Devuelve la unión (UNION ALL) de notificaciones de MaintenanceFailureRequestDetailNotification y WorkOrderNotification, identificando cada fila con un Id compuesto por prefijo según origen (''MaintenanceFailureRequestDetailNotification-{Id}'' o ''WorkOrderNotification-{Id}'').', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mfrdn.Status = 0 (o won.Status = 0) → StatusName = ''Sin Enviar'' else StatusName = ''Enviado''; si won.Type ∈ {1,2,3,4,5} → TypeName se traduce a ''Orden de Trabajo Asignada/Terminada/Anulada/Aceptada/Rechazada'' respectivamente else TypeName queda NULL si Type no está en {1..5}; si Origen MaintenanceFailureRequestDetailNotification → EntityName se fija como literal ''MaintenanceFailureRequest'' y TypeName como ''Solicitud de Mantenimiento'' else Para WorkOrderNotification, EntityName y EntityId se toman de la WorkOrder relacionada; si LEFT JOIN con Maintenance.WorkOrder por EntityId y EntityName=''MaintenanceFailureRequest'' → WorkOrderId puede ser NULL si la solicitud de falla aún no tiene orden de trabajo asociada', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.MaintenanceFailureRequestDetailNotification; Common.ThirdParty; Maintenance.WorkOrder; Maintenance.WorkOrderNotification', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewWorkOrderNotification';
GO
