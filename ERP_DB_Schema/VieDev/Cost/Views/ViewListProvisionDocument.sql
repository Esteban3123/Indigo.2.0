

CREATE VIEW [Cost].[ViewListProvisionDocument]
AS

SELECT   cd.Id
		,cd.Code
		,cd.GeneralExpenseId
		,cd.SupplierId
		,CONCAT(s.Code, ' - ', s.Name) SupplierCodeName
		,cd.SuppliersDistributionLinesId
		,CONCAT(dl.Code, ' - ', dl.Name) DistributionLineCodeName
		,cd.Value
		,cd.Status
		,CASE cd.Status
			WHEN 1 THEN 'Registrado'
			WHEN 2 THEN 'Confirmado'
			WHEN 3 THEN 'Anulado'
			WHEN 4 THEN 'Reversado'
			WHEN 5 THEN 'Confirmado Sin Legalizar'
			WHEN 6 THEN 'Confirmado Legalizado'
			ELSE '' END StatusName
		,cd.ConfirmUser
		,cd.ConfirmDate
FROM Cost.CostDistributionDirectCost cd WITH(NOLOCK)
JOIN Common.Supplier s WITH(NOLOCK) ON s.Id = cd.SupplierId
JOIN Common.SuppliersDistributionLines sdl WITH(NOLOCK) ON sdl.Id = cd.SuppliersDistributionLinesId
JOIN Common.DistributionLines dl WITH(NOLOCK) ON dl.Id = sdl.IdDistributionLine
WHERE cd.Status = 5 AND cd.ProvisionDocument = 1