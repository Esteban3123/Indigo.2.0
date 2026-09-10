-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 11/12/2015
-- Description:	Procedimiento que se encarga de confirmar el cruce de anticipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ConfirmPortfolioTransfer_Out] @PortfolioTransferXml AS    XML, 
                                                              @CodeUser AS                VARCHAR(20), 
                                                              @Reverse AS                 BIT, 
                                                              @IdJournalVoucherBilling AS INT, 
                                                              @CompanyType AS             INT, 
                                                              @CodeMessage             INT OUTPUT, 
                                                              @Message                 VARCHAR(MAX) OUTPUT, 
                                                              @IdTransfer              INT OUTPUT, 
                                                              @CodeTransfer            VARCHAR(20) OUTPUT, 
                                                              @Consecutive             VARCHAR(20) OUTPUT
AS
    BEGIN

        --Print 'transferConfirm'
        --Print Cast(@PortfolioTransferXml As Varchar(Max))
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
        --declare @resultValidateTransfer table (CodeMessage varchar(20),MessageResult varchar(max),Id int)
        --Se declara una tabla temporal para los detalles del comprobante
        DECLARE @JournalVourcherDetailTmp TABLE
        (Id            INTEGER, 
         IdAccounting  INTEGER, 
         IdMainAccount INTEGER, 
         IdThirdParty  INTEGER, 
         IdCostCenter  INTEGER, 
         DebitValue    DECIMAL(18, 2), 
         CreditValue   DECIMAL(18, 2), 
         Detail        VARCHAR(500), 
         IdRetention   INTEGER, 
         RetentionRate DECIMAL(5, 2), 
         BaseValue     DECIMAL(18, 0), 
         BillingValue  DECIMAL(18, 0)
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

            --Se valida el traslado
            --insert @resultValidateTransfer 

            DECLARE @CodeMessageValidate INT, @MessageValidate VARCHAR(MAX), @IdTransferValidate INT;
            EXEC [Portfolio].[SP_ValidateTransfers_Output] 
                 @PortfolioTransferXml, 
                 1, 
                 @CodeMessageValidate OUTPUT, 
                 @MessageValidate OUTPUT, 
                 @IdTransferValidate OUTPUT;
            IF @CodeMessageValidate = '999'
                BEGIN
                    DECLARE @error VARCHAR(MAX);
                    --select @error = MessageResult  from @resultValidateTransfer 
                    SET @error = @MessageValidate;

                    --select 999 as CodeMessage, @error  as Message, 0 Id, '' as CodeTransfer, '' as Consecutive
                    SET @CodeMessage = 999;
                    SET @Message = @error;
                    SET @IdTransfer = 0;
                    SET @CodeTransfer = '';
                    SET @Consecutive = '';
                    RETURN;
            END;

            --Se obtiene el total del valor de las facuras
            DECLARE @valueBills AS DECIMAL(18, 0)= 0;
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferDetail
                WHERE ChangeTracker = 0
            ) > 0
                BEGIN
                    SELECT @valueBills = SUM(Value)
                    FROM @PortfolioTransferDetail
                    WHERE ChangeTracker = 0;
            END;

            --Se obtiene el saldo del anticipo y el valor del traslado
            DECLARE @Balance AS DECIMAL(18, 0);
            DECLARE @TransferValue AS DECIMAL(18, 0);
            SELECT @Balance = Balance, 
                   @TransferValue = TransferValue
            FROM [Portfolio].PortfolioAdvance
            WHERE Id = @PortfolioAdvanceId;

            --Se obtiene el total del valor de los debitos y los creditos
            DECLARE @debit AS DECIMAL(18, 0)= 0;
            DECLARE @credit AS DECIMAL(18, 0)= 0;
            --Resultado de la suma de los debitos y los creditos
            DECLARE @debitCreditResult AS DECIMAL(18, 0)= 0;

            --Si hay otros conceptos
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferOtherConcept
                WHERE ChangeTracker = 0
            ) > 0
                BEGIN
                    SELECT @debit = ISNULL(SUM(Value), 0)
                    FROM @PortfolioTransferOtherConcept
                    WHERE Nature = 1
                          AND ChangeTracker = 0;
                    SELECT @credit = ISNULL(SUM(Value), 0)
                    FROM @PortfolioTransferOtherConcept
                    WHERE Nature = 2
                          AND ChangeTracker = 0;

                    --Se suma los debitos y los creditos
                    SET @debitCreditResult = @debit - @credit;
                    --print 'DEBITOS Y CREDITOS: ' + CAST(@debit AS VARCHAR(255)) + ' - ' + CAST(@credit AS VARCHAR(255)) + ' = ' + CAST(@debitCreditResult AS VARCHAR(255))
                    ---====================2016-01-15=================
                    ---Se comenta porque al sacar el valor absoluto
                    ---problemas al calcular saldo del anticipo
                    ---===============================================
                    --Se convierte el resultado a positivo
                    --if @debitCreditResult < 0 --Si el resultado de los debitos creditos da negativo
                    --Begin
                    --	set @debitCreditResult *= -1
                    --End
                    --================================================
                    --Cuando la resta de los debitos y creditos sea menor que el valor de las facturas afectamos el anticipo
                    IF @debitCreditResult < @valueBills
                        BEGIN
                            SET @valueBills = @valueBills - @debitCreditResult;
                            SET @Balance-=@valueBills;
                            SET @TransferValue+=@valueBills;
                            --Se actualiza el anticipo
                            UPDATE [Portfolio].PortfolioAdvance
                              SET 
                                  Balance = @Balance, 
                                  TransferValue = @TransferValue
                            WHERE Id = @PortfolioAdvanceId;
                    END;
            END;
                ELSE --Si no hay otros conceptos
                BEGIN
                    SET @Balance-=@valueBills;
                    SET @TransferValue+=@valueBills;
                    --Se actualiza el anticipo
                    UPDATE [Portfolio].PortfolioAdvance
                      SET 
                          Balance = @Balance, 
                          TransferValue = @TransferValue
                    WHERE Id = @PortfolioAdvanceId;
            END;

            --Xml que se genera para enviar al sp me devuelve los detalles del comprobante contable de provision o deterioro
            DECLARE @XmlProvisionAndDeterioration AS XML;

            --Tabla de resultados para la generacion de detalles de comprobante contable para provision y deterioro
            DECLARE @ResultProvisionAndDeterioration TABLE
            (Id            INT, 
             [Status]      INT, 
             [Message]     VARCHAR(MAX), 
             IdMainAccount INT, 
             IdThirdParty  INT, 
             IdCostCenter  INT, 
             DebitValue    DECIMAL(20, 4), 
             CreditValue   DECIMAL(20, 4)
            );

            --Obtengo los datos que se asignarán en el xml para enviar al sp
            SELECT @XmlProvisionAndDeterioration = CONVERT(XML,
            (
                SELECT Data.AccountReceivableId, 
                       @DocumentDate DocumentDate, 
                       Data.InvoiceNumber, 
                       Data.Value
                FROM
                (
                    SELECT ptd.AccountReceivableId, 
                           ar.InvoiceNumber, 
                           ptd.Value
                    FROM @PortfolioTransferDetail ptd
                         JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
                ) AS Data FOR XML AUTO, TYPE, ELEMENTS
            ));

            --Se ejecuta el sp que genera los detalles de comprobante para provision y deterioro
            INSERT INTO @ResultProvisionAndDeterioration
            EXEC Portfolio.SP_CreateDetailsJournalVoucher 
                 @XmlProvisionAndDeterioration;

            --Se valida que el sp no haya devuelto algun error
            IF
            (
                SELECT COUNT(*)
                FROM @ResultProvisionAndDeterioration
                WHERE [Status] = 0
                      OR [Status] = 2
            ) > 0
                BEGIN
                    DECLARE @errorsPD VARCHAR(MAX);
                    SELECT @errorsPD = COALESCE(@errorsPD + '', '') + [Message]
                    FROM @ResultProvisionAndDeterioration
                    WHERE [Status] = 0
                          OR [Status] = 2;
                    --select 999 as CodeMessage, @errorsPD as Message, 0 Id, '' as CodeTransfer, '' as Consecutive
                    SET @CodeMessage = 999;
                    SET @Message = @errorsPD;
                    SET @IdTransfer = 0;
                    SET @CodeTransfer = '';
                    SET @Consecutive = '';
                    RETURN;
            END;

            --Si no hay ningun error entonces se procede a crear los demas detalles de comprobante de provision y deterioro
            --siempre y cuando hayan registros en estado ok
            IF
            (
                SELECT COUNT(*)
                FROM @ResultProvisionAndDeterioration
                WHERE [Status] = 1
            ) > 0
                BEGIN
                    INSERT INTO @JournalVourcherDetailTmp
                           SELECT 0, 
                                  0, 
                                  IdMainAccount, 
                                  IdThirdParty, 
                                  IdCostCenter, 
                                  DebitValue, 
                                  CreditValue, 
                                  'Detalle generado con Provision/Deterioro', 
                                  NULL, 
                                  NULL, 
                                  NULL, 
                                  NULL
                           FROM @ResultProvisionAndDeterioration
                           WHERE [Status] = 1;
            END;

            --- disminuyo el valor que se esta cruzando en la cabecera de la CxC
            UPDATE Portfolio.AccountReceivable
              SET 
                  Balance-=
            (
                SELECT SUM(Value)
                FROM @PortfolioTransferDetail
                WHERE AccountReceivableId = Portfolio.AccountReceivable.Id
            )
            WHERE Id IN
            (
                SELECT AccountReceivableId
                FROM @PortfolioTransferDetail
            );

            --- disminuyo el valor que se esta cruzando en el detalle de cuentas contables
            UPDATE Portfolio.AccountReceivableAccounting
              SET 
                  Balance-=ptd.Value
            FROM Portfolio.AccountReceivableAccounting ara
                 INNER JOIN @PortfolioTransferDetail ptd ON ara.AccountReceivableId = ptd.AccountReceivableId
                                                            AND ptd.MainAccountId = ara.MainAccountId;
            DECLARE @Errors VARCHAR(MAX);
            --**************Pagos parciales de glosas************************
            IF(@CompanyType = 1)
                BEGIN --empresa privada

                    IF
                    (
                        SELECT COUNT(*)
                        FROM @PortfolioTransferDetail ptd
                             INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                             INNER JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                     AND ara.MainAccountId = ptd.MainAccountId
                                                                                     AND ara.MainAccountId = ar.AccountObjectionRemediedId
                             INNER JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                        WHERE gpg.State IN('1', '3', '4', '5', '6', '7', '15')
                             AND gpg.BalanceGlosa > 0
                    ) > 0
                        BEGIN
                            SELECT @Errors = STUFF(
                            (
                                SELECT CHAR(13) + CHAR(10) + 'La factura ' + ar.InvoiceNumber + ' no se puede pagar por ' + CASE gpg.State
                                                                                                                                WHEN 1
                                                                                                                                THEN 'Pendiente confirmar recepcion'
                                                                                                                                WHEN 3
                                                                                                                                THEN 'Pendiente envio de oficio'
                                                                                                                                WHEN 4
                                                                                                                                THEN 'Pendiente confirmar reiteracion'
                                                                                                                                WHEN 5
                                                                                                                                THEN 'Pendiente evaluacion reiteracion'
                                                                                                                                WHEN 6
                                                                                                                                THEN 'Pendiente conciliacion'
                                                                                                                                WHEN 7
                                                                                                                                THEN 'Pendiente confirmar factura conciliacion'
                                                                                                                                WHEN 15
                                                                                                                                THEN 'Factura cobro juridico'
                                                                                                                            END
                                FROM @PortfolioTransferDetail ptd
                                     JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                                     JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                       AND ara.MainAccountId = ptd.MainAccountId
                                                                                       AND ara.MainAccountId = ar.AccountObjectionRemediedId
                                     JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                                WHERE gpg.State IN('1', '3', '4', '5', '6', '7', '15')
                                     AND gpg.BalanceGlosa > 0 FOR XML PATH(N''), TYPE
                            ).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'');

                            --select 999 as CodeMessage, @Errors as Message, 0 Id, '' as CodeTransfer, '' as Consecutive 
                            SET @CodeMessage = 999;
                            SET @Message = @Errors;
                            SET @IdTransfer = 0;
                            SET @CodeTransfer = '';
                            SET @Consecutive = '';
                            RETURN;
                    END;

                    --validamos que existan registros para hacer los pagos parciales
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @PortfolioTransferDetail ptd
                             INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                             INNER JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                     AND ara.MainAccountId = ptd.MainAccountId
                                                                                     AND ara.MainAccountId = ar.AccountObjectionRemediedId
                             INNER JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                        WHERE gpg.State IN('2', '9', '11', '12', '14')
                             AND gpg.BalanceGlosa > 0
                    ) > 0
                        BEGIN

                            --se declara una tabla temporal para hacer pagos parciales
                            DECLARE @PartialPaymentCTmp TABLE
                            (Id           INT, 
                             CustomerId   INT, 
                             DocumentDate DATETIME, 
                             [State]      CHAR(1), 
                             Comments     VARCHAR(250)
                            );
                            DECLARE @PartialPaymentDTmp TABLE
                            (Id                       INT, 
                             PartialPaymentsCId       INT, 
                             PortfolioGlosaId         INT, 
                             InvoiceNumber            VARCHAR(50), 
                             InvoiceDate              DATETIME, 
                             RadicatedNumber          VARCHAR(50), 
                             RadicatedDate            DATETIME, 
                             PatientCode              VARCHAR(15), 
                             PatientName              VARCHAR(200), 
                             ContractCode             VARCHAR(15), 
                             ValuePendingConciliation DECIMAL(18, 0), 
                             ValuePayments            DECIMAL(18, 0)
                            );
                            --inserto la cabecera del pago parcial
                            INSERT INTO @PartialPaymentCTmp
                                   SELECT 0, 
                                          @CustomerId, 
                                          @DocumentDate, 
                                          2, 
                                          'Creado desde cruce de anticipos por CXC';
                            INSERT INTO @PartialPaymentDTmp
                                   SELECT 0, 
                                          0, 
                                          gpg.Id, 
                                          gpg.InvoiceNumber, 
                                          ar.AccountReceivableDate, 
                                          gpg.RadicatedNumber, 
                                          gpg.RadicatedDate, 
                                          gpg.PatientCode, 
                                          gpg.PatientName, 
                                          gpg.ContractCode, 
                                          gpg.BalanceGlosa, 
                                          ptd.Value
                                   FROM @PortfolioTransferDetail ptd
                                        INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                                        INNER JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                                AND ara.MainAccountId = ptd.MainAccountId
                                                                                                AND ara.MainAccountId = ar.AccountObjectionRemediedId
                                        INNER JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                                   WHERE gpg.State IN('2', '9', '11', '12', '14')
                                        AND gpg.BalanceGlosa > 0;
                            DECLARE @PartialPaymentCXML AS XML;
                            SELECT @PartialPaymentCXML = CONVERT(XML,
                            (
                                SELECT *
                                FROM @PartialPaymentCTmp PartialPaymentsC
                                     INNER JOIN @PartialPaymentDTmp PartialPaymentsD ON PartialPaymentsC.id = PartialPaymentsD.PartialPaymentsCId FOR XML AUTO, TYPE, ELEMENTS
                            ));
                            DECLARE @resultPartialPaymentC TABLE
                            (code          VARCHAR(20), 
                             MessageResult VARCHAR(MAX)
                            );
                            --ejecuto el sp de pagos parciales
                            INSERT INTO @resultPartialPaymentC
                            EXEC Glosas.SP_GeneratePartialPayments 
                                 @PartialPaymentCXML, 
                                 @CodeUser;
                            IF
                            (
                                SELECT code
                                FROM @resultPartialPaymentC
                            ) = '999'
                                BEGIN
                                    DECLARE @errorPP VARCHAR(MAX);
                                    SELECT @errorPP = MessageResult
                                    FROM @resultPartialPaymentC; 
                                    --select 999 as CodeMessage,@errorPP as Message, 0 Id, '' as CodeTransfer, '' as Consecutive
                                    SET @CodeMessage = 999;
                                    SET @Message = @errorPP;
                                    SET @IdTransfer = 0;
                                    SET @CodeTransfer = '';
                                    SET @Consecutive = '';
                                    RETURN;
                            END;
                    END;
            END;
                ELSE --Empresa Publica
                BEGIN

                    --print 'publico'
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @PortfolioTransferDetail ptd
                             INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                             INNER JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                     AND ara.MainAccountId = ptd.MainAccountId
                                                                                     AND ara.MainAccountId = ar.AccountRadicateId
                             INNER JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                        WHERE gpg.State IN('1', '3', '4', '5', '6', '7', '15')
                             AND gpg.BalanceGlosa > 0
                    ) > 0
                        BEGIN
                            SELECT @errors = STUFF(
                            (
                                SELECT CHAR(13) + CHAR(10) + 'La factura ' + ar.InvoiceNumber + ' no se puede pagar por ' + CASE gpg.State
                                                                                                                                WHEN 1
                                                                                                                                THEN 'Pendiente confirmar recepcion'
                                                                                                                                WHEN 3
                                                                                                                                THEN 'Pendiente envio de oficio'
                                                                                                                                WHEN 4
                                                                                                                                THEN 'Pendiente confirmar reiteracion'
                                                                                                                                WHEN 5
                                                                                                                                THEN 'Pendiente evaluacion reiteracion'
                                                                                                                                WHEN 6
                                                                                                                                THEN 'Pendiente conciliacion'
                                                                                                                                WHEN 7
                                                                                                                                THEN 'Pendiente confirmar factura conciliacion'
                                                                                                                                WHEN 15
                                                                                                                                THEN 'Factura cobro juridico'
                                                                                                                            END
                                FROM @PortfolioTransferDetail ptd
                                     JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                                     JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                       AND ara.MainAccountId = ptd.MainAccountId
                                                                                       AND ara.MainAccountId = ar.AccountRadicateId
                                     JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                                WHERE gpg.State IN('1', '3', '4', '5', '6', '7', '15')
                                     AND gpg.BalanceGlosa > 0 FOR XML PATH(N''), TYPE
                            ).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'');

                            --select 999 as CodeMessage, @Errors as Message, 0 Id, '' as CodeTransfer, '' as Consecutive 
                            SET @CodeMessage = 999;
                            SET @Message = @Errors;
                            SET @IdTransfer = 0;
                            SET @CodeTransfer = '';
                            SET @Consecutive = '';
                            RETURN;
                    END;
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @PortfolioTransferDetail ptd
                             INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                             INNER JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                     AND ara.MainAccountId = ptd.MainAccountId
                                                                                     AND ara.MainAccountId = ar.AccountRadicateId
                             INNER JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                        WHERE gpg.State IN('2', '9', '11', '12', '14')
                             AND gpg.BalanceGlosa > 0
                    ) > 0
                        BEGIN
                            --print @CustomerId
                            --se declara una tabla temporal para hacer pagos parciales
                            DECLARE @PartialPaymentCTmpPublic TABLE
                            (Id           INT, 
                             CustomerId   INT, 
                             DocumentDate DATETIME, 
                             [State]      CHAR(1), 
                             Comments     VARCHAR(250)
                            );
                            DECLARE @PartialPaymentDTmpPublic TABLE
                            (Id                       INT, 
                             PartialPaymentsCId       INT, 
                             PortfolioGlosaId         INT, 
                             InvoiceNumber            VARCHAR(50), 
                             InvoiceDate              DATETIME, 
                             RadicatedNumber          VARCHAR(50), 
                             RadicatedDate            DATETIME, 
                             PatientCode              VARCHAR(15), 
                             PatientName              VARCHAR(200), 
                             ContractCode             VARCHAR(15), 
                             ValuePendingConciliation DECIMAL(18, 0), 
                             ValuePayments            DECIMAL(18, 0)
                            );
                            --inserto la cabecera del pago parcial
                            INSERT INTO @PartialPaymentCTmpPublic
                                   SELECT 0, 
                                          @CustomerId, 
                                          @DocumentDate, 
                                          2, 
                                          'Creado desde cruce de anticipos por CXC';
                            DECLARE @PortfolioGlosaId INT;
                            DECLARE @InvoiceNumber VARCHAR(50);
                            DECLARE @InvoiceDate DATETIME;
                            DECLARE @RadicatedNumber VARCHAR(50);
                            DECLARE @RadicatedDate DATETIME;
                            DECLARE @PatientCode VARCHAR(15);
                            DECLARE @PatientName VARCHAR(200);
                            DECLARE @ContractCode VARCHAR(15);
                            DECLARE @ValuePendingConciliation DECIMAL(18, 0);
                            DECLARE @ValuePayments DECIMAL(18, 0);
                            DECLARE @BalanceAccounting DECIMAL(18, 0);
                            DECLARE @ValueGloasas DECIMAL(18, 0);
                            DECLARE glosas_cursor CURSOR
                            FOR SELECT gpg.Id, 
                                       gpg.InvoiceNumber, 
                                       ar.AccountReceivableDate, 
                                       gpg.RadicatedNumber, 
                                       gpg.RadicatedDate, 
                                       gpg.PatientCode, 
                                       gpg.PatientName, 
                                       ISNULL(gpg.ContractCode, '') AS ContractCode, 
                                       gpg.BalanceGlosa, 
                                       ptd.Value, 
                                       ara.Balance
                                FROM @PortfolioTransferDetail ptd
                                     INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId
                                     INNER JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId
                                                                                             AND ara.MainAccountId = ptd.MainAccountId
                                                                                             AND ara.MainAccountId = ar.AccountRadicateId
                                     INNER JOIN Glosas.GlosaPortfolioGlosada gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
                                WHERE gpg.State IN('2', '9', '11', '12', '14')
                                     AND gpg.BalanceGlosa > 0;
                            OPEN glosas_cursor;
                            FETCH NEXT FROM glosas_cursor INTO @PortfolioGlosaId, @InvoiceNumber, @InvoiceDate, @RadicatedNumber, @RadicatedDate, @PatientCode, @PatientName, @ContractCode, @ValuePendingConciliation, @ValuePayments, @BalanceAccounting;
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
                                    --si es mayor a 0 agrego un detalle a la tabla
                                    SET @ValueGloasas = @BalanceAccounting - @ValuePendingConciliation - @ValuePayments;
                                    IF(@ValueGloasas <= 0)
                                        BEGIN
                                            FETCH NEXT FROM glosas_cursor INTO @PortfolioGlosaId, @InvoiceNumber, @InvoiceDate, @RadicatedNumber, @RadicatedDate, @PatientCode, @PatientName, @ContractCode, @ValuePendingConciliation, @ValuePayments, @BalanceAccounting;
                                            CONTINUE;
                                    END;

                                    --inserto el detalle 
                                    INSERT INTO @PartialPaymentDTmpPublic
                                           SELECT 0, 
                                                  0, 
                                                  @PortfolioGlosaId, 
                                                  @InvoiceNumber, 
                                                  @InvoiceDate, 
                                                  @RadicatedNumber, 
                                                  @RadicatedDate, 
                                                  @PatientCode, 
                                                  @PatientName, 
                                                  @ContractCode, 
                                                  @ValuePendingConciliation, 
                                                  @ValueGloasas;
                                    FETCH NEXT FROM glosas_cursor INTO @PortfolioGlosaId, @InvoiceNumber, @InvoiceDate, @RadicatedNumber, @RadicatedDate, @PatientCode, @PatientName, @ContractCode, @ValuePendingConciliation, @ValuePayments, @BalanceAccounting;
                    END;
                            CLOSE glosas_cursor;
                            DEALLOCATE glosas_cursor;

                            --select * from @PartialPaymentDTmpPublic
                            --genero el xml para ejecutar el proceso de glosas

                            IF
                            (
                                SELECT COUNT(*)
                                FROM @PartialPaymentDTmpPublic
                            ) > 0
                                BEGIN
                                    DECLARE @PartialPaymentCXMLPublic AS XML;
                                    SELECT @PartialPaymentCXMLPublic = CONVERT(XML,
                                    (
                                        SELECT *
                                        FROM @PartialPaymentCTmpPublic PartialPaymentsC
                                             INNER JOIN @PartialPaymentDTmpPublic PartialPaymentsD ON PartialPaymentsC.id = PartialPaymentsD.PartialPaymentsCId FOR XML AUTO, TYPE, ELEMENTS
                                    ));
                                    DECLARE @resultPartialPaymentCPublic TABLE
                                    (code          VARCHAR(20), 
                                     MessageResult VARCHAR(MAX)
                                    );
                                    --ejecuto el sp de pagos parciales
                                    INSERT INTO @resultPartialPaymentCPublic
                                    EXEC Glosas.SP_GeneratePartialPayments 
                                         @PartialPaymentCXMLPublic, 
                                         @CodeUser;
                                    IF
                                    (
                                        SELECT code
                                        FROM @resultPartialPaymentCPublic
                                    ) = '999'
                                        BEGIN
                                            DECLARE @errorPPPublic VARCHAR(MAX);
                                            SELECT @errorPPPublic = MessageResult
                                            FROM @resultPartialPaymentCPublic;  
                                            --select 999 as CodeMessage,@errorPPPublic  as Message, 0 Id, '' as CodeTransfer, '' as Consecutive
                                            SET @CodeMessage = 999;
                                            SET @Message = @errorPPPublic;
                                            SET @IdTransfer = 0;
                                            SET @CodeTransfer = '';
                                            SET @Consecutive = '';
                                            RETURN;
                                    END;
                            END;
                    END;
            END;

            -- Verificos que facturas tienen una cuota y que facturas tienen mas de una cuota
            -- Afecto solo los que tengan una cuota
            UPDATE Portfolio.AccountReceivableShare
              SET 
                  Balance-=ptd.Value, 
                  TransferValue+=ptd.Value
            FROM Portfolio.AccountReceivableShare ars
                 INNER JOIN
            (
                SELECT AccountReceivableId, 
                       SUM(Value) AS Value
                FROM @PortfolioTransferDetail
                GROUP BY AccountReceivableId
            ) ptd ON ars.AccountReceivableId = ptd.AccountReceivableId
                 INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ars.AccountReceivableId
            WHERE ar.NumberShares = 1;
            -- Ahora afecto los que tengan mas de una cuota
            IF
            (
                SELECT COUNT(*)
                FROM Portfolio.AccountReceivableShare ars
                     INNER JOIN @PortfolioTransferDetail ptd ON ars.AccountReceivableId = ptd.AccountReceivableId
                     INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ars.AccountReceivableId
                WHERE ar.NumberShares > 1
            ) > 0
                BEGIN
                    DECLARE @AccountReceivableCursorId INT;
                    DECLARE @TransferDetailCursorId INT;
                    DECLARE accountReceivable_cursor CURSOR
                    FOR SELECT ars.AccountReceivableId, 
                               ptd.Id
                        FROM Portfolio.AccountReceivableShare ars
                             INNER JOIN @PortfolioTransferDetail ptd ON ars.AccountReceivableId = ptd.AccountReceivableId
                             INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ars.AccountReceivableId
                        WHERE ar.NumberShares > 1;
                    OPEN accountReceivable_cursor;
                    FETCH NEXT FROM accountReceivable_cursor INTO @AccountReceivableCursorId, @TransferDetailCursorId;
                    WHILE @@FETCH_STATUS = 0
                        BEGIN

                            --declare @idTest int = (select top(1) Id from @PortfolioTransferDetail where AccountReceivableId = @AccountReceivableCursorId)
                            --print cast(@AccountReceivableCursorId as varchar(max))

                            DECLARE @BalanceCursor NUMERIC(18, 0)=
                            (
                                SELECT SUM(Value)
                                FROM @PortfolioTransferDetail
                                WHERE Id = @TransferDetailCursorId
                            );
                            --declare @PortfolioTransferDetailCursorId int = (select Id from @PortfolioTransferDetail where AccountReceivableId = @AccountReceivableCursorId)
                            --print 'aqui'
                            -- Recorro las cuotas de la factura
                            DECLARE @ShareCursorId INT;
                            DECLARE @ShareBalance NUMERIC(18, 0);
                            DECLARE share_cursor CURSOR
                            FOR SELECT Id, 
                                       Balance
                                FROM Portfolio.AccountReceivableShare
                                WHERE AccountReceivableId = @AccountReceivableCursorId
                                      AND Balance > 0
                                ORDER BY Number;
                            OPEN share_cursor;
                            FETCH NEXT FROM share_cursor INTO @ShareCursorId, @ShareBalance;
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
                                    IF @BalanceCursor <= 0
                                        BEGIN
                                            BREAK;
                                    END;
                                    DECLARE @ValueAffectShare NUMERIC(18, 0)= 0;
                                    IF @BalanceCursor >= @ShareBalance
                                        BEGIN
                                            SET @ValueAffectShare = @ShareBalance;
                                    END;
                                        ELSE
                                        BEGIN
                                            SET @ValueAffectShare = @BalanceCursor;
                                    END;
                                    UPDATE Portfolio.AccountReceivableShare
                                      SET 
                                          TransferValue+=@ValueAffectShare, 
                                          Balance-=@ValueAffectShare
                                    WHERE Id = @ShareCursorId;
                                    INSERT INTO [Portfolio].[PortfolioTransferDetailIAccountShare]
                                    ([PortfolioTransferDetailId], 
                                     [AccountReceivableId], 
                                     [AccountReceivableShareId], 
                                     [Value]
                                    )
                                    VALUES
                                    (@TransferDetailCursorId, 
                                     @AccountReceivableCursorId, 
                                     @ShareCursorId, 
                                     @ValueAffectShare
                                    );
                                    FETCH NEXT FROM share_cursor INTO @ShareCursorId, @ShareBalance;
            END;
                            CLOSE share_cursor;
                            DEALLOCATE share_cursor;
                            FETCH NEXT FROM accountReceivable_cursor INTO @AccountReceivableCursorId, @TransferDetailCursorId;
            END;
                    CLOSE accountReceivable_cursor;
                    DEALLOCATE accountReceivable_cursor;
            END;

            --Se genera el comprobante contable
            --Se consulta si existe los parametros de cartera
            IF
            (
                SELECT COUNT(*)
                FROM Portfolio.SettingPortfolio
                WHERE OperatingUnitId = @OperatingUnitId
            ) = 0
                BEGIN
                    --select 999 as CodeMessage, 'No existe una configuración en Cartera para esta unidad operativa' as Message, 0 Id, '' as CodeTransfer, '' as Consecutive
                    SET @CodeMessage = 999;
                    SET @Message = 'No existe una configuración en Cartera para esta unidad operativa';
                    SET @IdTransfer = 0;
                    SET @CodeTransfer = '';
                    SET @Consecutive = '';
                    RETURN;
            END;

            --Obtengo el libro oficial
            DECLARE @LegalBookId INTEGER;
            SELECT @LegalBookId = id
            FROM GeneralLedger.LegalBook
            WHERE OfficialBook = 1;

            --Se declara una tabla con los datos para la cabecera del comprobante contable 
            DECLARE @JournalVourcherTmp TABLE
            (Id               INTEGER, 
             Consecutive      BIGINT, 
             LegalBookId      INTEGER, 
             IdJournalVoucher INTEGER, 
             VoucherDate      VARCHAR(30), 
             Imported         VARCHAR(5), 
             [Status]         TINYINT, 
             Detail           VARCHAR(MAX), 
             EntityCode       VARCHAR(20), 
             EntityId         INTEGER, 
             EntityName       VARCHAR(250), 
             IsClosedYear     TINYINT
            );

            --Se valida si se esta reversando o confirmando
            DECLARE @IdJournalVoucher AS INT;
            IF @Reverse = 1 --Si se esta reversando
                BEGIN
                    SET @IdJournalVoucher = @IdJournalVoucherBilling;
            END;
                ELSE --Si se esta confirmando
                BEGIN
                    SELECT @IdJournalVoucher = JournalVoucherTypeTranslationId
                    FROM Portfolio.SettingPortfolio
                    WHERE OperatingUnitId = @OperatingUnitId;
            END;
            DECLARE @DescriptionVoucher VARCHAR(MAX)= 'Generado desde Cruce Anticipo vs CxC - ' + @Code;
            SET @DescriptionVoucher+=' Anticipo: ' +
            (
                SELECT Code
                FROM [Portfolio].[PortfolioAdvance]
                WHERE Id = @PortfolioAdvanceId
            );
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferDetail
            ) > 0
                BEGIN
                    SET @DescriptionVoucher+=' Facturas: ' + STUFF(
                    (
                        SELECT N'   ' + ar.InvoiceNumber
                        FROM @PortfolioTransferDetail ptd
                             INNER JOIN Portfolio.AccountReceivable ar ON ar.Id = ptd.AccountReceivableId FOR XML PATH(N''), TYPE
                    ).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'');
            END;
            --Se inserta la cabecera del comprobante contable
            INSERT INTO @JournalVourcherTmp
            (Id, 
             Consecutive, 
             LegalBookId, 
             IdJournalVoucher, 
             VoucherDate, 
             Imported, 
             [Status], 
             Detail, 
             EntityCode, 
             EntityId, 
             EntityName, 
             IsClosedYear
            )
            VALUES
            (0, 
             0, 
             @LegalBookId, 
             @IdJournalVoucher, 
             @DocumentDate, 
             'False', 
             2, 
             @DescriptionVoucher, 
             @Code, 
             @Id, 
             'PortfolioTransfer', 
             0
            );

            --print 'ValueBills: ' + cast(@valueBills as varchar(max))
            --Se crea o no un registro al comprobante por el anticipo
            SET @valueBills = 0;
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferDetail
                WHERE ChangeTracker = 0
            ) > 0
                BEGIN
                    SELECT @valueBills = SUM(Value)
                    FROM @PortfolioTransferDetail
                    WHERE ChangeTracker = 0;
            END;
            DECLARE @SetJVD AS BIT= 0;
            --Si hay otros conceptos
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferOtherConcept
                WHERE ChangeTracker = 0
            ) > 0
                BEGIN
                    --Obtengo el valor de los debitos y los creditos si se agregaron otros conceptos
                    SELECT @debit = ISNULL(SUM(Value), 0)
                    FROM @PortfolioTransferOtherConcept
                    WHERE Nature = 1
                          AND ChangeTracker = 0;
                    SELECT @credit = ISNULL(SUM(Value), 0)
                    FROM @PortfolioTransferOtherConcept
                    WHERE Nature = 2
                          AND ChangeTracker = 0;
                    --Se suma los debitos y los creditos
                    SET @debitCreditResult = @debit - @credit;
                    --print '@debitCreditResult: ' + cast(@debitCreditResult as varchar(max))
                    ---====================2016-01-15=================
                    ---Se comenta porque al sacar el valor absoluto
                    ---problemas al calcular saldo del anticipo
                    ---===============================================
                    --Se convierte el resultado a positivo
                    --if @debitCreditResult < 0 --Si el resultado de los debitos creditos da negativo
                    --Begin
                    --	set @debitCreditResult *= -1
                    --End
                    --================================================
                    --Cuando la resta de los debitos y creditos sea menor que el valor de las facturas afectamos el anticipo
                    IF @debitCreditResult < @valueBills
                        BEGIN
                            SET @SetJVD = 1;
                    END;
            END;
                ELSE --Si no hay otros conceptos
                BEGIN
                    SET @SetJVD = 1;
            END;

            --Se crea el detalle del comprobante
            IF @SetJVD = 1
                BEGIN
                    --Genera un item por el anticipo para el comprobante contable
                    DECLARE @AdvanceMainAccountId AS INT, @AdvanceThirdPartyId AS INT, @AdvanceCostCenterId AS INT, @HandlesThirdParty AS BIT, @HandlesCostCenter AS BIT;
                    SELECT @AdvanceMainAccountId = pa.MainAccountId, 
                           @AdvanceThirdPartyId = pa.ThirdPartyId, 
                           @AdvanceCostCenterId = pa.CostCenterId, 
                           @HandlesThirdParty = ma.HandlesThirdParty, 
                           @HandlesCostCenter = ma.HandlesCostCenter
                    FROM Portfolio.PortfolioAdvance pa
                         INNER JOIN GeneralLedger.MainAccounts ma ON MainAccountId = ma.Id
                    WHERE pa.Id = @PortfolioAdvanceId;
                    --Se valida si el tercero es requerido
                    IF @HandlesThirdParty = 0
                        BEGIN
                            SET @AdvanceThirdPartyId = NULL;
                    END;
                    --Se valida si el centro costo es requerido
                    IF @HandlesCostCenter = 0
                        BEGIN
                            SET @AdvanceCostCenterId = NULL;
                    END;
                    DECLARE @ResultDebit AS DECIMAL(18, 0)= 0, @ResultCredit AS DECIMAL(18, 0)= 0;
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @PortfolioTransferOtherConcept
                        WHERE ChangeTracker = 0
                    ) > 0
                        BEGIN
                            SET @ResultDebit = (@valueBills - @debitCreditResult);
                            --PRINT '@ResultDebit: ' + CAST(@ResultDebit AS VARCHAR(255))
                    END;
                        ELSE
                        BEGIN
                            IF @Reverse = 1
                                BEGIN
                                    SELECT @ResultCredit = ISNULL(SUM(Value), 0)
                                    FROM @PortfolioTransferDetail
                                    WHERE ChangeTracker = 0;
                            END;
                                ELSE
                                BEGIN
                                    SELECT @ResultDebit = ISNULL(SUM(Value), 0)
                                    FROM @PortfolioTransferDetail
                                    WHERE ChangeTracker = 0;
                            END;
                    END;
                    --PRINT CAST(@ResultDebit AS VARCHAR(255)) + ' - ' + CAST(@ResultCredit AS VARCHAR(255))
                    --Se crea el registro en la tabla @JournalVourcherDetailTmp
                    INSERT INTO @JournalVourcherDetailTmp
                    (Id, 
                     IdAccounting, 
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
                    VALUES
                    (0, 
                     0, 
                     @AdvanceMainAccountId, 
                     @AdvanceThirdPartyId, 
                     @AdvanceCostCenterId, 
                     @ResultDebit, 
                     @ResultCredit, 
                     '', 
                     NULL, 
                     0, 
                     0, 
                     0
                    );
            END;

            --Si hay otros conceptos
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferOtherConcept
                WHERE ChangeTracker = 0
            ) > 0
                BEGIN
                    --Inserto los detalles de la tabla PortfolioTransferOtherConcept
                    INSERT INTO @JournalVourcherDetailTmp
                           SELECT 0, 
                                  0, 
                                  ptoc.MainAccountId,
                                  CASE ma.HandlesThirdParty
                                      WHEN 1
                                      THEN ptoc.ThirdPartyId
                                      ELSE NULL
                                  END AS ThirdPartyId,
                                  CASE ma.HandlesCostCenter
                                      WHEN 1
                                      THEN ptoc.CostCenterId
                                      ELSE NULL
                                  END AS CostCenterId,
                                  CASE ptoc.Nature
                                      WHEN 1
                                      THEN IIF(@Reverse = 0, ptoc.Value, 0)
                                      ELSE IIF(@Reverse = 1, ptoc.Value, 0)
                                  END AS DebitValue,
                                  CASE ptoc.Nature
                                      WHEN 1
                                      THEN IIF(@Reverse = 1, ptoc.Value, 0)
                                      ELSE IIF(@Reverse = 0, ptoc.Value, 0)
                                  END AS CreditValue, 
                                  '', 
                                  NULL, 
                                  0, 
                                  0, 
                                  0
                           FROM @PortfolioTransferOtherConcept ptoc
                                INNER JOIN GeneralLedger.MainAccounts ma ON ptoc.MainAccountId = ma.Id
                           WHERE ptoc.ChangeTracker = 0;
            END;

            --Si hay detalles
            IF
            (
                SELECT COUNT(*)
                FROM @PortfolioTransferDetail
                WHERE ChangeTracker = 0
            ) > 0
                BEGIN
                    --Inserto los detalles de la tabla PortfolioTransferDetail
                    INSERT INTO @JournalVourcherDetailTmp
                           SELECT 0, 
                                  0, 
                                  ptd.MainAccountId,
                                  CASE ma.HandlesThirdParty
                                      WHEN 1
                                      THEN ar.ThirdPartyId
                                      ELSE NULL
                                  END AS ThirdPartyId,
                                  CASE ma.HandlesCostCenter
                                      WHEN 1
                                      THEN ptd.CostCenterId
                                      ELSE NULL
                                  END AS CostCenterId,
                                  CASE @Reverse
                                      WHEN 1
                                      THEN ptd.Value
                                      ELSE 0
                                  END AS DebitValue,
                                  CASE @Reverse
                                      WHEN 0
                                      THEN ptd.Value
                                      ELSE 0
                                  END AS CreditValue, 
                                  '', 
                                  NULL, 
                                  0, 
                                  0, 
                                  0
                           FROM @PortfolioTransferDetail ptd
                                INNER JOIN GeneralLedger.MainAccounts ma ON ptd.MainAccountId = ma.Id
                                INNER JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
                           WHERE ptd.ChangeTracker = 0;
            END;

            --Se actualiza el estado en la cabecera de del traslado
            UPDATE Portfolio.PortfolioTransfer
              SET 
                  [Status] = 2, 
                  ConfirmationDate = [Common].[GETDATE](), 
                  ConfirmationUser = @CodeUser, 
                  ModificationDate = [Common].[GETDATE](), 
                  ModificationUser = @CodeUser
            WHERE Id = @Id;

            --Se elimina el registro de la tabla de control
            IF
            (
                SELECT COUNT(*)
                FROM Portfolio.PortfolioControl
                WHERE DocumentNumber = @Code
                      AND DocumentType = 2
            ) > 0
                BEGIN
                    DELETE FROM Portfolio.PortfolioControl
                    WHERE Id IN
                    (
                        SELECT Id
                        FROM Portfolio.PortfolioControl
                        WHERE DocumentNumber = @Code
                              AND DocumentType = 2
                    );
            END;

            --genero el XML para guardar el comprobante 		
            --select * from @JournalVourcherDetailTmp
            DECLARE @CodeMessageJv INT, @MessageJv VARCHAR(MAX), @IdJournalVoucherResultJv INT;

            --declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
            DECLARE @JournalVoucherXML AS XML;
            SELECT @JournalVoucherXML = CONVERT(XML,
            (
                SELECT *
                FROM @JournalVourcherTmp JournalVoucher
                     INNER JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.id = JournalVoucherDetail.IdAccounting FOR XML AUTO, TYPE, ELEMENTS
            ));
            --print cast(@JournalVoucherXML as varchar(max))
            --insert @resultJournalVoucher

            EXEC GeneralLedger.SP_SaveJournalVoucher_Output 
                 @JournalVoucherXML, 
                 @CodeUser, 
                 @CodeMessageJv OUTPUT, 
                 @MessageJv OUTPUT, 
                 @IdJournalVoucherResultJv OUTPUT;
            IF @CodeMessageJv = '999'
                BEGIN			
                    --PRINT CAST(@JournalVoucherXML AS VARCHAR(MAX))
                    DECLARE @errorJV VARCHAR(MAX);
                    --select @errorJV = MessageResult  from @resultJournalVoucher 
                    SET @errorJV = @MessageJv;

                    --select 999 as CodeMessage,@errorJV as Message, 0 Id, '' as CodeTransfer, '' as Consecutive
                    SET @CodeMessage = 999;
                    SET @Message = @errorJV;
                    SET @IdTransfer = 0;
                    SET @CodeTransfer = '';
                    SET @Consecutive = '';
                    RETURN;
            END;
            DECLARE @JournalVoucherId AS INT;
            --select @JournalVoucherId = IdJournalVoucher  from @resultJournalVoucher
            SET @JournalVoucherId = @IdJournalVoucherResultJv;
            DECLARE @ConsecutiveVoucher AS VARCHAR(MAX)= ISNULL(
            (
                SELECT CAST(Consecutive AS VARCHAR(30))
                FROM GeneralLedger.JournalVouchers
                WHERE id = @JournalVoucherId
            ), 0);

            --commit transaction
            --select 0 as CodeMessage, 'Se confirmó correctamente' as Message, @Id as Id, @Code as CodeTransfer, @Consecutive as Consecutive
            SET @CodeMessage = 0;
            SET @Message = 'Se confirmó correctamente';
            SET @IdTransfer = @Id;
            SET @CodeTransfer = @Code;
            SET @Consecutive = @ConsecutiveVoucher;
        END TRY
        BEGIN CATCH
            --rollback transaction
            --select 999 as CodeMessage, ERROR_MESSAGE() + ' linea: ' + cast(ERROR_LINE() as varchar(20) )  as Message, 0 Id, '' as CodeTransfer, '' as Consecutive
            SET @CodeMessage = 999;
            SET @Message =
            (
                SELECT ERROR_MESSAGE() + ' linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
            );
            SET @IdTransfer = 0;
            SET @CodeTransfer = '';
            SET @Consecutive = '';
        END CATCH;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma el cruce (aplicación) de un anticipo de cartera contra una o varias cuentas por cobrar (facturas), registrando el traslado de saldo entre el anticipo y los documentos de cobro correspondientes. Recibe los datos del traslado en formato XML con cabecera y detalles, valida la operación mediante SP_ValidateTransfers_Output, actualiza los saldos del anticipo (PortfolioAdvance) y las cuentas por cobrar (AccountReceivable), y genera el comprobante contable asociado en el libro mayor (JournalVouchers). También soporta la reversión del cruce y el manejo de otros conceptos del traslado, devolviendo como resultado el identificador, código y consecutivo del traslado confirmado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmPortfolioTransfer_Out';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmPortfolioTransfer_Out';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El traslado solo se confirma si SP_ValidateTransfers_Output no devuelve código 999; El saldo del anticipo (PortfolioAdvance.Balance) se reduce en el valor neto cruzado y TransferValue se incrementa en el mismo valor; El saldo de AccountReceivable y AccountReceivableAccounting se reducen exactamente en el valor cruzado por detalle; Para facturas con una sola cuota se afecta directamente AccountReceivableShare; para facturas con múltiples cuotas se aplican abonos en orden ascendente de Number hasta agotar el valor a cruzar; Cada abono de cuota múltiple genera un registro en PortfolioTransferDetailIAccountShare que mantiene la trazabilidad detalle-cuota-valor; No se permite cruzar facturas con glosas en estados 1,3,4,5,6,7,15 con saldo de glosa positivo; Las glosas en estados 2,9,11,12,14 con saldo > 0 se procesan como pagos parciales vía Glosas.SP_GeneratePartialPayments; El comprobante contable se genera con el libro contable marcado como OfficialBook = 1; El detalle por anticipo solo se inserta cuando hay valor neto a debitar/acreditar contra el anticipo (SetJVD = 1); Si MainAccounts.HandlesThirdParty/HandlesCostCenter = 0, esos campos se fuerzan a NULL en los detalles del comprobante; El estado del PortfolioTransfer se establece en 2 (confirmado) y se registran ConfirmationDate, ConfirmationUser, ModificationDate y ModificationUser; Cualquier excepción se captura y devuelve CodeMessage=999 con el mensaje y la línea del error', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer_Out';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de anticipo contra cuentas por cobrar; Anticipos de cartera (PortfolioAdvance); Cuentas por cobrar y sus cuotas; Glosas (estados pendientes vs susceptibles de pago parcial); Pagos parciales de glosas; Comprobante contable (Journal Voucher) de provisión/deterioro; Empresa pública vs privada (cuenta de objeción vs cuenta de radicación); Reversión y confirmación de traslado; Provisión y deterioro de cartera; Libro oficial contable', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer_Out';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SP_ValidateTransfers_Output devuelve CodeMessage = 999 → Se aborta el proceso retornando el error de validación sin aplicar cambios; si Existen registros en PortfolioTransferOtherConcept (ChangeTracker = 0) y (debit - credit) < valueBills → Se descuenta del saldo del anticipo solo (valueBills - (debit - credit)) y se actualiza Balance/TransferValue de PortfolioAdvance else Si no hay otros conceptos, se descuenta el total de valueBills al anticipo; si SP_CreateDetailsJournalVoucher retorna filas con Status = 0 o 2 → Se concatenan los mensajes de error y se retorna CodeMessage=999 abortando el flujo; si @CompanyType = 1 (empresa privada) → Se valida glosas usando AccountObjectionRemediedId como cuenta de objeción else Si CompanyType <> 1 (empresa pública), se valida glosas usando AccountRadicateId; si Existen glosas con State IN (1,3,4,5,6,7,15) y BalanceGlosa > 0 sobre las facturas a cruzar → Se construye mensaje ''La factura X no se puede pagar por <motivo>'' y se aborta con CodeMessage=999; si Existen glosas con State IN (2,9,11,12,14) y BalanceGlosa > 0 → Se arma XML de pago parcial y se invoca Glosas.SP_GeneratePartialPayments; si retorna code=999 se aborta; si AccountReceivable.NumberShares = 1 → Se actualiza directamente AccountReceivableShare con UPDATE masivo (Balance/TransferValue) else Si NumberShares > 1, se itera con cursor por cada cuota ordenada por Number aplicando el saldo a cruzar de menor a mayor cuota e insertando en PortfolioTransferDetailIAccountShare; si No existe configuración en Portfolio.SettingPortfolio para el OperatingUnitId → Se aborta con CodeMessage=999 y mensaje ''No existe una configuración en Cartera para esta unidad operativa''; si @Reverse = 1 → Se utiliza @IdJournalVoucherBilling como tipo de comprobante y los valores de detalles van a CreditValue (reversión) else Se toma JournalVoucherTypeTranslationId de SettingPortfolio y los valores van a DebitValue (confirmación); si MainAccounts.HandlesThirdParty = 0 / HandlesCostCenter = 0 → Se anula el ThirdPartyId / CostCenterId del detalle del comprobante para el anticipo y conceptos; si SP_SaveJournalVoucher_Output retorna CodeMessage = 999 → Se retorna error con el mensaje del comprobante sin completar el cruce; si Existe registro en PortfolioControl con DocumentNumber=@Code y DocumentType=2 → Se elimina dicho registro tras confirmar el traslado', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer_Out';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_ValidateTransfers_Output; Portfolio.SP_CreateDetailsJournalVoucher; Glosas.SP_GeneratePartialPayments; GeneralLedger.SP_SaveJournalVoucher_Output; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer_Out';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioAdvance; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; Portfolio.AccountReceivableShare; Portfolio.SettingPortfolio; Portfolio.PortfolioControl; Glosas.GlosaPortfolioGlosada; GeneralLedger.LegalBook; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer_Out';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer_Out';
-- GO
