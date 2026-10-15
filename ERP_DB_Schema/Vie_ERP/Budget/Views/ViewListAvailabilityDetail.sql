
CREATE VIEW [Budget].[ViewListAvailabilityDetail]
AS
SELECT 
	ad.Id,
	a.BudgetaryValidityId,
	a.Code AvailabilityCode, 
	a.Observations AvailabilityObservations,
	CONCAT(c.Code, ' - ', c.Name)  CategoryCodeName,
	CONCAT(fs.Code, ' - ', fs.Name) FinancialSourceCodeName, 
	CONCAT(rt.Code, ' - ', rt.Name) RevenueTypeCodeName,
	ad.Balance
FROM Budget.Availability a WITH (NOLOCK)
JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON a.Id = ad.AvailabilityId
JOIN Budget.Budget b WITH (NOLOCK) ON ad.BudgetId = b.Id
JOIN Budget.Category c WITH (NOLOCK) ON b.CategoryId = c.Id
JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id
JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
WHERE a.Status = 2