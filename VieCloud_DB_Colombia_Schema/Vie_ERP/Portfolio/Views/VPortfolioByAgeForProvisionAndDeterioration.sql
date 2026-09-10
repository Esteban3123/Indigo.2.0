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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de cartera envejecida para el cálculo de provisiones contables y deterioro de valor de cuentas por cobrar. Integra las cuentas por cobrar activas (facturas y documentos de cobro radicados y confirmados) con los datos del tercero pagador (NIT y nombre), el régimen o tipo de entidad (EPS contributivo, subsidiado, ARL, medicina prepagada, particulares, entre otros), el saldo de glosas pendientes y la expectativa de recaudo registrada en la última provisión aprobada. Se utiliza para reportes financieros y contables de envejecimiento de cartera, deterioro de activos financieros y cálculo de provisiones de difícil cobro, filtrando únicamente documentos con radicación confirmada y estados activos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VPortfolioByAgeForProvisionAndDeterioration';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la cartera vigente por edad para análisis de provisión y deterioro, integrando radicación confirmada, glosas y la última provisión activa por cuenta.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen cuentas por cobrar con Status=2 y AccountReceivableType IN (1,2); Las facturas tipo 2 deben tener radicación con RadicateInvoiceC.State=2 y RadicateInvoiceD.State=2 para aparecer; Las provisiones consideradas deben tener DocumentType=2 y Status=2', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cuentas por cobrar activas (Status=2) de tipo 1 o 2; Únicamente se considera la provisión más reciente (MAX(Id)) con DocumentType=2 y Status=2 por cuenta; Las facturas tipo 2 sin radicación confirmada (StateC=2 y StateD=2) quedan excluidas; BalanceGlosa y Expectative nunca son NULL (se aplica ISNULL a 0); Cada fila representa una sola cuenta por cobrar enriquecida con tercero, régimen, glosa y provisión vigente', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cartera por edades; cuenta por cobrar; provisión de cartera; deterioro de cartera; glosa; radicación de factura; régimen de afiliación; tercero pagador; expectativa de recaudo', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VPortfolioByAgeForProvisionAndDeterioration: Devuelve solo registros con DocumentDate NOT NULL, ar.Status=2 y AccountReceivableType IN (1,2), filtrando además por radicado confirmado (StateC=2 y StateD=2)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.AccountReceivableType = 1 → DocumentDate y RadicatedDate se toman de ar.AccountReceivableDate (cuenta de cobro propia) else DocumentDate se toma de ri.DocumentDate y RadicatedDate de ri.ConfirmDate (factura radicada); si cg.EntityType (1..12, 99) → Se traduce el código de régimen a su nombre legible (EPS Contributivo, Subsidiado, ARL, Fosyga, Particulares, etc.) else RegimenName queda como cadena vacía; si pp.DocumentType=2 AND pp.Status=2 → Se selecciona la provisión de mayor Id (MAX) por AccountReceivableId para tomar Expectative else Expectative=0 vía ISNULL', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Billing.Invoice; Contract.CareGroup; Glosas.GlosaPortfolioGlosada; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Portfolio.PortfolioProvision; Portfolio.PortfolioProvisionDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioByAgeForProvisionAndDeterioration';
GO
