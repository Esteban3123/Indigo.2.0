





CREATE VIEW [Inventory].[ViewListRequestDetailUnauthorized]
AS
	SELECT	 irdo.Id
			,ir.OperatingUnitId
			,ir.RequestType
			,irdo.Status
			,IIf(RequestType = 1, 'Unidad Funcional', 'Almacen') RequestTypeName
			,ir.ConfirmationDate
			,ir.Code
			,p.Fullname UserConfirmation
			,IIF(ip.Id IS NULL,CONCAT(isu.Code,' - ',isu.SupplieName), CONCAT(ip.Code, ' - ', ip.Name)) ItemDescription
			,CONCAT(fu.Code, ' - ', fu.Name) FunctionUnitCodeName
			,CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName
			,IIF(ir.MovementType = 1, 'Consumo',IIF(ir.MovementType = 2, 'Traslado', '')) MovementTypeName
			,irdo.OriginalQuantity
			,irdo.OutstandingQuantity
			,irdo.Quantity
			,(irdo.OriginalQuantity - irdo.Quantity) QuantityRejected
	FROM Inventory.InventoryRequest ir
	JOIN Inventory.InventoryRequestDetailOther irdo ON irdo.InventoryRequestId = ir.Id
	LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = irdo.InventoryProductId
	LEFT JOIN Inventory.InventorySupplie isu on isu.Id = irdo.SupplieId 
	LEFT JOIN Payroll.FunctionalUnit fu ON fu.Id = ir.TargetFunctionalUnitId
	LEFT JOIN Inventory.Warehouse w ON w.Id = ir.TargetWarehouseId
	LEFT JOIN Security.[User] u ON ir.ConfirmationUser = u.UserCode
	LEFT JOIN Security.Person p ON p.Id = u.IdPerson