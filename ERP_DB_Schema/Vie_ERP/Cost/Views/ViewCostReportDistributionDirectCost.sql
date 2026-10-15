
CREATE VIEW [Cost].[ViewCostReportDistributionDirectCost]
AS
	SELECT	cddc.Id,
			cddc.Code,
			cddc.Year,
			cddc.Month,
			CONCAT(cge.Code, ' - ', cge.Name) GeneralExpenseCodeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			cddc.Observation,
			ISNULL(ap.BillNumber, cddc.BillNumber) BillNumber,
			ISNULL(ap.BillDate, cddc.BillDate) BillDate,
			ISNULL(ap.ServicePeriodDate, cddc.ServicePeriodDate) ServicePeriodDate,
			cddc.Value,
			CASE cddc.Status
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
			END StatusName,
			cddc.CreationUser,
			c.Abbreviation CurrencyAbbreviation ,
			c.Id CurrencyId,
			iso.CurrencyName
	FROM Cost.CostDistributionDirectCost cddc  WITH(NOLOCK)
	JOIN Cost.CostGeneralExpense cge WITH(NOLOCK) ON cddc.GeneralExpenseId = cge.Id
	LEFT JOIN Payments.AccountPayable ap WITH(NOLOCK) ON cddc.AccountPayableId = ap.Id
	LEFT JOIN Common.ThirdParty tp WITH(NOLOCK) ON ISNULL(ap.IdThirdParty, cddc.ThirdPartyId) = tp.Id
	JOIN Common.Currency c WITH(NOLOCK) ON c.Id= cddc.CurrencyId
	JOIN Common.ISO4217 iso WITH(NOLOCK) on c.ISO4217Id=iso.Id