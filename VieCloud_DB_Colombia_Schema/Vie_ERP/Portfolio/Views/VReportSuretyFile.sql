CREATE VIEW [Portfolio].[VReportSuretyFile]
AS
with 	cte_companySettings as (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings), 

	cte_cashReceiptCurrency as (	SELECT c.Id CashReceiptsId, COALESCE(cr.CurrencyId,eba.CurrencyId,cs.OfficialCurrencyId) CurrencyId
									FROM Treasury.CashReceipts c WITH (NOLOCK)
									LEFT JOIN Treasury.CashRegisters cr WITH(NOLOCK) ON c.IdCashRegister=cr.Id
									LEFT JOIN Treasury.EntityBankAccounts eba WITH(NOLOCK) on c.IdBankAccount=eba.Id
									JOIN cte_companySettings cs WITH(NOLOCK) ON 1=1
									GROUP BY c.Id,COALESCE(cr.CurrencyId,eba.CurrencyId,cs.OfficialCurrencyId)),

base As (SELECT
	c.Code AS ReceiptCode,
	c.CollectType,
	c.Detail,
	t.Nit,
	t.Name,
	SaldosAvances.Account,
	eba.Number AS BankAccount,
	b.Name + ' - ' + eba.Number AS Bank,
	b.Name AS BankName,
	cr.Code + ' - ' + cr.Name AS CashRegister,
	c.DocumentDate,
	c.value,
	c.value + ISNULL(Withholding, 0) + ISNULL(IVARetention, 0) + ISNULL(ICARetention, 0) + ISNULL(OtherRetention, 0) AS NetValue,
	Withholding,
	IVARetention,
	ICARetention,
	OtherRetention,
	/*IdOperatingUnit,NameOperatingUnit,*/
	IngresosGroup.ValueOperatingUnit,
	TransferData.TransferDate,
	ISNULL(SaldosAvances.Balance, 0) AS Balance,
	CASE
		WHEN SaldosAvances.Balance IS NULL AND IngresosGroup.ValueOperatingUnit IS NULL THEN 'No Aplica'
		WHEN ISNULL(SaldosAvances.Balance, 0) = 0 THEN 'Cruzado'
		ELSE 'Pendiente'
	END AS Status,
	ci.Id CurrencyId,
	ci.Abbreviation CurrencyAbbreviation
FROM Treasury.CashReceipts c WITH (NOLOCK)
INNER JOIN Common.ThirdParty t WITH (NOLOCK) ON t.Id = c.IdThirdParty
LEFT JOIN 
(
	SELECT
		MIN(ISNULL(pt.DocumentDate, ipa.InvoiceDate)) AS TransferDate,
		c.Id AS CashReceiptId
	FROM Treasury.CashReceipts c WITH (NOLOCK)
	INNER JOIN Treasury.CashReceiptDetails cd WITH (NOLOCK) ON c.Id = cd.IdCashReceipt
	INNER JOIN Portfolio.PortfolioAdvance pa WITH (NOLOCK) ON pa.CashReceiptDetailId = cd.Id
	LEFT JOIN Portfolio.PortfolioTransfer pt WITH (NOLOCK) ON pt.PortfolioAdvanceId = pa.Id AND pt.Status = 2
	LEFT JOIN
	(
		SELECT ipa.PortfolioAdvanceId, MIN(i.InvoiceDate) InvoiceDate
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN Billing.InvoicePortfolioAdvance ipa WITH (NOLOCK) ON i.Id = ipa.InvoiceId
		WHERE i.DocumentType = 5
			AND i.Status = 1
		GROUP BY ipa.PortfolioAdvanceId
	) ipa ON pa.Id = ipa.PortfolioAdvanceId
	WHERE ISNULL(pt.Id, ipa.PortfolioAdvanceId) IS NOT NULL
	GROUP BY c.Id
) AS TransferData ON TransferData.CashReceiptId = c.Id
LEFT JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON eba.Id = c.IdBankAccount
LEFT JOIN Payroll.Bank b WITH (NOLOCK) ON eba.IdBank = b.Id
LEFT JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON cr.Id = c.IdCashRegister
LEFT JOIN 
(
	-- Saco Agrupado los valores que se han cruzado en las unidades operaqtivas de las facturas, ya sea por RC o por Traslado
	SELECT
		ReceiptId,
	   SUM(ValueOperatingUnit) AS ValueOperatingUnit
	FROM 
	(
		--- Saco todos los los recibos de caja que realizaron cruce con Facturas en el mismo formulario de RC
		SELECT
			c.Id AS ReceiptId
		   ,iif(car.ValueInCurrencyHeader =0,car.Value,car.ValueInCurrencyHeader) AS ValueOperatingUnit
		FROM Treasury.CashReceipts c WITH (NOLOCK)
		INNER JOIN Treasury.CashReceiptDetails cd WITH (NOLOCK) ON c.Id = cd.IdCashReceipt
		INNER JOIN Treasury.CashReceiptAccountReceivable car WITH (NOLOCK) ON car.CashReceiptDetailId = cd.Id
		INNER JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON car.AccountReceivableId = ar.Id
		INNER JOIN Common.OperatingUnit ou WITH (NOLOCK) ON ou.Id = ar.OperatingUnitId
		WHERE c.Status = 2

		UNION ALL

		--- Saco todos los recibos de caja que realizaron anticipos y que posteriormente podrian ser cruzados a través de un traslado
		SELECT
			c.Id AS ReceiptId,
			Common.CurrencyConverterWithDate( ptd.Value,isnull(pa.CurrencyId,cs.OfficialCurrencyId),cte.CurrencyId,pt.CreationDate) AS ValueOperatingUnit
		FROM Treasury.CashReceipts c WITH (NOLOCK)
		INNER JOIN Treasury.CashReceiptDetails cd WITH (NOLOCK) ON c.Id = cd.IdCashReceipt
		INNER JOIN Portfolio.PortfolioAdvance pa WITH (NOLOCK) ON pa.CashReceiptDetailId = cd.Id
		INNER JOIN Portfolio.PortfolioTransfer pt WITH (NOLOCK) ON pt.PortfolioAdvanceId = pa.Id AND pt.Status = 2
		INNER JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON ptd.PortfolioTrasferId = pt.Id
		INNER JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ptd.AccountReceivableId = ar.Id
		INNER JOIN Common.OperatingUnit ou WITH (NOLOCK) ON ou.Id = ar.OperatingUnitId
		INNER JOIN cte_cashReceiptCurrency cte ON cte.CashReceiptsId=c.Id
		INNER JOIN cte_companySettings cs on 1=1
		WHERE c.Status = 2

		UNION ALL 

		-- Saco todos los recibos de caja que realizaron anticipos cruzados con controles de servicios de capitacion
		SELECT
			c.Id AS ReceiptId,
			iif(pa.ValueInCurrencyHeader=0,pa.Value,pa.ValueInCurrencyHeader) AS ValueOperatingUnit
		FROM Treasury.CashReceipts c WITH (NOLOCK)
		INNER JOIN Portfolio.PortfolioAdvance pa WITH (NOLOCK) ON pa.CashReceiptId = c.Id		
		JOIN
		(
			SELECT ipa.PortfolioAdvanceId, SUM(ipa.Value) Value
			FROM Billing.Invoice i WITH (NOLOCK)
			JOIN Billing.InvoicePortfolioAdvance ipa WITH (NOLOCK) ON i.Id = ipa.InvoiceId
			WHERE i.DocumentType = 5
				AND i.Status = 1
			GROUP BY ipa.PortfolioAdvanceId
		) ipa ON pa.Id = ipa.PortfolioAdvanceId
	) AS Ingresos
	GROUP BY ReceiptId--,IdOperatingUnit, NameOperatingUnit
) AS IngresosGroup ON IngresosGroup.ReceiptId = c.Id
LEFT JOIN 
(
	SELECT
		ReceiptId,
		SUM(Withholding) AS Withholding,
		SUM(IVARetention) AS IVARetention,
		SUM(ICARetention) AS ICARetention,
		SUM(OtherRetention) AS OtherRetention
	FROM 
	(
		--- Cargo todos las retencion que se realizaron al momento de hacer el RC
		SELECT
			c.Id AS ReceiptId,
			CASE ma.RetencionType
				WHEN 1 THEN iif(cd.ValueInCurrencyHeader =0, cd.Value,cd.ValueInCurrencyHeader)
				ELSE 0
			END AS Withholding,
			CASE ma.RetencionType
				WHEN 2 THEN iif(cd.ValueInCurrencyHeader =0, cd.Value,cd.ValueInCurrencyHeader)
				ELSE 0
			END AS IVARetention,
			CASE ma.RetencionType
				WHEN 3 THEN iif(cd.ValueInCurrencyHeader =0, cd.Value,cd.ValueInCurrencyHeader)
				ELSE 0
			END AS ICARetention,
			CASE ma.RetencionType
				WHEN 4 THEN iif(cd.ValueInCurrencyHeader =0, cd.Value,cd.ValueInCurrencyHeader)
				ELSE 0
			END AS OtherRetention
		FROM Treasury.CashReceipts c WITH (NOLOCK)
		INNER JOIN Treasury.CashReceiptDetails cd WITH (NOLOCK) ON c.Id = cd.IdCashReceipt
		INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = cd.IdMainAccount
		WHERE c.[Status] = 2
			AND cd.CashReceiptConceptAffectation = 1
			AND cd.IdRetentionConcept IS NOT NULL
		
		UNION ALL
		
		--- Cargo todos las retencion que se realizaron al momento de hacer un traslado
		SELECT
			c.Id AS ReceiptId,
			CASE ma.RetencionType
				WHEN 1 THEN Common.CurrencyConverterWithDate( ptc.value,isnull(pa.CurrencyId,cs.OfficialCurrencyId),cte.CurrencyId,cast(pt.CreationDate as DATE))
				ELSE 0
			END AS Withholding,
			CASE ma.RetencionType
				WHEN 2 THEN Common.CurrencyConverterWithDate( ptc.value,isnull(pa.CurrencyId,cs.OfficialCurrencyId),cte.CurrencyId,cast(pt.CreationDate as DATE))
				ELSE 0
			END AS IVARetention,
			CASE ma.RetencionType
				WHEN 3 THEN Common.CurrencyConverterWithDate( ptc.value,isnull(pa.CurrencyId,cs.OfficialCurrencyId),cte.CurrencyId,cast(pt.CreationDate as DATE))
				ELSE 0
			END AS ICARetention,
			CASE ma.RetencionType
				WHEN 4 THEN Common.CurrencyConverterWithDate( ptc.value,isnull(pa.CurrencyId,cs.OfficialCurrencyId),cte.CurrencyId,cast(pt.CreationDate as DATE))
				ELSE 0
			END AS OtherRetention
		FROM Treasury.CashReceipts c WITH (NOLOCK)
		INNER JOIN Treasury.CashReceiptDetails cd WITH (NOLOCK) ON c.Id = cd.IdCashReceipt
		INNER JOIN Portfolio.PortfolioAdvance pa WITH (NOLOCK) ON pa.CashReceiptDetailId = cd.Id
		INNER JOIN Portfolio.PortfolioTransfer pt WITH (NOLOCK) ON pt.PortfolioAdvanceId = pa.Id
		INNER JOIN Portfolio.PortfolioTransferOtherConcept ptc WITH (NOLOCK) ON ptc.PortfolioTransferId = pt.Id
		INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = ptc.MainAccountId
		JOIN cte_cashReceiptCurrency cte on c.Id = cte.CashReceiptsId
		JOIN cte_companySettings cs WITH(NOLOCK) ON 1=1
		WHERE c.Status = 2
	) AS Retenciones
	GROUP BY ReceiptId
) RetencionesGroup ON RetencionesGroup.ReceiptId = c.Id
LEFT JOIN
(
	--Obtengo los saldos se todos los anticipos qie se hayan echo por recibos de caja con su respectivo saldo
	SELECT
		cr.Id AS ReceiptId,
		concat(ma.Number, ' - ', ma.[Name]) as Account,
		SUM(Common.CurrencyConverterWithDate(pa.Balance - ISNULL(ipa.Value, 0),isnull(pa.CurrencyId,cs.OfficialCurrencyId),cte.CurrencyId,pa.CreationDate)) AS Balance
		--SUM(pa.Balance - ISNULL(ipa.Value, 0)) AS Balance
	FROM Portfolio.PortfolioAdvance pa WITH (NOLOCK)
	INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) on ma.Id = pa.MainAccountId
	INNER JOIN Treasury.CashReceipts cr WITH (NOLOCK) ON cr.Id = pa.CashReceiptId
	LEFT JOIN
	(
		SELECT ipa.PortfolioAdvanceId, SUM(ipa.Value) Value
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN Billing.InvoicePortfolioAdvance ipa WITH (NOLOCK) ON i.Id = ipa.InvoiceId
		WHERE i.DocumentType = 5
			AND i.Status = 1
		GROUP BY ipa.PortfolioAdvanceId
	) ipa ON pa.Id = ipa.PortfolioAdvanceId
	JOIN cte_cashReceiptCurrency cte ON cte.CashReceiptsId=cr.Id
	JOIN cte_companySettings cs on 1=1
	WHERE (pa.Balance - ISNULL(ipa.Value, 0)) > 0
		AND NOT EXISTS
		(
			SELECT 1
			FROM Portfolio.PortfolioNoteDistribution pnd
			WHERE pnd.PortfolioAdvanceId = pa.Id
		)
	GROUP BY cr.Id, ma.Number, ma.[Name]
) AS SaldosAvances ON SaldosAvances.ReceiptId = c.Id
JOIN cte_cashReceiptCurrency cte on c.Id = cte.CashReceiptsId
JOIN Common.Currency ci WITH(NOLOCK) on cte.CurrencyId=ci.Id
WHERE c.Status = 2

UNION ALL

--- obtengo todos los acticipos que se crearon por distribucion de anticipo y por saldo inicial
SELECT
	CONCAT(padv.Code,'-','Anticipo') AS ReceiptCode,
	CASE padv.OpeningBalance
		WHEN 1 THEN 3
		ELSE 4
	END AS CollectType,
	CASE padv.OpeningBalance
		WHEN 1 THEN 'SALDO INICIAL'
		ELSE 'DISTRIBUCION DE ANTICIPO'
	END AS Detail,
	t.Nit,
	t.Name,
	concat(ma.Number, ' - ', ma.[Name]) as Account,
	eba.Number AS BankAccount,
	b.Name + ' - ' + eba.Number AS Bank,
	b.Name AS BankName,
	cr.Code + ' - ' + cr.Name AS CashRegister,
	padv.DocumentDate,
	padv.value,
	padv.value + ISNULL(Withholding, 0) + ISNULL(IVARetention, 0) + ISNULL(ICARetention, 0) + ISNULL(OtherRetention, 0) AS NetValue,
	Withholding,
	IVARetention,
	ICARetention,
	OtherRetention,
	/*IdOperatingUnit,NameOperatingUnit,*/
   IngresosGroup.ValueOperatingUnit,
   TransferData.TransferDate,
   ISNULL(padv.Balance, 0) AS Balance,
   CASE
		WHEN padv.Balance = 0 THEN 'Cruzado'
		ELSE 'Pendiente'
	END AS Status,
	ci.Id CurrencyId,
	ci.Abbreviation CurrencyAbbreviation
FROM Portfolio.PortfolioAdvance padv WITH (NOLOCK)
inner join GeneralLedger.MainAccounts ma WITH (NOLOCK) on ma.Id = padv.MainAccountId
INNER JOIN Common.ThirdParty t WITH (NOLOCK) ON t.Id = padv.ThirdPartyId
LEFT JOIN Treasury.CashReceipts c WITH (NOLOCK) ON padv.CashReceiptId = c.Id
LEFT JOIN 
(
	SELECT
		MIN(pt.DocumentDate) AS TransferDate,
		pt.PortfolioAdvanceId
	FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
	WHERE pt.Status = 2
	GROUP BY pt.PortfolioAdvanceId
) AS TransferData ON TransferData.PortfolioAdvanceId = padv.Id
LEFT JOIN Treasury.EntityBankAccounts eba WITH (NOLOCK) ON eba.Id = c.IdBankAccount
LEFT JOIN Payroll.Bank b WITH (NOLOCK) ON eba.IdBank = b.Id
LEFT JOIN Treasury.CashRegisters cr WITH (NOLOCK) ON cr.Id = c.IdCashRegister
LEFT JOIN 
(
	--- Saco agrupado los valores que han sido cruzado por traslados
	SELECT
		pt.PortfolioAdvanceId, 
		SUM(ptd.value) AS ValueOperatingUnit
	FROM Treasury.CashReceipts c WITH (NOLOCK)
	INNER JOIN Treasury.CashReceiptDetails cd WITH (NOLOCK) ON c.Id = cd.IdCashReceipt
	INNER JOIN Portfolio.PortfolioAdvance pa WITH (NOLOCK) ON pa.CashReceiptDetailId = cd.Id
	INNER JOIN Portfolio.PortfolioTransfer pt WITH (NOLOCK) ON pt.PortfolioAdvanceId = pa.Id AND pt.Status = 2
	INNER JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON ptd.PortfolioTrasferId = pt.Id
	INNER JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ptd.AccountReceivableId = ar.Id
	INNER JOIN Common.OperatingUnit ou WITH (NOLOCK) ON ou.Id = ar.OperatingUnitId
	WHERE c.Status = 2
	GROUP BY pt.PortfolioAdvanceId
) AS IngresosGroup ON IngresosGroup.PortfolioAdvanceId = padv.Id
LEFT JOIN 
(
	--- Cargo todos las retencion que se realizaron al momento de hacer un traslado
	SELECT
		pt.PortfolioAdvanceId,
		SUM
		(
			CASE ma.RetencionType
				WHEN 1 THEN ptc.value
				ELSE 0
			END
		) AS Withholding,
	   SUM
	   (
			CASE ma.RetencionType
				WHEN 2 THEN ptc.value
				ELSE 0
			END
		) AS IVARetention,
		SUM
		(
			CASE ma.RetencionType
				WHEN 3 THEN ptc.value
				ELSE 0
			END
		) AS ICARetention,
		SUM
		(
			CASE ma.RetencionType
				WHEN 4 THEN ptc.value
				ELSE 0
			END
		) AS OtherRetention
	FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
	INNER JOIN Portfolio.PortfolioTransferOtherConcept ptc WITH (NOLOCK) ON ptc.PortfolioTransferId = pt.Id
	INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = ptc.MainAccountId
	INNER JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pt.PortfolioAdvanceId=pa.Id
	WHERE pt.Status = 2
	GROUP BY pt.PortfolioAdvanceId
) RetencionesGroup ON RetencionesGroup.PortfolioAdvanceId = padv.Id
JOIN cte_companySettings cs on 1=1
JOIN Common.Currency ci WITH(NOLOCK) on ci.Id=isnull(padv.CurrencyId,cs.OfficialCurrencyId)
WHERE padv.Status = 2
	AND padv.CashReceiptDetailId IS NULL)
        SELECT
    ROW_NUMBER() OVER (
        ORDER BY
            ReceiptCode
    ) AS Id,
    ReceiptCode,
    CollectType,
    Detail,
    Nit,
    Name,
    Account,
    BankAccount,
    Bank,
    BankName,
    CashRegister,
    DocumentDate,
    value,
    NetValue,
    Withholding,
    IVARetention,
    ICARetention,
    OtherRetention,
    ValueOperatingUnit,
    TransferDate,
    Balance,
    Status,
    CurrencyId,
    CurrencyAbbreviation
FROM base;

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de archivo de garantías (surety file) de cartera. Consolida la información de los recibos de caja registrados en tesorería, integrando datos del tercero pagador (empresa, aseguradora, paciente), la cuenta bancaria o caja receptora, las retenciones aplicadas (renta, IVA, ICA, otras) y los anticipos de cartera generados. Calcula el saldo pendiente de cada recibo, el estado de cruce (Cruzado, Pendiente o No Aplica) y la fecha de traslado, considerando cruces directos con facturas, traslados de anticipo y controles de capitación. Sirve como fuente principal para reportes de gestión de cartera, conciliación de pagos recibidos y seguimiento de anticipos de aseguradoras o pagadores.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportSuretyFile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportSuretyFile';
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte los recibos de caja confirmados y los anticipos de cartera (saldo inicial o distribución), mostrando valor, retenciones, valor cruzado, traslados, saldo pendiente y estado de cruce por moneda.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido (se usa como moneda por defecto).; Los recibos de caja deben tener Status = 2 (confirmado) para ser incluidos.; Los anticipos de cartera independientes deben tener Status = 2 y CashReceiptDetailId NULL para incluirse en la rama de anticipos.; Las facturas usadas para cruce de anticipos deben tener DocumentType = 5 y Status = 1.; Los traslados de cartera (PortfolioTransfer) deben tener Status = 2 para considerarse efectivos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'NetValue = value + suma de las cuatro retenciones (Withholding, IVA, ICA, Otros) tratando NULL como 0.; La moneda del recibo se resuelve en cascada: caja registradora → cuenta bancaria → moneda oficial de la empresa.; Solo se reportan recibos de caja con Status = 2; los anulados/borradores se excluyen.; Solo se reportan anticipos cruzados/aplicados a través de facturas con DocumentType=5 y Status=1.; Solo se consideran traslados de cartera con Status = 2 como efectivos para fechas y valores cruzados.; Los anticipos vinculados a un detalle de recibo (CashReceiptDetailId NOT NULL) no se duplican en la rama de anticipos independientes.; Los anticipos destino creados por distribución de notas (PortfolioNoteDistribution.PortfolioAdvanceId) no se suman en SaldosAvances del recibo origen, porque ya se reportan por el tercero destino en la rama de anticipos independientes.; Los saldos de anticipos se convierten siempre a la moneda del recibo usando Common.CurrencyConverterWithDate.; Solo se reportan anticipos cuyo saldo restante (pa.Balance - ipa.Value) sea > 0 en la sub-consulta de SaldosAvances.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recibo de caja; Anticipo de cartera; Traslado de cartera; Retención en la fuente; Retención de IVA; Retención de ICA; Otras retenciones; Saldo inicial; Distribución de anticipo; Unidad operativa; Cuenta por cobrar; Cruce de cartera; Moneda oficial; Tercero (NIT); Caja registradora; Cuenta bancaria', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas de recibos de caja (Status=2) unidas a anticipos de cartera (Status=2 y CashReceiptDetailId IS NULL) con un Id generado por ROW_NUMBER() ordenado por ReceiptCode.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SaldosAvances.Balance IS NULL AND IngresosGroup.ValueOperatingUnit IS NULL → Status = ''No Aplica'' else Si Balance=0 → ''Cruzado''; en otro caso → ''Pendiente''; si padv.OpeningBalance = 1 (anticipo) → CollectType = 3 y Detail = ''SALDO INICIAL'' else CollectType = 4 y Detail = ''DISTRIBUCION DE ANTICIPO''; si padv.Balance = 0 (rama de anticipos) → Status = ''Cruzado'' else Status = ''Pendiente''; si ma.RetencionType = 1/2/3/4 → Clasifica el valor como Withholding, IVARetention, ICARetention u OtherRetention respectivamente else 0; si cd.ValueInCurrencyHeader = 0 (o pa.ValueInCurrencyHeader / car.ValueInCurrencyHeader) → Usa el valor en moneda local (Value) else Usa el valor en moneda del encabezado (ValueInCurrencyHeader); si i.DocumentType = 5 AND i.Status = 1 → La factura se considera aplicada al anticipo y su valor reduce el balance disponible (pa.Balance - ipa.Value)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Treasury.CashReceipts; Treasury.CashRegisters; Treasury.EntityBankAccounts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Common.ThirdParty; Common.OperatingUnit; Common.Currency; Portfolio.PortfolioAdvance; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransferOtherConcept; Portfolio.AccountReceivable; Billing.Invoice; Billing.InvoicePortfolioAdvance; GeneralLedger.MainAccounts; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportSuretyFile';
GO