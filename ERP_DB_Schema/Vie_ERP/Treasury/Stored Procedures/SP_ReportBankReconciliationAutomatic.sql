

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