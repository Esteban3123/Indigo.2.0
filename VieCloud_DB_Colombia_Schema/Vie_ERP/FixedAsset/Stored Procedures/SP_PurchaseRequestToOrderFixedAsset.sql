
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las solicitudes de compra de activos fijos que han sido confirmadas y tienen ítems aprobados pendientes de ordenar. Combina la cabecera de la solicitud con su detalle de activos fijos, enriqueciendo el resultado con el nombre y código de la unidad funcional solicitante, la sede o sucursal correspondiente, el ítem de activo fijo y la marca del artículo. Se utiliza para identificar qué solicitudes de compra de activos fijos están listas para generar una orden de compra, es decir, aquellas confirmadas con cantidades aún no ordenadas.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los detalles de solicitudes de compra de activos fijos confirmadas y aprobadas que aún tienen cantidad pendiente por ordenar, junto con su unidad funcional, sucursal, ítem y marca.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen solicitudes de compra con tipo de solicitud = 2 (Activo Fijo); Existen detalles de solicitud con Status = 1 (Aprobado) y OutstandingQuantity > 0; Cada detalle referencia un FixedAssetItem y un FixedAssetTrademark válidos; La solicitud tiene unidad funcional y sucursal asociadas', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran solicitudes de compra de tipo Activo Fijo (RequestTypeId=2); Solo se incluyen solicitudes Confirmadas (Status=2) y aún no ordenadas (Ordered=0); Solo se incluyen detalles Aprobados (Status=1) con cantidad pendiente por ordenar mayor a cero; Se exige correspondencia obligatoria con unidad funcional, sucursal, ítem de activo fijo y marca (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Activo fijo; Aprobación de solicitud; Cantidad pendiente por ordenar; Unidad funcional; Sucursal; Marca de activo fijo', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solicitudes con Ordered=0, Status=2 (Confirmada), RequestTypeId=2 (Activo Fijo) y cuyos detalles tienen Status=1 (Aprobado) y OutstandingQuantity>0', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseRequest; Payroll.FunctionalUnit; Payroll.BranchOffice; Inventory.PurchaseRequestDetail; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetTrademark', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_PurchaseRequestToOrderFixedAsset';
-- GO
