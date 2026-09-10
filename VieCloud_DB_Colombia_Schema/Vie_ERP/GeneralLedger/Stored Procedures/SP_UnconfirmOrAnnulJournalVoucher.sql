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
			@LegalBookId INT,
			@VoucherDate DATETIME,
			@IsClosedYear TINYINT,
			@AccountingMovementId INT,
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
		@VoucherDate = t.x.value('VoucherDate[1]','datetime'),
		@IsClosedYear = ISNULL(t.x.value('IsClosedYear[1]' ,'tinyint'), 0),
		@AccountingMovementId = ISNULL(t.x.value('AccountingMovementId[1]','int'), 0),
		@LegalBookId = t.x.value('LegalBookId[1]' ,'int'),
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
	-- Valido que el año no esté cerrado para el libro contable.
      IF EXISTS (SELECT 1 FROM GeneralLedger.LegalBook lb WITH (NOLOCK) WHERE lb.Id = @LegalBookId AND YEAR(@VoucherDate) <= lb.LastYearClose)
      BEGIN
          SET @CodeMessage = 999; 
                 SET @Message = CONCAT('El año ', YEAR(@VoucherDate), ' se encuentra cerrado para el libro contable especificado.'); 
                 SET @AccountingMovementId = 0;
          select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
	  RETURN
      END

    -- Valido que el mes contable esté abierto (si no es un movimiento de cierre).
    IF @IsClosedYear = 0 AND NOT EXISTS (SELECT 1 FROM GeneralLedger.ClosedMonth WITH (NOLOCK) WHERE Year = YEAR(@VoucherDate) AND Month = MONTH(@VoucherDate) AND Status = 1)
    BEGIN
			SET @CodeMessage = 999; 
			SET @Message = CONCAT('El periodo contable ', YEAR(@VoucherDate), '-', FORMAT(MONTH(@VoucherDate), '00'), ' no se encuentra abierto.');
			SET @AccountingMovementId = 0;
				select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
	RETURN
    END
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento contable que permite desconfirmar o anular comprobantes de diario (journal vouchers) en el libro mayor general. Recibe un XML con el identificador y la acción solicitada (desconfirmar = estado 4, o anular), valida que el comprobante exista, que sus documentos homólogos estén en el mismo estado y que no provenga de una interfaz externa. Al desconfirmar, revierte el impacto en los saldos contables (débitos y créditos) en la tabla de balances del libro mayor, regresando el comprobante al estado borrador. Es utilizado por el módulo de contabilidad general para corregir o reversar comprobantes ya confirmados antes de su cierre contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Desconfirma (estado 1) o anula (estado 3) comprobantes contables y sus homólogos, revirtiendo el impacto en los saldos del libro mayor cuando corresponde a desconfirmación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El comprobante contable identificado en el XML debe existir en GeneralLedger.JournalVouchers.; Todos los comprobantes homólogos (mismo AccountingMovementId) deben encontrarse en el mismo estado.; El estado solicitado debe ser 3 (Anulación) o 4 (Desconfirmación); cualquier otro valor se rechaza.; Para desconfirmar, todos los comprobantes (principal y homólogos) deben estar en estado 2 (confirmado).; Para desconfirmar, EntityName debe ser vacío o ''JournalVouchers'' (no puede ser un documento interfazado).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La operación siempre se aplica al comprobante y a todos sus homólogos (mismo AccountingMovementId) de forma conjunta.; Solo la desconfirmación afecta GeneralLedgerBalance; la anulación nunca toca saldos.; Los comprobantes interfazados (EntityName distinto de ''JournalVouchers'' o vacío) nunca pueden ser desconfirmados.; Solo se aceptan transiciones a estados 3 o 4; cualquier otro Status retorna error 999.; La reversión de saldos resta exactamente la suma agrupada de débitos y créditos de los detalles del comprobante.; El periodo contable de afectación distingue meses especiales 13 y 14 para cierres según IsClosedYear.; Toda salida del procedimiento entrega un resultset con CodeMessage, Message e IdJournalVoucher.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Libro mayor; Anulación contable; Desconfirmación de comprobante; Comprobantes homólogos; Documento interfazado; Saldos contables por periodo; Cierre de año (mes 13 y 14); Cuenta principal; Tercero; Centro de costo; Libro legal contable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] GeneralLedger.JournalVouchers: Cuando el estado solicitado es 4 y el comprobante está confirmado y no interfazado, se cambia Status a 1 y se actualiza ModificationUser/ModificationDate del comprobante y todos sus homólogos.; [UPDATE] GeneralLedger.GeneralLedgerBalance: Al desconfirmar (Status=4), se restan los DebitValue y CreditValue agrupados de los detalles del comprobante a los saldos del libro mayor coincidentes en Año, Mes (14 si IsClosedYear=1, 13 si IsClosedYear=2, sino MONTH(VoucherDate)), IdMainAccount, IdThirdParty e IdCostCenter.; [UPDATE] GeneralLedger.JournalVouchers: Cuando el estado solicitado es 3, se establece Status=3 y se llenan AnnulmentUser/AnnulmentDate además de ModificationUser/ModificationDate, sobre el comprobante y sus homólogos.; [RETURN_RESULT] RESULT: Siempre retorna un resultset con CodeMessage (0 éxito, 999 error), Message descriptivo (incluye consecutivo, libro y periodo) e IdJournalVoucher.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El comprobante con el Id recibido no existe → Retorna CodeMessage=999 con mensaje ''No se encontró el comprobante contable'' y termina.; si Existen homólogos con Status distinto entre sí → Retorna CodeMessage=999 indicando que los homólogos están en diferente estado y termina.; si @Status = 4 (Desconfirmar) y algún comprobante no está en Status=2 → Retorna CodeMessage=999 ''no está confirmado y no se puede desconfirmar'' y termina. else Continúa con validación de interfaz.; si @Status = 4 y EntityName no es vacío ni ''JournalVouchers'' → Retorna CodeMessage=999 ''fue un documento interfazado'' y termina. else Procede a actualizar Status=1 y revertir balances.; si @Status = 3 → Anula los comprobantes (Status=3) registrando AnnulmentUser/AnnulmentDate, sin afectar balances.; si @Status distinto de 3 y 4 → Retorna CodeMessage=999 ''Estado no soportado''.; si IsClosedYear=1 al calcular MonthMovement → Usa mes 14 (cierre) para reversar el saldo. else Si IsClosedYear=2 usa mes 13; en otro caso usa MONTH(VoucherDate).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.GeneralLedgerBalance; GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_UnconfirmOrAnnulJournalVoucher';
-- GO
