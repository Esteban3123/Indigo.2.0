
CREATE VIEW [Budget].[ViewListCommitmentDetail]
AS
SELECT
	ROW_NUMBER() OVER (ORDER BY cd.Id) Id,
	co.Code CommitmentCode, 
	co.Id CommitmentId, 
	co.ThirdPartyId, 
	co.Status, 
	cd.Id CommitmentDetailId, 
	cd.ExpiredDate, 
	cd.InitialValue, 
	cd.TotalCommitment, 
	cd.Balance,
	ca.Code CategoryCode, 
	ca.Name CategoryName, 
	ca.Code + ' - ' + ca.Name CategoryCodeName,
	rt.Code RevenueTypeCode, 
	rt.Name RevenueTypeName, 
	rt.Code + ' - ' + rt.Name RevenueTypeCodeName,
	co.Code + ' - ' + ca.Code + ' - ' + ca.Name + ' - ' + rt.Code + ' - ' + rt.Name NullText,
	fs.Code FinancialSourceCode,
	fs.Name FinancialSourceName,
	fs.Code + ' - ' + fs.Name FinancialSourceCodeName,
	co.Document,
	co.BudgetaryValidityId
FROM Budget.CommitmentDetail cd
JOIN Budget.Commitment co on co.Id = cd.CommitmentId
JOIN Budget.Category ca on ca.Id = cd.CategoryId
JOIN Budget.RevenueType rt on rt.Id = cd.RevenueTypeId
join Budget.FinancialSource fs on fs.Id = ca.FinancialSourceId
WHERE cd.Balance > 0