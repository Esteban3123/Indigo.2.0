
-- =============================================
-- Author:		HECTOR RODRIGUEZ
-- Create date: 2019 05 08
-- Description:	Consulta solicitudes de compra para ordenar
-- =============================================
CREATE PROCEDURE [Inventory].[SP_PurchaseRequestToOrder]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select PRD.PurchaseRequestId, PR.Code, PRD.Id, PRD.InventoryProductId, PRD.OutstandingQuantity,CodeNameProduct = INP.Code + '-' + INP.Name, PR.CreationDate
	from Inventory.PurchaseRequest PR
	INNER JOIN Inventory.PurchaseRequestDetail PRD
	ON PRD.PurchaseRequestId = PR.ID
	AND PRD.Status = 1 --Aprobado
	AND PRD.OutstandingQuantity > 0 --Pendiente por ordenar
	INNER JOIN Inventory.InventoryProduct INP
	ON INP.Id = PRD.InventoryProductId
	WHERE 
	PR.Ordered = 0 --Con pendiente por ordenar
	AND PR.Status = 2 --Confirmada
	AND PR.RequestTypeId = 1 --PRODUCT
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las solicitudes de compra de productos de inventario que están confirmadas, tienen ítems aprobados y aún no han sido convertidas en orden de compra. Combina la cabecera de la solicitud (código, fecha de creación) con el detalle de cada ítem pendiente por ordenar y el catálogo de productos para mostrar el código y nombre del artículo junto con la cantidad pendiente. Se usa en el proceso de compras para identificar qué solicitudes de insumos o medicamentos están listas para generar la orden formal al proveedor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_PurchaseRequestToOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_PurchaseRequestToOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles de solicitudes de compra de productos que están confirmadas, aprobadas y aún tienen cantidad pendiente por ordenar, para gestionar la generación de órdenes de compra.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir solicitudes de compra con tipo de requerimiento de producto, confirmadas y no ordenadas.; Los detalles de la solicitud deben estar aprobados y tener cantidad pendiente por ordenar.; El producto referenciado en el detalle debe existir en el catálogo de inventario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven detalles de solicitud cuyo estado sea aprobado (Status=1) y con cantidad pendiente por ordenar mayor a cero.; Solo se devuelven solicitudes confirmadas (Status=2) que aún no han sido ordenadas (Ordered=0).; Solo se consideran solicitudes cuyo tipo de requerimiento corresponde a productos (RequestTypeId=1), excluyendo otros tipos como activos fijos o servicios.; Cada detalle retornado está vinculado a un producto vigente del catálogo de inventario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Detalle de solicitud de compra; Producto de inventario; Cantidad pendiente por ordenar; Aprobación de solicitudes; Confirmación de solicitudes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PurchaseRequestDetail: Cuando la solicitud de compra está confirmada (Status=2), no ordenada (Ordered=0) y es de tipo producto (RequestTypeId=1), y el detalle está aprobado (Status=1) con OutstandingQuantity>0, se retorna el detalle junto con código y nombre del producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseRequest; Inventory.PurchaseRequestDetail; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrder';
-- GO
