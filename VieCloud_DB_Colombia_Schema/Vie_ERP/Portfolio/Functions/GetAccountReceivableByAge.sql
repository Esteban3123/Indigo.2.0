-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-27
-- Description:	Obtiene laas cuentas por cobrar para el informe de cartera por edades
-- =============================================
CREATE FUNCTION [Portfolio].[GetAccountReceivableByAge]
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
		),
		temp_DEV AS ( --Cambio realizado en el BUG-36941 por solicitud de Jesus Reyes y Adriana Parra
			SELECT DISTINCT d.InvoiceNumber
			FROM Glosas.GlosaDevolutionsReceptionD d WITH (NOLOCK)
			JOIN temp_receivable r ON r.InvoiceNumber = d.InvoiceNumber
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
				IIF(ar.PortfolioStatus = 1 AND dev.InvoiceNumber IS NOT NULL, 14, --Cambio realizado en el BUG-36941 por solicitud de Jesus Reyes y Adriana Parra
				IIF(ar.PortfolioStatus < 3, ar.PortfolioStatus,
				CASE ara.MainAccountId
					WHEN iif(ar.PortfolioStatus =3, ara.MainAccountId, ar.AccountRadicateId) THEN 3
					WHEN ar.AccountObjectionRemediedId THEN 4
					WHEN ar.AccountHardCollectionId THEN 15
					WHEN ar.AccountLegalCollectionId THEN 16
					ELSE ar.PortfolioStatus
				END)) PortfolioStatus,
				CASE
					IIF(ar.PortfolioStatus = 1 AND dev.InvoiceNumber IS NOT NULL, 14, --Cambio realizado en el BUG-36941 por solicitud de Jesus Reyes y Adriana Parra
					IIF(ar.PortfolioStatus < 3, ar.PortfolioStatus,CASE ara.MainAccountId
						WHEN iif(ar.PortfolioStatus =3, ara.MainAccountId, ar.AccountRadicateId) THEN 3
						WHEN ar.AccountObjectionRemediedId THEN 4
						WHEN ar.AccountHardCollectionId THEN 15
						WHEN ar.AccountLegalCollectionId THEN 16
						ELSE ar.PortfolioStatus
					END))
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
		/**************************************** DEVOLUCIÓN ****************************************/
		LEFT JOIN temp_DEV dev ON ar.InvoiceNumber = dev.InvoiceNumber
		/****************************************** MONEDA ******************************************/
		LEFT JOIN Common.Currency c on c.Id = ar.CurrencyId
		WHERE NOT (ISNULL(i.DocumentType, 1) = 5 AND ar.AccountReceivableType = 6)
			AND CAST(ar.AccountReceivableDate AS DATE) <= @ClosingDate
			AND CAST(ISNULL(i.AnnulmentDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > @ClosingDate
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula el informe de cartera por edades (aging) de las cuentas por cobrar, para un rango de fechas definido por una fecha inicial y una fecha de corte. Consolida en una sola fila por cuenta por cobrar los saldos vigentes, aplicando los recaudos (recibos de caja), traslados de cartera, notas de cartera (débitos y créditos), cruces de cuentas y reclasificaciones contables que hayan ocurrido hasta la fecha de corte. Integra información de facturas radicadas ante entidades pagadoras (EPS, aseguradoras) para determinar la fecha de radicación y el número de radicado asociado a cada factura, así como retenciones y provisiones. Es la fuente principal del reporte ejecutivo de envejecimiento de cartera, que permite gestionar el cobro por antigüedad de saldo y la clasificación del riesgo de recuperación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetAccountReceivableByAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetAccountReceivableByAge';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye la base del informe de cartera por edades a una fecha de corte, calculando por cada cuenta por cobrar su estado al corte, valores de retenciones, notas, traslados, recibos de caja, cruces y saldo vigente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ClosingDate define la fecha de corte; las cuentas por cobrar consideradas son las que no han llegado a saldo cero antes del corte (ISNULL(DateAccountBecomeZero,@ClosingDate) >= @ClosingDate); Solo se incluyen cuentas cuya AccountReceivableDate sea menor o igual a @ClosingDate; Se excluyen cuentas asociadas a facturas anuladas antes o en la fecha de corte (AnnulmentDate > @ClosingDate); Las radicaciones consideradas deben tener State=2 en encabezado y detalle, y ConfirmDate <= @ClosingDate; Las notas de cartera consideradas requieren Status=2 y NoteDate <= @ClosingDate; Los traslados de cartera deben tener Status IN (2,4), DocumentDate <= @ClosingDate y no estar reversados al corte (RecersalDate > @ClosingDate o NULL); Los recibos de caja deben tener Status IN (2,4), DocumentDate <= @ClosingDate y no estar reversados al corte (ReversedDate > @ClosingDate o NULL); Los cruces de cuentas (CrossingAccount) deben tener Status=2 y DocumentDate <= @ClosingDate; Las reclasificaciones tomadas son las más recientes con COALESCE(DocumentDate, CreationDate, @ClosingDate) <= @ClosingDate', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Balance = Valor del documento − retenciones (si CalculateTaxAdvance=2) − saldo inicial − notas crédito + notas débito − traslados − recibos de caja − cruces de CxP; Solo se consideran movimientos (notas, traslados, recibos, cruces) que estén confirmados/aprobados (Status 2 o 2/4) y no reversados a la fecha de corte; El estado de cartera reportado nunca degrada estados tempranos (PortfolioStatus<3 se preserva sin recálculo); InPeriod=1 si CUALQUIER movimiento (nota, traslado, recibo, cruce) ocurrió dentro de [@InitialDate, @ClosingDate]; Se excluyen pares (factura tipo 5, CxC tipo 6) y facturas anuladas al corte; Cuando OpeningBalance=1, el valor inicial de la CxC se ajusta restando InitialBalanceValue de AccountReceivableShare', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN_TABLE: Devuelve por cada AccountReceivable vigente al corte: identificación, número de factura, estado de cartera recalculado, consecutivo de radicación, valor del documento, retenciones, débitos/créditos por notas, traslados, recibos de caja, cruces, saldo y bandera InPeriod', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.PortfolioStatus < 3 → Se conserva el PortfolioStatus original de la cuenta por cobrar else Se recalcula el estado según la cuenta contable principal vigente (ara.MainAccountId): 3=Radicada Entidad, 4=Objetada/Glosada, 15=Difícil Recaudo, 16=Cobro Jurídico; en caso contrario conserva el original; si ar.OpeningBalance = 1 → Toma el RadicatedNumber y RadicatedDate desde temp_RI (radicaciones históricas) y se aplica el saldo inicial de AccountReceivableShare como ajuste else Toma RadicatedConsecutive y ConfirmDate directamente del encabezado RadicateInvoiceC; si ISNULL(icr.CalculateTaxAdvance, 0) = 2 → Las retenciones (InitialRetention + AdjustmetRetentionValue) se restan al calcular el Balance else Las retenciones no afectan el cálculo del Balance; si pn.NoteDate / pt.DocumentDate / cr.DocumentDate / ca.DocumentDate cae entre @InitialDate y @ClosingDate → Se marca InPeriod=1 indicando movimiento dentro del período del informe else InPeriod=0; si Para PortfolioNote: pn.Nature = 1 → AdjusmentValue se acumula como DebitValue else AdjusmentValue se acumula como CreditValue; si i.DocumentType = 5 AND ar.AccountReceivableType = 6 → La cuenta por cobrar se EXCLUYE del resultado else Se incluye', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.PortfolioReclassification; Portfolio.AccountReceivableAccounting; Billing.InvoiceCustomerRetention; Portfolio.AccountReceivableShare; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNoteAccountReceivableRetention; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxC; Billing.Invoice; GeneralLedger.MainAccounts; Portfolio.GetRegimes; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAge';
GO
