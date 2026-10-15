
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