CREATE VIEW [Inventory].[ViewReportDevolutionOfRequests]
AS
SELECT
ROW_NUMBER() OVER (ORDER BY P.Code) AS Id,
R.Code, 
IIF(R.RequestType = 1,'Unidad Funcional','Almacén') AS TipoDevolucion, 
(P.Code + ' - ' + P.Name) AS Producto, 
RD.OutstandingQuantity, RDD.Quantity,
RDT.Code AS CodeRequestDevolution,
RDT.[Description],
RDT.DocumentDate,
RDT.CreationUser
FROM
[Inventory].[InventoryRequestDevolutionDetail] AS RDD
INNER JOIN [Inventory].[InventoryRequestDevolution] AS RDT ON RDT.Id = RDD.InventoryRequestDevolutionId
INNER JOIN [Inventory].[InventoryRequestDetail] AS RD ON RD.Id = RDD.InventoryRequestDetailId
INNER JOIN [Inventory].[InventoryRequest] AS R ON R.Id = RD.InventoryRequestId
INNER JOIN [Inventory].[InventoryProduct] AS P ON P.Id = RD.InventoryProductId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de devoluciones de solicitudes de inventario. Integra los documentos de devolución con el detalle de cada ítem devuelto, la solicitud original y el producto correspondiente, mostrando el código y tipo de solicitud (si proviene de una unidad funcional o de un almacén), el producto devuelto, la cantidad pendiente de despacho y la cantidad efectivamente devuelta. Sirve para consultar y auditar todas las devoluciones de requisiciones de insumos, medicamentos y materiales, indicando quién generó la devolución, la fecha del documento y su descripción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportDevolutionOfRequests';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportDevolutionOfRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un reporte las devoluciones de solicitudes de inventario, mostrando producto, tipo de devolución (unidad funcional o almacén), cantidades pendientes y devueltas, y datos del documento de devolución.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación íntegra entre la devolución, su detalle, el detalle de la solicitud original, la solicitud y el producto (todos los JOIN son INNER).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan devoluciones con detalle existente y vinculadas a una solicitud y producto válidos (INNER JOIN).; El identificador de fila se genera dinámicamente con ROW_NUMBER ordenado por el código del producto.; El campo Producto siempre se presenta como ''Código - Nombre'' del producto.; El tipo de devolución solo puede tomar dos valores: ''Unidad Funcional'' o ''Almacén''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de inventario; Solicitud de inventario; Detalle de devolución; Detalle de solicitud; Producto de inventario; Unidad funcional; Almacén; Cantidad pendiente; Cantidad devuelta', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada detalle de devolución que cumpla los INNER JOIN con devolución, detalle de solicitud, solicitud y producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RequestType = 1 en la solicitud de inventario → Se etiqueta la devolución como ''Unidad Funcional'' else Se etiqueta como ''Almacén''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRequestDevolutionDetail; Inventory.InventoryRequestDevolution; Inventory.InventoryRequestDetail; Inventory.InventoryRequest; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportDevolutionOfRequests';
GO
