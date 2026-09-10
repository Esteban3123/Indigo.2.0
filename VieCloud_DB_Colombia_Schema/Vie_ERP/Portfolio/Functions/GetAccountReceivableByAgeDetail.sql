
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-27
-- Description:	Obtiene las cuentas por cobrar para el informe de cartera por edades detallado por movimiento contable
-- Modified: Giovanny Plazas L.
-- Modified date : 2022-11-01
-- =============================================
CREATE FUNCTION [Portfolio].[GetAccountReceivableByAgeDetail]
(
	@InitialDate DATE,
	@ClosingDate DATE
)
RETURNS TABLE
AS
RETURN
	WITH
	temp_receivable as (
		SELECT *
		FROM Portfolio.AccountReceivable WITH (NOLOCK)
		WHERE ISNULL(DateAccountBecomeZero,@ClosingDate) >= @ClosingDate
	),
	cte_PReclassification AS (SELECT p.Id,p.AccountReceivableId,p.SourceAccountId,p.TargetAccountId,p.Value,p.DocumentType,p.EntityCode
								from Portfolio.PortfolioReclassification p
								where CAST(COALESCE(p.DocumentDate, p.CreationDate,@ClosingDate) AS DATE) <= @ClosingDate 
								and p.SourceAccountId <> p.TargetAccountId
								),
	cte_PRUnion as (
				SELECT   max(p.id) id,
						p.AccountReceivableId,
						p.SourceAccountId mainAccountId,
						sum([Value]) [Value],
						0 TypeST,
						0 [InitValue]
				from cte_PReclassification p
				GROUP by p.DocumentType,
						p.AccountReceivableId,
						p.SourceAccountId,p.EntityCode

			UNION ALL

				SELECT max(p.id) Id,
						p.AccountReceivableId,
						p.TargetAccountId mainAccountId,
						0,
						1 TypeST,
						sum([Value]) [InitValue]
				from cte_PReclassification p
				group by  p.DocumentType, p.AccountReceivableId,
						p.TargetAccountId,p.EntityCode),

	 tem_PR as (	SELECT	cte.id,
							cte.mainAccountId,
							cte.[Value],
							cte.TypeST,
							cte.AccountReceivableId,
							cte.InitValue
					from (	SELECT max(id) id ,mainAccountId, AccountReceivableId
							from cte_PRUnion
							GROUP by mainAccountId,AccountReceivableId) sub
				JOIN cte_PRUnion cte 
				on sub.id = cte.id and cte.mainAccountId=sub.mainAccountId and cte.AccountReceivableId= sub.AccountReceivableId),
	cte_accounting as (
						SELECT ara.Id,ara.AccountReceivableId,ara.MainAccountId,ara.Value
						from Portfolio.AccountReceivableAccounting ara
						JOIN Portfolio.AccountReceivable ar on ara.AccountReceivableId = ar.Id
						where cast( ar.AccountReceivableDate as date) <=@ClosingDate
						)	 
	,temp_RI AS (
		SELECT rid.InvoiceNumber, MIN(ri.Id) Id, MIN(rid.RadicatedNumber) RadicatedNumber, MIN(rid.RadicatedDate) RadicatedDate
		FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
		JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
		inner join temp_receivable r on r.InvoiceNumber = rid.InvoiceNumber
		WHERE ri.State = 2 AND rid.State = 2 AND CAST(ri.ConfirmDate AS DATE) <= @ClosingDate
		GROUP BY rid.InvoiceNumber
	),
	temp_ARS AS (
		SELECT	ars.InvoiceNumber,
				pibara.value InitialBalanceValue,
				pibara.MainAccountId
		FROM Portfolio.PortfolioInitialBalanceAccountReceivable  ars WITH (NOLOCK)
		JOIN Portfolio.PortfolioInitialBalanceAccountReceivableAccounting pibara WITH(NOLOCK) ON ars.Id=pibara.PortfolioInitialBalanceAccountReceivableId
	),
	temp_ARA AS (
				
				SELECT ara.Id,ara.AccountReceivableId,isnull(cte.[Value],0) ValueReclasification, iif(ara.id = aramin.id, 0, ISNULL(cte.InitValue,0)) InitValue
				from cte_accounting ara
				inner JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ara.AccountReceivableId=ar.Id
				inner join tem_PR cte on ara.MainAccountId=cte.mainAccountId and ara.AccountReceivableId = cte.AccountReceivableId
				inner JOIN( select 
									min(ara.Id) Id,
									ara.AccountReceivableId
							from cte_accounting ara
							group by ara.AccountReceivableId) aramin on ar.Id=aramin.AccountReceivableId
				WHERE ar.OpeningBalance= 0 

				UNION ALL

				SELECT ara.Id,ara.AccountReceivableId,isnull(cte.[Value],0) ValueReclasification,ISNULL(cte.InitValue,0) InitValue
				from cte_accounting ara
				inner JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ara.AccountReceivableId=ar.Id
				inner JOIN( select min(ara.Id) Id , ara.AccountReceivableId
							from cte_accounting ara
							group by ara.AccountReceivableId) aramin on ar.Id=aramin.AccountReceivableId
				left join tem_PR cte on ara.MainAccountId=cte.mainAccountId and ara.AccountReceivableId = cte.AccountReceivableId
				WHERE ar.OpeningBalance= 0 AND
				 NOT EXISTS(SELECT 1 from tem_PR where AccountReceivableId =ara.AccountReceivableId ) AND aramin.Id = ARA.Id
				
				UNION ALL

				SELECT ara.Id,ara.AccountReceivableId,isnull(cte.[Value],0) ValueReclasification, iif(ara.id = aramin.id, 0, ISNULL(cte.InitValue,0)) InitValue
				from cte_accounting ara
				inner JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ara.AccountReceivableId=ar.Id
				left join temp_ARS ars on ar.InvoiceNumber = ars.InvoiceNumber and ara.MainAccountId = ars.MainAccountId
				left join tem_PR cte on ara.MainAccountId=cte.mainAccountId and ara.AccountReceivableId = cte.AccountReceivableId
				inner JOIN( select 
									min(ara.Id) Id, 
									ara.AccountReceivableId
							from cte_accounting ara
							group by ara.AccountReceivableId) aramin on ar.Id=aramin.AccountReceivableId
				WHERE ar.OpeningBalance=1 AND (ars.MainAccountId is not null OR cte.mainAccountId is not null
				)

				UNION ALL

				--BUG-41938: CxC de saldo inicial (OpeningBalance=1) sin registro en PortfolioInitialBalanceAccountReceivableAccounting ni en reclasificaciones.
				--Sin este bloque, MainAccountId queda NULL y el reporte de cartera no muestra MainAccountNumber/MainAccountName aunque el registro contable base sí exista.
				SELECT ara.Id,ara.AccountReceivableId, 0 ValueReclasification, 0 InitValue
				from cte_accounting ara
				inner JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ara.AccountReceivableId=ar.Id
				inner JOIN( select
									min(ara.Id) Id,
									ara.AccountReceivableId
							from cte_accounting ara
							group by ara.AccountReceivableId) aramin on ar.Id=aramin.AccountReceivableId
				WHERE ar.OpeningBalance=1 AND aramin.Id = ara.Id
					AND NOT EXISTS (
						SELECT 1
						FROM cte_accounting ara2
						LEFT JOIN temp_ARS ars2 ON ar.InvoiceNumber = ars2.InvoiceNumber AND ara2.MainAccountId = ars2.MainAccountId
						LEFT JOIN tem_PR cte2 ON ara2.MainAccountId = cte2.mainAccountId AND ara2.AccountReceivableId = cte2.AccountReceivableId
						WHERE ara2.AccountReceivableId = ara.AccountReceivableId
							AND (ars2.MainAccountId is not null OR cte2.mainAccountId is not null)
					)
	),
	temp_ICR AS (
		SELECT c.InvoiceId, MAX(c.CalculateTaxAdvance) CalculateTaxAdvance, SUM(c.Value) InitialRetention
		FROM Billing.InvoiceCustomerRetention c WITH (NOLOCK)
		JOIN temp_receivable r on r.InvoiceId = c.InvoiceId
		GROUP BY c.InvoiceId
	),
	
	temp_PN AS (
		SELECT 
			pnara.AccountReceivableId, 
			SUM(IIF(pn.Nature = 1, pnara.AdjusmentValue, 0)) DebitValue,
			SUM(IIF(pn.Nature = 1, 0, pnara.AdjusmentValue)) CreditValue,
			SUM(ISNULL(pnarr.AdjustmetRetentionValue, 0)) AdjustmetRetentionValue,
			MAX(IIF(@InitialDate IS NOT NULL AND CAST(pn.NoteDate AS DATE) >= @InitialDate AND CAST(pn.NoteDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod,
			pnara.AccountReceivableAccountingId
		FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
		JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
		JOIN temp_receivable r on r.Id = pnara.AccountReceivableId
		LEFT JOIN
		(
			SELECT PortfolioNoteAccountReceivableId, SUM(Value * IIF(Nature = 1, 1, -1)) AdjustmetRetentionValue
			FROM Portfolio.PortfolioNoteAccountReceivableRetention WITH (NOLOCK)
			GROUP BY PortfolioNoteAccountReceivableId
		) pnarr ON pnara.Id = pnarr.PortfolioNoteAccountReceivableId
		WHERE pn.Status = 2 AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
		GROUP BY pnara.AccountReceivableId,pnara.AccountReceivableAccountingId
	),
	temp_PT AS (SELECT 
				ptd.AccountReceivableId,
				CAST(SUM(ptd.ValueInCurrencyInvoice) AS DECIMAL(18,2)) TransferValue,
				MAX(IIF(@InitialDate IS NOT NULL AND CAST(pt.DocumentDate AS DATE) >= @InitialDate AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod
				,ara.Id AccountReceivableAccountingId
			FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
			JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
			JOIN Portfolio.AccountReceivableAccounting ara ON ptd.AccountReceivableId=ara.AccountReceivableId AND ptd.MainAccountId = ara.MainAccountId
			JOIN temp_receivable r on r.Id = ptd.AccountReceivableId
			WHERE pt.Status IN (2, 4) 
				AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate
				AND CAST(ISNULL(pt.RecersalDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > @ClosingDate
			GROUP BY ptd.AccountReceivableId,ara.Id
			),

	temp_CR AS (SELECT 
				crar.AccountReceivableId,
				SUM(crar.Value) CashReceiptValue,
				MAX(IIF(@InitialDate IS NOT NULL AND CAST(cr.DocumentDate AS DATE) >= @InitialDate AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod,
				ara.Id AccountReceivableAccountingId
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
				JOIN Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) ON crar.AccountReceivableId = ara.AccountReceivableId AND ara.MainAccountId= crd.IdMainAccount
				JOIN temp_receivable r on r.Id = crar.AccountReceivableId
				WHERE cr.Status IN (2 , 4)
					AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(cr.ReversedDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > @ClosingDate
				GROUP BY crar.AccountReceivableId,ara.Id
			),

	temp_CA AS (
			SELECT 
				cad.AccountReceivableId,
				SUM(cad.CrossingValue) CrossingValue,
				MAX(IIF(@InitialDate IS NOT NULL AND CAST(ca.DocumentDate AS DATE) >= @InitialDate AND CAST(ca.DocumentDate AS DATE) <= @ClosingDate, 1, 0)) InPeriod,
				cad.AccountReceivableAccountingId
			FROM Treasury.CrossingAccount ca WITH (NOLOCK)
			JOIN Treasury.CrossingAccountDetailCxC cad WITH (NOLOCK) ON ca.Id = cad.CrossingAccountId
			JOIN temp_receivable r on r.Id = cad.AccountReceivableId
			WHERE ca.Status = 2
				AND CAST(ca.DocumentDate AS DATE) <= @ClosingDate
			GROUP BY cad.AccountReceivableId,cad.AccountReceivableAccountingId
		)

	SELECT	ar.Id,
				ar.OperatingUnitId,
				ISNULL(i.InvoiceCategoryId,ar.InvoiceCategoryId) as InvoiceCategoryId,
				ISNULL(ar.CareGroupId, i.CareGroupId) CareGroupId,
				i.ContractId,
				ar.ThirdPartyId,			
				TRIM(ar.InvoiceNumber) InvoiceNumber, 
				i.AdmissionNumber,
				ar.AccountReceivableDate AS AccountReceivableDate, 
				ar.AccountReceivableType,
				ar.NumberShares,
				ar.Term,
				ar.OpeningBalance,
				-----------------------------------------------------------------------------------------------------------
				CASE ara.MainAccountId
					WHEN ar.AccountRadicateId THEN 3
					WHEN ar.AccountObjectionRemediedId THEN 4
					WHEN ar.AccountHardCollectionId THEN 15
					WHEN ar.AccountLegalCollectionId THEN 16
					WHEN ar.AccountConciliationId then 17
					ELSE 1
				END PortfolioStatus,
				CASE	
					CASE ara.MainAccountId
						WHEN ar.AccountRadicateId THEN 3
						WHEN ar.AccountObjectionRemediedId THEN 4
						WHEN ar.AccountHardCollectionId THEN 15
						WHEN ar.AccountLegalCollectionId THEN 16
						WHEN ar.AccountConciliationId then 17
						ELSE 1
					END
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
					WHEN 17 then 'Conciliada'
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
				ara.Value AS DocumentValue,
				ISNULL(icr.InitialRetention, 0) + ISNULL(pn.AdjustmetRetentionValue, 0) RetentionValue,
				CASE 
				WHEN coalesce(aram.InitValue,0)>0 THEN aram.InitValue
				WHEN ISNULL(ars.InitialBalanceValue, isnull(ara.Value,0)) < ISNULL(aram.ValueReclasification,0) THEN coalesce(aram.ValueReclasification,0)
				ELSE ISNULL(ars.InitialBalanceValue, isnull(ara.Value,0))
				END InitialValue,
				ISNULL(pn.DebitValue, 0) AS DebitValue,
				ISNULL(pn.CreditValue, 0) AS CreditValue,
				ISNULL(pt.TransferValue, 0) AS TransferValue,
				ISNULL(cr.CashReceiptValue, 0) AS CashReceiptValue,
				ISNULL(ca.CrossingValue, 0) AS CrossingValue,
				(
					CASE
					WHEN coalesce(aram.InitValue,0)>0 THEN aram.InitValue
					WHEN isnull(ara.Value,0) < ISNULL(aram.ValueReclasification,0) THEN aram.ValueReclasification
					ELSE  isnull(ara.Value,0) END --Valor Inicial

					- IIF(ISNULL(icr.CalculateTaxAdvance, 0) = 2, ISNULL(icr.InitialRetention, 0) + ISNULL(pn.AdjustmetRetentionValue, 0), 0) --Retenciones

					- ( CASE
						WHEN coalesce(aram.InitValue,0)>0 THEN aram.InitValue
						WHEN isnull(ara.Value,0) < ISNULL(aram.ValueReclasification,0) THEN aram.ValueReclasification
						ELSE  isnull(ara.Value,0) END --Valor Inicial

						- ISNULL(ars.InitialBalanceValue,	CASE
															WHEN coalesce(aram.InitValue,0)>0 THEN aram.InitValue
															WHEN isnull(ara.Value,0) < ISNULL(aram.ValueReclasification,0) THEN aram.ValueReclasification
															ELSE  isnull(ara.Value,0) END )) --Valor Inicial) --Valor saldo inicial
					+ ISNULL(pn.DebitValue, 0) - ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
					- ISNULL(aram.ValueReclasification,0) --documentos de reclasificacion que afectan la cuenta
				) AS Balance,
				(
					ISNULL(ara.Value,0) --Valor Inicial
					- IIF(ISNULL(icr.CalculateTaxAdvance, 0) = 2, ISNULL(icr.InitialRetention, 0) + ISNULL(pn.AdjustmetRetentionValue, 0), 0) --Retenciones
					- (isnull(ara.Value,0) - ISNULL(ars.InitialBalanceValue, ISNULL(ara.Value,0))) --Valor saldo inicial
					+ ISNULL(pn.DebitValue, 0) - ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
					- ISNULL(aram.ValueReclasification,0)  --documentos de reclasificacion que afectan la cuenta
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
		LEFT JOIN temp_ARS ars ON ar.OpeningBalance = 1 AND ar.InvoiceNumber = ars.InvoiceNumber AND ara.MainAccountId = ars.MainAccountId
		--LEFT JOIN temp_ARS ars ON ar.OpeningBalance = 1 AND ar.Id = ars.AccountReceivableId
		LEFT JOIN temp_PN pn ON ar.Id = pn.AccountReceivableId AND ara.Id=pn.AccountReceivableAccountingId
		LEFT JOIN temp_PT pt ON ar.Id = pt.AccountReceivableId AND ara.Id=pt.AccountReceivableAccountingId
		LEFT JOIN temp_CR cr ON ar.Id = cr.AccountReceivableId AND ara.Id= cr.AccountReceivableAccountingId
		LEFT JOIN temp_CA ca ON ar.Id = ca.AccountReceivableId AND ara.Id= ca.AccountReceivableAccountingId
		--LEFT JOIN Portfolio.PortfolioReclassification pr WITH(NOLOCK) ON pr.AccountReceivableId= ar.Id AND pr.SourceAccountId =ara.MainAccountId
		/****************************************** MONEDA ******************************************/
		LEFT JOIN Common.Currency c on c.Id = ar.CurrencyId
		WHERE NOT (ISNULL(i.DocumentType, 1) = 5 AND ar.AccountReceivableType = 6)
			AND CAST(ar.AccountReceivableDate AS DATE) <= @ClosingDate
			AND CAST(ISNULL(i.AnnulmentDate, DATEADD(DAY, 1, @ClosingDate)) AS DATE) > @ClosingDate

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el detalle de las cuentas por cobrar clasificadas por antigüedad (cartera por edades) para un rango de fechas dado, desglosando cada documento de cobro (factura o cuenta de cobro) a nivel de movimiento contable individual. Combina saldos de cuentas por cobrar, reclasificaciones contables entre cuentas, radicaciones de facturas confirmadas, saldos iniciales de apertura, retenciones de clientes y notas de cartera para obtener el saldo vigente de cada cuenta al cierre del período indicado. Se usa en los informes de cartera por edades para conocer cuánto se le debe cobrar a cada tercero o pagador, en qué cuenta contable está registrado y con qué antigüedad, filtrando únicamente los documentos cuyo saldo aún no había llegado a cero en la fecha de corte.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetAccountReceivableByAgeDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetAccountReceivableByAgeDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el detalle de cuentas por cobrar para el informe de cartera por edades, calculando saldos y estados al corte por movimiento contable, considerando reclasificaciones, retenciones, notas, traslados, recibos de caja y cruces.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAgeDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ClosingDate debe estar definida; se usa como fecha de corte en todos los filtros temporales.; Las cuentas por cobrar deben tener AccountReceivableDate <= @ClosingDate.; Solo se consideran radicaciones (RadicateInvoiceC/D) con State=2 y ConfirmDate <= @ClosingDate.; Solo se consideran notas de cartera (PortfolioNote) con Status=2 y NoteDate <= @ClosingDate.; Solo se consideran traslados (PortfolioTransfer) con Status IN (2,4), DocumentDate <= @ClosingDate y sin reverso vigente al corte.; Solo se consideran recibos de caja (CashReceipts) con Status IN (2,4), DocumentDate <= @ClosingDate y sin reverso vigente al corte.; Solo se consideran cruces (CrossingAccount) con Status=2 y DocumentDate <= @ClosingDate.; Las reclasificaciones de cartera consideradas deben tener SourceAccountId distinto de TargetAccountId y fecha (DocumentDate/CreationDate) <= @ClosingDate.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAgeDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las cuentas anuladas antes del corte (AnnulmentDate <= @ClosingDate) nunca aparecen en el reporte.; Las combinaciones DocumentType=5 con AccountReceivableType=6 siempre se excluyen.; Solo se consideran movimientos confirmados (estados 2 o 2/4 según el documento) y no reversados antes del corte.; El PortfolioStatus se determina exclusivamente por la coincidencia de la cuenta contable principal del registro contable con las cuentas configuradas en la cuenta por cobrar (radicación, objeción, difícil recaudo, jurídico, conciliación).; El balance descuenta retenciones únicamente cuando CalculateTaxAdvance = 2.; Para cuentas con OpeningBalance=1, el valor inicial se toma del saldo inicial de cartera (PortfolioInitialBalanceAccountReceivable) cuando exista para esa MainAccountId.; Las reclasificaciones se agrupan por DocumentType+AccountReceivableId+Account+EntityCode y se toma el id máximo por (mainAccountId, AccountReceivableId) como referencia al movimiento contable.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAgeDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN: Retorna una fila por cada combinación cuenta por cobrar / movimiento contable (AccountReceivableAccounting) vigente al corte, con su valor inicial, saldo, retenciones, notas, traslados, recibos y cruces.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAgeDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(DateAccountBecomeZero,@ClosingDate) >= @ClosingDate → La cuenta por cobrar se incluye en temp_receivable (sigue vigente al corte) else Se excluye porque su saldo ya quedó en cero antes del corte; si ar.OpeningBalance = 0 (cuenta no proviene de saldo inicial) → En temp_ARA se cruza con tem_PR (reclasificaciones) por mainAccountId+AccountReceivableId; si no existe reclasificación se toma la fila mínima del registro contable else Si OpeningBalance=1 se cruza adicionalmente con temp_ARS (saldo inicial) por InvoiceNumber+MainAccountId; si ara.MainAccountId coincide con AccountRadicateId / AccountObjectionRemediedId / AccountHardCollectionId / AccountLegalCollectionId / AccountConciliationId de la CxC → PortfolioStatus toma los valores 3 (Radicada), 4 (Objetada/Glosada), 15 (Difícil Recaudo), 16 (Cobro Jurídico) o 17 (Conciliada) respectivamente else PortfolioStatus = 1 (''Sin Radicar''); si ar.OpeningBalance = 1 → RadicatedConsecutive y RadicatedDate se toman del radicado del saldo inicial (ric) else Se toman de la radicación normal (ri.RadicatedConsecutive y ri.ConfirmDate); si ISNULL(icr.CalculateTaxAdvance, 0) = 2 → Las retenciones (InitialRetention + AdjustmetRetentionValue) se descuentan del Balance/CurrentBalance else No se descuentan retenciones del balance; si coalesce(aram.InitValue,0) > 0 → InitialValue = aram.InitValue (proviene de reclasificación destino) else Si ISNULL(ars.InitialBalanceValue, ara.Value) < aram.ValueReclasification entonces InitialValue = aram.ValueReclasification; en caso contrario InitialValue = ISNULL(ars.InitialBalanceValue, ara.Value); si pn.Nature = 1 en PortfolioNote → El AdjusmentValue se acumula como DebitValue else Se acumula como CreditValue; si @InitialDate IS NOT NULL AND fecha del documento entre @InitialDate y @ClosingDate (en notas, traslados, recibos o cruces) → Se marca InPeriod = 1 indicando que el movimiento ocurrió dentro del período reportado else InPeriod = 0; si ISNULL(i.DocumentType,1) = 5 AND ar.AccountReceivableType = 6 → La fila se EXCLUYE del resultado (combinación de tipo de documento y tipo de CxC no aplicable) else Se incluye; si i.AnnulmentDate <= @ClosingDate → La factura está anulada al corte y se excluye del resultado else Se incluye porque la anulación es posterior o no existe', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAgeDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAgeDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetAccountReceivableByAgeDetail';
GO
