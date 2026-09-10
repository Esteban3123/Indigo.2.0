CREATE PROCEDURE [Billing].[SP_GetListServiceOrderDetail]
	@AdmissionNumber VARCHAR(50),
	@ids XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @tableids TABLE (id INT);

	-- Parsear los IDs desde el XML
	INSERT INTO @tableids (id)
	SELECT x.value('.', 'int')
	FROM @ids.nodes('/ArrayOfInt/int') AS A(x);

	SELECT 
		sod.Id,
		CONCAT(ips.Code, ' - ', ips.Name) AS CodeNameIpsService,
		CONCAT(c.Code, ' - ', c.Name) AS CodeNameCareGroup,
		sod.RecordType,
		sod.Packaging,
		sod.InvoicedQuantity,
		so.AdmissionNumber,
		so.Status
	FROM Billing.ServiceOrderDetail sod WITH (NOLOCK)
	INNER JOIN Billing.ServiceOrder so WITH (NOLOCK) ON so.Id = sod.ServiceOrderId
	INNER JOIN Contract.IPSService ips WITH (NOLOCK) ON ips.Id = sod.IPSServiceId
	INNER JOIN Contract.CareGroup c WITH (NOLOCK) ON c.Id = sod.CareGroupId
	WHERE 
		sod.RecordType = 1 AND
		sod.Packaging = 0 AND
		sod.InvoicedQuantity > 0 AND
		so.Status <> 3 AND
		so.AdmissionNumber = @AdmissionNumber AND
		sod.Id NOT IN (SELECT id FROM @tableids);
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle de los ítems facturados en las órdenes de servicio asociadas a un número de ingreso (admisión) específico, filtrando únicamente ítems de tipo registro 1, sin empaque, con cantidad facturada mayor a cero y órdenes activas (estado distinto de 3). Recibe además una lista de IDs en formato XML para excluir ítems ya procesados previamente. Combina información del detalle de la orden de servicio con el servicio de salud de la IPS (código y nombre del procedimiento o examen) y el grupo de atención del contrato (reglas de facturación por entidad pagadora). Se utiliza en el proceso de facturación para consultar los servicios pendientes de liquidar o incluir en una factura para un paciente durante su ingreso, evitando duplicar ítems ya gestionados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetListServiceOrderDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetListServiceOrderDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles facturables activos de las órdenes de servicio asociadas a una admisión, excluyendo ítems ya seleccionados previamente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetListServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un número de admisión existente en Billing.ServiceOrder.; El XML de IDs debe seguir el formato /ArrayOfInt/int (puede ser vacío).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetListServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen detalles con RecordType=1 (un tipo específico de registro).; Solo se incluyen detalles no empaquetados (Packaging=0).; Solo se incluyen detalles con cantidad facturada mayor a cero.; Se excluyen órdenes de servicio con Status=3 (estado considerado anulado/cancelado).; Los IDs presentes en el XML de entrada nunca aparecen en el resultado.; Se concatena Código y Nombre tanto del servicio IPS como del grupo de atención para presentación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetListServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión hospitalaria; Orden de servicio; Detalle de facturación; Servicio IPS; Grupo de atención (CareGroup); Empaquetado de servicios; Cantidad facturada', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetListServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Result set: Devuelve detalles donde RecordType=1, Packaging=0, InvoicedQuantity>0, ServiceOrder.Status<>3 y cuyo Id no esté en la lista XML recibida, filtrando por AdmissionNumber.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetListServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetListServiceOrderDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetListServiceOrderDetail';
-- GO
