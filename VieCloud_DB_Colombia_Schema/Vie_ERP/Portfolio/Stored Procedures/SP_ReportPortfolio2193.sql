-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-23
-- Description:	Procedimiento para el reporte de edades de cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolio2193]
	@HisContainer AS VARCHAR(20),
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY

	DECLARE -- CRITERIOS --
			@ClosingDate DATE,
			@AfterClosingDate DATE,
			@OperatingUnit INT,
			-- FILTROS --
			@PersonType INT,
			@CustomerStart VARCHAR(MAX),
			@CustomerEnd VARCHAR(MAX),
			@DocumentType VARCHAR(MAX)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@ClosingDate = t.x.value('ClosingDate[1]','date')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT 
			@PersonType = t.x.value('PersonType[1]','int'),
			@CustomerStart = t.x.value('CustomerStart[1]','varchar(max)'),
			@CustomerEnd = t.x.value('CustomerEnd[1]','varchar(max)'),
			@DocumentType = t.x.value('DocumentType[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		SELECT @AfterClosingDate = DATEADD(DAY, 1, @ClosingDate),
				@CustomerStart = IIF(ISNULL(@CustomerStart, '') = '', '0', @CustomerStart),
				@CustomerEnd = IIF(ISNULL(@CustomerEnd, '') = '', 'ZZZZZZZZZZ', @CustomerEnd)

		SELECT CAST(Data AS INT) Data 
		INTO #Table_DocumentType
		FROM dbo.Split(@DocumentType, ',')

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT 
			d.*
		FROM
		(
			SELECT
				pr.RegimenName AS RegimenCalculated,
				tp.Nit AS ThirdPartyNit, 
				tp.Name AS ThirdPartyName, 
				ar.InvoiceNumber,
				ar.AccountReceivableDate,
				DATEDIFF(DAY, ISNULL(IIF(ar.OpeningBalance = 1, ISNULL(ric.RadicatedDate, ri.ConfirmDate), ri.ConfirmDate), @ClosingDate), @ClosingDate) AS Age,				
				(
					ar.Value --Valor Inicial
					- (ar.Value - ISNULL(ars.InitialBalanceValue, ar.Value)) --Valor saldo inicial
					+ ISNULL(pn.DebitValue, 0) - ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
				) AS Balance,				
				ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
				ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) ValueAcceptedFirstInstance,
				ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) ValueAcceptedSecondInstance,
				gpg.State GlosaState,
				0 AdvancedValue,
				ar.DeteriorationBalance
			FROM Portfolio.AccountReceivable AS ar WITH (NOLOCK)
			JOIN #Table_DocumentType AS t_dt ON ar.AccountReceivableType = t_dt.Data
			JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
			JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
			LEFT JOIN Billing.Invoice AS i WITH (NOLOCK) ON ar.InvoiceNumber = i.InvoiceNumber
			LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
			LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
			LEFT JOIN
			(
				SELECT rid.InvoiceNumber, MIN(ri.Id) Id, MIN(rid.RadicatedDate) RadicatedDate
				FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
				JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
				WHERE ri.State = 2 AND rid.State = 2
				GROUP BY rid.InvoiceNumber
			) ric ON ar.InvoiceNumber = ric.InvoiceNumber
			LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON ric.Id = ri.Id
			LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = CASE ar.PortfolioStatus
				WHEN 1 THEN ar.AccountWithoutRadicateId
				WHEN 2 THEN ar.AccountWithoutRadicateId
				WHEN 15 THEN ar.AccountHardCollectionId
				WHEN 16 THEN COALESCE(ar.AccountLegalCollectionId, ar.AccountRadicateId, ar.AccountWithoutRadicateId)
				ELSE ISNULL(ar.AccountRadicateId, ar.AccountWithoutRadicateId)
			END
			/********************************** BALANCE **********************************/
			LEFT JOIN
			(
				SELECT 
					ars.AccountReceivableId,
					SUM(ars.Balance - ars.DebitValue + ars.CreditValue + ars.TransferValue + ars.PaymentValue + ars.CrossingValue) InitialBalanceValue
				FROM Portfolio.AccountReceivableShare ars WITH (NOLOCK)			
				GROUP BY ars.AccountReceivableId
			) ars ON ar.OpeningBalance = 1 AND ar.Id = ars.AccountReceivableId
			LEFT JOIN
			(
				SELECT 
					pnara.AccountReceivableId, 
					SUM(IIF(pn.Nature = 1, pnara.AdjusmentValue, 0)) DebitValue,
					SUM(IIF(pn.Nature = 1, 0, pnara.AdjusmentValue)) CreditValue
				FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
				JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
				WHERE pn.Status = 2 AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
				GROUP BY pnara.AccountReceivableId
			) pn ON ar.Id = pn.AccountReceivableId
			LEFT JOIN
			(
				SELECT 
					ptd.AccountReceivableId,
					SUM(ptd.Value) TransferValue
				FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
				JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
				WHERE pt.Status IN (2, 4) 
					AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(pt.RecersalDate, @AfterClosingDate) AS DATE) > @ClosingDate
				GROUP BY ptd.AccountReceivableId
			) pt ON ar.Id = pt.AccountReceivableId
			LEFT JOIN
			(
				SELECT 
					crar.AccountReceivableId,
					SUM(crar.Value) CashReceiptValue
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
				WHERE cr.Status IN (2 , 4)
					AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate
					AND CAST(ISNULL(cr.ReversedDate, @AfterClosingDate) AS DATE) > @ClosingDate
				GROUP BY crar.AccountReceivableId
			) cr ON ar.Id = cr.AccountReceivableId
			LEFT JOIN
			(
				SELECT 
					cad.AccountReceivableId,
					SUM(cad.CrossingValue) CrossingValue
				FROM Treasury.CrossingAccount ca WITH (NOLOCK)
				JOIN Treasury.CrossingAccountDetailCxC cad WITH (NOLOCK) ON ca.Id = cad.CrossingAccountId
				WHERE ca.Status = 2
					AND CAST(ca.DocumentDate AS DATE) <= @ClosingDate
				GROUP BY cad.AccountReceivableId
			) ca ON ar.Id = ca.AccountReceivableId
			/********************************** ******* **********************************/
			LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
			WHERE CAST(ar.AccountReceivableDate AS DATE) <= @ClosingDate
				AND (@PersonType = 3 OR tp.PersonType = @PersonType)
				AND tp.Nit BETWEEN @CustomerStart AND @CustomerEnd
				AND CAST(ISNULL(i.AnnulmentDate, @AfterClosingDate) AS DATE) > @ClosingDate
				AND 
				(
					ar.Value --Valor Inicial
					- (ar.Value - ISNULL(ars.InitialBalanceValue, ar.Value)) --Valor saldo inicial
					+ ISNULL(pn.DebitValue, 0) - ISNULL(pn.CreditValue, 0) --Valor Notas
					- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
					- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
					- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
				) <> 0
		) AS d
		ORDER BY d.RegimenCalculated, d.ThirdPartyNit
		OPTION (RECOMPILE)

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH

	IF OBJECT_ID('tempdb..#Table_DocumentType') IS NOT NULL DROP TABLE #Table_DocumentType
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de edades de cartera (antigüedad de saldos) para el módulo de cartera. Calcula, a una fecha de corte definida, el saldo pendiente de cobro de cada cuenta por cobrar —facturas, cuentas de cobro— agrupando la deuda por tercero pagador (EPS, aseguradora, empresa) e indicando los días de antigüedad desde la radicación o confirmación del cobro. Integra información de cuentas por cobrar, facturas, radicados de cobro, notas de cartera, traslados, recibos de caja, cruces con cuentas por pagar y valores glosados (primera y segunda instancia), permitiendo visualizar el saldo real adeudado, el deterioro contable y el régimen asociado a cada deudor. Se usa para el seguimiento y gestión del recaudo, análisis de mora y provisiones de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolio2193';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolio2193';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de edades de cartera a una fecha de corte, calculando el saldo vigente de cada cuenta por cobrar con sus afectaciones (notas, traslados, recibos de caja, cruces) y datos de glosa.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener el nodo ClosingDate (fecha de corte); @xmlFilters debe contener PersonType, CustomerStart, CustomerEnd y DocumentType (lista de tipos separada por coma); La función dbo.Split debe poder parsear @DocumentType en enteros válidos correspondientes a AccountReceivableType', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas por cobrar cuya AccountReceivableDate sea <= fecha de corte; Solo se incluyen facturas no anuladas a la fecha de corte (AnnulmentDate > ClosingDate o nula); Solo se incluyen radicados con estado 2 (RadicateInvoiceC y RadicateInvoiceD); Solo se consideran notas de cartera con Status = 2 y NoteDate <= fecha de corte; Solo se consideran traslados con Status IN (2,4), DocumentDate <= fecha de corte y no reversados antes/igual a la fecha de corte; Solo se consideran recibos de caja con Status IN (2,4), DocumentDate <= fecha de corte y no reversados antes/igual a la fecha de corte; Solo se consideran cruces con Status = 2 y DocumentDate <= fecha de corte; Las glosas solo se consideran si su RadicatedDate <= fecha de corte; El saldo se calcula como: Valor inicial − (Valor − Saldo inicial) + Notas débito − Notas crédito − Cruce de anticipo − Recibos de caja − Cruzado con cxp; Solo se reportan cuentas con saldo calculado distinto de cero; Notas: si Nature=1 suma a DebitValue, en caso contrario a CreditValue; AdvancedValue siempre se reporta como 0', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Edades de cartera; Cuenta por cobrar; Factura; Radicación de factura; Régimen; Tercero/Cliente (NIT); Glosa (primera y segunda instancia); Notas de cartera (débito/crédito); Anticipo y cruce de anticipo; Recibo de caja; Cruce con cuenta por pagar; Saldo de deterioro; Cobro jurídico / cobro pre-jurídico', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas con tercero, factura, edad, saldo y valores de glosa para cuentas por cobrar cuyo saldo calculado <> 0 a la fecha de corte, ordenadas por régimen y NIT.; [RETURN_RESULT] resultset: En caso de error en TRY, devuelve un resultset con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CustomerStart es NULL o vacío → Se asigna ''0'' como inicio de rango de NIT else Se conserva el valor recibido; si @CustomerEnd es NULL o vacío → Se asigna ''ZZZZZZZZZZ'' como fin de rango de NIT else Se conserva el valor recibido; si @PersonType = 3 → No se filtra por tipo de persona (incluye todos) else Se filtra tp.PersonType = @PersonType; si ar.OpeningBalance = 1 → La edad se calcula desde RadicatedDate (o ConfirmDate si no hay) hasta ClosingDate else La edad se calcula desde ri.ConfirmDate hasta ClosingDate; si no existe, edad = 0; si CASE sobre ar.PortfolioStatus → Status 1/2 → AccountWithoutRadicateId; 15 → AccountHardCollectionId; 16 → AccountLegalCollectionId/AccountRadicateId/AccountWithoutRadicateId (COALESCE); otros → AccountRadicateId o AccountWithoutRadicateId; si gpg.EvaluationDateGlosa <= @ClosingDate → Se reporta ValueAcceptedFirstInstance else Se reporta 0; si gpg.EvaluationDateReiteration <= @ClosingDate → Se reporta ValueAcceptedSecondInstance else Se reporta 0', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.Split; Portfolio.AccountReceivable; Common.ThirdParty; Common.Person; Billing.Invoice; GeneralLedger.MainAccounts; Portfolio.GetRegimes; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivableShare; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxC; Glosas.GlosaPortfolioGlosada', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolio2193';
-- GO
