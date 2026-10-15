



CREATE VIEW [Portfolio].[VReportSuretyFile]
AS
with 	cte_companySettings as (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings), 

	cte_cashReceiptCurrency as (	SELECT c.Id CashReceiptsId, COALESCE(cr.CurrencyId,eba.CurrencyId,cs.OfficialCurrencyId) CurrencyId
									FROM Treasury.CashReceipts c WITH (NOLOCK)
									LEFT JOIN Treasury.CashRegisters cr WITH(NOLOCK) ON c.IdCashRegister=cr.Id
									LEFT JOIN Treasury.EntityBankAccounts eba WITH(NOLOCK) on c.IdBankAccount=eba.Id
									JOIN cte_companySettings cs WITH(NOLOCK) ON 1=1
									GROUP BY c.Id,COALESCE(cr.CurrencyId,eba.CurrencyId,cs.OfficialCurrencyId))

SELECT
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
		/*IdOperatingUnit,NameOperatingUnit,*/
	   SUM(ValueOperatingUnit) AS ValueOperatingUnit
	FROM 
	(
		--- Saco todos los los recibos de caja que realizaron cruce con Facturas en el mismo formulario de RC
		SELECT
			c.Id AS ReceiptId
			/*,ou.Id as IdOperatingUnit
			,ou.UnitName as NameOperatingUnit*/
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
			/*ou.Id as IdOperatingUnit,ou.UnitName as NameOperatingUnit,*/
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
	AND padv.CashReceiptDetailId IS NULL

---NOTA: Se hubiera podido hacer en un solo select empezando la consulta por el PortfolioAdvance, pero medilaser lo usa para saber todo lo que ha recaudado asi esto no genere anticipo entonces no les serviria a ellos, por tal razon toco hacer el union all