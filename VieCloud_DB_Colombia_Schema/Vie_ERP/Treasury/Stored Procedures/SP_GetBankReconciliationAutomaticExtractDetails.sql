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
            IIF(braed.Reconciled = 1 AND cr.Status IN (2, 4), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'PaymentMethods'
        JOIN Treasury.CashReceipts cr ON cr.Id = brad.EntityId AND cr.Code = brad.EntityCode
        UNION
        SELECT 
            braed.Id,
            IIF(braed.Reconciled = 1 AND vt.Status IN (2, 4), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'VoucherTransaction'
        JOIN Treasury.VoucherTransaction vt ON vt.Id = brad.EntityId AND vt.Code = brad.EntityCode
        UNION
        SELECT 
            braed.Id,
            IIF(braed.Reconciled = 1 AND tn.Status IN (2, 4), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'TreasuryNote'
        JOIN Treasury.TreasuryNote tn ON tn.Id = brad.EntityId AND tn.Code = brad.EntityCode
        UNION
        SELECT 
            braed.Id,
            IIF(braed.Reconciled = 1 AND c.Status IN (2, 4), 1, 0) Reconciled
        FROM Treasury.BankReconciliationAutomaticExtractDetail braed
        JOIN Treasury.BankReconciliationAutomatic bra ON bra.Id = braed.BankReconciliationAutomaticId
        JOIN Treasury.BankReconciliationAutomaticAssociation braa ON braa.BankReconciliationAutomaticExtractId = braed.Id 
        JOIN Treasury.BankReconciliationAutomaticDetail brad ON brad.Id = braa.BankReconciliationAutomaticDetailId AND brad.BankReconciliationAutomaticId = bra.Id AND brad.EntityName = 'Consignment'
        JOIN Treasury.Consignment c ON c.Id = brad.EntityId AND c.Code = brad.EntityCode
			)
					
		INSERT INTO @Table_Result
			SELECT 
				IIF(bra.Status <> 3, braed.Id, NULL) AS BankReconciliationAutomaticExtractDetailId,
				ubsd.Id AS UploadBankStatementsDetailId,
				ubsd.TransactionDate, 
				ubsd.ConsecutiveBank, 
				ubsd.TransactionCode, 
				ubsd.DescriptionTransaction,
				ISNULL(braed.DocumentType, ubsd.DocumentType) AS DocumentType, 	
				ubsd.BankCheck,
				ubsd.PaymentReferenceOne,
				ubsd.PaymentReferenceTwo,
				IIF(ubsd.ValueDebit > 0, 1, 2) AS Nature,
				ABS(ubsd.ValueDebit - ubsd.ValueCredit) AS Value,
				ISNULL(cte.Reconciled, 0) AS Reconciled,
				braed.CodeNoteReconciled
			FROM Treasury.UploadBankStatements ubs WITH (NOLOCK)
			LEFT JOIN Treasury.UploadBankStatementsDetail ubsd WITH (NOLOCK) ON ubsd.UploadBankStatementsId = ubs.Id  
			LEFT JOIN Treasury.BankReconciliationAutomaticExtractDetail braed WITH (NOLOCK) ON ubsd.Id = braed.UploadBankStatementsDetailId 
			OUTER APPLY (
				SELECT TOP 1 braInner.*
				FROM Treasury.BankReconciliationAutomatic braInner WITH (NOLOCK)
				WHERE 
					(braInner.Id = braed.BankReconciliationAutomaticId OR 
					 braInner.Id = braed.BankReconciliationAutomaticOriginId)
					AND braInner.Status <> 3
					AND braInner.DocumentDate >= @StartDate AND braInner.DocumentDate < @EndDate
			) bra
			LEFT JOIN cte_bankReconciliationAutomatic cte WITH (NOLOCK) ON cte.Id = braed.Id
			WHERE 
				ubs.EntityBankAccountId = @EntityBankAccountId AND 
				ubs.Year = @Year AND 
				ubs.Month = @Month AND 
        (@ReconciliationStatus = 2 OR ubs.Status = 2) AND
				ISNULL(bra.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId
			GROUP BY
				braed.Id, bra.Status, ubsd.Id, ubsd.TransactionDate, ubsd.ConsecutiveBank, ubsd.TransactionCode, ubsd.DescriptionTransaction, ubsd.DocumentType, braed.DocumentType, ubsd.BankCheck, 
				ubsd.PaymentReferenceOne, ubsd.PaymentReferenceTwo, ubsd.ValueDebit, ubsd.ValueCredit, cte.Reconciled,braed.CodeNoteReconciled; 

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle de los movimientos del extracto bancario asociados a un proceso de conciliación bancaria automática, filtrando por cuenta bancaria, mes y año. Para cada línea del extracto bancario (fecha, valor, referencia de pago, naturaleza débito/crédito) determina si ya fue conciliada, cruzando los registros con los documentos contables correspondientes: recibos de caja, comprobantes de egreso/vouchers, notas de tesorería y consignaciones. El estado de conciliación de cada movimiento se recalcula dinámicamente según el estado actual de la conciliación automática y el estado del documento asociado, priorizando siempre las filas ya guardadas en el proceso. Se usa para visualizar y gestionar el progreso de la conciliación bancaria automática, mostrando qué líneas del extracto bancario han sido reconciliadas contra los registros contables internos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de movimientos del extracto bancario para una conciliación automática de un mes/año y cuenta bancaria, indicando por línea su naturaleza, valor y si está conciliada según el estado de los documentos asociados.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe proveer BankReconciliationAutomaticId, EntityBankAccountId, Month y Year en /Data.; Debe existir un registro en Treasury.BankReconciliationAutomatic con el Id recibido para obtener su Status.; Debe existir un cargue de extracto (UploadBankStatements) para la EntityBankAccountId, Year y Month indicados.; Si el Status de la conciliación no es 2, el UploadBankStatements debe tener Status = 2 para que se devuelvan resultados.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran conciliaciones cuyo DocumentDate cae en el mes/año solicitado (>= primer día del mes y < primer día del mes siguiente).; Se excluyen conciliaciones con Status = 3 al resolver bra mediante OUTER APPLY.; Nature=1 representa débito (ValueDebit>0) y Nature=2 representa crédito; el Value reportado es siempre el valor absoluto.; Una línea solo se marca conciliada si el documento contable asociado (recibo, comprobante, nota o consignación) tiene Status=2 o si la conciliación general ya está en Status=2.; El filtro ISNULL(bra.Id, @BankReconciliationAutomaticId)=@BankReconciliationAutomaticId garantiza que se incluyan tanto líneas ligadas a la conciliación solicitada como líneas aún sin conciliación asociada.; Los errores no se propagan al cliente: se imprimen y se retorna un resultado vacío.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación bancaria automática; Extracto bancario; Recibo de caja (CashReceipts); Comprobante/transacción de tesorería (VoucherTransaction); Nota de tesorería (TreasuryNote); Consignación bancaria; Débito/Crédito (naturaleza del movimiento); Cuenta bancaria de la entidad; Estado de conciliación', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Inserta una fila por cada UploadBankStatementsDetail del periodo (Year/Month) y cuenta bancaria solicitada, calculando Nature=1 si ValueDebit>0 sino 2, Value=ABS(ValueDebit-ValueCredit), y Reconciled según cte (estado del documento asociado).; [RETURN_RESULT] Result: Devuelve el contenido de @Table_Result ordenado por TransactionDate.; [DELETE] @Table_Result: En caso de error en el TRY, imprime el mensaje y línea del error y vacía la tabla de resultados antes de retornar.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si bra.Status <> 3 (la conciliación asociada al detalle no está anulada/eliminada) → Se expone el BankReconciliationAutomaticExtractDetailId; en caso contrario se retorna NULL en ese campo.; si braed.Reconciled = 1 AND (@ReconciliationStatus = 2 OR Status del documento asociado = 2) → La línea se marca como Reconciled=1 en el resultado; de lo contrario, Reconciled=0.; si EntityName del detalle de conciliación = ''PaymentMethods'' | ''VoucherTransaction'' | ''TreasuryNote'' | ''Consignment'' → Se hace JOIN respectivo a CashReceipts, VoucherTransaction, TreasuryNote o Consignment para validar el estado del documento origen.; si @ReconciliationStatus = 2 → Se permiten extractos cuyo UploadBankStatements.Status no sea 2 (se ignora ese filtro), reflejando una conciliación ya cerrada/aprobada. else Se exige ubs.Status = 2 para incluir el extracto.; si braed.DocumentType IS NOT NULL → Se prefiere el DocumentType del detalle de conciliación; si es nulo, se usa ubsd.DocumentType.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.BankReconciliationAutomatic; Treasury.BankReconciliationAutomaticExtractDetail; Treasury.BankReconciliationAutomaticAssociation; Treasury.BankReconciliationAutomaticDetail; Treasury.CashReceipts; Treasury.VoucherTransaction; Treasury.TreasuryNote; Treasury.Consignment; Treasury.UploadBankStatements; Treasury.UploadBankStatementsDetail', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticExtractDetails';
-- GO
