

CREATE VIEW [Budget].[ViewListObligationDetail]
AS
SELECT
	od.Id,
	od.CommitmentDetailId,
	c.Code CommitmentCode,
	c.Document CommitmentDocument,
	ct.Id CategoryId,	
	CONCAT(ct.Code, ' - ', ct.Name) CategoryCodeName,
	CONCAT(fs.Code, ' - ', fs.Name) FinancialSourceCodeName,
	rt.Id RevenueTypeId,
	CONCAT(rt.Code, ' - ', rt.Name) RevenueTypeCodeName,
	od.InitialValue,
	od.Balance,
	od.EntityId,
	od.EntityCode,
	od.EntityName
FROM Budget.Commitment c WITH (NOLOCK)
JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON c.Id = cd.CommitmentId
JOIN Budget.ObligationDetail od WITH (NOLOCK) ON cd.Id = od.CommitmentDetailId
JOIN Budget.Category ct WITH (NOLOCK) ON cd.CategoryId = ct.Id
JOIN Budget.FinancialSource fs WITH (NOLOCK) ON ct.FinancialSourceId = fs.Id
JOIN Budget.RevenueType rt WITH (NOLOCK) ON cd.RevenueTypeId = rt.Id
JOIN Budget.Budget b WITH (NOLOCK) ON ct.Id = b.CategoryId AND rt.Id = b.RevenueTypeId
JOIN Budget.BudgetHeader bh WITH (NOLOCK) ON b.BudgetHeaderId = bh.Id
JOIN Budget.BudgetaryValidity bv WITH (NOLOCK) ON bh.BudgetaryValidityId = bv.Id AND bv.Status = 2
WHERE od.EntityId IS NOT NULL