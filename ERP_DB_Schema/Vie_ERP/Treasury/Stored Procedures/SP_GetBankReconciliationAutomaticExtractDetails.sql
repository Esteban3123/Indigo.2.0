CREATE PROCEDURE [Treasury].[SP_GetBankReconciliationAutomaticExtractDetails]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @EntityBankAccountId INT,
			@BankReconciliationAutomaticId INT,
			@Month INT,
            @Year INT,
            @ReconciliationStatus TINYINT

    SELECT @BankReconciliationAutomaticId = t.x.value('BankReconciliationAutomaticId[1]','int'),
		   @EntityBankAccountId = t.x.value('EntityBankAccountId[1]','int'),
		   @Month = t.x.value('Month[1]','int'),
		   @Year = t.x.value('Year[1]','int')
	FROM @xmlCriterias.nodes('/Data') t(x)

-- Obtener el estado de la conciliación
SELECT @ReconciliationStatus = Status 
FROM Treasury.BankReconciliationAutomatic 
WHERE Id = @BankReconciliationAutomaticId

	DECLARE @StartDate DATE = DATEFROMPARTS(@Year, @Month, 1)
	DECLARE @EndDate DATE = DATEADD(DAY, 1, EOMONTH(@StartDate))

	DECLARE @Table_Result AS TABLE
	(
		[BankReconciliationAutomaticExtractDetailId] [int],
		[UploadBankStatementsDetailId] [int],
		[TransactionDate] [DATETIME] NOT NULL,
		[ConsecutiveBank] [varchar](60) NOT NULL,
		[TransactionCode] [varchar](60) NOT NULL,
		[DescriptionTransaction] [varchar](60) NOT NULL,
		[DocumentType] [int] NULL,
		[BankCheck] [BIGINT] NULL,	
		[PaymentReferenceOne] [varchar](15) NULL,			
		[PaymentReferenceTwo] [varchar](15) NULL,
		[Nature] [tinyint] NOT NULL,
		[Value] [decimal](20, 2) NOT NULL,
		[Reconciled] [bit] NOT NULL,
		[CodeNoteReconciled] [varchar](MAX) NULL
		----------------------------------		
	)

	BEGIN TRY					
		    WITH cte_bankReconciliationAutomatic AS (
        SELECT 
            braed.Id,
            IIF(braed.Reconciled = 1 AND (@ReconciliationStatus = 2 OR cr.Status = 2), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'PaymentMethods'
        JOIN Treasury.CashReceipts cr ON cr.Id = brad.EntityId AND cr.Code = brad.EntityCode
        UNION
        SELECT 
            braed.Id,
            IIF(braed.Reconciled = 1 AND (@ReconciliationStatus = 2 OR vt.Status = 2), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'VoucherTransaction'
        JOIN Treasury.VoucherTransaction vt ON vt.Id = brad.EntityId AND vt.Code = brad.EntityCode
        UNION
        SELECT 
            braed.Id,
            IIF(braed.Reconciled = 1 AND (@ReconciliationStatus = 2 OR tn.Status = 2), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'TreasuryNote'
        JOIN Treasury.TreasuryNote tn ON tn.Id = brad.EntityId AND tn.Code = brad.EntityCode
        UNION
        SELECT 
            braed.Id,
            IIF(braed.Reconciled = 1 AND (@ReconciliationStatus = 2 OR c.Status = 2), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'Consignment'
        JOIN Treasury.Consignment c ON c.Id = brad.EntityId AND c.Code = brad.EntityCode
    ),

    cte_datos AS (
        SELECT 
            BankReconciliationAutomaticExtractDetailId =
                IIF(bra.Status <> 3, braed.Id, NULL),
            UploadBankStatementsDetailId = ubsd.Id,
            ubsd.TransactionDate, 
            ubsd.ConsecutiveBank, 
            ubsd.TransactionCode, 
            ubsd.DescriptionTransaction,
            DocumentType = ISNULL(braed.DocumentType, ubsd.DocumentType), 	
            ubsd.BankCheck,
            ubsd.PaymentReferenceOne,
            ubsd.PaymentReferenceTwo,
            Nature = IIF(ubsd.ValueDebit > 0, 1, 2),
            Value  = ABS(ubsd.ValueDebit - ubsd.ValueCredit),
            Reconciled = ISNULL(cte.Reconciled, 0),
            braed.CodeNoteReconciled,

            -- Prioriza la fila “guardada” (Id no nulo) sobre la que tiene Id NULL
            rn = ROW_NUMBER() OVER (
                    PARTITION BY ubsd.Id
                    ORDER BY 
                        CASE 
                            WHEN bra.Status <> 3 AND braed.Id IS NOT NULL THEN 0  -- preferir esta
                            ELSE 1
                        END,
                        braed.Id DESC
                 )
        FROM Treasury.UploadBankStatements ubs WITH (NOLOCK)
        LEFT JOIN Treasury.UploadBankStatementsDetail ubsd WITH (NOLOCK) 
            ON ubsd.UploadBankStatementsId = ubs.Id  
        LEFT JOIN Treasury.BankReconciliationAutomaticExtractDetail braed WITH (NOLOCK) 
            ON ubsd.Id = braed.UploadBankStatementsDetailId 
        OUTER APPLY (
            SELECT TOP 1 braInner.*
            FROM Treasury.BankReconciliationAutomatic braInner WITH (NOLOCK)
            WHERE 
                (braInner.Id = braed.BankReconciliationAutomaticId OR 
                 braInner.Id = braed.BankReconciliationAutomaticOriginId)
                AND braInner.Status <> 3
                AND braInner.DocumentDate >= @StartDate 
                AND braInner.DocumentDate < @EndDate
        ) bra
        LEFT JOIN cte_bankReconciliationAutomatic cte WITH (NOLOCK) 
            ON cte.Id = braed.Id
        WHERE 
            ubs.EntityBankAccountId = @EntityBankAccountId AND 
            ubs.Year = @Year AND 
            ubs.Month = @Month AND 
            (@ReconciliationStatus = 2 OR ubs.Status = 2) AND
            ISNULL(bra.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId
    )

    INSERT INTO @Table_Result
    SELECT
        BankReconciliationAutomaticExtractDetailId,
        UploadBankStatementsDetailId,
        TransactionDate,
        ConsecutiveBank,
        TransactionCode,
        DescriptionTransaction,
        DocumentType,
        BankCheck,
        PaymentReferenceOne,
        PaymentReferenceTwo,
        Nature,
        Value,
        Reconciled,
        CodeNoteReconciled
    FROM cte_datos
    WHERE rn = 1;   -- <-- elimina los duplicados



	SELECT	*
	FROM @Table_Result tr
	ORDER BY tr.TransactionDate

	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))

		DELETE FROM @Table_Result		
	END CATCH
END