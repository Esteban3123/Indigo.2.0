-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-06
-- Description:	Procedimiento almacenado para conciliar cartera con contabilidad
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportReconcilePortfolio]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@TypeReport INT,
			@EntityNames VARCHAR(MAX),
			@JournalVoucherTypes VARCHAR(MAX),
			@MainAccounts VARCHAR(MAX),
			-------------
			@FilterByEntityName BIT = 0,
			@FilterByJournalVoucherTypes BIT = 0,
			@FilterByMainAccounts BIT = 0
	
	DECLARE @Table_EntityNames AS TABLE(EntityName VARCHAR(250))
	DECLARE @Table_JournalVoucherTypes AS TABLE(Id INT)
	DECLARE @Table_MainAccounts AS TABLE(Id INT)
	DECLARE @AccountReceivableAccount AS TABLE(MainAccountId INT)

	BEGIN TRY

		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@TypeReport = t.x.value('TypeReport[1]','int'),
				@EntityNames = t.x.value('EntityNames[1]','varchar(max)'),
				@JournalVoucherTypes = t.x.value('JournalVoucherTypes[1]','varchar(max)'),
				@MainAccounts = t.x.value('MainAccounts[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@EntityNames, '') <> ''
		BEGIN
			SET @FilterByEntityName = 1

			INSERT INTO @Table_EntityNames
				SELECT Data Data 
				FROM dbo.Split(@EntityNames, ',')
		END

		IF ISNULL(@JournalVoucherTypes, '') <> ''
		BEGIN
			SET @FilterByJournalVoucherTypes = 1

			INSERT INTO @Table_JournalVoucherTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@JournalVoucherTypes, ',')
		END

		IF ISNULL(@MainAccounts, '') <> ''
		BEGIN
			SET @FilterByMainAccounts = 1

			INSERT INTO @Table_MainAccounts
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@MainAccounts, ',')
		END

		INSERT INTO @AccountReceivableAccount
			SELECT DISTINCT ara.MainAccountId
			FROM Portfolio.AccountReceivableAccounting ara WITH (NOLOCK)

		/******************************************  OBTENCION DE DATOS ******************************************/

		SELECT	k.DocumentDate,
				jv.DocumentDate AccountingDocumentDate,
				en.Description,
				ISNULL(k.EntityName, jv.EntityName) EntityName,
				ISNULL(k.EntityCode, jv.EntityCode) EntityCode,
				jv.JournalVoucherType,
				jv.Consecutive,
				ISNULL(k.MainAccount, jv.MainAccount) MainAccount,
				--------------------------------------------------------			
				ISNULL(k.DebitValue, 0) DebitValue,
				--ISNULL(jv.DebitValue, 0) AccountingDebitValue,
				---Se realiza la conversion del valor del comprobante si llega a tener currency diferente al del documento original creado
				Common.CurrencyConverterByModule(ISNULL(jv.DebitValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),NULL,jv.EntityName,jv.CreationDate) AccountingDebitValue, 
				--------------------------------------------------------
				ISNULL(k.CreditValue, 0) CreditValue,
				Common.CurrencyConverterByModule(ISNULL(jv.CreditValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),NULL,jv.EntityName,jv.CreationDate) AccountingCreditValue, 
				isnull(k.CurrencyId,jv.OfficialCurrencyId) CurrencyId,--12
				cy.Abbreviation
		FROM
		(
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('Invoice' AS VARCHAR(250)) EntityName, ar.InvoiceNumber EntityCode, ar.InvoiceId EntityId,
						CAST(ar.AccountReceivableDate AS DATE) DocumentDate,
						SUM(IIF(ar.OpeningBalance = 0, ar.Value, ara.Value)) DebitValue, 
						SUM(0) CreditValue,
						ar.CurrencyId
				FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
				JOIN
				(
					SELECT AccountReceivableId, MIN(Id) Id
					FROM Portfolio.AccountReceivableAccounting WITH (NOLOCK)
					GROUP BY AccountReceivableId
				) aram ON ar.Id = aram.AccountReceivableId
				JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) on aram.Id = ara.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ara.MainAccountId = ma.Id
				LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
				WHERE ISNULL(i.DocumentType, 0) NOT IN (5)
					AND CAST(ar.AccountReceivableDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ar.InvoiceNumber, ar.InvoiceId, CAST(ar.AccountReceivableDate AS DATE),ar.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('InvoiceCancellation' AS VARCHAR(250)) EntityName, ar.InvoiceNumber EntityCode, ar.InvoiceId EntityId,
						CAST(ar.AccountReceivableDate AS DATE) DocumentDate,
						SUM(0) DebitValue, 
						SUM(IIF(ar.OpeningBalance = 0, ar.Value, ara.Value)) CreditValue,
						ar.CurrencyId
				FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
				JOIN
				(
					SELECT AccountReceivableId, MIN(Id) Id
					FROM Portfolio.AccountReceivableAccounting WITH (NOLOCK)
					GROUP BY AccountReceivableId
				) aram ON ar.Id = aram.AccountReceivableId
				JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) on aram.Id = ara.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ara.MainAccountId = ma.Id
				LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
				WHERE ISNULL(i.DocumentType, 0) NOT IN (5)
					AND ar.Status = 3
					AND CAST(COALESCE(ar.AnnulmentDate, i.AnnulmentDate, ar.AccountReceivableDate) AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ar.InvoiceNumber, ar.InvoiceId, CAST(ar.AccountReceivableDate AS DATE),ar.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('PortfolioReclassification' AS VARCHAR(250)) EntityName, pr.Code EntityCode, prm.EntityId EntityId,
						CAST(ISNULL(pr.DocumentDate, pr.CreationDate) AS DATE) DocumentDate,
						SUM(IIF(pr.TargetAccountId = ma.Id, pr.Value, 0)) DebitValue, 
						SUM(IIF(pr.SourceAccountId = ma.Id, pr.Value, 0)) CreditValue,
						ar.CurrencyId
				FROM Portfolio.PortfolioReclassification pr WITH (NOLOCK)
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON pr.AccountReceivableId = ar.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pr.SourceAccountId = ma.Id OR pr.TargetAccountId = ma.Id
				JOIN
				(
					SELECT pr.DocumentType, pr.Code, MAX(Id) EntityId
					FROM Portfolio.PortfolioReclassification pr WITH (NOLOCK)
					GROUP BY pr.DocumentType, pr.Code
				) prm ON pr.DocumentType = prm.DocumentType AND pr.Code = prm.Code
				WHERE pr.Status = 2
					AND CAST(ISNULL(pr.DocumentDate, pr.CreationDate) AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							pr.Code, prm.EntityId, CAST(ISNULL(pr.DocumentDate, pr.CreationDate) AS DATE),ar.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('PortfolioTransfer' AS VARCHAR(250)) EntityName, pt.Code EntityCode, pt.Id EntityId,
						CAST(pt.DocumentDate AS DATE) DocumentDate,
						SUM(0) DebitValue, 
						SUM(ptd.Value) CreditValue,
						ar.CurrencyId
				FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
				JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ptd.AccountReceivableId = ar.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ptd.MainAccountId = ma.Id
				WHERE pt.Status IN (2, 4)
					AND CAST(pt.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							pt.Code, pt.Id, CAST(pt.DocumentDate AS DATE),ar.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('PortfolioNote' AS VARCHAR(250)) EntityName, pn.Code EntityCode, pn.Id EntityId,
						CAST(pn.NoteDate AS DATE) DocumentDate,
						SUM(IIF(pn.Nature = 1, pnara.AdjusmentValue, 0)) DebitValue, 
						SUM(IIF(pn.Nature = 1, 0, pnara.AdjusmentValue)) CreditValue,
						pn.CurrencyId
				FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
				JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON pnara.AccountReceivableId = ar.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pnara.MainAccountId = ma.Id
				WHERE pn.Status = 2
					AND CAST(pn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							pn.Code, pn.Id, CAST(pn.NoteDate AS DATE),pn.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('PortfolioNote' AS VARCHAR(250)) EntityName, pn.Code EntityCode, pn.Id EntityId,
						CAST(pn.NoteDate AS DATE) DocumentDate,
						SUM(ptd.Value) DebitValue, 
						SUM(0) CreditValue,
						pn.CurrencyId
				FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
				JOIN Portfolio.PortfolioTransfer pt WITH (NOLOCK) ON pn.PortfolioTransferId = pt.Id
				JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ptd.AccountReceivableId = ar.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ptd.MainAccountId = ma.Id
				WHERE pn.Status = 2
					AND CAST(pn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							pn.Code, pn.Id, CAST(pn.NoteDate AS DATE),pn.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('CrossingAccount' AS VARCHAR(250)) EntityName, ca.Code EntityCode, ca.Id EntityId,
						CAST(ca.DocumentDate AS DATE) DocumentDate,
						SUM(cadcxc.CrossingValue) DebitValue, 
						SUM(0) CreditValue,
						ar.CurrencyId
				FROM Treasury.CrossingAccount ca WITH (NOLOCK)
				JOIN Treasury.CrossingAccountDetailCxC cadcxc WITH (NOLOCK) ON ca.Id = cadcxc.CrossingAccountId
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON cadcxc.AccountReceivableId = ar.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON cadcxc.MainAccountId = ma.Id
				WHERE ca.Status = 2
					AND CAST(ca.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							ca.Code, ca.Id, CAST(ca.DocumentDate AS DATE), ar.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('CashReceipts' AS VARCHAR(250)) EntityName, cr.Code EntityCode, cr.Id EntityId,
						CAST(cr.DocumentDate AS DATE) DocumentDate,
						SUM(IIF(crd.Nature = 1, crar.Value, 0)) DebitValue, 
						SUM(IIF(crd.Nature = 1, 0, crar.Value)) CreditValue,
						ar.CurrencyId
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON crar.AccountReceivableId = ar.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON crd.IdMainAccount = ma.Id
				WHERE cr.Status IN (2, 4)
					AND CAST(cr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							cr.Code, cr.Id, CAST(cr.DocumentDate AS DATE), ar.CurrencyId
			UNION ALL
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						CAST('TreasuryNote' AS VARCHAR(250)) EntityName, tn.Code EntityCode, tn.Id EntityId,
						CAST(tn.NoteDate AS DATE) DocumentDate,
						SUM(IIF(crd.Nature = 1, 0, crar.Value)) DebitValue, 
						SUM(IIF(crd.Nature = 1, crar.Value, 0)) CreditValue,
						tn.CurrencyId
				FROM Treasury.TreasuryNote tn WITH (NOLOCK)
				JOIN Treasury.CashReceipts cr WITH (NOLOCK) ON tn.CashRegisterId = cr.Id
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON crar.AccountReceivableId = ar.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON crd.IdMainAccount = ma.Id
				WHERE tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							tn.Code, tn.Id, CAST(tn.NoteDate AS DATE),tn.CurrencyId
		) k
		FULL JOIN
		(
			SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
					CASE jv.EntityName
						WHEN 'Invoice' THEN IIF(SUM(jvd.DebitValue) > SUM(jvd.CreditValue), 'Invoice', 'InvoiceCancellation')
						WHEN 'InvoiceEntityCapitated' THEN IIF(SUM(jvd.DebitValue) > SUM(jvd.CreditValue), 'Invoice', 'InvoiceCancellation')
						WHEN 'DocumentInvoiceProductSales' THEN IIF(SUM(jvd.DebitValue) > SUM(jvd.CreditValue), 'Invoice', 'InvoiceCancellation')
						WHEN 'BasicBilling' THEN IIF(SUM(jvd.DebitValue) > SUM(jvd.CreditValue), 'Invoice', 'InvoiceCancellation')
						ELSE jv.EntityName
					END EntityName, 
					COALESCE(dips.InvoiceNumber, bb.InvoiceNumber, jv.EntityCode) EntityCode, 
					COALESCE(dips.InvoiceId, bb.InvoiceId, jv.EntityId) EntityId,
					CAST(jv.VoucherDate AS DATE) DocumentDate,
					jv.Consecutive, 
					jvt.Id JournalVoucherTypeId,
					CONCAT(jvt.Code, ' - ', jvt.Name) JournalVoucherType,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue,
					lb.OfficialCurrencyId,
					jv.CreationDate
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			join GeneralLedger.LegalBook lb WITH (NOLOCK) on jv.LegalBookId = lb.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id 
			JOIN @AccountReceivableAccount ara ON ma.Id = ara.MainAccountId
			LEFT JOIN
			(
				SELECT dips.Id, dips.InvoiceId, i.InvoiceNumber
				FROM Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK)
				JOIN Billing.Invoice i WITH (NOLOCK) ON dips.InvoiceId = i.Id
			) dips ON jv.EntityId = dips.Id AND jv.EntityName = 'DocumentInvoiceProductSales'
			LEFT JOIN
			(
				SELECT bb.Id, bb.InvoiceId, i.InvoiceNumber
				FROM Billing.BasicBilling bb WITH (NOLOCK)
				JOIN Billing.Invoice i WITH (NOLOCK) ON bb.InvoiceId = i.Id
			) bb ON jv.EntityId = bb.Id AND jv.EntityName = 'BasicBilling'
			WHERE jv.Status = 2
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
						jv.EntityName, 
						COALESCE(dips.InvoiceNumber, bb.InvoiceNumber, jv.EntityCode), 
						COALESCE(dips.InvoiceId, bb.InvoiceId, jv.EntityId), 
						CAST(jv.VoucherDate AS DATE), 
						jv.Consecutive, jvt.Id, CONCAT(jvt.Code, ' - ', jvt.Name),lb.OfficialCurrencyId,jv.CreationDate
		) jv ON ISNULL(k.EntityName, '') = ISNULL(jv.EntityName, '') 
			AND ISNULL(k.EntityId, 0) = ISNULL(jv.EntityId, 0)
			AND k.MainAccountId = jv.MainAccountId
		LEFT JOIN Common.GetEntityNameDescriptions() en ON ISNULL(k.EntityName, jv.EntityName) = en.EntityName
		LEFT JOIN @Table_EntityNames ten ON ISNULL(k.EntityName, jv.EntityName) = ten.EntityName
		LEFT JOIN @Table_JournalVoucherTypes tjvt ON jv.JournalVoucherTypeId = tjvt.Id
		LEFT JOIN @Table_MainAccounts tma ON ISNULL(k.MainAccountId, jv.MainAccountId) = tma.Id
		JOIN Common.Currency cy WITH(NOLOCK) on cy.Id = ISNULL(k.CurrencyId,jv.OfficialCurrencyId)
		WHERE (@FilterByEntityName = 0 OR ten.EntityName IS NOT NULL)
			AND (@FilterByJournalVoucherTypes = 0 OR tjvt.Id IS NOT NULL)
			AND (@FilterByMainAccounts = 0 OR tma.Id IS NOT NULL)
			AND 
			(
				@TypeReport = 3
				OR
				(
					@TypeReport = 2
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) = ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) = ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
				OR
				(
					@TypeReport = 1
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) <> ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) <> ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
			)
		ORDER BY	ISNULL(k.DocumentDate, jv.DocumentDate), 
					ISNULL(k.EntityName, jv.EntityName), 
					ISNULL(k.EntityCode, jv.EntityCode),
					ISNULL(k.MainAccount, jv.MainAccount),
					ISNULL(k.CurrencyId, jv.OfficialCurrencyId)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para generar el reporte de conciliación entre cartera y contabilidad general en un rango de fechas. Compara los documentos de cuentas por cobrar (facturas, anulaciones, reclasificaciones de cartera) registrados en el módulo de Portafolio con los comprobantes contables registrados en el libro mayor, cruzando por cuenta contable principal, tercero y período. Utiliza los criterios enviados como XML (fecha inicio, fecha fin, tipo de reporte, entidades, tipos de comprobante y cuentas contables) para filtrar los resultados y detectar diferencias o descuadres entre el saldo de cartera y los movimientos contables. Es la base del informe de conciliación cartera-contabilidad que usan los equipos de tesorería, cartera y contabilidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcilePortfolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcilePortfolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de conciliación entre los movimientos de cartera (facturas, anulaciones, reclasificaciones, transferencias, notas, cruces y recibos) y sus comprobantes contables, comparando débitos y créditos por cuenta contable y entidad en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener DateStart, DateEnd y TypeReport (1=diferencias, 2=coincidencias, 3=todos); Las listas EntityNames, JournalVoucherTypes y MainAccounts, si vienen, deben ser CSV parseables por dbo.Split (los IDs deben ser convertibles a INT); Existencia de cuentas contables en GeneralLedger.MainAccounts referenciadas por Portfolio.AccountReceivableAccounting; Las fechas DateStart y DateEnd deben ser válidas para el rango BETWEEN aplicado a cada origen', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo concilia cuentas contables que existen en Portfolio.AccountReceivableAccounting; Las facturas con DocumentType=5 nunca se incluyen en los movimientos de cartera; Sólo se consideran documentos contables aprobados/contabilizados (Status=2) en JournalVouchers, PortfolioReclassification, PortfolioNote, CrossingAccount y TreasuryNote; En transferencias de cartera y recibos de caja se aceptan estados 2 y 4; Las anulaciones de factura sólo aplican cuando ar.Status = 3; La conciliación se realiza por la combinación EntityName + EntityId + MainAccountId con FULL JOIN, mostrando huérfanos en ambos lados; Los valores del comprobante contable se convierten a la moneda del documento original mediante Common.CurrencyConverterByModule cuando difieren; Las comparaciones de igualdad/diferencia entre débitos y créditos se realizan con redondeo a 2 decimales; Los valores nulos de débito/crédito se tratan como 0 vía ISNULL; Los errores nunca se propagan: se capturan y devuelven como resultset con código ''999''', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas conciliadas (cartera vs contabilidad) cuando @TypeReport=3 (todos), o sólo coincidencias cuando @TypeReport=2 (débitos o créditos redondeados a 2 decimales iguales), o sólo diferencias cuando @TypeReport=1 (débitos o créditos no iguales); [RETURN_RESULT] Resultset_Error: Ante cualquier excepción en el TRY, devuelve un resultset con CodeResult=''999'' y MessageResult con ERROR_MESSAGE()+línea', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@EntityNames,'''') <> '''' → Activa filtro por EntityName y carga la tabla con los valores parseados else No filtra por nombre de entidad; si ISNULL(@JournalVoucherTypes,'''') <> '''' → Activa filtro por tipos de comprobante contable else No filtra por tipo de comprobante; si ISNULL(@MainAccounts,'''') <> '''' → Activa filtro por cuentas contables principales else No filtra por cuenta contable; si @TypeReport = 1 → Sólo retorna registros donde el débito o crédito de cartera difiere del contable (redondeado a 2 decimales); si @TypeReport = 2 → Sólo retorna registros donde el débito o crédito de cartera coincide con el contable (redondeado a 2 decimales); si @TypeReport = 3 → Retorna todos los registros sin filtro de coincidencia/diferencia; si Para Invoice: i.DocumentType NOT IN (5) → Incluye la cuenta por cobrar como ''Invoice'' con valor débito (Value u OpeningBalance) en el rango de AccountReceivableDate; si ar.Status = 3 y i.DocumentType NOT IN (5) → Trata el documento como ''InvoiceCancellation'' con valor crédito, usando AnnulmentDate (de AR o de la factura) o AccountReceivableDate; si PortfolioReclassification.Status = 2 → Incluye reclasificación: débito si la cuenta es TargetAccountId y crédito si es SourceAccountId; si PortfolioTransfer.Status IN (2,4) → Incluye transferencia de cartera como crédito por el valor del detalle; si PortfolioNote.Status = 2 y pn.Nature = 1 → Registra la nota como débito por AdjustmentValue; si Nature distinto de 1, lo registra como crédito; si PortfolioNote.Status = 2 con PortfolioTransferId → Suma como débito el valor del detalle de la transferencia asociada a la nota; si CrossingAccount.Status = 2 → Incluye el cruce como débito por CrossingValue; si CashReceipts.Status IN (2,4) y crd.Nature = 1 → Registra recibo de caja como débito; si Nature distinto de 1, como crédito; si TreasuryNote.Status = 2 y crd.Nature = 1 → Registra nota de tesorería como crédito; si Nature distinto de 1, como débito (invertido respecto al recibo); si JournalVouchers.Status = 2 y la cuenta está en AccountReceivableAccounting → Incluye el comprobante contable y, según EntityName (Invoice/InvoiceEntityCapitated/DocumentInvoiceProductSales/BasicBilling), reclasifica como ''Invoice'' si SUM(Debit)>SUM(Credit) o ''InvoiceCancellation'' en caso contrario; si jv.EntityName = ''DocumentInvoiceProductSales'' o ''BasicBilling'' → Resuelve InvoiceNumber/InvoiceId desde Inventory.DocumentInvoiceProductSales o Billing.BasicBilling para empatar con la cartera', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.CurrencyConverterByModule; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcilePortfolio';
-- GO
