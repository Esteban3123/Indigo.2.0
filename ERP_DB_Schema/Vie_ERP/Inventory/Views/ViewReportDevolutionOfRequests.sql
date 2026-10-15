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