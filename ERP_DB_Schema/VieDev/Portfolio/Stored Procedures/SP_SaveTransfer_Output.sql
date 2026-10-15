
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/12/2015
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar o anular el cruce de anricipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SaveTransfer_Output] @PortfolioTransferXml AS XML, 
                                                     @CodeUser AS             VARCHAR(20), 
                                                     @CodeMessage          INT OUTPUT, 
                                                     @Message              VARCHAR(MAX) OUTPUT, 
                                                     @IdTransfer           INT OUTPUT, 
                                                     @CodeTransfer         VARCHAR(20) OUTPUT
AS
    BEGIN
        --select 999 as CodeMessage, 'Secuencia no encontrada'  as Message, 0 Id, '' as CodeTransfer
        --Se declaran las variables para obtener la cabecera
        DECLARE @Id INT, @Code VARCHAR(20), @DocumentDate DATETIME, @CustomerId INT, @ThirdPartyId INT, @PortfolioAdvanceId INT, @TransferType TINYINT, @MainAccountId INT, @CostCenterId INT, @Observations VARCHAR(300), @OperatingUnitId INT, @Status TINYINT;

        --Tabla temporal para obtener los detalles de portfolioTransfer
        DECLARE @PortfolioTransferDetail TABLE
        (Id                  INT, 
         PortfolioTrasferId  INT, 
         AccountReceivableId INT, 
         MainAccountId       INT, 
         CostCenterId        INT, 
         Value               DECIMAL(18, 0), 
         ChangeTracker       INT
        );

        --Tabla temporal para obtener los detalles de portfolioTransferOtherConcept
        DECLARE @PortfolioTransferOtherConcept TABLE
        (Id                     INT, 
         PortfolioTrasferId     INT, 
         PortfolioNoteConceptId INT, 
         MainAccountId          INT, 
         CostCenterId           INT, 
         Nature                 TINYINT, 
         Value                  DECIMAL(18, 0), 
         ChangeTracker          INT, 
         ThirdPartyId           INT
        );

        --Se declara la variable que retorna la validacion del traslado
        DECLARE @resultValidateTransfer TABLE
        (CodeMessage   VARCHAR(20), 
         MessageResult VARCHAR(MAX), 
         Id            INT
        );

        --begin transaction
        BEGIN TRY

            --Se obtiene la cabecera del xml(PortfolioTransfer)
            SELECT @Id = t.x.value('Id[1]', 'int'), 
                   @Code = t.x.value('Code[1]', 'varchar(20)'), 
                   @DocumentDate = CONVERT(DATETIME, t.x.value('DocumentDate[1]', 'varchar(20)'), 103), 
                   @CustomerId = t.x.value('CustomerId[1]', 'int'), 
                   @ThirdPartyId = t.x.value('ThirdPartyId[1]', 'int'), 
                   @PortfolioAdvanceId = t.x.value('PortfolioAdvanceId[1]', 'int'), 
                   @TransferType = t.x.value('TransferType[1]', 'tinyint'), 
                   @MainAccountId = t.x.value('MainAccountId[1]', 'int'), 
                   @CostCenterId = CASE
                                       WHEN t.x.value('CostCenterId[1]', 'int') = 0
                                       THEN NULL
                                       ELSE t.x.value('CostCenterId[1]', 'int')
                                   END, 
                   @Observations = t.x.value('Observations[1]', 'varchar(300)'), 
                   @OperatingUnitId = t.x.value('OperatingUnitId[1]', 'int'), 
                   @Status = t.x.value('Status[1]', 'tinyint')
            FROM @PortfolioTransferXml.nodes('/PortfolioTransfer') t(x);

            --Se obtiene los detalles del xml(PortfolioTransferDetail)
            INSERT INTO @PortfolioTransferDetail
                   SELECT t.x.value('Id[1]', 'int') AS Id, 
                          t.x.value('PortfolioTrasferId[1]', 'int') AS PortfolioTrasferId, 
                          t.x.value('AccountReceivableId[1]', 'int') AS AccountReceivableId, 
                          t.x.value('MainAccountId[1]', 'int') AS MainAccountId,
                          CASE
                              WHEN t.x.value('CostCenterId[1]', 'int') = 0
                              THEN NULL
                              ELSE t.x.value('CostCenterId[1]', 'int')
                          END AS CostCenterId, 
                          t.x.value('Value[1]', 'decimal(18, 0)') AS Value, 
                          t.x.value('ChangeTracker[1]', 'int') AS ChangeTracker
                   FROM @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferDetail') t(x);

            --Se obtiene los detalles del xml(PortfolioTrabsferOtherConcept)
            INSERT INTO @PortfolioTransferOtherConcept
                   SELECT t.x.value('Id[1]', 'int') AS Id, 
                          t.x.value('PortfolioTransferId[1]', 'int') AS PortfolioTransferId, 
                          t.x.value('PortfolioNoteConceptId[1]', 'int') AS PortfolioNoteConceptId, 
                          t.x.value('MainAccountId[1]', 'int') AS MainAccountId,
                          CASE
                              WHEN t.x.value('CostCenterId[1]', 'int') = 0
                              THEN NULL
                              ELSE t.x.value('CostCenterId[1]', 'int')
                          END AS CostCenterId, 
                          t.x.value('Nature[1]', 'tinyint') AS Nature, 
                          t.x.value('Value[1]', 'decimal(18, 0)') AS Value, 
                          t.x.value('ChangeTracker[1]', 'int') AS ChangeTracker,
                          CASE
                              WHEN t.x.value('ThirdPartyId[1]', 'int') = 0
                              THEN NULL
                              ELSE t.x.value('ThirdPartyId[1]', 'int')
                          END AS ThirdPartyId
                   FROM @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferOtherConcept') t(x);

            --Se crea el consecutivo siempre y cuando el código este vacío
            IF @Code = ''
               AND @Id = 0
                BEGIN
                    -- Consultamos la secuencia numerica del form
                    DECLARE @idSequenceDetail INT;
                    DECLARE @pattern VARCHAR(300);
                    DECLARE @NextS INT;
                    SELECT @pattern = cs.Pattern, 
                           @NextS = bsd.[Next], 
                           @idSequenceDetail = bsd.Id
                    FROM Portfolio.PortfolioSequenceDetail bsd
                         INNER JOIN Portfolio.PortfolioSequence bs ON bs.Id = bsd.IdSequensePortfolioC
                         INNER JOIN Common.Sequense cs ON cs.Id = bsd.IdSequense
                    WHERE bs.IdForm = '687';
                    IF(@idSequenceDetail IS NULL)
                        BEGIN
                            SELECT 999 AS CodeMessage, 
                                   'Secuencia no encontrada' AS Message, 
                                   0 Id, 
                                   '' AS CodeTransfer;
                            RETURN;
                    END;
                    SELECT @Code = dbo.GetSequence('', @pattern, @NextS);
                    IF @Code = '__ERROR_MAXVALUE__'
                        BEGIN
                            --select 999 as CodeMessage, 'La secuencia alcanzo su valor maximo'  as Message, 0 Id, '' as CodeTransfer
                            SET @CodeMessage = 999;
                            SET @Message = 'La secuencia alcanzo su valor maximo';
                            SET @IdTransfer = 0;
                            SET @CodeTransfer = '';
                            RETURN;
                    END;
                    UPDATE Portfolio.PortfolioSequenceDetail
                      SET 
                          [Next]+=1
                    WHERE Id = @idSequenceDetail;
            END;

            --Se pregunta por el estado y dependiendo del estado se realizan las acciones
            IF @Status = 1 --Estado Registrado
                BEGIN
                    --Si el objeto viene sin ser creado(ChangeTracker = Added)
                    IF @Id = 0
                        BEGIN
                            --Se crea un registro en la tabla de control(PortfolioControl)
                            INSERT INTO Portfolio.PortfolioControl
                            (DocumentNumber, 
                             DocumentType, 
                             DocumentUser, 
                             DocumentDate
                            )
                            VALUES
                            (@Code, 
                             2, 
                             @CodeUser, 
                             @DocumentDate
                            );
                    END;

                    --Se valida el traslado
                    INSERT INTO @resultValidateTransfer
                    EXEC [Portfolio].[SP_ValidateTransfers] 
                         @PortfolioTransferXml, 
                         0;
                    IF
                    (
                        SELECT CodeMessage
                        FROM @resultValidateTransfer
                    ) = '999'
                        BEGIN
                            DECLARE @error VARCHAR(MAX);
                            SELECT @error = MessageResult
                            FROM @resultValidateTransfer; 
                            --select 999 as CodeMessage, @error  as Message, 0 Id, '' as CodeTransfer
                            SET @CodeMessage = 999;
                            SET @Message = @error;
                            SET @IdTransfer = 0;
                            SET @CodeTransfer = '';
                            RETURN;
                    END;
            END;
                ELSE
                IF @Status = 3 --Estado anulado
                    BEGIN
                        --Se elimina el registro de la tabla de control(PortfolioControl)
                        DELETE FROM Portfolio.PortfolioControl
                        WHERE DocumentNumber = @Code
                              AND DocumentType = 2;
                END;

            --Se empieza el registro del objeto
            IF @Id = 0 --Si el registro es nuevo guardo
                BEGIN
                    --Inserto la cabecera
                    INSERT INTO [Portfolio].[PortfolioTransfer]
                    ([Code], 
                     [DocumentDate], 
                     [CustomerId], 
                     [ThirdPartyId], 
                     [PortfolioAdvanceId], 
                     [TransferType], 
                     [MainAccountId], 
                     [CostCenterId], 
                     [Observations], 
                     [OperatingUnitId], 
                     [Status], 
                     [CreationUser], 
                     [CreationDate]
                    )
                    VALUES
                    (@Code, 
                     @DocumentDate, 
                     @CustomerId, 
                     @ThirdPartyId, 
                     @PortfolioAdvanceId, 
                     @TransferType, 
                     @MainAccountId, 
                     @CostCenterId, 
                     @Observations, 
                     @OperatingUnitId, 
                     @Status, 
                     @CodeUser, 
                     [Common].[GETDATE]()
                    );

                    --Obtengo el id de la cabcera
                    SET @Id = SCOPE_IDENTITY();
            END;
                ELSE --Si se esta modificando
                BEGIN
                    --Actualizo la cabecera
                    DECLARE @AnnulmentUser AS VARCHAR(20)= CASE
                                                               WHEN @Status <> 3
                                                               THEN NULL
                                                               ELSE @CodeUser
                                                           END;
                    DECLARE @AnnulmentDate AS DATETIME= CASE
                                                            WHEN @Status <> 3
                                                            THEN NULL
                                                            ELSE [Common].[GETDATE]()
                                                        END;
                    UPDATE [Portfolio].[PortfolioTransfer]
                      SET 
                          Code = @Code, 
                          DocumentDate = @DocumentDate, 
                          CustomerId = @CustomerId, 
                          ThirdPartyId = @ThirdPartyId, 
                          PortfolioAdvanceId = @PortfolioAdvanceId, 
                          TransferType = @TransferType, 
                          MainAccountId = @MainAccountId, 
                          CostCenterId = @CostCenterId, 
                          Observations = @Observations, 
                          OperatingUnitId = @OperatingUnitId, 
                          [Status] = @Status, 
                          ModificationUser = @CodeUser, 
                          ModificationDate = [Common].[GETDATE](), 
                          AnnulmentUser = @AnnulmentUser, 
                          AnnulmentDate = @AnnulmentDate
                    WHERE Id = @Id;
            END;

            -------------------------------------------------------  PortfolioTransferDetail ------------------------------------------------------------------------------
            --Inserto los detalles(PortfolioTransferDetail) nuevos si hay
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferDetail
                WHERE Id = 0
            ) > 0
                BEGIN
                    INSERT INTO [Portfolio].[PortfolioTransferDetail]
                    ([PortfolioTrasferId], 
                     [AccountReceivableId], 
                     [MainAccountId], 
                     [CostCenterId], 
                     [Value]
                    )
                           SELECT @Id, 
                                  AccountReceivableId, 
                                  MainAccountId, 
                                  CostCenterId, 
                                  Value
                           FROM @PortfolioTransferDetail
                           WHERE Id = 0;
            END;
            --Actualizo los detalles(PortfolioTransferDetail) si hay
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferDetail
                WHERE Id > 0
            ) > 0
                BEGIN
                    UPDATE ptd
                      SET 
                          ptd.AccountReceivableId = ptdtemp.AccountReceivableId, 
                          ptd.MainAccountId = ptdtemp.MainAccountId, 
                          ptd.CostCenterId = ptdtemp.CostCenterId, 
                          ptd.Value = ptdtemp.Value
                    FROM [Portfolio].[PortfolioTransferDetail] ptd
                         INNER JOIN @PortfolioTransferDetail ptdtemp ON ptd.Id = ptdtemp.Id;
            END;
            --Elimino los detalles(PortfolioTransferDetail) si hay
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferDetail
                WHERE Id > 0
                      AND ChangeTracker = 1
            ) > 0
                BEGIN
                    DELETE [Portfolio].[PortfolioTransferDetail]
                    WHERE Id IN
                    (
                        SELECT Id
                        FROM @PortfolioTransferDetail
                        WHERE Id > 0
                              AND ChangeTracker = 1
                    );
            END;
            -------------------------------------------------------- Fin PortfolioTransferDetail -------------------------------------------------------------------------------
            --------------------------------------------------------- PortfolioTransferOtherConcept -----------------------------------------------------------------------------
            --Inserto los otros conceptos(PortfolioTransferOtherConcept) nuevos si hay
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferOtherConcept
                WHERE Id = 0
            ) > 0
                BEGIN
                    INSERT INTO [Portfolio].[PortfolioTransferOtherConcept]
                    ([PortfolioTransferId], 
                     [PortfolioNoteConceptId], 
                     [MainAccountId], 
                     [CostCenterId], 
                     [Nature], 
                     [Value], 
                     [ThirdPartyId]
                    )
                           SELECT @Id, 
                                  PortfolioNoteConceptId, 
                                  MainAccountId, 
                                  CostCenterId, 
                                  Nature, 
                                  Value, 
                                  ThirdPartyId
                           FROM @PortfolioTransferOtherConcept
                           WHERE Id = 0;
            END;
            --Actualizo los otros conceptos(PortfolioTransferOtherConcept) si hay
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferOtherConcept
                WHERE Id > 0
            ) > 0
                BEGIN
                    UPDATE ptoc
                      SET 
                          ptoc.PortfolioNoteConceptId = ptoctemp.PortfolioNoteConceptId, 
                          ptoc.MainAccountId = ptoctemp.MainAccountId, 
                          ptoc.CostCenterId = ptoctemp.CostCenterId, 
                          ptoc.Nature = ptoctemp.Nature, 
                          ptoc.Value = ptoctemp.Value, 
                          ptoc.ThirdPartyId = ptoctemp.ThirdPartyId
                    FROM [Portfolio].[PortfolioTransferOtherConcept] ptoc
                         INNER JOIN @PortfolioTransferOtherConcept ptoctemp ON ptoc.Id = ptoctemp.Id;
            END;
            --Elimino los detalles(PortfolioTransferOtherConcept) si hay
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferOtherConcept
                WHERE Id > 0
                      AND ChangeTracker = 1
            ) > 0
                BEGIN
                    DELETE [Portfolio].[PortfolioTransferOtherConcept]
                    WHERE Id IN
                    (
                        SELECT Id
                        FROM @PortfolioTransferOtherConcept
                        WHERE Id > 0
                              AND ChangeTracker = 1
                    );
            END;
            -------------------------------------------------------- Fin PortfolioTransferOtherConcept -------------------------------------------------------------------------------
            --commit transaction
            --select 0 as CodeMessage, 'Se guardó correctamente' as Message, @Id as Id, @Code as CodeTransfer}
            SET @CodeMessage = 0;
            SET @Message = 'Se guardó correctamente';
            SET @IdTransfer = @Id;
            SET @CodeTransfer = @Code;
        END TRY
        BEGIN CATCH
            --rollback transaction
            --select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' as CodeTransfer
            SET @CodeMessage = 999;
            SET @Message =
            (
                SELECT ERROR_MESSAGE()
            );
            SET @IdTransfer = 0;
            SET @CodeTransfer = '';
        END CATCH;
    END;