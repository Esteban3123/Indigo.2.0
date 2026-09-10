

-- ===============================================================================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-05-13
-- Description:	Procedimiento que se encarga de obtener los detalles de la conciliación bancaria automatica
-- ==============================================================================================================
CREATE PROCEDURE [Treasury].[SP_ReportBankReconciliationAutomatic]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @BankReconciliationAutomaticId INT

	DECLARE @Table_Result AS TABLE
	(
		[DetailIsBookRegister] [bit],
		[DetailDocumentType] [tinyint] NOT NULL,
		[DetailCode] [varchar](20) NOT NULL,
		[DetailDocumentDate] DATETIME,
		[DetailDocumentNumber] varchar(60),
		[DetailObservations] VARCHAR(MAX),				
		[DetailNature] [tinyint] NOT NULL,
		[DetailValue] [decimal](18, 2) NOT NULL
	

	)

	BEGIN TRY

		SELECT	
			@BankReconciliationAutomaticId = t.x.value('BankReconciliationAutomaticId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/******************************************************************************************************/
		print 'entra'
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
				FROM Treasury.BankReconciliationAutomaticDetail brd
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
				WHERE brd.BankReconciliationAutomaticId = @BankReconciliationAutomaticId
					AND brd.Reconciled = 0

		/******************************************************************************************************/
			

		INSERT INTO @Table_Result
			SELECT	0 IsBookRegister,					
					CASE 
						WHEN  RTRIM(LTRIM(LOWER(ubsd.DescriptionTransaction))) = 'recibo de caja' THEN '1'
						WHEN  RTRIM(LTRIM(LOWER(ubsd.DescriptionTransaction))) = 'comprobante de egreso'  THEN '2'
						WHEN  RTRIM(LTRIM(LOWER(ubsd.DescriptionTransaction))) = 'nota'  THEN '3'
						WHEN  RTRIM(LTRIM(LOWER(ubsd.DescriptionTransaction))) = 'consignacion'  THEN '4'
						WHEN  RTRIM(LTRIM(LOWER(ubsd.DescriptionTransaction))) = 'traslado'  THEN '5'
						ELSE '6'
					END AS DetailDocumentType,
					'' Code, 
					bred.DocumentDate,
					ubsd.TransactionCode,
					ubsd.DescriptionTransaction,
					bred.Nature,
					bred.Value			
			FROM Treasury.BankReconciliationAutomaticExtractDetail bred
			JOIN Treasury.UploadBankStatementsDetail ubsd ON bred.UploadBankStatementsDetailId = ubsd.Id
			WHERE bred.BankReconciliationAutomaticId = @BankReconciliationAutomaticId
		
	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))		

		DELETE @Table_Result
	END CATCH
	--------------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT	tr.DetailIsBookRegister,
			tr.DetailDocumentType,
			CASE DetailDocumentType
				WHEN 1 THEN 'Recibos de Caja' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Libro', ' No registrados en Extracto')
				WHEN 2 THEN 'Comprobantes de Egreso' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Libro', ' No registrados en Extracto')
				WHEN 3 THEN 'Notas' + IIF(tr.DetailNature = 1, ' Débito', ' Crédito') + + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Libro', ' No registrados en Extracto')
				WHEN 4 THEN 'Consignaciones' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Libro', ' No registrados en Extracto')
				WHEN 5 THEN 'Fondo de Caja Menor' + IIF(tr.DetailIsBookRegister = 1, ' No registrados en Libro', ' No registrados en Extracto')
			END DocumentTypeName,
			tr.DetailCode,
			tr.DetailDocumentDate,
			tr.DetailDocumentNumber,
			tr.DetailObservations,
			tr.DetailValue,
			tr.DetailNature,
			CASE tr.DetailNature
				WHEN 1 THEN 'Menos'
				WHEN 2 THEN 'Mas'
			END DetailNatureName

	FROM Treasury.BankReconciliationAutomatic br
	JOIN Treasury.EntityBankAccounts eba ON br.EntityBankAccountId = eba.Id
	JOIN Payroll.Bank b ON eba.IdBank = b.Id
	LEFT JOIN @Table_Result tr ON br.Id = @BankReconciliationAutomaticId
	WHERE br.Id = @BankReconciliationAutomaticId

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte detallado de una conciliación bancaria automática, identificando las partidas que no cuadran entre el libro contable y el extracto bancario. Para cada diferencia encontrada, clasifica el documento de origen (recibo de caja, comprobante de egreso, nota de tesorería, consignación o fondo de caja menor) cruzando los detalles registrados en BankReconciliationAutomaticDetail contra las tablas maestras de CashReceipts, VoucherTransaction, TreasuryNote, Consignment y ConstitutionCashSmaller. También incorpora las partidas del extracto bancario importado (BankReconciliationAutomaticExtractDetail / UploadBankStatementsDetail) que no tienen correspondencia en el libro, indicando para cada ítem si está pendiente en el libro o en el extracto, el tipo de documento, fecha, número, observaciones, valor y naturaleza (débito o crédito).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBankReconciliationAutomatic';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de detalle de una conciliación bancaria automática, contrastando los movimientos no conciliados del libro contra los movimientos del extracto bancario, clasificando cada partida por tipo de documento y naturaleza.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data/BankReconciliationAutomaticId con el identificador de la conciliación a reportar.; Debe existir un registro en Treasury.BankReconciliationAutomatic cuyo Id coincida con el extraído del XML.; La cuenta bancaria asociada (EntityBankAccounts) y el banco (Payroll.Bank) deben existir para que se devuelva la cabecera.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan partidas del libro que no están conciliadas (brd.Reconciled = 0).; El reporte se filtra siempre por un único BankReconciliationAutomaticId extraído del XML.; Cada fila se etiqueta como proveniente del libro (IsBookRegister=1) o del extracto (IsBookRegister=0), nunca ambos.; La naturaleza 1 representa débito/''Menos'' y la naturaleza 2 representa crédito/''Mas''.; Los recibos de caja considerados como pagos válidos para conciliación son únicamente los que están en Status 2 o 4.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación bancaria automática; Recibo de caja; Comprobante de egreso; Nota débito/crédito de tesorería; Consignación bancaria; Caja menor (constitución); Extracto bancario; Método de pago (tarjeta de crédito, consignación, cheque, nota); Cuenta bancaria de la entidad; Traslado bancario', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Inserta como ''registros del libro'' (IsBookRegister=1) los detalles de BankReconciliationAutomaticDetail de la conciliación indicada que aún no están conciliados (Reconciled=0), enriqueciendo fecha, número y observación según DocumentType (1=CashReceipts, 2=VoucherTransaction, 3=TreasuryNote, 4=Consignment, 5=ConstitutionCashSmaller).; [INSERT] @Table_Result: Inserta como ''registros del extracto'' (IsBookRegister=0) los movimientos de BankReconciliationAutomaticExtractDetail unidos a UploadBankStatementsDetail para la conciliación indicada, mapeando DescriptionTransaction (recibo de caja=1, comprobante de egreso=2, nota=3, consignacion=4, traslado=5, otro=6) a DetailDocumentType.; [DELETE] @Table_Result: Si ocurre cualquier error durante los INSERT, se vacía la tabla de resultados (DELETE @Table_Result) y solo se imprimen el mensaje y línea del error, devolviendo un resultado sin detalles.; [RETURN_RESULT] Resultset: Devuelve un resultset con los detalles agregados, calculando DocumentTypeName por tipo y agregando sufijo '' No registrados en Libro'' o '' No registrados en Extracto'' según DetailIsBookRegister, y DetailNatureName (''Menos'' si Nature=1, ''Mas'' si Nature=2); para Notas también añade '' Débito'' (Nature=1) o '' Crédito'' (otro).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si brd.DocumentType ∈ {1,2,3,4,5} → Selecciona la fecha, número de documento y observación desde la tabla origen correspondiente (CashReceipts, VoucherTransaction, TreasuryNote, Consignment o ConstitutionCashSmaller). else No se obtienen valores para fecha/número/observación.; si Para DocumentType=1 (recibo de caja): pm.PaymentMethodTypes = 3 vs 4 → Si es 3 usa CardNumber y etiqueta ''Tarjeta de Crédito''; si es 4 usa DepositNumber y etiqueta ''Consignación''. else El número de documento queda nulo y solo se muestra el detalle del recibo.; si Para DocumentType=2 (comprobante de egreso): vt.PaymentMethod = 1 vs 2 → Si es 1 toma CheckNumber como número de documento; si es 2 toma NoteNumber. else DocumentNumber queda nulo.; si RTRIM(LTRIM(LOWER(ubsd.DescriptionTransaction))) coincide con ''recibo de caja''/''comprobante de egreso''/''nota''/''consignacion''/''traslado'' → Asigna DetailDocumentType 1/2/3/4/5 respectivamente. else Asigna DetailDocumentType=6 (otros).; si cr.Status IN (2,4) → Solo se consideran recibos de caja en estos estados al unir con BankReconciliationAutomaticDetail. else Recibos en otros estados quedan fuera del cruce y aparecerán con campos nulos.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.BankReconciliationAutomaticDetail; Treasury.CashReceipts; Treasury.PaymentMethods; Treasury.VoucherTransaction; Treasury.TreasuryNote; Treasury.Consignment; Treasury.ConstitutionCashSmaller; Treasury.BankReconciliationAutomaticExtractDetail; Treasury.UploadBankStatementsDetail; Treasury.BankReconciliationAutomatic; Treasury.EntityBankAccounts; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBankReconciliationAutomatic';
-- GO
