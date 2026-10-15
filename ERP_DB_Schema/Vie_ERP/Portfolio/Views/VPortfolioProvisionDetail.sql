
CREATE VIEW [Portfolio].[VPortfolioProvisionDetail]
AS
	SELECT	ppd.Id, 
			ppd.ConfirmDateAccountReceivable, 
			ppd.PortfolioProvisionId, 
			ppd.AccountReceivableId, 
			ppd.InvoiceNumber, 
			ppd.Days,
			ppd.AgesId, 
			ppd.FacturerValue, 
			ppd.BalanceAccountReceivable, 
			ppd.ValueGlosado, 
			ppd.Expectative, 
			ppd.[Percentage], 
			ppd.NetPresentValue, 
			ppd.AccumulatedDeterioration, 
			ppd.Value,		
			cg.EntityType AS Regimen, 
			COALESCE(
            pgr.RegimenName,
            CASE WHEN i.DocumentType = 6 THEN N'Factura Básica'
                 WHEN i.DocumentType = 7 THEN N'Factura de Productos'
                 ELSE NULL END
			) AS RegimenName, 
			CONCAT(tp.Nit, ' - ', tp.[Name]) AS ThirdPartyNitName, 		
			IIF(ap.Id IS NULL, sp.NameMaximumAgeRange, CONCAT('De: ', ap.InitialRange, ' a ', ap.EndRange)) Range,
			lb.TypeBook,
			ppd.LegalBookId,
			ppd.PortfolioDeteriorationClassificationId,
			CONCAT(pdc.Code, ' - ', pdc.Description) AS PortfolioClassification
	FROM Portfolio.PortfolioProvision pp
	JOIN Portfolio.PortfolioProvisionDetail ppd ON pp.Id = ppd.PortfolioProvisionId
	JOIN Portfolio.AccountReceivable ar ON ppd.AccountReceivableId = ar.Id
	JOIN Common.ThirdParty tp ON ar.ThirdPartyId = tp.Id	
	LEFT JOIN GeneralLedger.MainAccounts mar ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pgr ON mar.Number = pgr.AccountNumber
	LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
	LEFT JOIN [Contract].CareGroup cg ON ISNULL(ar.CareGroupId, i.CareGroupId) = cg.Id
	LEFT JOIN Portfolio.AgesPortfolio ap ON ppd.AgesId = ap.Id
	LEFT JOIN Portfolio.SettingPortfolio sp ON pp.OperatingUnitId = sp.OperatingUnitId
	LEFT JOIN GeneralLedger.LegalBook lb ON lb.Id = ppd.LegalBookId
	LEFT JOIN Portfolio.PortfolioDeteriorationClassification pdc ON pdc.Id = ppd.PortfolioDeteriorationClassificationId