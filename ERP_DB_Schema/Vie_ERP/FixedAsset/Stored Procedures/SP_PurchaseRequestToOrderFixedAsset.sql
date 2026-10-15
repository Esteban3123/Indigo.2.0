
-- =============================================
-- Author:		HECTOR RODRIGUEZ
-- Create date: 2019 05 22
-- Description:	Consulta solicitudes de compra para ordenar
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_PurchaseRequestToOrderFixedAsset]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select PRD.PurchaseRequestId, PR.Code, PR.CreationDate, PR.FunctionalUnitId, CodeNameFunctionalUnit = FU.Code + '-' + FU.Name,
	FU.BranchOfficeId, CodeNameBranchOffice = BO.Code + '-' + BO.Name 
	,PRD.Id, PRD.FixedAssetItemId, PRD.OutstandingQuantity, PRD.TrademarkId, PRD.Model, CodeNameFixedAsset = FAI.Code + '-' + FAI.Description
	, CodeNameTrademark = FAT.Code + '-' + FAT.Name
	from Inventory.PurchaseRequest PR WITH(NOLOCK)
	INNER JOIN Payroll.FunctionalUnit FU WITH(NOLOCK)
	ON FU.ID = PR.FunctionalUnitId
	INNER JOIN Payroll.BranchOffice BO WITH(NOLOCK)
	ON BO.ID = FU.BranchOfficeId
	INNER JOIN Inventory.PurchaseRequestDetail PRD WITH(NOLOCK)
	ON PRD.PurchaseRequestId = PR.ID
	AND PRD.Status = 1 --Aprobado
	AND PRD.OutstandingQuantity > 0 --Pendiente por ordenar
	INNER JOIN FixedAsset.FixedAssetItem FAI WITH(NOLOCK)
	ON FAI.Id = PRD.FixedAssetItemId
	INNER JOIN FIXEDASSET.FixedAssetTrademark FAT WITH(NOLOCK)
	ON FAT.Id = PRD.TrademarkId
	
	WHERE 
	PR.Ordered = 0 --Con pendiente por ordenar
	AND PR.Status = 2 --Confirmada
	AND PR.RequestTypeId = 2 --Fixed Asset
	
END