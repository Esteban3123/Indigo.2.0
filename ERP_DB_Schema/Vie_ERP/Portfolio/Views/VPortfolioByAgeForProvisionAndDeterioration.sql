CREATE VIEW [Portfolio].[VPortfolioByAgeForProvisionAndDeterioration]
AS
SELECT
	ar.AccountReceivableId,
	ar.DocumentCode,
	ar.DocumentDate,
	ar.RadicatedDate,
	ar.Value,
	ar.Balance,
	ar.GlosaPortfolioGlosadaId,
	ar.BalanceGlosa,
	ar.Expectative,
	ar.DeteriorationBalance,
	ar.Regimen,
	ar.RegimenName,
	ar.ThirdPartyNit,
	ar.ThirdPartyName
FROM
(
	SELECT
		ar.Id as AccountReceivableId, 
		ar.InvoiceNumber AS DocumentCode, 
		CASE ar.AccountReceivableType 
			WHEN 1 THEN ar.AccountReceivableDate 
			ELSE ri.DocumentDate 
		END DocumentDate, 
		CASE ar.AccountReceivableType 
			WHEN 1 THEN ar.AccountReceivableDate 
			ELSE ri.ConfirmDate 
		END AS RadicatedDate, 
		ar.[Value], 
		ar.Balance,		
		gpg.Id as GlosaPortfolioGlosadaId,  
		ISNULL(gpg.BalanceGlosa, 0) AS BalanceGlosa,
		ISNULL(pp.Expectative, 0) Expectative,
		ar.DeteriorationBalance,

		cg.EntityType AS Regimen, 
		CASE cg.EntityType
			WHEN 1 THEN 'EPS Contributivo' 
			WHEN 2 THEN 'EPS Subsidiado' 
			WHEN 3 THEN 'ET Vinculados Municipios' 
			WHEN 4 THEN 'ET Vinculados Departamentos' 
			WHEN 5 THEN 'ARL Riesgos Laborales' 
			WHEN 6 THEN 'MP Medicina Prepagada' 
			WHEN 7 THEN 'IPS Privada' 
			WHEN 8 THEN 'IPS Publica' 
			WHEN 9 THEN 'Regimen Especial'
			WHEN 10 THEN 'Accidentes de transito' 
			WHEN 11 THEN 'Fosyga' 
			WHEN 12 THEN 'Otros' 
			WHEN 99 THEN 'Particulares' 
			ELSE ''
		END AS RegimenName,
		tpar.Nit AS ThirdPartyNit, 
		tpar.Name AS ThirdPartyName 	
	FROM Portfolio.AccountReceivable AS ar WITH (NOLOCK) 
	INNER JOIN Common.ThirdParty AS tpar WITH (NOLOCK) ON tpar.Id = ar.ThirdPartyId 
	LEFT JOIN Billing.Invoice AS i WITH (NOLOCK) ON i.Id = ar.InvoiceId 
	LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON cg.Id = i.CareGroupId 
	LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber 
	LEFT JOIN
	(
		SELECT Ric.DocumentDate,Ric.ConfirmDate, Ric.RadicatedConsecutive, Ric.RadicatedDate, RID.InvoiceNumber, Ric.CreationUser, RID.InvoiceDate,
				Ric.[State] as StateC, RID.[State] as StateD
		FROM Portfolio.RadicateInvoiceD AS RID WITH (NOLOCK) 
		INNER JOIN Portfolio.RadicateInvoiceC AS RIC WITH (NOLOCK) ON Ric.Id = RID.RadicateInvoiceCId AND ISNULL(RID.State, 0) = 2 AND Ric.State = 2
	) AS ri ON ri.InvoiceNumber = ar.InvoiceNumber
	LEFT JOIN
	(
		SELECT ppd.AccountReceivableId, ppd.Value, ppd.Expectative
		FROM Portfolio.PortfolioProvision pp
		JOIN Portfolio.PortfolioProvisionDetail ppd ON pp.Id = ppd.PortfolioProvisionId
		JOIN
		(
			SELECT ppd.AccountReceivableId, MAX(pp.Id) Id
			FROM Portfolio.PortfolioProvision pp
			JOIN Portfolio.PortfolioProvisionDetail ppd ON pp.Id = ppd.PortfolioProvisionId
			WHERE pp.DocumentType = 2 AND pp.Status = 2
			GROUP BY ppd.AccountReceivableId
		) pm ON ppd.AccountReceivableId = pm.AccountReceivableId AND pp.Id = pm.Id
		WHERE pp.DocumentType = 2 AND pp.Status = 2
	) pp ON ar.Id = pp.AccountReceivableId
	WHERE ar.Status = 2 AND ar.AccountReceivableType IN (1, 2)
		AND ri.StateC = 2 AND ri.StateD = 2
) ar
WHERE ar.DocumentDate IS NOT NULL