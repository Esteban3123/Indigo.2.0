

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
        @BankReconciliationAutomaticId INT,
        @ReconciliationStatus TINYINT

SELECT @BankReconciliationAutomaticId = t.x.value('BankReconciliationAutomaticId[1]','int'),
       @EntityBankAccountId = t.x.value('EntityBankAccountId[1]','int'),
       @DocumentDate = t.x.value('DocumentDate[1]','date')
FROM @xmlCriterias.nodes('/Data') t(x)

-- Obtener el estado de la conciliación
SELECT @ReconciliationStatus = Status 
FROM Treasury.BankReconciliationAutomatic 
WHERE Id = @BankReconciliationAutomaticId

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
        AND cr.Status in (2, 4) 
        AND cr.DocumentDate >= @StartDate AND cr.DocumentDate < @EndDate
        AND ISNULL(br.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId
        AND NOT EXISTS (
            SELECT 1
            FROM Treasury.TreasuryNoteCashReceiptsDetail tncrd
            WHERE tncrd.CashReceiptsId = cr.Id
        )
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
        AND vt.Status in (2, 4)
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
        AND vt.Status in (2, 4)
        AND vt.DocumentDate >= @StartDate AND vt.DocumentDate < @EndDate
        AND ISNULL(br.Id, @BankReconciliationAutomaticId) = @BankReconciliationAutomaticId
		/****************************************  NOTAS *****************************************/

    INSERT INTO @Table_Result
    SELECT
        ISNULL(brd.Id, 0) Id,
        3 DocumentType,
        ISNULL(tn.Nature,
            CASE tn.NoteType
                WHEN 3 THEN CASE
                    WHEN EXISTS (SELECT 1 FROM Treasury.VoucherTransaction vt WHERE vt.Id = tn.VoucherTransactionId AND vt.IdEntityBankAccount = @EntityBankAccountId) THEN 1
                    ELSE 2
                END
                WHEN 4 THEN 2
                WHEN 5 THEN 2
            END) Nature,
        ISNULL(tn.[Value],
            CASE tn.NoteType
                WHEN 3 THEN CASE
                    WHEN EXISTS (SELECT 1 FROM Treasury.VoucherTransaction vt WHERE vt.Id = tn.VoucherTransactionId AND vt.IdEntityBankAccount = @EntityBankAccountId)
                        THEN (SELECT vt.Value FROM Treasury.VoucherTransaction vt WHERE vt.Id = tn.VoucherTransactionId)
                    ELSE (SELECT SUM(vtd.Value) FROM Treasury.VoucherTransactionDetails vtd WHERE vtd.IdVoucherTransaction = tn.VoucherTransactionId AND vtd.IdEntityBankAccount = @EntityBankAccountId)
                END
                WHEN 4 THEN (SELECT SUM(pm.Value) FROM Treasury.PaymentMethods pm WHERE pm.IdCashReceipt = tn.CashReceiptId)
                WHEN 5 THEN (SELECT c.Value FROM Treasury.Consignment c WHERE c.Id = tn.ConsignmentId)
            END) Value,
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
        ISNULL(CASE tn.NoteType WHEN 3 THEN 'Reversión Comprobante de Egreso: ' WHEN 4 THEN 'Reversión Recibo de Caja: ' WHEN 5 THEN 'Reversión Consignación: ' END, '') + ISNULL(tn.Description, '') Observations,
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
    WHERE
        (
            tn.EntityBankAccountId = @EntityBankAccountId
            OR (tn.NoteType = 3 AND EXISTS (SELECT 1 FROM Treasury.VoucherTransaction vt WHERE vt.Id = tn.VoucherTransactionId AND vt.IdEntityBankAccount = @EntityBankAccountId))
            OR (tn.NoteType = 3 AND EXISTS (SELECT 1 FROM Treasury.VoucherTransactionDetails vtd WHERE vtd.IdVoucherTransaction = tn.VoucherTransactionId AND vtd.IdEntityBankAccount = @EntityBankAccountId))
            OR (tn.NoteType = 4 AND EXISTS (SELECT 1 FROM Treasury.CashReceipts cr WHERE cr.Id = tn.CashReceiptId AND cr.IdBankAccount = @EntityBankAccountId))
            OR (tn.NoteType = 5 AND EXISTS (SELECT 1 FROM Treasury.Consignment c WHERE c.Id = tn.ConsignmentId AND c.EntityBankAccountId = @EntityBankAccountId))
        )
        AND tn.Status IN (2,4)
        AND tn.NoteType NOT IN (6, 7)
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
        AND c.Status in (2,4) 
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene el detalle de movimientos contables para el proceso de conciliación bancaria automática de un período y cuenta bancaria específicos. Consolida tres tipos de documentos de tesorería: recibos de caja (ingresos), comprobantes de egreso y notas de tesorería, cruzándolos con los registros de la conciliación automática para determinar cuáles ya fueron conciliados con el extracto bancario. Para cada movimiento retorna información del tercero (NIT y nombre), el valor, el número de documento de pago (tarjeta, consignación, cheque), el usuario que creó y confirmó la transacción, y el estado de conciliación, permitiendo al usuario visualizar qué documentos del libro contable ya tienen su correspondiente transacción bancaria asociada y cuáles aún están pendientes de conciliar.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único resultado los movimientos del mes (recibos de caja, comprobantes de egreso, notas de tesorería y consignaciones) asociados a una cuenta bancaria, marcando cuáles ya fueron conciliados en una conciliación bancaria automática.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener BankReconciliationAutomaticId, EntityBankAccountId y DocumentDate.; Debe existir un registro en Treasury.BankReconciliationAutomatic con el Id recibido para poder leer su Status.; El rango de fechas se calcula a partir del primer día del mes de DocumentDate hasta el primer día del mes siguiente.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de fechas siempre corresponde al mes calendario completo de DocumentDate (desde el día 1 hasta el día 1 del mes siguiente, exclusivo).; Nunca se incluyen documentos asociados a una conciliación con Status=3 (anulada) distinta a la actual.; Las notas de tesorería de tipos 3, 4, 5, 6 y 7 nunca se devuelven.; Los recibos de caja vinculados a una nota de tesorería (vía TreasuryNoteCashReceiptsDetail) nunca se devuelven (para evitar duplicar el movimiento, ya cubierto por la nota).; Los DocumentType en la salida siempre son: 1=Recibo de Caja, 2=Comprobante de Egreso, 3=Nota de Tesorería, 4=Consignación.; Si Reconciled o Id de detalle no existen, se retornan como 0/false (ISNULL).; Ante cualquier excepción el procedimiento no devuelve filas (catch silencioso con DELETE de la tabla resultado).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación bancaria automática; Recibo de caja; Comprobante de egreso; Nota de tesorería; Consignación bancaria; Método de pago (tarjeta de crédito, consignación, cheque); Tercero (NIT); Cuenta bancaria de la entidad; Estado de confirmación de documento', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Inserta los recibos de caja (DocumentType=1, Nature=1) de la cuenta bancaria cuyo CashReceipts.IdBankAccount coincide y cuya DocumentDate cae en el mes; solo si el recibo está confirmado (Status=2) o la conciliación ya está confirmada (@ReconciliationStatus=2); excluye los recibos asociados a una nota de tesorería (TreasuryNoteCashReceiptsDetail).; [INSERT] @Table_Result: Inserta los comprobantes de egreso (DocumentType=2, Nature=2) cuyo VoucherTransaction.IdEntityBankAccount coincide y DocumentDate está en el mes, solo si el comprobante está confirmado o la conciliación ya está confirmada.; [INSERT] @Table_Result: Inserta los detalles de comprobantes de egreso (DocumentType=2, Nature=1) cuyo VoucherTransactionDetails.IdEntityBankAccount coincide con la cuenta bancaria solicitada, en el rango de fechas y con comprobante confirmado o conciliación confirmada.; [INSERT] @Table_Result: Inserta las notas de tesorería (DocumentType=3) de la cuenta bancaria en el rango de fechas, EXCLUYENDO los NoteType 3, 4, 5, 6 y 7, y solo si la nota está confirmada o la conciliación ya lo está.; [INSERT] @Table_Result: Inserta las consignaciones (DocumentType=4, Nature=1) de la cuenta bancaria en el rango de fechas, solo si la consignación está confirmada (Status=2) o la conciliación ya está confirmada.; [RETURN_RESULT] @Table_Result: Devuelve todos los registros consolidados ordenados por DocumentDate ascendente.; [DELETE] @Table_Result: Si ocurre un error en cualquier INSERT, en el CATCH se borra @Table_Result y se imprime el mensaje y la línea del error (no se relanza la excepción).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ReconciliationStatus = 2 (conciliación confirmada) → Se ignora el estado del documento y se incluyen los movimientos sin importar su Status. else Solo se incluyen documentos con Status=2 (confirmados).; si Para recibos de caja: PaymentMethods.PaymentMethodTypes → Si es 3 expone CardNumber y la observación se prefija con ''Tarjeta de Crédito''; si es 4 expone DepositNumber y se prefija con ''Consignación''.; si Para comprobantes de egreso: VoucherTransaction.PaymentMethod → Si es 1 el DocumentNumber es CheckNumber; si es 2 es NoteNumber.; si OUTER APPLY sobre BankReconciliationAutomatic con Status<>3 y fecha en el mes → Se considera al documento dentro del scope solo si no tiene conciliación previa (br.Id IS NULL) o si pertenece a la conciliación actual (br.Id = @BankReconciliationAutomaticId).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.BankReconciliationAutomatic; Treasury.BankReconciliationAutomaticDetail; Treasury.CashReceipts; Treasury.PaymentMethods; Treasury.TreasuryNoteCashReceiptsDetail; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.TreasuryNote; Treasury.Consignment; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationAutomaticDetails';
-- GO
