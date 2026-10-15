






CREATE VIEW [Inventory].[ViewListRequestDetailAuthorized]

AS
		SELECT	ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id
				,k.InventoryRequestDetailOtherId
				,k.OperatingUnitId
				,k.RequestType
				,k.Status
				,k.RequestTypeName
				,k.ConfirmationDate
				,k.Code
				,k.UserConfirmation
				,k.ItemDescription
				,k.FunctionUnitCodeName
				,k.WarehouseCodeName
				,k.MovementTypeName
				,k.Quantity
				,k.QuantityDelivered
				,k.OutstandingQuantity

		FROM(	SELECT 
				c.InventoryRequestDetailOtherId
				,c.OperatingUnitId
				,c.RequestType
				,c.Status
				,c.RequestTypeName
				,c.ConfirmationDate
				,c.Code
				,c.UserConfirmation
				,c.ItemDescription
				,c.FunctionUnitCodeName
				,c.WarehouseCodeName
				,c.MovementTypeName
				,c.Quantity
				,c.QuantityDelivered
				,c.OutstandingQuantity

		FROM (	SELECT
				irdo.Id InventoryRequestDetailOtherId
				,ir.OperatingUnitId
				,ir.RequestType
				,irdo.Status
				,IIf(RequestType = 1, 'Unidad Funcional', 'Almacen') RequestTypeName
				,ir.ConfirmationDate
				,ir.Code
				,p.Fullname UserConfirmation
				,CONCAT(ISNULL(ip.Code, isu.Code), ' - ', ISNULL(ip.Name, isu.SupplieName)) ItemDescription
				,CONCAT(fu.Code, ' - ', fu.Name) FunctionUnitCodeName
				,CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName
				,IIF(ir.MovementType = 1, 'Consumo',IIF(ir.MovementType = 2, 'Traslado', '')) MovementTypeName
				,irdo.Quantity
				,SUM(ISNULL(tod.Quantity, 0)) QuantityDelivered
				,(irdo.Quantity - ISNULL(tod.Quantity, 0)) OutstandingQuantity
		FROM Inventory.InventoryRequest ir 
		JOIN Inventory.InventoryRequestDetailOther irdo ON irdo.InventoryRequestId = ir.Id
		LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = irdo.InventoryProductId
		LEFT JOIN Inventory.InventorySupplie isu ON isu.Id = irdo.SupplieId
		LEFT JOIN Inventory.TransferOrderDetail tod ON tod.InventoryRequestDetailOtherId = irdo.Id
		LEFT JOIN Payroll.FunctionalUnit fu ON fu.Id = ir.TargetFunctionalUnitId
		LEFT JOIN Inventory.Warehouse w ON w.Id = ir.TargetWarehouseId
		LEFT JOIN Security.[User] u ON ir.ConfirmationUser = u.UserCode
		LEFT JOIN Security.Person p ON p.Id = u.IdPerson
		WHERE irdo.Status = 2 AND irdo.OutstandingQuantity > 0 and irdo.Quantity > 0
		GROUP BY irdo.Id, ir.OperatingUnitId, ir.RequestType, irdo.Status, ir.ConfirmationDate, ir.Code, p.Fullname, ip.Code
		,ip.Name, fu.Code, fu.Name, w.Code, w.Name, ir.MovementType, irdo.Quantity, tod.Quantity, isu.Code, isu.SupplieName) c

		UNION ALL

		SELECT	 tod.InventoryRequestDetailOtherId
				,NULL OperatingUnitId
				,NULL RequestType
				,ot.Status
				,'' RequestTypeName
				,ot.ConfirmationDate
				,ot.Code
				,p.Fullname UserConfirmation
				,'' ItemDescription
				,'' FunctionUnitCodeName
				,CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName
				,'' MovementTypeName
				,tod.Quantity
				,0 QuantityDelivered
				,0 OutstandingQuantity
		FROM Inventory.TransferOrderDetail tod
		JOIN Inventory.TransferOrder ot ON ot.Id = tod.TransferOrderId
		LEFT JOIN Inventory.InventoryRequestDetailOther irdo ON irdo.Id = tod.InventoryRequestDetailOtherId
		LEFT JOIN Inventory.Warehouse w ON w.Id = ot.SourceWarehouseId
		LEFT JOIN Security.[User] u ON ot.ConfirmationUser = u.UserCode
		LEFT JOIN Security.Person p ON p.Id = u.IdPerson
		WHERE ot.Status = 2 AND irdo.OutstandingQuantity > 0) k