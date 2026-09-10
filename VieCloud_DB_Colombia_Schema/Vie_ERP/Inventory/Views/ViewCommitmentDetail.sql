

CREATE VIEW [Inventory].[ViewCommitmentDetail]
AS
	SELECT
		CONCAT(c.EntityName, '-', cd.Id) UUID,
		IIF(ic.Id IS NULL, c.EntityName, 'InventoryContract') EntityName,
		IIF(ic.Id IS NULL, c.EntityCode, ic.Code) EntityCode,
		c.Code,
		c.Document,
		cd.Id CommitmentDetailId,
		CONCAT(ct.Code, ' - ', ct.Name) CategoryCodeName,
		CONCAT(fs.Code, ' - ', fs.Name) FinancialSourceCodeName,
		CONCAT(rt.Code, ' - ', rt.Name) RevenueTypeCodeName,
		cd.Balance
	FROM Budget.Commitment c
	JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
	JOIN Budget.RevenueType rt ON cd.RevenueTypeId = rt.Id
	JOIN Budget.Category ct ON cd.CategoryId = ct.Id
	LEFT JOIN Budget.FinancialSource fs ON ct.FinancialSourceId = ct.Id
	LEFT JOIN Inventory.InventoryContractModification icm ON c.EntityId = icm.Id AND c.EntityName = 'InventoryContractModification'
	LEFT JOIN Inventory.InventoryContractAssignment ica ON c.EntityId = ica.Id AND c.EntityName = 'InventoryContractAssignment'
	LEFT JOIN Inventory.InventoryContract ic ON ISNULL(icm.ContractId, ica.ContractId) = ic.Id OR (c.EntityId = ic.Id AND c.EntityName = 'InventoryContract')
	WHERE c.Status = 2
UNION ALL
	SELECT
		CONCAT('PurchaseOrder', '-', cd.Id) UUID,
		'PurchaseOrder' EntityName,
		po.Code EntityCode,
		c.Code,
		c.Document,
		cd.Id CommitmentDetailId,
		CONCAT(ct.Code, ' - ', ct.Name) CategoryCodeName,
		CONCAT(fs.Code, ' - ', fs.Name) FinancialSourceCodeName,
		CONCAT(rt.Code, ' - ', rt.Name) RevenueTypeCodeName,
		cd.Balance
	FROM Inventory.PurchaseOrder po
	JOIN
	(
		SELECT ic.Id ContractId, c.Id, c.Code, c.Document
		FROM Budget.Commitment c
		LEFT JOIN Inventory.InventoryContractModification icm ON c.EntityId = icm.Id AND c.EntityName = 'InventoryContractModification'
		LEFT JOIN Inventory.InventoryContractAssignment ica ON c.EntityId = ica.Id AND c.EntityName = 'InventoryContractAssignment'
		LEFT JOIN Inventory.InventoryContract ic ON ISNULL(icm.ContractId, ica.ContractId) = ic.Id OR (c.EntityId = ic.Id AND c.EntityName = 'InventoryContract')
		WHERE c.Status = 2 AND ic.Id IS NOT NULL
	) c ON po.ContractId = c.ContractId
	JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
	JOIN Budget.RevenueType rt ON cd.RevenueTypeId = rt.Id
	JOIN Budget.Category ct ON cd.CategoryId = ct.Id
	LEFT JOIN Budget.FinancialSource fs ON ct.FinancialSourceId = ct.Id
	WHERE po.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de compromisos presupuestales activos (estado 2) relacionados con el módulo de inventario, integrando contratos de suministro, modificaciones de contrato (adendas), asignaciones de contrato y órdenes de compra como entidades origen del compromiso. Para cada línea de detalle muestra el identificador único del registro, el tipo de entidad que originó el compromiso (contrato de inventario, modificación, asignación o orden de compra), el código de esa entidad, el código y documento del compromiso presupuestal, la categoría presupuestal con su fuente financiera, el tipo de ingreso o renta, y el saldo disponible pendiente de ejecutar. Permite a los usuarios de presupuesto y compras consultar cuánto saldo queda comprometido por contrato u orden de compra, cruzando la información presupuestal con los documentos comerciales de inventario para control de ejecución y disponibilidad de recursos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewCommitmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewCommitmentDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el detalle de compromisos presupuestales activos vinculados a contratos de inventario, modificaciones, cesiones y órdenes de compra, exponiendo categoría, fuente financiera, tipo de ingreso y saldo por línea.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen compromisos en Budget.Commitment con Status = 2 (activos/vigentes); Cada compromiso tiene detalles en Budget.CommitmentDetail con categoría y tipo de ingreso válidos; Para órdenes de compra, Inventory.PurchaseOrder debe tener Status = 2 y un ContractId vinculable a un compromiso de contrato de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen compromisos con Status = 2; Solo se exponen órdenes de compra con Status = 2; El UUID resultante es único por combinación de origen (EntityName del compromiso o ''PurchaseOrder'') y CommitmentDetailId; Cuando existe un contrato resoluble (directo, por modificación o por cesión), el detalle se reporta como perteneciente a ''InventoryContract''; Las órdenes de compra solo aparecen si su contrato está asociado a un compromiso activo de tipo contrato de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Compromiso presupuestal; Detalle de compromiso; Categoría presupuestal; Fuente de financiación; Tipo de ingreso/renta; Contrato de inventario; Modificación de contrato; Cesión/asignación de contrato; Orden de compra; Saldo del compromiso', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas combinadas (UNION ALL) de detalles de compromiso: una rama por entidad origen del compromiso (contrato, modificación, cesión u otra) y otra rama por orden de compra asociada al contrato del compromiso', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ic.Id IS NULL (no hay contrato de inventario resoluble) → EntityName/EntityCode toman los valores propios del compromiso (c.EntityName, c.EntityCode) else EntityName se fuerza a ''InventoryContract'' y EntityCode toma el código del contrato resuelto (ic.Code); si c.EntityName = ''InventoryContractModification'' → Resuelve el contrato vía Inventory.InventoryContractModification.ContractId; si c.EntityName = ''InventoryContractAssignment'' → Resuelve el contrato vía Inventory.InventoryContractAssignment.ContractId; si c.EntityName = ''InventoryContract'' → El compromiso apunta directamente al contrato (c.EntityId = ic.Id); si Segunda rama del UNION: po.Status = 2 y existe contrato vinculado al compromiso (ic.Id IS NOT NULL) → Genera filas con EntityName = ''PurchaseOrder'' y EntityCode = po.Code para enlazar la orden de compra con el detalle del compromiso del contrato', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Commitment; Budget.CommitmentDetail; Budget.RevenueType; Budget.Category; Budget.FinancialSource; Inventory.InventoryContractModification; Inventory.InventoryContractAssignment; Inventory.InventoryContract; Inventory.PurchaseOrder', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewCommitmentDetail';
GO
