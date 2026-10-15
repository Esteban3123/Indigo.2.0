CREATE VIEW [Payments].[ViewCostDistributionDirectCostDetailIva]
AS

SELECT	cddcdi.Id,
		cddc.AccountPayableId, 
		apdc.Id AccountPayableDetailConceptId,
		gli.Name,
		gli.Percentage,
		cddcdi.IvaValue
FROM Cost.CostDistributionDirectCost cddc
JOIN Cost.CostDistributionDirectCostDetail cddcd ON cddc.Id = cddcd.DistributionDirectCostId
JOIN Cost.CostDistributionDirectCostDetailIva cddcdi ON cddcd.Id = cddcdi.CostDistributionDirectCostDetailId
JOIN GeneralLedger.GeneralLedgerIVA gli ON cddcdi.GeneralLedgerIvaId = gli.Id
OUTER APPLY (
	SELECT *
	FROM Payments.AccountPayableDetailConcept apdc
	WHERE cddc.AccountPayableId = apdc.IdAccountPayable
		AND apdc.IsDirectCost = 1
		AND cddcd.MainAccountId = apdc.IdAccount
		AND ISNULL(cddcd.CostCenterId, 0) = ISNULL(apdc.IdCostCenter, 0)
		AND ISNULL(cddcd.ThirdPartyId, apdc.IdThirdParty) = apdc.IdThirdParty
		AND cddcd.Value = apdc.Value
) apdc
WHERE cddc.Status = 2