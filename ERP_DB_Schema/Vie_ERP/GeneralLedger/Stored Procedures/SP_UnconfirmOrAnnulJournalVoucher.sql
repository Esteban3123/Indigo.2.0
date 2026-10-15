CREATE PROCEDURE [GeneralLedger].[SP_UnconfirmOrAnnulJournalVoucher]
    @JournalVoucherXml AS XML,
    @CodeUser AS VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CodeMessage INT = 0,
            @Message VARCHAR(MAX) = '',
            @IdJournalVoucher INT = 0,
            @Status INT,
            @EntityName VARCHAR(250);

	CREATE TABLE #JournalVoucherTmp (
		Id						INT,
        AccountingMovementId	INT,
        Status					TINYINT,
        EntityName				VARCHAR(350),
        Consecutive				BIGINT,
		VoucherDate				DATETIME,
		LegalBookId				INT,
        IsClosedYear			TINYINT
	)

    -- Extraer datos del XML
    SELECT
        @Status = t.x.value('Status[1]', 'int'),
        @EntityName = ISNULL(t.x.value('EntityName[1]','varchar(250)'), 'JournalVouchers'),
		@IdJournalVoucher = ISNULL(t.x.value('Id[1]','int'),0)
    FROM @JournalVoucherXml.nodes('/JournalVoucher') t(x);

	
	INSERT INTO #JournalVoucherTmp(Id, AccountingMovementId, Status, EntityName, Consecutive, VoucherDate, LegalBookId, IsClosedYear)
		SELECT 
			Id,
			AccountingMovementId,
			Status,
			EntityName,
			Consecutive,
			VoucherDate,
			LegalBookId,
			IsClosedYear
		FROM GeneralLedger.JournalVouchers WITH (NOLOCK)
		WHERE Id = @IdJournalVoucher;

    -- Validación: Comprobante asociado existe
    IF NOT EXISTS (SELECT 1 FROM #JournalVoucherTmp)
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'No se encontró el comprobante contable.';
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @IdJournalVoucher AS IdJournalVoucher;
        RETURN;
    END

	-- Se insertan comprobantes contables homologos
	INSERT INTO #JournalVoucherTmp(Id, AccountingMovementId, Status, EntityName, Consecutive, VoucherDate, LegalBookId, IsClosedYear)
		SELECT 
			jv.Id,
			jv.AccountingMovementId,
			jv.Status,
			jv.EntityName,
			jv.Consecutive,
			jv.VoucherDate,
			jv.LegalBookId,
			jv.IsClosedYear
		FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
		JOIN #JournalVoucherTmp jvt ON jv.AccountingMovementId = jvt.AccountingMovementId 
		WHERE jv.Id <> jvt.Id;

    -- Validación: Homólogos en diferente estado
    IF EXISTS (
        SELECT 1 FROM #JournalVoucherTmp jvo
        JOIN #JournalVoucherTmp jvd ON jvo.AccountingMovementId = jvd.AccountingMovementId AND jvo.Id <> jvd.Id
        WHERE jvo.Status <> jvd.Status
    )
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'El comprobante contable tiene documentos homólogos en diferente estado';
        SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @IdJournalVoucher AS IdJournalVoucher;
        RETURN;
    END

    IF @Status = 4 -- DESCONFIRMAR
    BEGIN
        -- Solo se puede desconfirmar si está confirmado y no interfazado
        IF EXISTS (SELECT 1 FROM #JournalVoucherTmp WHERE Status <> 2)
        BEGIN
            SET @CodeMessage = 999;
            SET @Message = 'El comprobante contable no está confirmado y no se puede desconfirmar.';
            SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @IdJournalVoucher AS IdJournalVoucher;
            RETURN;
        END

        -- Validación: No interfazado
        IF EXISTS (SELECT 1 FROM #JournalVoucherTmp WHERE ISNULL(EntityName, '') NOT IN ('', 'JournalVouchers'))
        BEGIN
            SET @CodeMessage = 999;
            SET @Message = 'El comprobante contable no se puede desconfirmar ya que fue un documento interfazado.';
            SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @IdJournalVoucher AS IdJournalVoucher;
            RETURN;
        END

        -- DESCONFIRMAR: Cambia estado a 1, revierte impacto en balances
        UPDATE jv
        SET jv.Status = 1,
            jv.ModificationUser = @CodeUser,
            jv.ModificationDate = [Common].[GETDATE]()
		FROM GeneralLedger.JournalVouchers jv
		JOIN #JournalVoucherTmp jvt ON jv.Id = jvt.Id;

        -- Revertir balances de manera set-based
        ;WITH JVDetails AS (
            SELECT 
                jv.Id AS JournalVoucherId,
                jv.VoucherDate,
                CASE 
                    WHEN jv.IsClosedYear = 1 THEN 14
                    WHEN jv.IsClosedYear = 2 THEN 13
                    ELSE MONTH(jv.VoucherDate)
                END AS MonthMovement
            FROM #JournalVoucherTmp jv
        ),
        DetailsGrouped AS (
            SELECT 
                YEAR(jvd.VoucherDate) AS [Year],
                jvd.MonthMovement AS [Month],
                d.IdMainAccount,
                d.IdThirdParty,
                d.IdCostCenter,
                SUM(d.DebitValue) AS DebitValue,
                SUM(d.CreditValue) AS CreditValue
            FROM JVDetails jvd
            INNER JOIN GeneralLedger.JournalVoucherDetails d ON d.IdAccounting = jvd.JournalVoucherId
            GROUP BY YEAR(jvd.VoucherDate), jvd.MonthMovement, d.IdMainAccount, d.IdThirdParty, d.IdCostCenter
        )
        UPDATE glb
        SET glb.DebitValue = glb.DebitValue - dg.DebitValue,
            glb.CreditValue = glb.CreditValue - dg.CreditValue
        FROM GeneralLedger.GeneralLedgerBalance glb
        INNER JOIN DetailsGrouped dg
            ON glb.Year = dg.[Year]
            AND glb.Month = dg.[Month]
            AND glb.IdMainAccount = dg.IdMainAccount
            AND ISNULL(glb.IdThirdParty, 0) = ISNULL(dg.IdThirdParty, 0)
            AND ISNULL(glb.IdCostCenter, 0) = ISNULL(dg.IdCostCenter, 0);

        SET @CodeMessage = 0;

		SELECT @Message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Consecutivo ', jv.Consecutive, ' - Libro ', lb.Code, ' - ', lb.Name, ' - Periodo: ', YEAR(jv.VoucherDate))
				FROM #JournalVoucherTmp jv
				JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SET @Message = 'Los siguientes comprobantes contables fueron desconfirmados correctamente: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') + CHAR(13) + CHAR(10) + 'Impacto contable revertido.'
    END
    ELSE IF @Status = 3
    BEGIN

        UPDATE jv
        SET jv.Status = 3,
            jv.ModificationUser = @CodeUser,
            jv.ModificationDate = [Common].[GETDATE](),
            jv.AnnulmentUser = @CodeUser,
            jv.AnnulmentDate = [Common].[GETDATE]()
        FROM GeneralLedger.JournalVouchers jv
		JOIN #JournalVoucherTmp jvt ON jv.Id = jvt.Id;

        SET @CodeMessage = 0;

		SELECT @Message = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Consecutivo ', jv.Consecutive, ' - Libro ', lb.Code, ' - ', lb.Name, ' - Periodo: ', YEAR(jv.VoucherDate))
				FROM #JournalVoucherTmp jv
				JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SET @Message = 'Los siguientes comprobantes contables fueron anulados correctamente: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
    END
    ELSE
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'Estado no soportado. Solo se permite estado 3 (Anulación) o 4 (Desconfirmación).';
    END

    SELECT @CodeMessage AS CodeMessage, @Message AS [Message], @IdJournalVoucher AS IdJournalVoucher;
END
