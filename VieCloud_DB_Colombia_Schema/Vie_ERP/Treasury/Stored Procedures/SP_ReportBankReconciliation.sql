-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-01-15
-- Description:	Procedimiento que se encarga de obtener los detalles de la conciliación bancaria
-- ==============================================================================================================
CREATE PROCEDURE [Treasury].[SP_ReportBankReconciliation]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @BankReconciliationId INT

	DECLARE @Table_Result AS TABLE
	(
		[DetailIsBookRegister] [bit],
		[DetailDocumentType] [tinyint] NOT NULL,
		[DetailCode] [varchar](20) NOT NULL,
		[DetailDocumentDate] DATETIME,
		[DetailDocumentNumber] VARCHAR(50),
		[DetailObservations] VARCHAR(MAX),
		[DetailNature] [tinyint] NOT NULL,
		[DetailValue] [decimal](20, 4) NOT NULL
	)

	BEGIN TRY

		SELECT	
				@BankReconciliationId = t.x.value('BankReconciliationId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/******************************************************************************************************/
		
		INSERT INTO @Table_Result
			SELECT	1 IsBookRegister,
					brd.DocumentType, 
					brd.EntityCode, 
					CASE brd.DocumentType
						WHEN 1 THEN cr.DocumentDate
						WHEN 2 THEN vt.DocumentDate
						WHEN 3 THEN tn.NoteDate
						WHEN 4 THEN c.DocumentDate
						WHEN 5 THEN ccs.DocumentDate
					END DocumentDate,
					CASE brd.DocumentType
						WHEN 1 THEN cr.DocumentNumber
						WHEN 2 THEN 
							CASE vt.PaymentMethod
								WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
								WHEN 2 THEN vt.NoteNumber
							END
					END DocumentNumber,
					CASE brd.DocumentType
						WHEN 1 THEN cr.Detail
						WHEN 2 THEN vt.Detail
						WHEN 3 THEN tn.Description
						WHEN 4 THEN c.Description
					END Observations,
					brd.Nature, 
					brd.Value
				FROM Treasury.BankReconciliationDetail brd
				LEFT JOIN 
				(
					SELECT	pm.Id, 
							cr.DocumentDate, 
							CASE pm.PaymentMethodTypes
								WHEN 3 THEN pm.CardNumber
								WHEN 4 THEN pm.DepositNumber
							END DocumentNumber,
							CONCAT
							(
								ISNULL
								(
									CASE pm.PaymentMethodTypes
										WHEN 3 THEN 'Tarjeta de Crédito'
										WHEN 4 THEN 'Consignación'
									END + ': ', ''
								), cr.Detail
							) Detail
					FROM Treasury.CashReceipts cr 
					JOIN Treasury.PaymentMethods pm ON cr.Id = pm.IdCashReceipt
					WHERE cr.Status IN (2, 4)
				) cr ON brd.DocumentType = 1 AND brd.EntityId = cr.Id
				LEFT JOIN Treasury.VoucherTransaction vt ON brd.DocumentType = 2 AND brd.EntityId = vt.Id
				LEFT JOIN Treasury.TreasuryNote tn ON brd.DocumentType = 3 AND brd.EntityId = tn.Id
				LEFT JOIN Treasury.Consignment c ON brd.DocumentType = 4 AND brd.EntityId = c.Id
				LEFT JOIN Treasury.ConstitutionCashSmaller ccs ON brd.DocumentType = 5 AND brd.EntityId = ccs.Id
				WHERE brd.BankReconciliationId = @BankReconciliationId
					AND brd.Reconciled = 0

		/******************************************************************************************************/

		INSERT INTO @Table_Result
			SELECT	0 IsBookRegister,
					bred.DocumentType, 
					'' Code, 
					bred.DocumentDate,
					bred.DocumentNumber,
					bred.Description,
					bred.Nature, 
					bred.Value
			FROM Treasury.BankReconciliationExtractDetail bred
			WHERE bred.BankReconciliationId = @BankReconciliationId
		
	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))		

		DELETE @Table_Result
	END CATCH

	SELECT	tr.DetailIsBookRegister,
			tr.DetailDocumentType,
			CASE DetailDocumentType
				WHEN 1 THEN 'Recibos de Caja' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Extracto', ' No registrados en Libro')
				WHEN 2 THEN 'Comprobantes de Egreso' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Extracto', ' No registrados en Libro')
				WHEN 3 THEN 'Notas' + IIF(tr.DetailNature = 1, ' Débito', ' Crédito') + + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Extracto', ' No registrados en Libro')
				WHEN 4 THEN 'Consignaciones' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Extracto', ' No registrados en Libro')
				WHEN 5 THEN 'Fondo de Caja Menor' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Extracto', ' No registrados en Libro')
			END DocumentTypeName,
			tr.DetailCode,
			tr.DetailDocumentDate,
			tr.DetailDocumentNumber,
			tr.DetailObservations,
			tr.DetailNature,
			CASE tr.DetailNature
				WHEN 1 THEN 'Menos'
				WHEN 2 THEN 'Mas'
			END DetailNatureName,
			tr.DetailValue
	FROM Treasury.BankReconciliation br
	JOIN Treasury.EntityBankAccounts eba ON br.EntityBankAccountId = eba.Id
	JOIN Payroll.Bank b ON eba.IdBank = b.Id
	LEFT JOIN @Table_Result tr ON br.Id = @BankReconciliationId
	WHERE br.Id = @BankReconciliationId
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de una conciliación bancaria, mostrando las partidas pendientes de conciliar tanto desde el libro contable como desde el extracto bancario. Cruza los movimientos del libro (recibos de caja, comprobantes de egreso, notas de tesorería, consignaciones y fondos de caja menor registrados en BankReconciliationDetail) con los movimientos del extracto bancario (BankReconciliationExtractDetail), identificando cuáles no han sido conciliados aún. Para cada ítem del libro, recupera la fecha, número de documento y observaciones desde la tabla correspondiente según el tipo de documento (CashReceipts, VoucherTransaction, TreasuryNote, Consignment o ConstitutionCashSmaller), e indica si el movimiento es débito o crédito. El resultado sirve para cuadre y auditoría bancaria, permitiendo visualizar las diferencias entre los registros internos de tesorería y el extracto del banco.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBankReconciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBankReconciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de detalle de una conciliación bancaria, listando los movimientos del libro pendientes y los movimientos del extracto bancario asociados, con sus etiquetas de tipo de documento y naturaleza.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /Data/BankReconciliationId con el identificador de la conciliación a reportar.; Debe existir el registro de conciliación bancaria referenciado, con su cuenta bancaria de entidad y banco asociados.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen como partidas del libro aquellas con Reconciled=0 (pendientes de conciliar).; Para los recibos de caja (DocumentType=1) solo se consideran aquellos con Status IN (2,4).; Las partidas del extracto siempre se devuelven con DetailCode vacío y IsBookRegister=0.; Ante cualquier error en la construcción del resultado, no se retornan filas de detalle (la tabla temporal queda vacía).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación bancaria; Recibo de caja; Comprobante de egreso; Nota débito/crédito de tesorería; Consignación bancaria; Caja menor; Tarjeta de crédito; Cheque; Extracto bancario; Naturaleza contable', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado (SELECT final): Devuelve los detalles del libro (BankReconciliationDetail no conciliados) y del extracto (BankReconciliationExtractDetail) para la conciliación solicitada, enriquecidos con DocumentTypeName y DetailNatureName.; [INSERT] @Table_Result: Inserta como registros de libro (IsBookRegister=1) los BankReconciliationDetail con BankReconciliationId=@BankReconciliationId y Reconciled=0, resolviendo fecha/número/observaciones según DocumentType (1=CashReceipts, 2=VoucherTransaction, 3=TreasuryNote, 4=Consignment, 5=ConstitutionCashSmaller).; [INSERT] @Table_Result: Inserta como registros de extracto (IsBookRegister=0) todos los BankReconciliationExtractDetail asociados al BankReconciliationId.; [DELETE] @Table_Result: En caso de error en TRY, vacía la tabla temporal en el bloque CATCH e imprime el mensaje y línea del error, devolviendo un resultado vacío.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DocumentType del detalle de libro → Selecciona la fuente del documento: 1→CashReceipts, 2→VoucherTransaction, 3→TreasuryNote, 4→Consignment, 5→ConstitutionCashSmaller, determinando fecha, número y observaciones.; si DocumentType=1 y PaymentMethodTypes del recibo → Si PaymentMethodTypes=3 usa CardNumber con prefijo ''Tarjeta de Crédito''; si =4 usa DepositNumber con prefijo ''Consignación''. else Otros métodos de pago no aportan número ni prefijo, solo el detalle del recibo.; si DocumentType=2 y PaymentMethod del comprobante → Si PaymentMethod=1 toma CheckNumber; si =2 toma NoteNumber como número de documento.; si DocumentType=3 (Notas) → Etiqueta como ''Notas Débito'' si Nature=1 o ''Notas Crédito'' en otro caso.; si IsBookRegister del registro → Si =1 etiqueta el tipo como ''No registrados en Extracto''; si =0 como ''No registrados en Libro''.; si Nature del detalle → 1→''Menos'', 2→''Mas'' como nombre de naturaleza contable.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.BankReconciliationDetail; Treasury.CashReceipts; Treasury.PaymentMethods; Treasury.VoucherTransaction; Treasury.TreasuryNote; Treasury.Consignment; Treasury.ConstitutionCashSmaller; Treasury.BankReconciliationExtractDetail; Treasury.BankReconciliation; Treasury.EntityBankAccounts; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliation';
-- GO
