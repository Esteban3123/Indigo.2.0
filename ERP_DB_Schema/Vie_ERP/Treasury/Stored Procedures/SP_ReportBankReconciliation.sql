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