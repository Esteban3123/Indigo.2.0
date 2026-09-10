

CREATE VIEW [Billing].[ViewListServiceOrderDetail]
AS

select 
sod.Id,
CONCAT(ips.Code,' - ',ips.Name) as CodeNameIpsService,
CONCAT(c.Code, ' - ', c.Name) as CodeNameCareGroup,
sod.RecordType,
sod.Packaging,
sod.InvoicedQuantity,
so.AdmissionNumber,
so.Status
from Billing.ServiceOrderDetail sod
JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
JOIN Contract.IPSService ips on ips.Id = sod.IPSServiceId
JOIN Contract.CareGroup c ON c.Id = sod.CareGroupId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de los ítems incluidos en cada orden de servicio de facturación, combinando información del servicio de salud prestado (código CUPS y nombre del procedimiento, examen o medicamento), el grupo de atención contractual aplicable (grupo de facturación o cuidado), la cantidad facturada, el tipo de registro, el empaque y el número de ingreso del paciente junto con el estado de la orden. Integra el detalle de la orden (ServiceOrderDetail), el encabezado de la orden (ServiceOrder), el catálogo de servicios propios de la IPS (IPSService) y los grupos de atención contractuales (CareGroup) en una sola consulta lista para reportería de facturación, auditoría de glosas y revisión de servicios cobrados por ingreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListServiceOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListServiceOrderDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles de órdenes de servicio facturadas, mostrando para cada ítem el servicio IPS y el grupo de atención asociados con sus códigos y nombres concatenados, junto con datos de la orden cabecera.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListServiceOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle debe tener una orden de servicio existente (JOIN obligatorio con Billing.ServiceOrder); Cada detalle debe referenciar un servicio IPS válido en Contract.IPSService; Cada detalle debe referenciar un grupo de atención válido en Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListServiceOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo CodeNameIpsService siempre se compone como ''Code - Name'' del servicio IPS; El campo CodeNameCareGroup siempre se compone como ''Code - Name'' del grupo de atención; Solo se exponen detalles cuyas claves foráneas (ServiceOrderId, IPSServiceId, CareGroupId) resuelven en sus tablas maestras', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListServiceOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden de servicio; detalle de facturación; servicio IPS; grupo de atención; número de admisión; tipo de registro; empaquetado; cantidad facturada', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListServiceOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListServiceOrderDetail: Devuelve una fila por cada ServiceOrderDetail que tenga correspondencia en ServiceOrder, IPSService y CareGroup; los detalles huérfanos en cualquiera de esos catálogos quedan excluidos por los INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListServiceOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListServiceOrderDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListServiceOrderDetail';
GO
