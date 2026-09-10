
-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 27-10-2015
-- Description:	Procedimiento el cual se encarga de realizar el comprobante contable y la acumulacion
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_SaveJournalVoucherTmp] @JournalVoucherXml AS XML, 
                                                           @CodeUser AS          VARCHAR(20)
AS
    BEGIN
        DECLARE @IdJournalVoucher INT, @Consecutive BIGINT, @LegalBookId INT, @AccountingMovementId INT, @IdJournalVoucherType INT, @VoucherDate DATETIME, @Imported BIT, @Status TINYINT, @Detail VARCHAR(500), @EntityCode VARCHAR(20), @EntityId INT, @EntityName VARCHAR(250), @IsClosedYear BIT;
        --Tabla para almacenar los detalles del comprobante
        DECLARE @TableDetail TABLE
        (Id            INT, 
         IdMainAccount INT, 
         IdThirdParty  INT, 
         IdCostCenter  INT, 
         Nature        INT, 
         DebitValue    DECIMAL(18, 2), 
         CreditValue   DECIMAL(18, 2), 
         Detail        VARCHAR(500), 
         IdRetention   INT, 
         RetentionRate DECIMAL(5, 2), 
         BaseValue     DECIMAL(18, 0), 
         BillingValue  DECIMAL(18, 0), 
         IsDelete      BIT
        );

        --begin tran tVoucher
        BEGIN TRY
            SELECT @IdJournalVoucher = ISNULL(t.x.value('Id[1]', 'int'), 0), 
                   @Consecutive = t.x.value('Consecutive[1]', 'bigint'), 
                   @LegalBookId = t.x.value('LegalBookId[1]', 'int'), 
                   @AccountingMovementId = t.x.value('AccountingMovementId[1]', 'int'), 
                   @IdJournalVoucherType = t.x.value('IdJournalVoucher[1]', 'int'), 
                   @VoucherDate = CONVERT(DATETIME, t.x.value('VoucherDate[1]', 'datetime'), 103), 
                   @Imported = t.x.value('Imported[1]', 'bit'), 
                   @Status = t.x.value('Status[1]', 'tinyint'), 
                   @Detail = t.x.value('Detail[1]', 'varchar(500)'), 
                   @EntityCode = t.x.value('EntityCode[1]', 'varchar(20)'), 
                   @EntityId = t.x.value('EntityId[1]', 'int'), 
                   @EntityName = t.x.value('EntityName[1]', 'varchar(250)'), 
                   @IsClosedYear = t.x.value('IsClosedYear[1]', 'bit')
            FROM @JournalVoucherXml.nodes('/JournalVoucher') t(x);

            --- Se asigna la fecha de enero solo para el arranque
            IF [Common].[GETDATE]() < CAST('01-01-2016' AS DATE)
                BEGIN
                    SET @VoucherDate = CAST('01-01-2016' AS DATE);
            END;
            IF @Imported = 1
               AND @Status = 2
                BEGIN
                    INSERT INTO @TableDetail
                           SELECT Id, 
                                  IdMainAccount, 
                                  IdThirdParty, 
                                  IdCostCenter,
                                  CASE
                                      WHEN DebitValue > 0
                                      THEN 1
                                      ELSE 2
                                  END AS Nature, 
                                  DebitValue, 
                                  CreditValue, 
                                  Detail, 
                                  IdRetention, 
                                  RetentionRate, 
                                  BaseValue, 
                                  BillingValue, 
                                  0 AS IsDelete
                           FROM GeneralLedger.JournalVoucherDetails
                           WHERE IdAccounting = @IdJournalVoucher;
            END;
                ELSE
                BEGIN
                    INSERT INTO @TableDetail
                           SELECT ISNULL(t.x.value('Id[1]', 'int'), 0) AS Id, 
                                  t.x.value('IdMainAccount[1]', 'int') AS IdMainAccount,
                                  CASE
                                      WHEN t.x.value('IdThirdParty[1]', 'int') = 0
                                      THEN NULL
                                      ELSE t.x.value('IdThirdParty[1]', 'int')
                                  END AS IdThirdParty,
                                  CASE
                                      WHEN t.x.value('IdCostCenter[1]', 'int') = 0
                                      THEN NULL
                                      ELSE t.x.value('IdCostCenter[1]', 'int')
                                  END AS IdCostCenter,
                                  CASE
                                      WHEN t.x.value('DebitValue[1]', 'decimal(18, 2)') > 0
                                      THEN 1
                                      ELSE 2
                                  END AS Nature, 
                                  t.x.value('DebitValue[1]', 'decimal(18, 2)') AS DebitValue, 
                                  t.x.value('CreditValue[1]', 'decimal(18, 2)') AS CreditValue, 
                                  t.x.value('Detail[1]', 'varchar(500)') AS Detail, 
                                  t.x.value('IdRetention[1]', 'int') AS IdRetention, 
                                  t.x.value('RetentionRate[1]', 'decimal(5,2)') AS RetentionRate, 
                                  t.x.value('BaseValue[1]', 'decimal(18, 0)') AS BaseValue, 
                                  t.x.value('BillingValue[1]', 'decimal(18, 0)') AS BillingValue, 
                                  ISNULL(t.x.value('IsDelete[1]', 'bit'), 0) AS IsDelete
                           FROM @JournalVoucherXml.nodes('/JournalVoucher/JournalVoucherDetail') t(x);
            END;

            ---- Elimino los detalles del comprobante que se tienen que eliminar
            DELETE FROM GeneralLedger.JournalVoucherDetails
            WHERE Id IN
            (
                SELECT Id
                FROM @TableDetail
                WHERE IsDelete = 1
            );
            DELETE FROM @TableDetail
            WHERE IsDelete = 1; -- Elimino los registro de la tabla temporal
            --Validaciones para poder realizar el comprobante contable
            --Valido que no vengan movimientos en negativo
            IF
            (
                SELECT COUNT(*)
                FROM @TableDetail
                WHERE DebitValue < 0
                      OR CreditValue < 0
            ) > 0
                BEGIN
                    PRINT '1';
                    SELECT 999 AS CodeMessage, 
                           'El comprobante contable no se puede crear ya que existen movimientos con valores negativos' AS Message, 
                           0 IdJournalVoucher;
                    RETURN;
            END;

            --Valido que exista un libro contable principal
            IF @LegalBookId IS NULL
                BEGIN
                    IF
                    (
                        SELECT COUNT(*)
                        FROM GeneralLedger.LegalBook
                        WHERE OfficialBook = 1
                    ) = 0
                        BEGIN
                            PRINT '2';
                            SELECT 999 AS CodeMessage, 
                                   'El comprobante contable no se puede crear ya que no existe un libro oficial' AS Message, 
                                   0 IdJournalVoucher;
                            RETURN;
                    END;
                        ELSE
                        BEGIN
                            SELECT TOP 1 @LegalBookId = Id
                            FROM GeneralLedger.LegalBook
                            WHERE OfficialBook = 1;
                    END;
            END;

            --Valido que el mes este abrierto
            IF
            (
                SELECT COUNT(*)
                FROM [GeneralLedger].[ClosedMonth]
                WHERE [Year] = YEAR(@VoucherDate)
                      AND [Month] = MONTH(@VoucherDate)
                      AND [Status] = 1
            ) = 0
                BEGIN --- Si el mes no esta abierto
                    PRINT '3';
                    SELECT 999 AS CodeMessage, 
                           'El mes ' + CAST(MONTH(@VoucherDate) AS VARCHAR(2)) + ' no se encuentra abierto' AS Message, 
                           0 IdJournalVoucher;
                    RETURN;
            END;

            --validamos que no se pueda hacer un documento con fecha mayor al sistema asi el mes este abierto
            IF(@Status = 2)
                BEGIN
                    IF(CAST(@VoucherDate AS DATE) > CAST([Common].[GETDATE]() AS DATE))
                        BEGIN
                            SELECT 999 AS CodeMessage, 
                                   'No se pueden generar documentos con fechas superiores a la del sistema' AS Message, 
                                   0 IdJournalVoucher;
                            RETURN;
                    END;
            END;
            IF NOT(@Imported = 1
                   AND @Status = 1)
                BEGIN
                    --Valido que haya una partida doble
                    DECLARE @sumDebit DECIMAL(18, 2), @SumCredit DECIMAL(18, 2);
                    SELECT @sumDebit = SUM(DebitValue), 
                           @SumCredit = SUM(CreditValue)
                    FROM @TableDetail;
                    IF @sumDebit <> @SumCredit
                        BEGIN
                            PRINT '4';
                            SELECT 999 AS CodeMessage, 
                                   'El comprobante contable se encuentra desbalanceado, Debitos:' + CAST(@sumDebit AS VARCHAR(20)) + ' Creditos:' + CAST(@SumCredit AS VARCHAR(20)) AS Message, 
                                   0 IdJournalVoucher;
                            --rollback tran tVoucher
                            RETURN;
                    END;
            END;
            -- Valido que las cuentas que manejen tercero tengan tercero y los mismo con los centro de costo
            IF
            (
                SELECT COUNT(*)
                FROM @TableDetail td
                     INNER JOIN GeneralLedger.MainAccounts ma ON td.IdMainAccount = ma.Id
                WHERE ma.HandlesThirdParty = 1
                      AND td.IdThirdParty IS NULL
            ) > 0
                BEGIN
                    --rollback tran tVoucher
                    PRINT '5';
                    SELECT 999 AS CodeMessage, 
                           'Existen cuentas contable del detalle del comprobante que manejan tercero pero el tercero esta vacio' AS Message, 
                           0 IdJournalVoucher;
                    RETURN;
            END;
            IF
            (
                SELECT COUNT(*)
                FROM @TableDetail td
                     INNER JOIN GeneralLedger.MainAccounts ma ON td.IdMainAccount = ma.Id
                WHERE ma.HandlesCostCenter = 1
                      AND td.IdCostCenter IS NULL
            ) > 0
                BEGIN
                    --rollback tran tVoucher
                    PRINT '6';
                    SELECT 999 AS CodeMessage, 
                           'Existen cuentas contable del detalle del comprobante que manejan centro de costo pero el centro de costo esta vacio' AS Message, 
                           0 IdJournalVoucher;
                    RETURN;
            END;
            IF
            (
                SELECT COUNT(*)
                FROM @TableDetail
            ) <>
            (
                SELECT COUNT(*)
                FROM @TableDetail t
                     INNER JOIN GeneralLedger.MainAccounts m ON m.Id = t.IdMainAccount
                WHERE m.LegalBookId = @LegalBookId
            )
                BEGIN
                    PRINT '7';
                    SELECT 999 AS CodeMessage, 
                           'Existen cuentas contables del detalle del comprobante que no pertenecen al libro ' + ISNULL(
                    (
                        SELECT Name
                        FROM GeneralLedger.LegalBook
                        WHERE Id = @LegalBookId
                    ), '') AS Message, 
                           0 IdJournalVoucher;
                    RETURN;
            END;
            ---- Fin de las validaciones
            ---- verifico a que libros tiene que afectar
            IF @AccountingMovementId IS NULL
               OR @AccountingMovementId = 0
                BEGIN
                    INSERT INTO [GeneralLedger].[AccountingMovement]
                    ([LegalBookId], 
                     [JournalVoucherTypeId], 
                     [VoucherDate], 
                     [Detail], 
                     [EntityCode], 
                     [EntityId], 
                     [EntityName], 
                     [JournalVoucherXml], 
                     [CreationUser], 
                     [CreationDate]
                    )
                    VALUES
                    (@LegalBookId, 
                     @IdJournalVoucherType, 
                     @VoucherDate, 
                     @Detail, 
                     @EntityCode, 
                     @EntityId, 
                     @EntityName, 
                     @JournalVoucherXml, 
                     @CodeUser, 
                     [Common].[GETDATE]()
                    );
                    SET @AccountingMovementId = SCOPE_IDENTITY();
            END;
            DECLARE @TableBookMovement TABLE
            (LegalBookId      INT, 
             JournalVoucherId INT
            ); --Tabla para saber que libros tengo que afectar
            INSERT INTO @TableBookMovement
                   SELECT vb.LegalBookId, 
                          ISNULL(j.Id, 0)
                   FROM [GeneralLedger].[VieBot] vb
                        LEFT JOIN GeneralLedger.JournalVouchers j ON j.LegalBookId = vb.LegalBookId
                                                                     AND j.AccountingMovementId = @AccountingMovementId
                   WHERE Form = @EntityName
                         AND Allow = 1;
            IF @EntityName = 'JournalVouchers'
                BEGIN -- Si se esta creando desde comprobante contable entonces afecto el libro que se haya seleccionado
                    INSERT INTO @TableBookMovement
                    VALUES
                    (@LegalBookId, 
                     @IdJournalVoucher
                    );
            END;
            IF
            (
                SELECT COUNT(*)
                FROM @TableBookMovement
            ) > 0
                BEGIN
                    UPDATE [GeneralLedger].[AccountingMovement]
                      SET 
                          JournalVoucherXml = ''
                    WHERE Id = @AccountingMovementId;
            END;
                ELSE
                BEGIN
                    SELECT 0 AS CodeMessage, 
                           'No se genero ningun comprobante ya que asi esta configurado en VieBot' AS Message, 
                           0 AS IdJournalVoucher;
                    RETURN;
            END;
            DECLARE @MovementLegalBookId INT;
            DECLARE @MovementJournalVoucherId INT;
            DECLARE @TableDetailHomologation TABLE
            (Id            INT, 
             IdMainAccount INT, 
             IdThirdParty  INT, 
             IdCostCenter  INT, 
             Nature        INT, 
             DebitValue    DECIMAL(18, 2), 
             CreditValue   DECIMAL(18, 2), 
             Detail        VARCHAR(500), 
             IdRetention   INT, 
             RetentionRate DECIMAL(5, 2), 
             BaseValue     DECIMAL(18, 0), 
             BillingValue  DECIMAL(18, 0)
            );
            DECLARE @TableBalance TABLE
            (IdMainAccount INT, 
             IdThirdParty  INT, 
             IdCostCenter  INT, 
             DebitValue    DECIMAL(18, 2), 
             CreditValue   DECIMAL(18, 2)
            );
            DECLARE legalbook_cursor CURSOR
            FOR SELECT LegalBookId, 
                       JournalVoucherId
                FROM @TableBookMovement;
            OPEN legalbook_cursor;
            FETCH NEXT FROM legalbook_cursor INTO @MovementLegalBookId, @MovementJournalVoucherId;
            WHILE @@FETCH_STATUS = 0
                BEGIN
                    PRINT 'Armadus';
                    PRINT @MovementLegalBookId;
                    PRINT @MovementJournalVoucherId;
                    DELETE FROM @TableDetailHomologation;
                    IF @LegalBookId = @MovementLegalBookId
                        BEGIN
                            PRINT 'BUUU';
                            SELECT *
                            FROM @TableDetail;
                            INSERT INTO @TableDetailHomologation
                                   SELECT Id, 
                                          IdMainAccount, 
                                          IdThirdParty, 
                                          IdCostCenter, 
                                          Nature, 
                                          DebitValue, 
                                          CreditValue, 
                                          Detail, 
                                          IdRetention, 
                                          RetentionRate, 
                                          BaseValue, 
                                          BillingValue
                                   FROM @TableDetail;
                    END;
                        ELSE
                        BEGIN
                            IF
                            (
                                SELECT COUNT(*)
                                FROM @TableDetail
                            ) <>
                            (
                                SELECT COUNT(*)
                                FROM @TableDetail t
                                     INNER JOIN GeneralLedger.HomologationAccount h ON h.OfficialMainAccountId = t.IdMainAccount
                                     INNER JOIN GeneralLedger.MainAccounts m ON m.Id = h.MainAccountId
                                WHERE m.LegalBookId = @MovementLegalBookId
                            )
                                BEGIN
                                    PRINT '8';
                                    SELECT 999 AS CodeMessage, 
                                           'Las cuentas contables del detalle del comprobante no estan homologadas al libro ' +
                                    (
                                        SELECT ISNULL(Name, '')
                                        FROM GeneralLedger.LegalBook
                                        WHERE Id = @MovementLegalBookId
                                    ) AS Message, 
                                           0 IdJournalVoucher;
                                    RETURN;
                            END;
                            PRINT 'BUUU22';
                            INSERT INTO @TableDetailHomologation -- Inserto las homologaciones
                                   SELECT t.Id, 
                                          h.MainAccountId, 
                                          t.IdThirdParty, 
                                          t.IdCostCenter, 
                                          t.Nature, 
                                          t.DebitValue, 
                                          t.CreditValue, 
                                          t.Detail, 
                                          t.IdRetention, 
                                          t.RetentionRate, 
                                          t.BaseValue, 
                                          t.BillingValue
                                   FROM @TableDetail t
                                        INNER JOIN GeneralLedger.HomologationAccount h ON h.OfficialMainAccountId = t.IdMainAccount
                                        INNER JOIN GeneralLedger.MainAccounts m ON m.Id = h.MainAccountId
                                   WHERE m.LegalBookId = @MovementLegalBookId;
                    END;
                    DECLARE @ConfirmationUser AS VARCHAR(20), @ConfirmationDate AS DATETIME;
                    IF @Status = 2
                        BEGIN --Si estan confirmando el comprobante
                            SET @ConfirmationUser = @CodeUser;
                            SET @ConfirmationDate = [Common].[GETDATE]();
                    END;
                    IF @MovementJournalVoucherId = 0
                        BEGIN -- Si el comprobante contable es nuevo
                            SELECT @Consecutive = Consecutive
                            FROM GeneralLedger.JournalVoucherTypes
                            WHERE Id = @IdJournalVoucherType;
                            UPDATE GeneralLedger.JournalVoucherTypes
                              SET 
                                  Consecutive = @Consecutive + 1
                            WHERE Id = @IdJournalVoucherType;
                            INSERT INTO [GeneralLedger].[JournalVouchers]
                            ([Consecutive], 
                             [AccountingMovementId], 
                             [LegalBookId], 
                             [IdJournalVoucher], 
                             [VoucherDate], 
                             [Status], 
                             Imported, 
                             [Detail], 
                             [EntityCode], 
                             [EntityId], 
                             [EntityName], 
                             [IsClosedYear], 
                             [CreationUser], 
                             [CreationDate], 
                             [ConfirmationUser], 
                             [ConfirmationDate]
                            )
                            VALUES
                            (@Consecutive, 
                             @AccountingMovementId, 
                             @MovementLegalBookId, 
                             @IdJournalVoucherType, 
                             @VoucherDate, 
                             @Status, 
                             @Imported, 
                             @Detail, 
                             @EntityCode, 
                             @EntityId, 
                             @EntityName, 
                             @IsClosedYear, 
                             @CodeUser, 
                             [Common].[GETDATE](), 
                             @ConfirmationUser, 
                             @ConfirmationDate
                            );
                            SET @MovementJournalVoucherId = SCOPE_IDENTITY();
                            UPDATE @TableBookMovement
                              SET 
                                  JournalVoucherId = @MovementJournalVoucherId
                            WHERE LegalBookId = @MovementLegalBookId;
                    END;
                        ELSE
                        BEGIN -- Si se esta modificando
                            UPDATE [GeneralLedger].[JournalVouchers]
                              SET 
                                  VoucherDate = @VoucherDate, 
                                  Detail = @Detail, 
                                  ModificationUser = @CodeUser, 
                                  ModificationDate = [Common].[GETDATE](), 
                                  [Status] = @Status
                            WHERE Id = @MovementJournalVoucherId;
                    END;
                    IF @Status = 2
                       OR @Status = 4
                        BEGIN --Si se esta confirmando o reversando
                            DELETE FROM @TableBalance;
                            INSERT INTO @TableBalance
                                   SELECT IdMainAccount, 
                                          IdThirdParty, 
                                          IdCostCenter, 
                                          SUM(DebitValue), 
                                          SUM(CreditValue)
                                   FROM @TableDetailHomologation
                                   GROUP BY IdMainAccount, 
                                            IdThirdParty, 
                                            IdCostCenter;
                            IF @Status = 2
                                BEGIN --Confirmar Acumulo en los saldos de contailidad
                                    -- Actualizo los que ya esten
                                    UPDATE gb
                                      SET 
                                          gb.DebitValue = gb.DebitValue + tb.DebitValue, 
                                          gb.CreditValue = gb.CreditValue + tb.CreditValue
                                    FROM GeneralLedger.GeneralLedgerBalance gb
                                         INNER JOIN @TableBalance tb ON gb.IdMainAccount = tb.IdMainAccount
                                                                        AND ISNULL(gb.IdThirdParty, 0) = ISNULL(tb.IdThirdParty, 0)
                                                                        AND ISNULL(gb.IdCostCenter, 0) = ISNULL(tb.IdCostCenter, 0)
                                                                        AND gb.Year = YEAR(@VoucherDate)
                                                                        AND gb.Month = MONTH(@VoucherDate);
                                    -- Los que no existan los inserto
                                    IF
                                    (
                                        SELECT COUNT(*)
                                        FROM @TableBalance tb
                                             LEFT JOIN GeneralLedger.GeneralLedgerBalance gb ON gb.IdMainAccount = tb.IdMainAccount
                                                                                                AND ISNULL(gb.IdThirdParty, 0) = ISNULL(tb.IdThirdParty, 0)
                                                                                                AND ISNULL(gb.IdCostCenter, 0) = ISNULL(tb.IdCostCenter, 0)
                                                                                                AND gb.Year = YEAR(@VoucherDate)
                                                                                                AND gb.Month = MONTH(@VoucherDate)
                                        WHERE gb.Id IS NULL
                                    ) > 0
                                        BEGIN
                                            INSERT INTO [GeneralLedger].[GeneralLedgerBalance]
                                            ([Month], 
                                             [Year], 
                                             [IdMainAccount], 
                                             [IdThirdParty], 
                                             [IdCostCenter], 
                                             [DebitValue], 
                                             [CreditValue]
                                            )
                                                   SELECT MONTH(@VoucherDate), 
                                                          YEAR(@VoucherDate), 
                                                          tb.IdMainAccount, 
                                                          tb.IdThirdParty, 
                                                          tb.IdCostCenter, 
                                                          tb.DebitValue, 
                                                          tb.CreditValue
                                                   FROM @TableBalance tb
                                                        LEFT JOIN GeneralLedger.GeneralLedgerBalance gb ON gb.IdMainAccount = tb.IdMainAccount
                                                                                                           AND ISNULL(gb.IdThirdParty, 0) = ISNULL(tb.IdThirdParty, 0)
                                                                                                           AND ISNULL(gb.IdCostCenter, 0) = ISNULL(tb.IdCostCenter, 0)
                                                                                                           AND gb.Year = YEAR(@VoucherDate)
                                                                                                           AND gb.Month = MONTH(@VoucherDate)
                                                   WHERE gb.Id IS NULL;
                                    END;
                            END;
                                ELSE
                                IF @Status = 4
                                    BEGIN -- Desconfirmar
                                        -- Resto el valor debito y credito de los Saldos de contabilidad
                                        UPDATE gb
                                          SET 
                                              gb.DebitValue = gb.DebitValue - tb.DebitValue, 
                                              gb.CreditValue = gb.CreditValue - tb.CreditValue
                                        FROM GeneralLedger.GeneralLedgerBalance gb
                                             INNER JOIN @TableBalance tb ON gb.IdMainAccount = tb.IdMainAccount
                                                                            AND ISNULL(gb.IdThirdParty, 0) = ISNULL(tb.IdThirdParty, 0)
                                                                            AND ISNULL(gb.IdCostCenter, 0) = ISNULL(tb.IdCostCenter, 0)
                                                                            AND gb.Year = YEAR(@VoucherDate)
                                                                            AND gb.Month = MONTH(@VoucherDate);
                                END;
                    END;
                    IF @Status = 3
                        BEGIN -- Anular
                            UPDATE GeneralLedger.JournalVouchers
                              SET 
                                  AnnulmentUser = @CodeUser, 
                                  AnnulmentDate = [Common].[GETDATE]()
                            WHERE Id = @MovementJournalVoucherId;
                    END;
                        ELSE
                        IF @Status = 4
                            BEGIN
                                UPDATE GeneralLedger.JournalVouchers
                                  SET 
                                      [Status] = 1
                                WHERE Id = @MovementJournalVoucherId;
                        END;
                    --Inserto los nuevos si hay
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @TableDetailHomologation
                        WHERE Id = 0
                    ) > 0
                        BEGIN
                            INSERT INTO GeneralLedger.JournalVoucherDetails
                            (IdAccounting, 
                             IdMainAccount, 
                             IdThirdParty, 
                             IdCostCenter, 
                             DebitValue, 
                             CreditValue, 
                             Detail, 
                             IdRetention, 
                             RetentionRate, 
                             BaseValue, 
                             BillingValue
                            )
                                   SELECT @MovementJournalVoucherId, 
                                          IdMainAccount, 
                                          IdThirdParty, 
                                          IdCostCenter, 
                                          SUM(DebitValue), 
                                          SUM(CreditValue), 
                                          Detail, 
                                          IdRetention, 
                                          RetentionRate, 
                                          SUM(BaseValue), 
                                          SUM(BillingValue)
                                   FROM @TableDetailHomologation
                                   WHERE Id = 0
                                   GROUP BY Id, 
                                            IdMainAccount, 
                                            IdThirdParty, 
                                            IdCostCenter, 
                                            Detail, 
                                            IdRetention, 
                                            RetentionRate, 
                                            Nature;
                    END;
                    --Actualizo los registros si hay
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @TableDetailHomologation
                        WHERE Id > 0
                    ) > 0
                        BEGIN
                            UPDATE jvd
                              SET 
                                  jvd.IdMainAccount = td.IdMainAccount, 
                                  jvd.IdThirdParty = td.IdThirdParty, 
                                  jvd.IdCostCenter = td.IdCostCenter, 
                                  jvd.Detail = td.Detail, 
                                  jvd.DebitValue = td.DebitValue, 
                                  jvd.CreditValue = td.CreditValue, 
                                  jvd.IdRetention = td.IdRetention, 
                                  jvd.RetentionRate = td.RetentionRate, 
                                  jvd.BaseValue = td.BaseValue, 
                                  jvd.BillingValue = td.BillingValue
                            FROM GeneralLedger.JournalVoucherDetails jvd
                                 INNER JOIN @TableDetailHomologation td ON jvd.Id = td.Id;
                    END;
                    FETCH NEXT FROM legalbook_cursor INTO @MovementLegalBookId, @MovementJournalVoucherId;
                END;
            CLOSE legalbook_cursor;
            DEALLOCATE legalbook_cursor;
            --- Fin verificacion de libros a afectar
            --commit tran tVoucher
            SELECT 0 AS CodeMessage, 
                   'Se genero correctamente el comprobante contable' AS Message, 
            (
                SELECT JournalVoucherId
                FROM @TableBookMovement
                WHERE LegalBookId = @LegalBookId
            ) AS IdJournalVoucher;
        END TRY
        BEGIN CATCH
            --rollback tran tVoucher
            SELECT 999 AS CodeMessage, 
                   ERROR_MESSAGE() AS Message, 
                   0 IdJournalVoucher;
        END CATCH;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza un comprobante contable (journal voucher) en el libro mayor, procesando los detalles de movimientos contables enviados como XML. Recibe las líneas del comprobante con cuentas contables principales, terceros, centros de costo, valores débito y crédito, retenciones y bases de facturación; inserta, actualiza o elimina los registros correspondientes en la tabla de detalles del comprobante (JournalVoucherDetails). Verifica que el mes contable no esté cerrado (ClosedMonth) y que el libro legal (LegalBook) sea oficial antes de permitir la operación. Se usa en el proceso de registro y acumulación de asientos contables del módulo de contabilidad general (General Ledger).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucherTmp';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucherTmp';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Libro contable oficial; Partida doble (débito/crédito); Cierre de mes contable; Plan de cuentas (PUC); Tercero; Centro de costo; Homologación de cuentas entre libros; Saldos contables por período; Confirmación, anulación y reverso de comprobantes; Consecutivo por tipo de comprobante; Retención', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherTmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Common.GETDATE() < ''2016-01-01'' → Forzar la fecha del comprobante a 01-01-2016 (fecha de arranque del sistema); si Imported = 1 AND Status = 2 → Cargar el detalle desde GeneralLedger.JournalVoucherDetails existente filtrando por IdAccounting else Cargar el detalle desde el XML recibido; si LegalBookId IS NULL y no existe ningún LegalBook con OfficialBook=1 → Retornar error 999 ''no existe un libro oficial'' else Si LegalBookId es NULL toma el primer LegalBook con OfficialBook=1; si No existe ClosedMonth con Year/Month de VoucherDate y Status=1 → Retornar error 999 indicando que el mes no está abierto; si Status = 2 AND VoucherDate > GETDATE() → Retornar error 999 ''No se pueden generar documentos con fechas superiores a la del sistema''; si NOT (Imported=1 AND Status=1) y SUM(DebitValue) <> SUM(CreditValue) → Retornar error 999 indicando que el comprobante está desbalanceado (no cumple partida doble); si Existen detalles cuya MainAccount.HandlesThirdParty=1 con IdThirdParty NULL → Retornar error 999 ''cuentas que manejan tercero pero el tercero está vacío''; si Existen detalles cuya MainAccount.HandlesCostCenter=1 con IdCostCenter NULL → Retornar error 999 ''cuentas que manejan centro de costo pero el centro de costo está vacío''; si Hay cuentas en el detalle que no pertenecen al LegalBook indicado → Retornar error 999 indicando que existen cuentas que no pertenecen al libro; si AccountingMovementId IS NULL o = 0 → Insertar nuevo registro en AccountingMovement y obtener su Id mediante SCOPE_IDENTITY; si EntityName = ''JournalVouchers'' → Agregar manualmente al conjunto de libros a afectar el LegalBookId seleccionado con el IdJournalVoucher recibido; si No hay libros a afectar según VieBot (Form=EntityName y Allow=1) y EntityName <> ''JournalVouchers'' → Retornar mensaje 0 ''No se generó ningún comprobante ya que así está configurado en VieBot'' y salir; si Para un libro distinto al principal, las cuentas no están totalmente homologadas en HomologationAccount → Retornar error 999 ''Las cuentas contables del detalle del comprobante no están homologadas al libro X''; si LegalBook procesado = LegalBookId principal → Usar las cuentas tal cual del detalle else Reemplazar IdMainAccount por la cuenta homologada (HomologationAccount.MainAccountId) del libro destino; si JournalVoucherId del libro = 0 (comprobante nuevo) → Tomar Consecutive de JournalVoucherTypes, incrementarlo en 1 y crear nuevo registro en JournalVouchers else Actualizar VoucherDate, Detail, ModificationUser/Date y Status del JournalVoucher existente; si Status = 2 (confirmar) → Sumar DebitValue/CreditValue agrupados al saldo de GeneralLedgerBalance del Año/Mes; si no existe la combinación cuenta/tercero/centro insertar fila nueva; si Status = 4 (desconfirmar/reversar) → Restar DebitValue/CreditValue del saldo correspondiente en GeneralLedgerBalance y poner Status=1 en JournalVouchers; si Status = 3 (anular) → Actualizar AnnulmentUser y AnnulmentDate en JournalVouchers; si Status = 2 (confirmación) → Asignar ConfirmationUser=CodeUser y ConfirmationDate=GETDATE() al insertar en JournalVouchers; si Detalle con Id=0 en homologación → Insertar nuevas filas en JournalVoucherDetails agrupadas por cuenta/tercero/centro; si Detalle con Id>0 en homologación → Actualizar el JournalVoucherDetails existente con los nuevos valores; si DebitValue > 0 en línea de detalle → Asignar Nature=1 (débito) else Nature=2 (crédito)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherTmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherTmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherDetails; GeneralLedger.LegalBook; GeneralLedger.ClosedMonth; GeneralLedger.MainAccounts; GeneralLedger.VieBot; GeneralLedger.JournalVouchers; GeneralLedger.HomologationAccount; GeneralLedger.JournalVoucherTypes; GeneralLedger.GeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherTmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherTmp';
-- GO
