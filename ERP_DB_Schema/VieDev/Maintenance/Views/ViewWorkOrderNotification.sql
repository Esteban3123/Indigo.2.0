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