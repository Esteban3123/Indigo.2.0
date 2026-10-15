CREATE VIEW [Portfolio].[ViewPortfolioNoteAccountReceivableDetail]
AS
	SELECT	pnard.Id,
			pnard.PortfolioNoteAccountReceivableId,
			----------------------------------
			v.BillingGroupCodeName,
			v.CodeName,
			v.AlternativeCodeName,
			----------------------------------
			v.MainAccountNumberName,
			v.CostCenterCodeName,
			----------------------------------
			v.Quantity,
			v.UnitSalesPrice,
			v.TotalSalesPrice,
			pnard.Value
	FROM Portfolio.PortfolioNoteAccountReceivableDetail pnard
	JOIN Portfolio.ViewInvoiceDetails v ON pnard.EntityName = v.EntityName AND pnard.EntityId = v.EntityId