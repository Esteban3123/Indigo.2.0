

-- ===============================================================================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-04-25
-- Description:	Procedimiento que se encarga de obtener los detalles de la conciliación bancaria automatica
-- ==============================================================================================================
CREATE PROCEDURE [Treasury].[SP_GetBankReconciliationAutomaticDetails]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DocumentDate DATE,
			@EntityBankAccountId INT,
			@BankReconciliationAutomaticId INT

	SELECT	@BankReconciliationAutomaticId = t.x.value('BankReconciliationAutomaticId[1]','int'),
			@EntityBankAccountId = t.x.value('EntityBankAccountId[1]','int'),
			@DocumentDate = t.x.value('DocumentDate[1]','date')
			
	FROM @xmlCriterias.nodes('/Data') t(x)

	DECLARE @StartDate DATE = DATEFROMPARTS(YEAR(@DocumentDate), MONTH(@DocumentDate), 1)
	DECLARE @EndDate DATE = DATEADD(DAY, 1, EOMONTH(@StartDate))

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
		[Comments] [varchar](100) NULL,
		[ReconciledStatus] [tinyint] NULL,
		----------------------------------
		DocumentDate DATETIME,
		ThirdPartyNitName VARCHAR(MAX),
		NitThirdParty VARCHAR(25),
		DocumentNumber VARCHAR(50),
		Observations VARCHAR(MAX),
		CreationUser VARCHAR(20),
		ConfirmationUser VARCHAR(20)
	)

	BEGIN TRY

		/************************************ RECIBOS DE CAJA ************************************/

		INSERT INTO @Table_Result
			SELECT
					ISNULL(brd.Id, 0) Id, 
					1 DocumentType, 
					1 Nature,
					pm.Value, 
					cr.Id EntityId, 
					cr.Code EntityCode, 
					'PaymentMethods' EntityName,
					ISNULL(brd.Reconciled, 0) Reconciled,
					Null Comments,
					brd.ReconciledStatus,
					cr.DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
					tp.Nit NitThirdParty,
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
			LEFT JOIN Treasury.BankReconciliationAutomaticDetail brd ON brd.DocumentType = 1 AND cr.Id = brd.EntityId  
			OUTER APPLY (
				SELECT TOP 1 braInner.*
				FROM Treasury.BankReconciliationAutomatic braInner WITH (NOLOCK)
				WHERE 
					(braInner.Id = brd.BankReconciliationAutomaticId OR 
					 braInner.Id = brd.BankReconciliationAutomaticOriginId)
					AND braInner.Status <> 3
					AND braInner.DocumentDate >= @StartDate AND braInner.DocumentDate < @EndDate
			) br
			WHERE cr.IdBankAccount = @EntityBankAccountId
				AND cr.Status = 2
				AND cr.DocumentDate >= @StartDate AND cr.DocumentDate < @EndDate
				AND ISNULL(br.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId  
		/********************************  COMPROBANTES DE EGRESO ********************************/

		INSERT INTO @Table_Result
			SELECT	
					ISNULL(brd.Id, 0) Id, 
					2 DocumentType, 
					2 Nature,
					vt.Value, 
					vt.Id EntityId, 
					vt.Code EntityCode, 
					'VoucherTransaction' EntityName, 
					ISNULL(brd.Reconciled, 0) Reconciled,
					Null Comments,
					brd.ReconciledStatus,
					vt.DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
					tp.Nit NitThirdParty,
					CASE vt.PaymentMethod
						WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
						WHEN 2 THEN vt.NoteNumber
					END DocumentNumber,
					vt.Detail Observations,
					vt.CreationUser,
					vt.ConfirmationUser
			FROM Treasury.VoucherTransaction vt
			LEFT JOIN Common.ThirdParty tp ON vt.IdThirdParty = tp.Id
			LEFT JOIN Treasury.BankReconciliationAutomaticDetail brd ON brd.DocumentType = 2 AND vt.Id = brd.EntityId 
			OUTER APPLY (
				SELECT TOP 1 braInner.*
				FROM Treasury.BankReconciliationAutomatic braInner WITH (NOLOCK)
				WHERE 
					(braInner.Id = brd.BankReconciliationAutomaticId OR 
					 braInner.Id = brd.BankReconciliationAutomaticOriginId)
					AND braInner.Status <> 3
					AND braInner.DocumentDate >= @StartDate AND braInner.DocumentDate < @EndDate
			) br
			WHERE vt.IdEntityBankAccount = @EntityBankAccountId
				AND vt.Status = 2
				AND vt.DocumentDate >= @StartDate AND vt.DocumentDate < @EndDate
				AND ISNULL(br.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId  
			UNION ALL

			SELECT
					ISNULL(brd.Id, 0) Id, 
					2 DocumentType, 
					1 Nature,
					vtd.Value, 
					vt.Id EntityId, 
					vt.Code EntityCode, 
					'VoucherTransaction' EntityName, 
					ISNULL(brd.Reconciled, 0) Reconciled,
					NUll Comments,
					brd.ReconciledStatus,
					vt.DocumentDate,
					NULL ThirdPartyNitName,
					NULL NitThirdParty,
					CASE vt.PaymentMethod
						WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
						WHEN 2 THEN vt.NoteNumber
					END DocumentNumber,
					vt.Detail Observations,
					vt.CreationUser,
					vt.ConfirmationUser
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			LEFT JOIN Treasury.BankReconciliationAutomaticDetail brd ON brd.DocumentType = 2 AND vt.Id = brd.EntityId 
			OUTER APPLY (
				SELECT TOP 1 braInner.*
				FROM Treasury.BankReconciliationAutomatic braInner WITH (NOLOCK)
				WHERE 
					(braInner.Id = brd.BankReconciliationAutomaticId OR 
					 braInner.Id = brd.BankReconciliationAutomaticOriginId)
					AND braInner.Status <> 3
					AND braInner.DocumentDate >= @StartDate AND braInner.DocumentDate < @EndDate
			) br
			WHERE vtd.IdEntityBankAccount = @EntityBankAccountId
				AND vt.Status = 2
				AND vt.DocumentDate >= @StartDate AND vt.DocumentDate < @EndDate
				AND ISNULL(br.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId  
		/****************************************  NOTAS *****************************************/

		INSERT INTO @Table_Result
			SELECT	
					ISNULL(brd.Id, 0) Id, 
					3 DocumentType, 
					tn.Nature,
					tn.[Value] Value, 
					tn.Id EntityId, 
					tn.Code EntityCode, 
					'TreasuryNote' EntityName, 
					ISNULL(brd.Reconciled, 0) Reconciled,
					Null Comments,
					brd.ReconciledStatus,
					tn.NoteDate,
					NULL ThirdPartyNitName,
					NULL NitThirdParty,
					NULL DocumentNumber,
					tn.Description Observations,
					tn.CreationUser,
					tn.ConfirmationUser
			FROM Treasury.TreasuryNote tn			
			LEFT JOIN Treasury.BankReconciliationAutomaticDetail brd ON brd.DocumentType = 3 AND tn.Id = brd.EntityId  
			OUTER APPLY (
				SELECT TOP 1 braInner.*
				FROM Treasury.BankReconciliationAutomatic braInner WITH (NOLOCK)
				WHERE 
					(braInner.Id = brd.BankReconciliationAutomaticId OR 
					 braInner.Id = brd.BankReconciliationAutomaticOriginId)
					AND braInner.Status <> 3
					AND braInner.DocumentDate >= @StartDate AND braInner.DocumentDate < @EndDate
			) br
			WHERE tn.EntityBankAccountId = @EntityBankAccountId
					AND tn.Status = 2
					AND tn.NoteType NOT IN (3, 4, 5, 6, 7)
					AND tn.NoteDate >= @StartDate AND tn.NoteDate < @EndDate
					AND ISNULL(br.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId  
		/************************************  CONSIGNACIONES ************************************/

		INSERT INTO @Table_Result
			SELECT
					ISNULL(brd.Id, 0) Id, 
					4 DocumentType, 
					1 Nature,
					c.Value, 
					c.Id EntityId, 
					c.Code EntityCode, 
					'Consignment' EntityName, 
					ISNULL(brd.Reconciled, 0) Reconciled,
					Null Comments,
					brd.ReconciledStatus,
					c.DocumentDate,
					NULL ThirdPartyNitName,
					NULL NitThirdParty,
					NULL DocumentNumber,
					c.Description Observations,
					c.CreationUser,
					c.ConfirmationUser
			FROM Treasury.Consignment c
			LEFT JOIN Treasury.BankReconciliationAutomaticDetail brd ON brd.DocumentType = 4 AND c.Id = brd.EntityId 
			OUTER APPLY (
				SELECT TOP 1 braInner.*
				FROM Treasury.BankReconciliationAutomatic braInner WITH (NOLOCK)
				WHERE 
					(braInner.Id = brd.BankReconciliationAutomaticId OR 
					 braInner.Id = brd.BankReconciliationAutomaticOriginId)
					AND braInner.Status <> 3
					AND braInner.DocumentDate >= @StartDate AND braInner.DocumentDate < @EndDate
			) br
			WHERE c.EntityBankAccountId = @EntityBankAccountId
				AND c.Status = 2
				AND c.DocumentDate >= @StartDate AND c.DocumentDate < @EndDate
				AND ISNULL(br.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId  
	
		SELECT	*
		FROM @Table_Result tr
		ORDER BY tr.DocumentDate

	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))

		DELETE FROM @Table_Result		
	END CATCH

END