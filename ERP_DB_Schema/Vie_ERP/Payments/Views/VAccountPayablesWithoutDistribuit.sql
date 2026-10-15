
CREATE VIEW [Payments].[VAccountPayablesWithoutDistribuit]
AS
	SELECT	ap.Id, ap.Code, ap.BillNumber, ap.DocumentDate BillDate, 
			ap.IdThirdParty, ap.InvoiceValue, ap.Coments,
			ap.Status, cddc.Id AS CostDistributionDirectCostId
	FROM Payments.AccountPayable ap
	LEFT JOIN Cost.CostDistributionDirectCost  cddc
		ON ap.IdThirdParty = cddc.ThirdPartyId 
			AND cddc.Status <> 3
			AND ap.Id = cddc.AccountPayableId
	WHERE ap.Status = 1 AND cddc.Id IS NULL