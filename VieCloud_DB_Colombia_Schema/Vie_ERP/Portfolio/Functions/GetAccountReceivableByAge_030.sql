-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-27
-- Description:	Obtiene laas cuentas por cobrar para el informe de cartera por edades
-- =============================================
CREATE FUNCTION [Portfolio].[GetAccountReceivableByAge_030]
(
	@InitialDate DATE,
	@ClosingDate DATE
)
RETURNS TABLE
AS
RETURN
	WITH 
	temp_receivable AS (
		SELECT *
		FROM Portfolio.AccountReceivable WITH (NOLOCK)
		WHERE ISNULL(DateAccountBecomeZero,@ClosingDate) >= @ClosingDate
		OR (DateAccountBecomeZero IS NOT NULL AND DateAccountBecomeZero >= @InitialDate AND DateAccountBecomeZero <= @ClosingDate)
	),
	temp_RI AS (
		SELECT rid.InvoiceNumber, MIN(ri.Id) Id, MIN(rid.RadicatedNumber) RadicatedNumber, MIN(rid.RadicatedDate) RadicatedDate
		FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
		JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
		inner join temp_receivable r on r.InvoiceNumber = rid.InvoiceNumber
		WHERE ri.State = 2 AND rid.State = 2 AND CAST(ri.ConfirmDate AS DATE) <= @ClosingDate
		GROUP BY rid.InvoiceNumber
	),
	temp_PRL AS 
	(
		SELECT MAX(c.Id) Id, c.AccountReceivableId
		FROM Portfolio.PortfolioReclassification c WITH (NOLOCK)
		join temp_receivable r on r.Id = c.AccountReceivableId
		WHERE CAST(COALESCE(c.DocumentDate, c.CreationDate, @ClosingDate) AS DATE) <= @ClosingDate
		GROUP BY c.AccountReceivableId
	),
	temp_PR AS 
	(
		SELECT pr.AccountReceivableId, pr.TargetAccountId
		FROM Portfolio.PortfolioReclassification pr WITH (NOLOCK)
		JOIN temp_PRL ult ON ult.Id = pr.Id
	),
	temp_ARA AS (
		SELECT ara.AccountReceivableId, MAX(ara.Id) Id
		FROM Portfolio.AccountReceivableAccounting ara WITH (NOLOCK)
		LEFT JOIN temp_PR pr ON pr.AccountReceivableId = ara.AccountReceivableId
			AND pr.TargetAccountId = ara.MainAccountId
		GROUP BY ara.AccountReceivableId
	),
	temp_ICR AS (
		SELECT c.InvoiceId, MAX(c.CalculateTaxAdvance) CalculateTaxAdvance, SUM(c.Value) InitialRetention
		FROM Billing.InvoiceCustomerRetention c WITH (NOLOCK)
		join temp_receivable r on r.InvoiceId = c.InvoiceId
		GROUP BY c.InvoiceId
	),
	temp_ARS AS (
		SELECT 
			ars.AccountReceivableId,
			SUM(ars.Balance - ars.DebitValue + ars.CreditValue + ars.TransferValue + ars.PaymentValue + ars.CrossingValue) InitialBalanceValue
		FROM Portfolio.AccountReceivableShare ars WITH (NOLOCK)		
		join temp_receivable r on r.Id = ars.AccountReceivableId
		GROUP BY ars.AccountReceivableId
	),
	temp_PN AS (
		SELECT 
			pnara.AccountReceivableId, 
			SUM(IIF(pn.Nature = 1, pnara.AdjusmentValue, 0)) DebitValue,
			SUM(IIF(pn.Nature = 1, 0, pnara.AdjusmentValue)) CreditValue,
			SUM(ISNULL(pnarr.AdjustmetRetentionValue, 0)) AdjustmetRetentionValue,
			MAX(IIF(@InitialDate IS NOT NULL AND CAST(pn.NoteDate AS DATE) >= @InitialDate AND CAST(pn.NoteDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod
		FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
		JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
		join temp_receivable r on r.Id = pnara.AccountReceivableId
		LEFT JOIN
		(
			SELECT PortfolioNoteAccountReceivableId, SUM(Value * IIF(Nature = 1, 1, -1)) AdjustmetRetentionValue
			FROM Portfolio.PortfolioNoteAccountReceivableRetention WITH (NOLOCK)
			GROUP BY PortfolioNoteAccountReceivableId
		) pnarr ON pnara.Id = pnarr.PortfolioNoteAccountReceivableId
		WHERE pn.Status = 2 AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
		GROUP BY pnara.AccountReceivableId
	),
	temp_PT AS (SELECT 
				ptd.AccountReceivableId,
				CAST(SUM(ptd.ValueInCurrencyInvoice) AS DECIMAL(18,2)) TransferValue,
				MAX(IIF(@InitialDate IS NOT NULL AND CAST(pt.DocumentDate AS DATE) >= @InitialDate AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod
			FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
			JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
			join temp_receivable r on r.Id = ptd.AccountReceivableId
			WHERE pt.Status IN (2, 4) 
				AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate
				AND CAST(ISNULL(pt.RecersalDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > @ClosingDate
			GROUP BY ptd.AccountReceivableId
			),

	temp_CR AS (SELECT 
				crar.AccountReceivableId,
				SUM(crar.Value) CashReceiptValue,
				MAX(IIF(@InitialDate IS NOT NULL AND CAST(cr.DocumentDate AS DATE) >= @InitialDate AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
				join temp_receivable r on r.Id = crar.AccountReceivableId
				WHERE cr.Status IN (2 , 4)
					AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(cr.ReversedDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > @ClosingDate
				GROUP BY crar.AccountReceivableId
			),

	temp_CA AS (
			SELECT 
				cad.AccountReceivableId,
				SUM(cad.CrossingValue) CrossingValue,
				MAX(IIF(@InitialDate IS NOT NULL AND CAST(ca.DocumentDate AS DATE) >= @InitialDate AND CAST(ca.DocumentDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod
			FROM Treasury.CrossingAccount ca WITH (NOLOCK)
			JOIN Treasury.CrossingAccountDetailCxC cad WITH (NOLOCK) ON ca.Id = cad.CrossingAccountId
			join temp_receivable r on r.Id = cad.AccountReceivableId
			WHERE ca.Status = 2
				AND CAST(ca.DocumentDate AS DATE) <= @ClosingDate
			GROUP BY cad.AccountReceivableId
		)

	SELECT	ar.Id,
				ar.OperatingUnitId,
				ISNULL(i.InvoiceCategoryId,ar.InvoiceCategoryId) as InvoiceCategoryId,
				ISNULL(ar.CareGroupId, i.CareGroupId) CareGroupId,
				i.ContractId,
				ar.ThirdPartyId,			
				ar.InvoiceNumber, 
				i.AdmissionNumber,
				ar.AccountReceivableDate AS AccountReceivableDate, 
				ar.AccountReceivableType,
				ar.NumberShares,
				ar.Term,
				ar.OpeningBalance,
				-----------------------------------------------------------------------------------------------------------
				IIF(ar.PortfolioStatus < 3, ar.PortfolioStatus, 
				CASE ara.MainAccountId
					WHEN iif(ar.PortfolioStatus =3, ara.MainAccountId, ar.AccountRadicateId) THEN 3
					WHEN ar.AccountObjectionRemediedId THEN 4
					WHEN ar.AccountHardCollectionId THEN 15
					WHEN ar.AccountLegalCollectionId THEN 16
					ELSE ar.PortfolioStatus
				END) PortfolioStatus,
				CASE	
					IIF(ar.PortfolioStatus < 3, ar.PortfolioStatus,CASE ara.MainAccountId
						WHEN iif(ar.PortfolioStatus =3, ara.MainAccountId, ar.AccountRadicateId) THEN 3
						WHEN ar.AccountObjectionRemediedId THEN 4
						WHEN ar.AccountHardCollectionId THEN 15
						WHEN ar.AccountLegalCollectionId THEN 16
						ELSE ar.PortfolioStatus
					END)
					-----------------------
					WHEN 1 THEN 'Sin Radicar'
					WHEN 2 THEN 'Radicada Sin Confirmar'
					WHEN 3 THEN 'Radicada Entidad'
					WHEN 4 THEN 'Objetada o Glosada'
					WHEN 7 THEN 'Certificada Parcial'
					WHEN 8 THEN 'Certificada Total'
					WHEN 14 THEN 'Devolucion Factura'
					WHEN 15 THEN 'Cuenta de Dificil Recaudo'
					WHEN 16 THEN 'Cobro Jurídico'
				END as PortfolioStatusName,
				IIF(ar.OpeningBalance = 1, ric.RadicatedNumber, ri.RadicatedConsecutive) RadicatedConsecutive,
				ri.CreationUser AS RadicatedUser,
				IIF(ar.OpeningBalance = 1, ISNULL(ric.RadicatedDate, ri.ConfirmDate), ri.ConfirmDate) AS RadicatedDate,
				ri.State RadicatedState,
				pgr.RegimenName AS RegimenCalculated,	
				mar.Number AS AccountWithoutRadicateNumber, 
				ar.AccountWithoutRadicateId,
				ar.AccountRadicateId,
				ar.AccountHardCollectionId,
				ara.MainAccountId,
				-----------------------------------------------------------------------------------------------------------
				ar.Value AS DocumentValue,
				ISNULL(icr.InitialRetention, 0) + ISNULL(pn.AdjustmetRetentionValue, 0) RetentionValue,
				(ar.Value - ISNULL(ars.InitialBalanceValue, ar.Value)) InitialValue,
				ISNULL(pn.DebitValue, 0) AS DebitValue,
				ISNULL(pn.CreditValue, 0) AS CreditValue,
				ISNULL(pt.TransferValue, 0) AS TransferValue,
				ISNULL(cr.CashReceiptValue, 0) AS CashReceiptValue,
				ISNULL(ca.CrossingValue, 0) AS CrossingValue,
				(
					ar.Value --Valor Inicial
					- IIF(ISNULL(icr.CalculateTaxAdvance, 0) = 2, ISNULL(icr.InitialRetention, 0) + ISNULL(pn.AdjustmetRetentionValue, 0), 0) --Retenciones
					- (ar.Value - ISNULL(ars.InitialBalanceValue, ar.Value)) --Valor saldo inicial
					+ ISNULL(pn.DebitValue, 0) - ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
				) AS Balance,
				(
					ar.Value --Valor Inicial
					- IIF(ISNULL(icr.CalculateTaxAdvance, 0) = 2, ISNULL(icr.InitialRetention, 0) + ISNULL(pn.AdjustmetRetentionValue, 0), 0) --Retenciones
					- (ar.Value - ISNULL(ars.InitialBalanceValue, ar.Value)) --Valor saldo inicial
					+ ISNULL(pn.DebitValue, 0) - ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
				) as CurrentBalance,
				--ar.Balance AS CurrentBalance,
				IIF
				(
					ISNULL(pn.InPeriod, 0) = 1,
					1,
					IIF
					(
						ISNULL(pt.InPeriod, 0) = 1,
						1,
						IIF
						(
							ISNULL(cr.InPeriod, 0) = 1,
							1,
							IIF
							(
								ISNULL(ca.InPeriod, 0) = 1,
								1,
								0
							)
						)
					)
				) InPeriod
				, c.Id CurrencyId
				, c.Name CurrencyName
		FROM temp_receivable AS ar WITH (NOLOCK)
		LEFT JOIN Billing.Invoice AS i WITH (NOLOCK) ON ar.InvoiceId = i.Id
		LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
		LEFT JOIN Portfolio.GetRegimes() pgr ON mar.Number = pgr.AccountNumber
		LEFT JOIN temp_RI ric ON ar.InvoiceNumber = ric.InvoiceNumber
		LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON ric.Id = ri.Id
		/************************************** ESTADO AL CORTE **************************************/
		LEFT JOIN temp_ARA aram ON ar.Id = aram.AccountReceivableId
		LEFT JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) ON aram.Id = ara.Id
		/**************************************** RETENCIONES ****************************************/
		LEFT JOIN temp_ICR icr ON i.Id = icr.InvoiceId
		/****************************************** BALANCE ******************************************/
		LEFT JOIN temp_ARS ars ON ar.OpeningBalance = 1 AND ar.Id = ars.AccountReceivableId
		LEFT JOIN temp_PN pn ON ar.Id = pn.AccountReceivableId
		LEFT JOIN temp_PT pt ON ar.Id = pt.AccountReceivableId
		LEFT JOIN temp_CR cr ON ar.Id = cr.AccountReceivableId
		LEFT JOIN temp_CA ca ON ar.Id = ca.AccountReceivableId
		/****************************************** MONEDA ******************************************/
		LEFT JOIN Common.Currency c on c.Id = ar.CurrencyId
		WHERE NOT (ISNULL(i.DocumentType, 1) = 5 AND ar.AccountReceivableType = 6)
			AND CAST(ar.AccountReceivableDate AS DATE) <= @ClosingDate
			AND CAST(ISNULL(i.AnnulmentDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > @ClosingDate
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula el informe de cartera por edades para un rango de fechas dado (fecha inicial y fecha de corte). Para cada cuenta por cobrar (facturas, cuentas de cobro) vigente en el período, consolida el saldo inicial, los recaudos (recibos de caja, cruce de cuentas), las notas de cartera (débitos y créditos), las transferencias, las reclasificaciones contables y los radicados ante entidades pagadoras (EPS, aseguradoras), permitiendo analizar la antigüedad de la deuda y el estado de cobro de cada documento. Es el motor de cálculo del informe de cartera por edades del módulo de portafolio, combinando cuentas por cobrar, radicación de facturas, movimientos de tesorería y ajustes contables en un único resultado por cuenta.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetAccountReceivableByAge_030';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetAccountReceivableByAge_030';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el dataset de cuentas por cobrar vigentes y movimientos asociados (radicaciones, notas, transferencias, recibos, cruces, retenciones) para el informe de cartera por edades en un rango de fechas dado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ClosingDate debe estar definido para acotar todos los movimientos al corte (CAST(... ) <= @ClosingDate); @InitialDate se utiliza únicamente para marcar movimientos ''InPeriod''; si es NULL, ningún movimiento queda marcado en período; Las cuentas por cobrar deben tener AccountReceivableDate <= @ClosingDate; La factura asociada no debe estar anulada al corte (AnnulmentDate IS NULL o > @ClosingDate)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran radicados con State=2 tanto en encabezado como en detalle, y con ConfirmDate <= @ClosingDate; Solo se consideran notas de cartera con Status=2 y NoteDate <= @ClosingDate; Las transferencias de cartera deben tener Status IN (2,4), DocumentDate <= @ClosingDate y no estar reversadas antes del corte (RecersalDate > @ClosingDate o NULL); Los recibos de caja deben tener Status IN (2,4), DocumentDate <= @ClosingDate y no estar reversados antes del corte (ReversedDate > @ClosingDate o NULL); Los cruces de cuentas (CrossingAccount) deben tener Status=2 y DocumentDate <= @ClosingDate; Las reclasificaciones consideradas son las de mayor Id por cuenta por cobrar cuya fecha (DocumentDate o CreationDate) sea <= @ClosingDate; El saldo (Balance/CurrentBalance) se calcula como: Valor - retenciones (si CalculateTaxAdvance=2) - saldo inicial - notas crédito + notas débito - transferencias - recibos de caja - cruces; InPeriod=1 si al menos uno de los movimientos (notas, transferencias, recibos, cruces) cae dentro del rango [@InitialDate, @ClosingDate]; Las naturalezas en notas: Nature=1 se acumula como DebitValue, otras como CreditValue; en retenciones de nota Nature=1 suma y otras restan', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cartera por edades; cuenta por cobrar; radicación de factura; glosa/objeción; cobro jurídico; difícil recaudo; nota de cartera (débito/crédito); retención tributaria; anticipo; recibo de caja; cruce de cuentas; reclasificación de cartera; régimen; saldo inicial / saldo al corte; anulación de factura; moneda', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por cuenta por cobrar vigente al corte con sus valores de saldo inicial, débitos/créditos por notas, transferencias, recibos de caja, cruces, retenciones y saldo calculado al corte.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.PortfolioStatus < 3 → Conserva el PortfolioStatus original de la cuenta por cobrar else Recalcula el estado según la cuenta contable principal vigente: 3=Radicada Entidad, 4=Objetada/Glosada, 15=Difícil Recaudo, 16=Cobro Jurídico; si no coincide ninguna, mantiene PortfolioStatus original; si ar.OpeningBalance = 1 → Toma RadicatedConsecutive y RadicatedDate desde temp_RI (Portfolio.RadicateInvoiceD) y suma temp_ARS para calcular InitialValue else Toma el RadicatedConsecutive y ConfirmDate directamente desde Portfolio.RadicateInvoiceC; si ISNULL(icr.CalculateTaxAdvance,0) = 2 → Resta del balance la retención inicial más la retención ajustada por notas else No descuenta retenciones del balance (resta 0); si ISNULL(i.DocumentType,1) = 5 AND ar.AccountReceivableType = 6 → Excluye la cuenta por cobrar del resultado (filtro NOT en WHERE); si DateAccountBecomeZero IS NULL o >= @ClosingDate, o (DateAccountBecomeZero entre @InitialDate y @ClosingDate) → Incluye la cuenta por cobrar en temp_receivable else La cuenta queda excluida del informe', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.PortfolioReclassification; Portfolio.AccountReceivableAccounting; Billing.InvoiceCustomerRetention; Portfolio.AccountReceivableShare; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNoteAccountReceivableRetention; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxC; Billing.Invoice; GeneralLedger.MainAccounts; Portfolio.GetRegimes; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge_030';
GO
