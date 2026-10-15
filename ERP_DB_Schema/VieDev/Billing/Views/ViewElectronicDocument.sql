

CREATE VIEW [Billing].[ViewElectronicDocument]
AS
	with cte_OffcialCurrency as (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings with(NOLOCK))
	
	SELECT	ed.Id,
			ed.OperatingUnitId,
			ed.CustomerPartyId,
			ed.EntityId,
			ed.EntityName,
			ed.DocumentDate,
			ed.DocumentType,
			ed.Status,
			ed.ShippingDate,
			isnull(ed.Prefix,'') Prefix,
			ed.DocumentNumber,
			ed.CUFE,
			adc.NOMCENATE CenterAttention,
			i.InvoiceNumber InvoiceNumber,
			c.Id CurrencyId,
			c.Abbreviation CurrencyAbbreviation,
			0 AS NoteTypeDetail
	FROM Billing.ElectronicDocument ed WITH(NOLOCK)
	JOIN Billing.Invoice i WITH(NOLOCK) ON ed.EntityId = i.Id
	JOIN cte_OffcialCurrency cte on 1=1
	JOIN Common.Currency c WITH(NOLOCK) on c.Id =ISNULL(i.CurrencyId,cte.OfficialCurrencyId)
	LEFT JOIN dbo.ADINGRESO adi WITH(NOLOCK) ON i.AdmissionNumber = adi.NUMINGRES
	LEFT JOIN dbo.ADCENATEN adc WITH(NOLOCK) ON adi.CODCENATE = adc.CODCENATE
	WHERE ed.EntityName = 'Invoice'
UNION ALL
	SELECT	ed.Id,
			ed.OperatingUnitId,
			ed.CustomerPartyId,
			ed.EntityId,
			ed.EntityName,
			ed.DocumentDate,
			ed.DocumentType,
			ed.Status,
			ed.ShippingDate,
			isnull(ed.Prefix,'') Prefix,
			ed.DocumentNumber,
			ed.CUFE,
			NULL CenterAttention,
			STUFF
			(
				(
					SELECT	', ' + bnd.InvoiceNumber
					FROM Billing.BillingNoteDetail bnd 
					WHERE ed.EntityId = bnd.BillingNoteId
					FOR XML PATH ('')
				), 1, 1, ''
			) InvoiceNumber,
			c.Id CurrencyId,
			c.Abbreviation CurrencyAbbreviation,
			cast(IIF(COALESCE(pn.NoteType,0)=6 or pn.EntityName='Glosas' ,1,0) as BIT) AS NoteTypeDetail
	FROM Billing.ElectronicDocument ed
	LEFT JOIN Billing.BillingNote bn on ed.EntityId = bn.id
	LEFT JOIN Portfolio.PortfolioNote pn on bn.EntityId = pn.id
	LEFT JOIN Billing.Invoice i on i.id = bn.EntityId and bn.EntityName = 'Invoice'
	JOIN cte_OffcialCurrency cte on 1=1
	JOIN Common.Currency c WITH(NOLOCK) on c.Id =COALESCE(pn.CurrencyId,i.CurrencyId,cte.OfficialCurrencyId)
	WHERE ed.EntityName = 'BillingNote'