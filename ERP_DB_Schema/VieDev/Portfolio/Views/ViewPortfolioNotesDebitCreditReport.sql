




CREATE VIEW [Portfolio].[ViewPortfolioNotesDebitCreditReport]
AS
	SELECT  pn.Id,
			pn.Code,
			pn.NoteDate,
			pn.CustomerId,
			pn.Observations,
			pn.Nature,
			pn.NoteType,
			pn.PortfolioAdvanceId,
			pn.OperatingUnitId,
			pn.Status,
			pn.CreationUser,
			pn.CreationDate,
			pn.ModificationUser,
			pn.ModificationDate,
			pn.ConfirmationUser,
			pn.ConfirmationDate,
			pn.PortfolioTransferId,
			pn.AnnulmentUser,
			pn.AnnulmentDate,
			STRING_AGG(CAST(bn.CUDE AS VARCHAR(MAX)), ', ') AS CUDE,
			STRING_AGG(CAST(bn.QR AS VARCHAR(MAX)), ', ') AS QR,
			STRING_AGG(CAST(bn.Code AS VARCHAR(MAX)), ', ') AS Consecutive,
			min(ed.ValidationDate) ValidationDate,
			max(ed.Status) ElectronicDocumentStatus,
			min(ed.ShippingDate) ShippingDate,
			pn.CurrencyId,
			i.CodeAbbreviation CurrencyAbbreviation,
			i.CurrencyName
	from Portfolio.PortfolioNote pn WITH(NOLOCK)
	JOIN Common.Currency c WITH(NOLOCK) ON pn.CurrencyId=c.Id
	JOIN Common.ISO4217 i WITH(NOLOCK) ON c.ISO4217Id=i.Id
	LEFT JOIN Billing.BillingNote bn on bn.EntityId = pn.Id and bn.EntityName = 'PortfolioNote'
	LEFT JOIN Billing.ElectronicDocument ed on ed.EntityId = bn.Id and ed.EntityName='BillingNote'
	GROUP by 
			pn.Id,
			pn.Code,
			pn.NoteDate,
			pn.CustomerId,
			pn.Observations,
			pn.Nature,
			pn.NoteType,
			pn.PortfolioAdvanceId,
			pn.OperatingUnitId,
			pn.Status,
			pn.CreationUser,
			pn.CreationDate,
			pn.ModificationUser,
			pn.ModificationDate,
			pn.ConfirmationUser,
			pn.ConfirmationDate,
			pn.PortfolioTransferId,
			pn.AnnulmentUser,
			pn.AnnulmentDate,
			pn.CurrencyId,
			i.CodeAbbreviation,
			i.CurrencyName