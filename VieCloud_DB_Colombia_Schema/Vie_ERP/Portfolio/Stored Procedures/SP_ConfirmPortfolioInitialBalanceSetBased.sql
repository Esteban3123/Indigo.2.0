CREATE PROCEDURE Portfolio.SP_ConfirmPortfolioInitialBalanceSetBased
    @PortfolioInitialBalanceId int,
    @AuditUser varchar(20),
    @ElectronicDocumentContainer varchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @PortfolioInitialBalanceId <= 0
        THROW 51000, 'El identificador del saldo inicial no es válido.', 1;

    IF NULLIF(LTRIM(RTRIM(@AuditUser)), '') IS NULL
        THROW 51000, 'El usuario de auditoría es obligatorio.', 1;

    DECLARE @Now datetime = Common.GETDATE();
    DECLARE @OperatingUnitId int;
    DECLARE @PortfolioCreationDate datetime;
    DECLARE @StagedCount int;
    DECLARE @CandidateCount int;
    DECLARE @ConflictCount int;
    DECLARE @InvoicesCreated int = 0;
    DECLARE @AccountReceivablesCreated int = 0;
    DECLARE @AccountingExpected int = 0;
    DECLARE @AccountingCreated int = 0;
    DECLARE @SharesExpected int = 0;
    DECLARE @SharesCreated int = 0;
    DECLARE @ElectronicDocumentsExpected int = 0;
    DECLARE @ElectronicDocumentsCreated int = 0;
    DECLARE @ElectronicsPropertiesExpected int = 0;
    DECLARE @ElectronicsPropertiesCreated int = 0;
    DECLARE @ElectronicsRIPSExpected int = 0;
    DECLARE @ElectronicsRIPSCreated int = 0;
    DECLARE @InitialBalanceInvoicesExpected int = 0;
    DECLARE @InitialBalanceInvoicesCreated int = 0;
    DECLARE @HeaderUpdated int = 0;
    DECLARE @ApplicationLockResult int;
    DECLARE @ApplicationLockResource nvarchar(255) =
        N'Portfolio.SP_ConfirmPortfolioInitialBalanceSetBased:' + CONVERT(nvarchar(20), @PortfolioInitialBalanceId);

    CREATE TABLE #ExistingInvoices
    (
        StagingId int NOT NULL PRIMARY KEY,
        InvoiceId int NOT NULL
    );

    CREATE TABLE #ExistingAccountReceivables
    (
        StagingId int NOT NULL PRIMARY KEY,
        AccountReceivableId int NOT NULL,
        InvoiceId int NULL
    );

    CREATE TABLE #Conflicts
    (
        StagingId int NOT NULL PRIMARY KEY,
        InvoiceNumber varchar(20) NOT NULL,
        AccountReceivableType tinyint NOT NULL,
        ConflictReason varchar(40) NOT NULL,
        ExistingAccountReceivableId int NULL,
        ExistingInvoiceId int NULL
    );

    CREATE TABLE #Candidates
    (
        StagingId int NOT NULL PRIMARY KEY,
        InvoiceNumber varchar(20) NOT NULL UNIQUE,
        AccountReceivableType tinyint NOT NULL
    );

    CREATE TABLE #CreatedInvoices
    (
        InvoiceId int NOT NULL PRIMARY KEY,
        InvoiceNumber varchar(20) NOT NULL UNIQUE
    );

    CREATE TABLE #CreatedAccountReceivables
    (
        AccountReceivableId int NOT NULL PRIMARY KEY,
        InvoiceNumber varchar(20) NOT NULL,
        AccountReceivableType tinyint NOT NULL,
        InvoiceId int NOT NULL,
        UNIQUE (InvoiceNumber, AccountReceivableType)
    );

    CREATE TABLE #CreatedElectronicDocuments
    (
        ElectronicDocumentId int NOT NULL PRIMARY KEY,
        InvoiceId int NOT NULL UNIQUE,
        DocumentNumber varchar(20) NOT NULL UNIQUE
    );

    CREATE TABLE #CreatedElectronicsProperties
    (
        ElectronicsPropertiesId int NOT NULL PRIMARY KEY,
        InvoiceId int NOT NULL UNIQUE,
        EntityCode varchar(50) NULL
    );

    CREATE TABLE #CreatedElectronicsRIPS
    (
        ElectronicsRIPSId int NOT NULL PRIMARY KEY,
        ElectronicsPropertiesId int NOT NULL UNIQUE
    );

    CREATE TABLE #CreatedInitialBalanceInvoices
    (
        InitialBalanceInvoiceId int NOT NULL PRIMARY KEY,
        AccountReceivableId int NOT NULL UNIQUE,
        InvoiceId int NOT NULL UNIQUE,
        InvoiceNumber varchar(20) NOT NULL UNIQUE
    );

    BEGIN TRY
        BEGIN TRANSACTION;

        EXEC @ApplicationLockResult = sys.sp_getapplock
            @Resource = @ApplicationLockResource,
            @LockMode = 'Exclusive',
            @LockOwner = 'Transaction',
            @LockTimeout = 60000;

        IF @ApplicationLockResult < 0
            THROW 51000, 'No fue posible obtener el bloqueo exclusivo del saldo inicial.', 1;

        SELECT
            @OperatingUnitId = header.OperatingUnitId,
            @PortfolioCreationDate = header.CreationDate
        FROM Portfolio.PortfolioInitialBalance header WITH (UPDLOCK, HOLDLOCK)
        WHERE header.Id = @PortfolioInitialBalanceId
          AND header.Status = 1;

        IF @OperatingUnitId IS NULL
            THROW 51000, 'El saldo inicial no existe o no está en estado pendiente (Status=1).', 1;

        IF EXISTS
        (
            SELECT 1
            FROM Portfolio.PortfolioInitialBalanceAdvance advance
            WHERE advance.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
        )
            THROW 51000, 'Este procedimiento no admite saldos iniciales con anticipos.', 1;

        SELECT @StagedCount = COUNT(*)
        FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
        WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId;

        IF @StagedCount = 0
            THROW 51000, 'El saldo inicial no contiene facturas para confirmar.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
            WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
              AND NULLIF(LTRIM(RTRIM(staging.InvoiceNumber)), '') IS NULL
        )
            THROW 51000, 'El staging contiene números de factura vacíos.', 1;

        /* Billing.Invoice exige unicidad global por InvoiceNumber. */
        IF EXISTS
        (
            SELECT staging.InvoiceNumber
            FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
            WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
            GROUP BY staging.InvoiceNumber
            HAVING COUNT(*) > 1
        )
            THROW 51000, 'El staging contiene InvoiceNumber duplicado y no puede correlacionarse inequívocamente.', 1;

        /* El hijo final tiene UQ(AccountReceivableId, MainAccountId). */
        IF EXISTS
        (
            SELECT accounting.PortfolioInitialBalanceAccountReceivableId, accounting.MainAccountId
            FROM Portfolio.PortfolioInitialBalanceAccountReceivableAccounting accounting
            JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
              ON staging.Id = accounting.PortfolioInitialBalanceAccountReceivableId
            WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
            GROUP BY accounting.PortfolioInitialBalanceAccountReceivableId, accounting.MainAccountId
            HAVING COUNT(*) > 1
        )
            THROW 51000, 'El staging contable contiene MainAccountId duplicado para una factura.', 1;

        /*
            Los locks se toman sobre las claves únicas que actúan como backstop.
            HOLDLOCK queda limitado a Invoice/AR dentro de esta transacción, no
            cambia el isolation level global ni promueve una transacción distribuida.
        */
        /*
            Se bloquean primero Billing.Invoice y luego AccountReceivable, el
            mismo orden del DML posterior. Los JOIN son internos; los LEFT JOIN
            de clasificación se hacen después sobre tablas temporales y no
            dependen de locking hints en el lado nullable de un outer join.
        */
        INSERT INTO #ExistingInvoices (StagingId, InvoiceId)
        SELECT staging.Id, existingInvoice.Id
        FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
        JOIN Billing.Invoice existingInvoice WITH (UPDLOCK, HOLDLOCK)
          ON existingInvoice.InvoiceNumber = staging.InvoiceNumber
        WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId;

        INSERT INTO #ExistingAccountReceivables
        (
            StagingId,
            AccountReceivableId,
            InvoiceId
        )
        SELECT staging.Id, existingAR.Id, existingAR.InvoiceId
        FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
        JOIN Portfolio.AccountReceivable existingAR WITH (UPDLOCK, HOLDLOCK)
          ON existingAR.InvoiceNumber = staging.InvoiceNumber
         AND existingAR.AccountReceivableType = staging.AccountReceivableType
        WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId;

        INSERT INTO #Conflicts
        (
            StagingId,
            InvoiceNumber,
            AccountReceivableType,
            ConflictReason,
            ExistingAccountReceivableId,
            ExistingInvoiceId
        )
        SELECT
            staging.Id,
            staging.InvoiceNumber,
            staging.AccountReceivableType,
            CASE
                WHEN existingAR.AccountReceivableId IS NOT NULL THEN 'ACCOUNT_RECEIVABLE_EXISTS'
                ELSE 'BILLING_INVOICE_EXISTS'
            END,
            existingAR.AccountReceivableId,
            COALESCE(existingAR.InvoiceId, existingInvoice.InvoiceId)
        FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
        LEFT JOIN #ExistingInvoices existingInvoice
          ON existingInvoice.StagingId = staging.Id
        LEFT JOIN #ExistingAccountReceivables existingAR
          ON existingAR.StagingId = staging.Id
        WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
          AND
          (
              existingInvoice.StagingId IS NOT NULL
              OR existingAR.StagingId IS NOT NULL
          );

        /* Un AR conflictivo solo se omite si su Invoice vinculada está íntegra. */
        IF EXISTS
        (
            SELECT 1
            FROM #Conflicts conflict
            JOIN Portfolio.AccountReceivable existingAR
              ON existingAR.Id = conflict.ExistingAccountReceivableId
            LEFT JOIN Billing.Invoice linkedInvoice ON linkedInvoice.Id = existingAR.InvoiceId
            WHERE conflict.ExistingAccountReceivableId IS NOT NULL
              AND
              (
                  existingAR.InvoiceId IS NULL
                  OR linkedInvoice.Id IS NULL
                  OR linkedInvoice.InvoiceNumber <> existingAR.InvoiceNumber
              )
        )
            THROW 51000, 'Existe un conflicto de cartera sin Billing.Invoice válida; el encabezado permanece pendiente.', 1;

        INSERT INTO #Candidates (StagingId, InvoiceNumber, AccountReceivableType)
        SELECT staging.Id, staging.InvoiceNumber, staging.AccountReceivableType
        FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
        LEFT JOIN #Conflicts conflict ON conflict.StagingId = staging.Id
        WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
          AND conflict.StagingId IS NULL;

        SELECT @CandidateCount = COUNT(*) FROM #Candidates;
        SELECT @ConflictCount = COUNT(*) FROM #Conflicts;

        SELECT @ElectronicDocumentsExpected = COUNT(*)
        FROM #Candidates candidate
        JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
          ON staging.Id = candidate.StagingId
        WHERE NULLIF(LTRIM(RTRIM(staging.CUFE)), '') IS NOT NULL;

        SELECT @ElectronicsPropertiesExpected = COUNT(*)
        FROM #Candidates candidate
        JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
          ON staging.Id = candidate.StagingId
        WHERE NULLIF(LTRIM(RTRIM(staging.CUV)), '') IS NOT NULL;

        SET @ElectronicsRIPSExpected = @ElectronicsPropertiesExpected;
        SET @InitialBalanceInvoicesExpected = @ElectronicsPropertiesExpected;

        IF @StagedCount <> @CandidateCount + @ConflictCount
            THROW 51000, 'La clasificación entre candidatos y conflictos no cubre todo el staging.', 1;

        IF @CandidateCount = 0
            THROW 51000, 'Todas las facturas presentan conflicto; no se confirmó el encabezado.', 1;

        IF @ElectronicDocumentsExpected > 0
        AND NULLIF(LTRIM(RTRIM(@ElectronicDocumentContainer)), '') IS NULL
            THROW 51000, 'El contenedor de documentos electrónicos es obligatorio para facturas con CUFE.', 1;

        IF LEN(ISNULL(@ElectronicDocumentContainer, '')) > 50
            THROW 51000, 'El contenedor de documentos electrónicos supera 50 caracteres.', 1;

        /* FirstOrDefault de SettingPortfolio debe ser inequívoco. */
        IF EXISTS
        (
            SELECT 1 FROM #Candidates candidate
            WHERE candidate.AccountReceivableType IN (1, 7)
        )
        AND
        (
            SELECT COUNT(*)
            FROM Portfolio.SettingPortfolio setting
            WHERE setting.OperatingUnitId = @OperatingUnitId
        ) > 1
            THROW 51000, 'Existe más de un SettingPortfolio para la unidad operativa.', 1;

        DECLARE @SettingPortfolioId int =
        (
            SELECT MAX(setting.Id)
            FROM Portfolio.SettingPortfolio setting
            WHERE setting.OperatingUnitId = @OperatingUnitId
        );

        IF EXISTS
        (
            SELECT 1 FROM #Candidates candidate
            WHERE candidate.AccountReceivableType IN (1, 7)
        )
        AND @SettingPortfolioId IS NOT NULL
        AND
        (
            SELECT COUNT(*)
            FROM Portfolio.DeteriorationBasicBillingPortfolio deterioration
            WHERE deterioration.SettingPortfolioId = @SettingPortfolioId
        ) > 1
            THROW 51000, 'La configuración de deterioro básico es ambigua para la unidad operativa.', 1;

        INSERT INTO Billing.Invoice
        (
            OperatingUnitId,
            DocumentType,
            InvoiceNumber,
            ThirdPartyId,
            InvoiceDate,
            InvoiceExpirationDate,
            TotalInvoice,
            CapitationPatientValue,
            ThirdPartySalesValue,
            ThirdPartyDiscountValue,
            ResponsibleRecoveryFee,
            TotalPatientSalesPrice,
            PatientDiscount,
            PatientDiscountPercentage,
            TotalPatientWithDiscount,
            ValueVoucher,
            PatientPaidValue,
            ThirdPartyAccountReceivableValue,
            PatientAccountReceivableValue,
            CREETaxRetentionValue,
            CREETaxRetentionBaseValue,
            Observation,
            IsCutAccount,
            Status,
            InvoicedUser,
            InvoicedDate,
            OutputDate,
            InitialDate,
            CutType,
            CUFE,
            InvoiceValue,
            ValueTax,
            TotalValue,
            CurrencyId,
            TRMValue,
            TaxDevolutionValue,
            IsElectronicTicket
        )
        OUTPUT inserted.Id, inserted.InvoiceNumber
        INTO #CreatedInvoices (InvoiceId, InvoiceNumber)
        SELECT
            @OperatingUnitId,
            CONVERT(tinyint, 1),
            staging.InvoiceNumber,
            staging.ThirdPartyId,
            staging.AccountReceivableDate,
            staging.ExpiredDate,
            CONVERT(numeric(20, 2), staging.Value),
            CONVERT(decimal(18, 0), 0),
            CONVERT(decimal(20, 2), staging.Value),
            CONVERT(numeric(20, 2), 0),
            CONVERT(tinyint, 1),
            CONVERT(numeric(20, 2), 0),
            CONVERT(numeric(18, 0), 0),
            CONVERT(numeric(5, 2), 0),
            CONVERT(numeric(18, 0), 0),
            CONVERT(numeric(18, 0), 0),
            CONVERT(numeric(20, 2), 0),
            CONVERT(numeric(20, 2), staging.Value),
            CONVERT(numeric(18, 0), 0),
            CONVERT(numeric(18, 0), 0),
            CONVERT(numeric(18, 0), 0),
            'Factura por saldo inicial',
            CONVERT(bit, 0),
            CONVERT(tinyint, 1),
            '234',
            staging.AccountReceivableDate,
            staging.AccountReceivableDate,
            staging.AccountReceivableDate,
            CONVERT(tinyint, 1),
            CONVERT(varchar(250), NULLIF(LTRIM(RTRIM(staging.CUFE)), '')),
            CONVERT(decimal(20, 2), staging.Value),
            CONVERT(decimal(20, 2), 0),
            CONVERT(decimal(20, 2), staging.Value),
            1,
            CONVERT(numeric(20, 5), 1),
            CONVERT(numeric(20, 2), 0),
            CONVERT(bit, 0)
        FROM #Candidates candidate
        JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
          ON staging.Id = candidate.StagingId;

        SET @InvoicesCreated = @@ROWCOUNT;

        INSERT INTO Portfolio.AccountReceivable
        (
            Code,
            OperatingUnitId,
            AccountReceivableType,
            ThirdPartyId,
            CustomerId,
            InvoiceId,
            InvoiceNumber,
            AccountReceivableDate,
            Term,
            ExpiredDate,
            Observations,
            PortfolioStatus,
            OpeningBalance,
            PaymentAgreement,
            RegistrationAdjusted,
            MainAccountWithoutFilingId,
            NumberShares,
            Value,
            Balance,
            DeteriorationPayment,
            ProvisionPayment,
            Status,
            CostCenterId,
            InvoiceCategoryId,
            AccountWithoutRadicateId,
            AccountRadicateId,
            AccountObjectionRemediedId,
            AccountConciliationId,
            AccountLegalCollectionId,
            AccountDebtorOrder,
            AccountCreditorOrder,
            DebitAccountDeteriorationId,
            CreditAccountDeteriorationId,
            ReversalAccountDeteriorationId,
            PreviousPeriodReversalAccountDeteriorationId,
            AffectBudget,
            BudgetId,
            CreationUser,
            CreationDate,
            ConfirmationUser,
            ConfirmationDate,
            AccountHardCollectionId,
            DeteriorationBalance,
            ProvisionBalance,
            DeteriorationBalanceCurrentYear,
            DeteriorationBalancePreviousYear,
            CurrentDeteriorationYear,
            CurrencyId,
            TRMValue
        )
        OUTPUT
            inserted.Id,
            inserted.InvoiceNumber,
            inserted.AccountReceivableType,
            inserted.InvoiceId
        INTO #CreatedAccountReceivables
        (
            AccountReceivableId,
            InvoiceNumber,
            AccountReceivableType,
            InvoiceId
        )
        SELECT
            staging.InvoiceNumber,
            @OperatingUnitId,
            staging.AccountReceivableType,
            staging.ThirdPartyId,
            staging.CustomerId,
            invoice.InvoiceId,
            staging.InvoiceNumber,
            staging.AccountReceivableDate,
            staging.Term,
            staging.ExpiredDate,
            staging.Observations,
            staging.PortfolioStatus,
            CONVERT(bit, 1),
            CONVERT(bit, 0),
            CONVERT(bit, 0),
            staging.AccountWithoutRadicateId,
            staging.NumberShares,
            CONVERT(numeric(18, 2), staging.Value),
            CONVERT(numeric(18, 2), staging.Balance),
            CONVERT(decimal(18, 2), 0),
            CONVERT(decimal(18, 2), 0),
            CONVERT(tinyint, 2),
            staging.CostCenterId,
            staging.InvoiceCategoryId,
            staging.AccountWithoutRadicateId,
            staging.AccountRadicateId,
            staging.AccountObjectionRemediedId,
            staging.AccountConciliationId,
            staging.AccountLegalCollectionId,
            staging.AccountDebtorOrder,
            staging.AccountCreditorOrder,
            CASE
                WHEN staging.AccountReceivableType IN (1, 7) THEN basicDeterioration.DebitDeteriorationAccount
                WHEN staging.AccountReceivableType = 2 THEN contractStructure.DebitAccountDeteriorationId
            END,
            CASE
                WHEN staging.AccountReceivableType IN (1, 7) THEN basicDeterioration.CreditDeteriorationAccount
                WHEN staging.AccountReceivableType = 2 THEN contractStructure.CreditAccountDeteriorationId
            END,
            CASE
                WHEN staging.AccountReceivableType IN (1, 7) THEN basicDeterioration.ReversalDeteriorationAccount
                WHEN staging.AccountReceivableType = 2 THEN contractStructure.ReversalAccountDeteriorationId
            END,
            CASE
                WHEN staging.AccountReceivableType IN (1, 7) THEN basicDeterioration.PreviousPeriodReversalAccount
                WHEN staging.AccountReceivableType = 2 THEN contractStructure.PreviousPeriodReversalAccountDeteriorationId
            END,
            staging.AffectBudget,
            staging.BudgetId,
            @AuditUser,
            @Now,
            @AuditUser,
            @Now,
            CASE
                WHEN staging.AccountReceivableType = 2
                 AND staging.AccountHardCollectionId IS NULL
                    THEN contractStructure.AccountHardCollectionId
                ELSE staging.AccountHardCollectionId
            END,
            staging.DeteriorationBalance,
            CONVERT(decimal(18, 2), 0),
            staging.DeteriorationBalanceCurrentYear,
            staging.DeteriorationBalancePreviousYear,
            staging.CurrentDeteriorationYear,
            NULL,
            CONVERT(numeric(20, 5), 0)
        FROM #Candidates candidate
        JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
          ON staging.Id = candidate.StagingId
        JOIN #CreatedInvoices invoice
          ON invoice.InvoiceNumber = candidate.InvoiceNumber
        LEFT JOIN Portfolio.DeteriorationBasicBillingPortfolio basicDeterioration
          ON basicDeterioration.SettingPortfolioId = @SettingPortfolioId
         AND staging.AccountReceivableType IN (1, 7)
        OUTER APPLY
        (
            /*
                GetActiveList ordena por Code y el flujo legacy conserva First()
                por cada par de cuentas. Id hace determinístico un empate de Code.
            */
            SELECT TOP (1)
                structure.DebitAccountDeteriorationId,
                structure.CreditAccountDeteriorationId,
                structure.ReversalAccountDeteriorationId,
                structure.PreviousPeriodReversalAccountDeteriorationId,
                structure.AccountHardCollectionId
            FROM Contract.ContractAccountingStructure structure
            WHERE structure.Status = 1
              AND staging.AccountReceivableType = 2
              AND staging.AccountWithoutRadicateId IS NOT NULL
              AND staging.AccountRadicateId IS NOT NULL
              AND structure.AccountWithoutRadicateId = staging.AccountWithoutRadicateId
              AND structure.AccountRadicateId = staging.AccountRadicateId
            ORDER BY structure.Code, structure.Id
        ) contractStructure;

        SET @AccountReceivablesCreated = @@ROWCOUNT;

        SELECT @AccountingExpected = COUNT(*)
        FROM Portfolio.PortfolioInitialBalanceAccountReceivableAccounting accounting
        JOIN #Candidates candidate
          ON candidate.StagingId = accounting.PortfolioInitialBalanceAccountReceivableId;

        INSERT INTO Portfolio.AccountReceivableAccounting
        (
            AccountReceivableId,
            MainAccountId,
            ThirdPartyId,
            CostCenterId,
            Value,
            Balance
        )
        SELECT
            createdAR.AccountReceivableId,
            accounting.MainAccountId,
            accounting.ThirdPartyId,
            accounting.CostCenterId,
            accounting.Value,
            accounting.Value
        FROM Portfolio.PortfolioInitialBalanceAccountReceivableAccounting accounting
        JOIN #Candidates candidate
          ON candidate.StagingId = accounting.PortfolioInitialBalanceAccountReceivableId
        JOIN #CreatedAccountReceivables createdAR
          ON createdAR.InvoiceNumber = candidate.InvoiceNumber
         AND createdAR.AccountReceivableType = candidate.AccountReceivableType;

        SET @AccountingCreated = @@ROWCOUNT;

        SELECT @SharesExpected = COUNT(*)
        FROM Portfolio.PortfolioInitialBalanceAccountReceivableShare share
        JOIN #Candidates candidate
          ON candidate.StagingId = share.PortfolioInitialBalanceAccountReceivableId;

        INSERT INTO Portfolio.AccountReceivableShare
        (
            AccountReceivableId,
            Number,
            ExpiredDate,
            Value,
            Balance
        )
        SELECT
            createdAR.AccountReceivableId,
            share.Number,
            share.ExpiredDate,
            share.Value,
            share.Value
        FROM Portfolio.PortfolioInitialBalanceAccountReceivableShare share
        JOIN #Candidates candidate
          ON candidate.StagingId = share.PortfolioInitialBalanceAccountReceivableId
        JOIN #CreatedAccountReceivables createdAR
          ON createdAR.InvoiceNumber = candidate.InvoiceNumber
         AND createdAR.AccountReceivableType = candidate.AccountReceivableType;

        SET @SharesCreated = @@ROWCOUNT;

        /* CUFE: conserva la misma cadena normalizada y genera el documento XML sombra. */
        INSERT INTO Billing.ElectronicDocument
        (
            OperatingUnitId,
            CustomerPartyId,
            EntityId,
            EntityName,
            DocumentDate,
            DocumentType,
            Status,
            CreationDate,
            Container,
            FilePath,
            Prefix,
            DocumentNumber,
            CUFE,
            DianVersion,
            Retry,
            Year,
            Consecutive
        )
        OUTPUT
            inserted.Id,
            inserted.EntityId,
            inserted.DocumentNumber
        INTO #CreatedElectronicDocuments
        (
            ElectronicDocumentId,
            InvoiceId,
            DocumentNumber
        )
        SELECT
            @OperatingUnitId,
            staging.ThirdPartyId,
            invoice.InvoiceId,
            'Invoice',
            staging.AccountReceivableDate,
            CONVERT(tinyint, 1),
            CONVERT(tinyint, 3),
            @Now,
            @ElectronicDocumentContainer,
            CONVERT(varchar(250),
                'E:\ProgramData\Indigo Technologies\ElectronicDocuments\'
                + @ElectronicDocumentContainer
                + '\001\'
                + CONVERT(varchar(4), DATEPART(year, @PortfolioCreationDate))
                + '\'
                + CONVERT(varchar(2), DATEPART(month, @PortfolioCreationDate))
                + '\Saldos Iniciales\'
                + staging.InvoiceNumber),
            '',
            invoice.InvoiceNumber,
            CONVERT(varchar(250), NULLIF(LTRIM(RTRIM(staging.CUFE)), '')),
            CONVERT(decimal(18, 2), 2.1),
            0,
            DATEPART(year, staging.AccountReceivableDate),
            0
        FROM #Candidates candidate
        JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
          ON staging.Id = candidate.StagingId
        JOIN #CreatedInvoices invoice
          ON invoice.InvoiceNumber = candidate.InvoiceNumber
        WHERE NULLIF(LTRIM(RTRIM(staging.CUFE)), '') IS NOT NULL;

        SET @ElectronicDocumentsCreated = @@ROWCOUNT;

        /* CUV: registra el encabezado RIPS y deja el detalle para RCM/Cosmos. */
        INSERT INTO Billing.ElectronicsProperties
        (
            EntityId,
            EntityName,
            EntityCode,
            StatusRIPS,
            CUV,
            CreationUser,
            CreationDate
        )
        OUTPUT
            inserted.Id,
            inserted.EntityId,
            inserted.EntityCode
        INTO #CreatedElectronicsProperties
        (
            ElectronicsPropertiesId,
            InvoiceId,
            EntityCode
        )
        SELECT
            invoice.InvoiceId,
            'Invoice',
            invoice.InvoiceNumber,
            CONVERT(tinyint, 2),
            CONVERT(varchar(max), NULLIF(LTRIM(RTRIM(staging.CUV)), '')),
            @AuditUser,
            @Now
        FROM #Candidates candidate
        JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
          ON staging.Id = candidate.StagingId
        JOIN #CreatedInvoices invoice
          ON invoice.InvoiceNumber = candidate.InvoiceNumber
        WHERE NULLIF(LTRIM(RTRIM(staging.CUV)), '') IS NOT NULL;

        SET @ElectronicsPropertiesCreated = @@ROWCOUNT;

        INSERT INTO Billing.ElectronicsRIPS
        (
            ElectronicsPropertiesId,
            RadicateDate,
            sendDate,
            Retry,
            CosmoDBId,
            FilePath,
            CreationUser,
            CreationDate
        )
        OUTPUT
            inserted.Id,
            inserted.ElectronicsPropertiesId
        INTO #CreatedElectronicsRIPS
        (
            ElectronicsRIPSId,
            ElectronicsPropertiesId
        )
        SELECT
            properties.ElectronicsPropertiesId,
            @Now,
            @Now,
            0,
            '',
            '',
            @AuditUser,
            @Now
        FROM #CreatedElectronicsProperties properties;

        SET @ElectronicsRIPSCreated = @@ROWCOUNT;

        INSERT INTO Portfolio.InitialBalanceInvoice
        (
            AccountReceivableId,
            InvoiceId,
            ObligatedPartyDocument,
            InvoiceNumber,
            CosmosId,
            CUV,
            Status,
            CreationUser,
            CreationDate
        )
        OUTPUT
            inserted.Id,
            inserted.AccountReceivableId,
            inserted.InvoiceId,
            inserted.InvoiceNumber
        INTO #CreatedInitialBalanceInvoices
        (
            InitialBalanceInvoiceId,
            AccountReceivableId,
            InvoiceId,
            InvoiceNumber
        )
        SELECT
            createdAR.AccountReceivableId,
            invoice.InvoiceId,
            '',
            invoice.InvoiceNumber,
            NULL,
            CONVERT(varchar(max), NULLIF(LTRIM(RTRIM(staging.CUV)), '')),
            CONVERT(tinyint, 1),
            @AuditUser,
            @Now
        FROM #Candidates candidate
        JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
          ON staging.Id = candidate.StagingId
        JOIN #CreatedInvoices invoice
          ON invoice.InvoiceNumber = candidate.InvoiceNumber
        JOIN #CreatedAccountReceivables createdAR
          ON createdAR.InvoiceNumber = candidate.InvoiceNumber
         AND createdAR.AccountReceivableType = candidate.AccountReceivableType
        WHERE NULLIF(LTRIM(RTRIM(staging.CUV)), '') IS NOT NULL;

        SET @InitialBalanceInvoicesCreated = @@ROWCOUNT;

        IF @InvoicesCreated <> @CandidateCount
         OR @AccountReceivablesCreated <> @CandidateCount
         OR @AccountingCreated <> @AccountingExpected
         OR @SharesCreated <> @SharesExpected
         OR @ElectronicDocumentsCreated <> @ElectronicDocumentsExpected
         OR @ElectronicsPropertiesCreated <> @ElectronicsPropertiesExpected
         OR @ElectronicsRIPSCreated <> @ElectronicsRIPSExpected
         OR @InitialBalanceInvoicesCreated <> @InitialBalanceInvoicesExpected
            THROW 51000, 'La reconciliación de filas creadas no coincide con el staging.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM #Candidates candidate
            JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
              ON staging.Id = candidate.StagingId
            JOIN #CreatedInvoices invoice
              ON invoice.InvoiceNumber = candidate.InvoiceNumber
            LEFT JOIN #CreatedElectronicDocuments electronicDocument
              ON electronicDocument.InvoiceId = invoice.InvoiceId
            WHERE (NULLIF(LTRIM(RTRIM(staging.CUFE)), '') IS NOT NULL AND electronicDocument.InvoiceId IS NULL)
               OR (NULLIF(LTRIM(RTRIM(staging.CUFE)), '') IS NULL AND electronicDocument.InvoiceId IS NOT NULL)
        )
            THROW 51000, 'La reconciliación CUFE no coincide con las facturas candidatas.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM #Candidates candidate
            JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
              ON staging.Id = candidate.StagingId
            JOIN #CreatedInvoices invoice
              ON invoice.InvoiceNumber = candidate.InvoiceNumber
            LEFT JOIN #CreatedElectronicsProperties properties
              ON properties.InvoiceId = invoice.InvoiceId
            LEFT JOIN #CreatedInitialBalanceInvoices initialBalanceInvoice
              ON initialBalanceInvoice.InvoiceId = invoice.InvoiceId
            WHERE (NULLIF(LTRIM(RTRIM(staging.CUV)), '') IS NOT NULL
                   AND (properties.InvoiceId IS NULL OR initialBalanceInvoice.InvoiceId IS NULL))
               OR (NULLIF(LTRIM(RTRIM(staging.CUV)), '') IS NULL
                   AND (properties.InvoiceId IS NOT NULL OR initialBalanceInvoice.InvoiceId IS NOT NULL))
        )
            THROW 51000, 'La reconciliación CUV no coincide con las facturas candidatas.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM #CreatedAccountReceivables createdAR
            LEFT JOIN Billing.Invoice invoice ON invoice.Id = createdAR.InvoiceId
            WHERE invoice.Id IS NULL
               OR invoice.InvoiceNumber <> createdAR.InvoiceNumber
        )
            THROW 51000, 'La correlación entre AccountReceivable y Billing.Invoice no es válida.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM #CreatedElectronicDocuments createdElectronicDocument
            JOIN Billing.Invoice invoice
              ON invoice.Id = createdElectronicDocument.InvoiceId
            JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
              ON staging.InvoiceNumber = createdElectronicDocument.DocumentNumber
             AND staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
            WHERE invoice.CUFE <> CONVERT(varchar(250), NULLIF(LTRIM(RTRIM(staging.CUFE)), ''))
               OR createdElectronicDocument.DocumentNumber <> invoice.InvoiceNumber
               OR NOT EXISTS
               (
                   SELECT 1
                   FROM Billing.ElectronicDocument electronicDocument
                   WHERE electronicDocument.Id = createdElectronicDocument.ElectronicDocumentId
                     AND electronicDocument.OperatingUnitId = @OperatingUnitId
                     AND electronicDocument.CustomerPartyId = staging.ThirdPartyId
                     AND electronicDocument.EntityId = invoice.Id
                     AND electronicDocument.EntityName = 'Invoice'
                     AND electronicDocument.DocumentDate = staging.AccountReceivableDate
                     AND electronicDocument.DocumentType = 1
                     AND electronicDocument.Status = 3
                     AND electronicDocument.CreationDate = @Now
                     AND electronicDocument.CUFE = invoice.CUFE
                     AND electronicDocument.Container = @ElectronicDocumentContainer
                     AND electronicDocument.Prefix = ''
                     AND electronicDocument.DocumentNumber = invoice.InvoiceNumber
                     AND electronicDocument.DianVersion = CONVERT(decimal(18, 2), 2.1)
                     AND electronicDocument.Retry = 0
                     AND electronicDocument.Year = DATEPART(year, staging.AccountReceivableDate)
                     AND electronicDocument.Consecutive = 0
                     AND electronicDocument.ShippingDate IS NULL
                     AND electronicDocument.ValidationDate IS NULL
                     AND electronicDocument.ZipKey IS NULL
                     AND electronicDocument.HttpContent IS NULL
                     AND electronicDocument.FilePath = CONVERT(varchar(250),
                         'E:\ProgramData\Indigo Technologies\ElectronicDocuments\'
                         + @ElectronicDocumentContainer
                         + '\001\'
                         + CONVERT(varchar(4), DATEPART(year, @PortfolioCreationDate))
                         + '\'
                         + CONVERT(varchar(2), DATEPART(month, @PortfolioCreationDate))
                         + '\Saldos Iniciales\'
                         + invoice.InvoiceNumber)
               )
        )
            THROW 51000, 'La reconciliación de Billing.ElectronicDocument no es válida.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM #CreatedElectronicsProperties propertiesCreated
            JOIN Billing.ElectronicsProperties properties
              ON properties.Id = propertiesCreated.ElectronicsPropertiesId
            JOIN Billing.Invoice invoice
              ON invoice.Id = propertiesCreated.InvoiceId
            JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
              ON staging.InvoiceNumber = invoice.InvoiceNumber
             AND staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
            WHERE properties.EntityId <> invoice.Id
               OR properties.EntityName <> 'Invoice'
               OR properties.EntityCode <> invoice.InvoiceNumber
               OR properties.StatusRIPS <> 2
               OR properties.CUV <> CONVERT(varchar(max), NULLIF(LTRIM(RTRIM(staging.CUV)), ''))
               OR properties.CreationUser <> @AuditUser
               OR properties.CreationDate <> @Now
               OR properties.ModificationUser IS NOT NULL
               OR properties.ModificationDate IS NOT NULL
               OR properties.ConditionSalesId IS NOT NULL
        )
            THROW 51000, 'La reconciliación de Billing.ElectronicsProperties no es válida.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM #CreatedElectronicsRIPS ripsCreated
            LEFT JOIN Billing.ElectronicsRIPS rips
              ON rips.Id = ripsCreated.ElectronicsRIPSId
             AND rips.ElectronicsPropertiesId = ripsCreated.ElectronicsPropertiesId
             AND rips.RadicateDate = @Now
             AND rips.sendDate = @Now
             AND rips.Retry = 0
             AND rips.CosmoDBId = ''
             AND rips.FilePath = ''
             AND rips.CreationUser = @AuditUser
             AND rips.CreationDate = @Now
             AND rips.ModificationUser IS NULL
             AND rips.ModificationDate IS NULL
            WHERE rips.Id IS NULL
        )
            THROW 51000, 'La reconciliación de Billing.ElectronicsRIPS no es válida.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM #CreatedInitialBalanceInvoices initialBalanceInvoiceCreated
            JOIN Portfolio.PortfolioInitialBalanceAccountReceivable staging
              ON staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
             AND staging.InvoiceNumber = initialBalanceInvoiceCreated.InvoiceNumber
            LEFT JOIN Portfolio.InitialBalanceInvoice initialBalanceInvoice
              ON initialBalanceInvoice.Id = initialBalanceInvoiceCreated.InitialBalanceInvoiceId
             AND initialBalanceInvoice.AccountReceivableId = initialBalanceInvoiceCreated.AccountReceivableId
             AND initialBalanceInvoice.InvoiceId = initialBalanceInvoiceCreated.InvoiceId
             AND initialBalanceInvoice.InvoiceNumber = initialBalanceInvoiceCreated.InvoiceNumber
             AND initialBalanceInvoice.ObligatedPartyDocument = ''
             AND initialBalanceInvoice.CosmosId IS NULL
             AND initialBalanceInvoice.CUV = CONVERT(varchar(max), NULLIF(LTRIM(RTRIM(staging.CUV)), ''))
             AND initialBalanceInvoice.Status = 1
             AND initialBalanceInvoice.CreationUser = @AuditUser
             AND initialBalanceInvoice.CreationDate = @Now
             AND initialBalanceInvoice.ModificationUser IS NULL
             AND initialBalanceInvoice.ModificationDate IS NULL
            WHERE initialBalanceInvoice.Id IS NULL
        )
            THROW 51000, 'La reconciliación de Portfolio.InitialBalanceInvoice no es válida.', 1;

        IF
        (
            SELECT COUNT(*)
            FROM Portfolio.AccountReceivableAccounting accounting
            JOIN #CreatedAccountReceivables createdAR
              ON createdAR.AccountReceivableId = accounting.AccountReceivableId
        ) <> @AccountingExpected
            THROW 51000, 'La reconciliación final de movimientos contables no coincide.', 1;

        IF
        (
            SELECT COUNT(*)
            FROM Portfolio.AccountReceivableShare share
            JOIN #CreatedAccountReceivables createdAR
              ON createdAR.AccountReceivableId = share.AccountReceivableId
        ) <> @SharesExpected
            THROW 51000, 'La reconciliación final de cuotas no coincide.', 1;

        UPDATE Portfolio.PortfolioInitialBalance
        SET
            AllBudgetAssigned = CASE
                WHEN EXISTS
                (
                    SELECT 1
                    FROM Portfolio.PortfolioInitialBalanceAccountReceivable staging
                    WHERE staging.PortfolioInitialBalanceId = @PortfolioInitialBalanceId
                      AND staging.AffectBudget = 0
                ) THEN 0
                ELSE 1
            END,
            Status = 2,
            ConfirmationDate = @Now,
            ConfirmationUser = @AuditUser,
            ModificationDate = @Now,
            ModificationUser = @AuditUser
        WHERE Id = @PortfolioInitialBalanceId
          AND Status = 1;

        SET @HeaderUpdated = @@ROWCOUNT;

        IF @HeaderUpdated <> 1
            THROW 51000, 'No fue posible confirmar el encabezado del saldo inicial.', 1;

        COMMIT TRANSACTION;

        SELECT
            @PortfolioInitialBalanceId AS PortfolioInitialBalanceId,
            @StagedCount AS Staged,
            @CandidateCount AS Created,
            @ConflictCount AS OmittedConflicts,
            (SELECT COUNT(*) FROM #Conflicts WHERE ConflictReason = 'ACCOUNT_RECEIVABLE_EXISTS') AS AccountReceivableConflicts,
            (SELECT COUNT(*) FROM #Conflicts WHERE ConflictReason = 'BILLING_INVOICE_EXISTS') AS BillingInvoiceConflicts,
            @InvoicesCreated AS InvoicesCreated,
            @AccountReceivablesCreated AS AccountReceivablesCreated,
            @AccountingCreated AS AccountingCreated,
            @SharesCreated AS SharesCreated,
            @ElectronicDocumentsCreated AS ElectronicDocumentsCreated,
            @ElectronicsPropertiesCreated AS ElectronicsPropertiesCreated,
            @ElectronicsRIPSCreated AS ElectronicsRIPSCreated,
            @InitialBalanceInvoicesCreated AS InitialBalanceInvoicesCreated,
            CONVERT(tinyint, 2) AS HeaderStatus;

    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO