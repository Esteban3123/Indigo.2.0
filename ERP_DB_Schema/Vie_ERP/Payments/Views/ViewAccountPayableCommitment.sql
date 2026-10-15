CREATE VIEW [Payments].[ViewAccountPayableCommitment]
AS
SELECT	CAST(apc.Id AS VARCHAR(20)) Id,
		apc.AccountPayableId,
		b.Id BudgetId,
		c.Code CategoryCode,
		c.Name CategoryName,
		fs.Code FinancialSourceCode,
		fs.Name FinancialSourceName,
		rt.Code RevenueTypeCode,
		rt.Name RevenueTypeName,
		apc.Value
FROM Payments.AccountPayableCommitments apc
JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
JOIN Budget.Budget b ON cd.CategoryId = b.CategoryId AND cd.RevenueTypeId = b.RevenueTypeId
JOIN Budget.Category c ON b.CategoryId = c.Id
JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id