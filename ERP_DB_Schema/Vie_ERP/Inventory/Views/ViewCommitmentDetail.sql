

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