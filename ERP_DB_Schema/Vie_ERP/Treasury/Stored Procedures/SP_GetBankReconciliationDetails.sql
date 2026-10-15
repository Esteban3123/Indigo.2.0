-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-01-15
-- Description:	Procedimiento que se encarga de obtener los detalles de la conciliación bancaria
-- ==============================================================================================================
CREATE PROCEDURE [Treasury].[SP_GetBankReconciliationDetails]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DocumentDate DATE,
			@EntityBankAccountId INT,
			@BankReconciliationId INT

	SELECT	@BankReconciliationId = t.x.value('BankReconciliationId[1]','int'),
			@EntityBankAccountId = t.x.value('EntityBankAccountId[1]','int'),
			@DocumentDate = t.x.value('DocumentDate[1]','date')
			
	FROM @xmlCriterias.nodes('/Data') t(x)

	DECLARE @Table_Result AS TABLE
	(
		[Id] [int],
		[DocumentType] [tinyint] NOT NULL,
		[Nature] [tinyint] NOT NULL,
		[Value] [decimal](20, 4) NOT NULL,
		[EntityId] [int] NOT NULL,
		[EntityCode] [varchar](20) NOT NULL,
		[EntityName] [varchar](250) NOT NULL,
		[Reconciled] [bit] NOT NULL,
		----------------------------------
		DocumentDate DATETIME,
		ThirdPartyNitName VARCHAR(MAX),
		DocumentNumber VARCHAR(50),
		Observations VARCHAR(MAX),
		CreationUser VARCHAR(20),
		ConfirmationUser VARCHAR(20)
	)

	BEGIN TRY
		
		INSERT INTO @Table_Result
			SELECT	brd.Id, brd.DocumentType, brd.Nature, brd.Value, brd.EntityId, brd.EntityCode, brd.EntityName, brd.Reconciled,
					CASE brd.DocumentType
						WHEN 1 THEN cr.DocumentDate
						WHEN 2 THEN vt.DocumentDate
						WHEN 3 THEN tn.NoteDate
						WHEN 4 THEN c.DocumentDate
						WHEN 5 THEN ccs.DocumentDate
					END DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
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
					CASE brd.DocumentType
						WHEN 1 THEN cr.CreationUser
						WHEN 2 THEN vt.CreationUser
						WHEN 3 THEN tn.CreationUser
						WHEN 4 THEN c.CreationUser
						WHEN 5 THEN ccs.CreationUser
					END CreationUser,
					CASE brd.DocumentType
						WHEN 1 THEN cr.ConfirmationUser
						WHEN 2 THEN vt.ConfirmationUser
						WHEN 3 THEN tn.ConfirmationUser
						WHEN 4 THEN c.ConfirmationUser
						WHEN 5 THEN ccs.ConfirmationUser
					END ConfirmationUser
			FROM Treasury.BankReconciliationDetail brd
			LEFT JOIN 
			(
				SELECT	pm.Id, 
						cr.DocumentDate, 
						cr.IdThirdParty, 
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
						) Detail,
						cr.CreationUser, 
						cr.ConfirmationUser
				FROM Treasury.CashReceipts cr 
				JOIN Treasury.PaymentMethods pm ON cr.Id = pm.IdCashReceipt
				WHERE cr.IdBankAccount = @EntityBankAccountId
					AND cr.Status IN (2, 4)
			) cr ON brd.DocumentType = 1 AND brd.EntityId = cr.Id
			LEFT JOIN Treasury.VoucherTransaction vt ON brd.DocumentType = 2 AND brd.EntityId = vt.Id
			LEFT JOIN Treasury.TreasuryNote tn ON brd.DocumentType = 3 AND brd.EntityId = tn.Id
			LEFT JOIN Treasury.Consignment c ON brd.DocumentType = 4 AND brd.EntityId = c.Id
			LEFT JOIN Treasury.ConstitutionCashSmaller ccs ON brd.DocumentType = 5 AND brd.EntityId = ccs.Id
			LEFT JOIN Common.ThirdParty tp ON tp.Id = CASE brd.DocumentType
														WHEN 1 THEN cr.IdThirdParty
														WHEN 2 THEN vt.IdThirdParty
														END
			WHERE brd.BankReconciliationId = @BankReconciliationId AND CAST(tn.NoteDate AS DATE) <= @DocumentDate

		/************************************ RECIBOS DE CAJA ************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					1 DocumentType, 
					1 Nature,
					pm.Value, 
					pm.Id EntityId, 
					cr.Code EntityCode, 
					'PaymentMethods' EntityName, 
					1 Reconciled,
					cr.DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
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
					) Observations,
					cr.CreationUser,
					cr.ConfirmationUser
			FROM Treasury.CashReceipts cr
			JOIN Treasury.PaymentMethods pm ON cr.Id = pm.IdCashReceipt
			JOIN Common.ThirdParty tp ON cr.IdThirdParty = tp.Id
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 1 AND pm.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 1 AND pm.Id = tr.EntityId
			WHERE cr.IdBankAccount = @EntityBankAccountId
				AND cr.Status IN (2, 4)
				AND CAST(cr.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		/********************************  COMPROBANTES DE EGRESO ********************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					2 DocumentType, 
					2 Nature,
					vt.Value, 
					vt.Id EntityId, 
					vt.Code EntityCode, 
					'VoucherTransaction' EntityName, 
					1 Reconciled,
					vt.DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
					CASE vt.PaymentMethod
						WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
						WHEN 2 THEN vt.NoteNumber
					END DocumentNumber,
					vt.Detail Observations,
					vt.CreationUser,
					vt.ConfirmationUser
			FROM Treasury.VoucherTransaction vt
			left JOIN Common.ThirdParty tp ON vt.IdThirdParty = tp.Id
			LEFT JOIN (Treasury.BankReconciliationDetail brd
			inner JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId =  @EntityBankAccountId and brd.Reconciled = 1 and brd.DocumentType = 2) ON vt.Id = brd.EntityId 
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 2 AND vt.Id = tr.EntityId
			WHERE vt.IdEntityBankAccount =  @EntityBankAccountId
				AND vt.Status IN (2, 4)
				AND CAST(vt.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

			UNION ALL

			SELECT	0 Id, 
					2 DocumentType, 
					1 Nature,
					vtd.Value, 
					vt.Id EntityId, 
					vt.Code EntityCode, 
					'VoucherTransaction' EntityName, 
					1 Reconciled,
					vt.DocumentDate,
					NULL ThirdPartyNitName,
					CASE vt.PaymentMethod
						WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
						WHEN 2 THEN vt.NoteNumber
					END DocumentNumber,
					vt.Detail Observations,
					vt.CreationUser,
					vt.ConfirmationUser
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			LEFT JOIN (Treasury.BankReconciliationDetail brd
			inner JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) 
			AND br.EntityBankAccountId = @EntityBankAccountId and brd.Reconciled = 1 and brd.DocumentType = 2) ON vt.Id = brd.EntityId LEFT JOIN @Table_Result tr ON tr.DocumentType = 2 AND vt.Id = tr.EntityId
			WHERE vtd.IdEntityBankAccount = @EntityBankAccountId
				AND vt.Status IN (2, 4)
				AND CAST(vt.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		/****************************************  NOTAS *****************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					3 DocumentType, 
					CASE tn.NoteType
						WHEN 1 THEN tn.Nature
						WHEN 3 THEN vt.Nature
					END Nature,
					CASE tn.NoteType
						WHEN 1 THEN tn.Value
						WHEN 3 THEN vt.Value
						WHEN 4 THEN cr.Value
						WHEN 5 THEN c.Value
					END Value, 
					tn.Id EntityId, 
					tn.Code EntityCode, 
					'TreasuryNote' EntityName, 
					1 Reconciled,
					tn.NoteDate,
					NULL ThirdPartyNitName,
					NULL DocumentNumber,
					ISNULL(CASE tn.NoteType						
						WHEN 3 THEN 'Reversión Comprobante de Egreso'
						WHEN 4 THEN 'Reversión Recibo de Caja'
						WHEN 5 THEN 'Reversión Consignación'
					END, '') + tn.Description Observations,
					tn.CreationUser,
					tn.ConfirmationUser
			FROM Treasury.TreasuryNote tn			
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 3 AND tn.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN
			(
				SELECT vt.Id, 1 Nature, vt.Value, vt.IdEntityBankAccount
				FROM Treasury.VoucherTransaction vt
				WHERE vt.IdEntityBankAccount = @EntityBankAccountId

				UNION ALL

				SELECT vtd.IdVoucherTransaction, 2 Nature, vtd.Value, vtd.IdEntityBankAccount
				FROM Treasury.VoucherTransactionDetails vtd
				WHERE vtd.IdEntityBankAccount = @EntityBankAccountId
			) vt ON tn.NoteType = 3 AND tn.VoucherTransactionId = vt.Id
			LEFT JOIN
			(
				SELECT cr.Id, 2 Nature, pm.Value
				FROM Treasury.CashReceipts cr
				JOIN Treasury.PaymentMethods pm ON cr.Id = pm.IdCashReceipt
				WHERE cr.IdBankAccount = @EntityBankAccountId
			) cr ON tn.NoteType = 4 AND tn.CashReceiptId = cr.Id
			LEFT JOIN
			(
				SELECT c.Id, 2 Nature, c.Value
				FROM Treasury.Consignment c
				WHERE c.EntityBankAccountId = @EntityBankAccountId
			) c ON tn.NoteType = 5 AND tn.ConsignmentId = c.Id
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 3 AND tn.Id = tr.EntityId
			WHERE tn.EntityBankAccountId = @EntityBankAccountId OR VT.IdEntityBankAccount = @EntityBankAccountId
					AND tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) <= @DocumentDate
					AND br.Id IS NULL
					AND tr.Id IS NULL

		/************************************  CONSIGNACIONES ************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					4 DocumentType, 
					1 Nature,
					c.Value, 
					c.Id EntityId, 
					c.Code EntityCode, 
					'Consignment' EntityName, 
					1 Reconciled,
					c.DocumentDate,
					NULL ThirdPartyNitName,
					NULL DocumentNumber,
					c.Description Observations,
					c.CreationUser,
					c.ConfirmationUser
			FROM Treasury.Consignment c
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 4 AND c.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 4 AND c.Id = tr.EntityId
			WHERE c.EntityBankAccountId = @EntityBankAccountId
				AND c.Status IN (2, 4)
				AND CAST(c.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		/***************************************  TRASLADO ***************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					5 DocumentType, 
					2 Nature,
					ccs.Value, 
					ccs.Id EntityId, 
					ccs.Code EntityCode, 
					'ConstitutionCashSmaller' EntityName, 
					1 Reconciled,
					ccs.DocumentDate,
					NULL ThirdPartyNitName,
					NULL DocumentNumber,
					NULL Observations,
					ccs.CreationUser,
					ccs.ConfirmationUser
			FROM Treasury.ConstitutionCashSmaller ccs
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 5 AND ccs.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 5 AND ccs.Id = tr.EntityId
			WHERE ccs.EntityBankAccountId = @EntityBankAccountId
				AND ccs.Status = 2
				AND CAST(ccs.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		
	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))

		DELETE FROM @Table_Result		
	END CATCH

	SELECT  DISTINCT Id, 
			DocumentType, 
			Nature,
			Value, 
			EntityId, 
			EntityCode, 
			EntityName, 
			Reconciled,
			DocumentDate,
			ThirdPartyNitName,
			DocumentNumber,
			Observations,
			CreationUser,
			ConfirmationUser
	FROM @Table_Result tr
	ORDER BY tr.DocumentDate
	 

END