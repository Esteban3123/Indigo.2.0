CREATE TABLE [Portfolio].[AccountReceivable] (
    [Id]                                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                         VARCHAR (20)    NOT NULL,
    [OperatingUnitId]                              INT             NOT NULL,
    [AccountReceivableType]                        TINYINT         NOT NULL,
    [ThirdPartyId]                                 INT             NOT NULL,
    [CustomerId]                                   INT             NULL,
    [SellerId]                                     INT             NULL,
    [InvoiceId]                                    INT             NULL,
    [InvoiceNumber]                                VARCHAR (20)    NOT NULL,
    [AccountReceivableDate]                        DATETIME        NOT NULL,
    [Term]                                         INT             NOT NULL,
    [ExpiredDate]                                  DATETIME        NOT NULL,
    [Observations]                                 VARCHAR (300)   NOT NULL,
    [PortfolioStatus]                              TINYINT         NOT NULL,
    [OpeningBalance]                               BIT             NOT NULL,
    [RecognitionId]                                INT             NULL,
    [PaymentAgreement]                             BIT             NOT NULL,
    [RegistrationAdjusted]                         BIT             NOT NULL,
    [MainAccountWithoutFilingId]                   INT             NULL,
    [NumberShares]                                 INT             NOT NULL,
    [Value]                                        NUMERIC (18, 2) NOT NULL,
    [Balance]                                      NUMERIC (18, 2) NOT NULL,
    [DeteriorationBalance]                         DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationBalance] DEFAULT ((0)) NOT NULL,
    [DeteriorationPayment]                         DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationPayment] DEFAULT ((0)) NOT NULL,
    [ProvisionBalance]                             DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_ProvisionBalance] DEFAULT ((0)) NOT NULL,
    [ProvisionPayment]                             DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_ProvisionPayment] DEFAULT ((0)) NOT NULL,
    [Status]                                       TINYINT         NOT NULL,
    [CostCenterId]                                 INT             NULL,
    [InvoiceCategoryId]                            INT             NULL,
    [AccountWithoutRadicateId]                     INT             CONSTRAINT [DF_AccountReceivable_AccountWithoutRadicateId] DEFAULT ((203)) NULL,
    [AccountRadicateId]                            INT             CONSTRAINT [DF_AccountReceivable_AccountRadicateId] DEFAULT ((203)) NULL,
    [AccountObjectionRemediedId]                   INT             CONSTRAINT [DF_AccountReceivable_AccountObjectionRemediedId] DEFAULT ((203)) NULL,
    [AccountConciliationId]                        INT             NULL,
    [AccountLegalCollectionId]                     INT             NULL,
    [AccountHardCollectionId]                      INT             NULL,
    [AccountDebtorOrder]                           INT             NULL,
    [AccountCreditorOrder]                         INT             NULL,
    [CreditProvisionAccountId]                     INT             NULL,
    [DebitProvisionAccountId]                      INT             NULL,
    [DebitAccountDeteriorationId]                  INT             NULL,
    [CreditAccountDeteriorationId]                 INT             NULL,
    [ReversalAccountDeteriorationId]               INT             NULL,
    [PreviousPeriodReversalAccountDeteriorationId] INT             NULL,
    [AffectBudget]                                 BIT             CONSTRAINT [DF_AccountReceivable_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                                     INT             NULL,
    [CreationUser]                                 VARCHAR (20)    NOT NULL,
    [CreationDate]                                 DATETIME        NOT NULL,
    [ModificationUser]                             VARCHAR (20)    NULL,
    [ModificationDate]                             DATETIME        NULL,
    [ConfirmationUser]                             VARCHAR (20)    NULL,
    [ConfirmationDate]                             DATETIME        NULL,
    [AnnulmentUser]                                VARCHAR (20)    NULL,
    [AnnulmentDate]                                DATETIME        NULL,
    [TimeStamp]                                    ROWVERSION      NOT NULL,
    [DeteriorationBalanceCurrentYear]              DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationBalanceCurrentYear] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalancePreviousYear]             DECIMAL (18, 2) CONSTRAINT [DF_AccountReceivable_DeteriorationBalancePreviousYear] DEFAULT ((0)) NOT NULL,
    [CurrentDeteriorationYear]                     INT             NULL,
    [CareGroupId]                                  INT             NULL,
    [CurrencyId]                                   INT             NULL,
    [TRMValue]                                     NUMERIC (20, 5) CONSTRAINT [DF__AccountRe__TRMVa__1F3FE61D] DEFAULT ((1)) NOT NULL,
    [DateAccountBecomeZero]                        DATE            NULL,
    CONSTRAINT [PK_AccountReceivable__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_AccountReceivable] CHECK ([DeteriorationBalance]<=abs([Balance])),
    CONSTRAINT [CK_AccountReceivable_1] CHECK ([ProvisionBalance]<=abs([Balance])),
    CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalance] CHECK ([DeteriorationBalance]=([DeteriorationBalanceCurrentYear]+[DeteriorationBalancePreviousYear])),
    CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalancesNotNegatives] CHECK ([DeteriorationBalance]>=(0) AND [DeteriorationBalanceCurrentYear]>=(0) AND [DeteriorationBalancePreviousYear]>=(0)),
    CONSTRAINT [FK_AccountReceivable_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_AccountReceivable_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_AccountReceivable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_AccountReceivable_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts] FOREIGN KEY ([AccountWithoutRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts1] FOREIGN KEY ([MainAccountWithoutFilingId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts10] FOREIGN KEY ([DebitProvisionAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts2] FOREIGN KEY ([AccountRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts3] FOREIGN KEY ([AccountObjectionRemediedId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts4] FOREIGN KEY ([AccountConciliationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts5] FOREIGN KEY ([AccountLegalCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts6] FOREIGN KEY ([AccountDebtorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts7] FOREIGN KEY ([AccountCreditorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts8] FOREIGN KEY ([AccountHardCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_MainAccounts9] FOREIGN KEY ([CreditProvisionAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivable_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Recognition] FOREIGN KEY ([RecognitionId]) REFERENCES [Budget].[Recognition] ([Id]),
    CONSTRAINT [FK_AccountReceivable_Seller] FOREIGN KEY ([SellerId]) REFERENCES [Common].[Seller] ([Id]),
    CONSTRAINT [FK_AccountReceivable_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_1];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalance];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalancesNotNegatives];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Invoice];




GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_1];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalance];


GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [CK_AccountReceivable_ValidateDeteriorationBalancesNotNegatives];


GO



GO



GO



GO



GO



GO
ALTER TABLE [Portfolio].[AccountReceivable] NOCHECK CONSTRAINT [FK_AccountReceivable_Invoice];


GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_AccountReceivable__ThirdPartyId__INC__AccountHardCollectionId__AccountRadicateId__AccountReceivableDate__AccountReceivableTyp]
    ON [Portfolio].[AccountReceivable]([ThirdPartyId] ASC)
    INCLUDE([AccountHardCollectionId], [AccountRadicateId], [AccountReceivableDate], [AccountReceivableType], [AccountWithoutRadicateId], [Balance], [InvoiceNumber], [NumberShares], [OpeningBalance], [PortfolioStatus], [Term], [Value]);


GO
CREATE NONCLUSTERED INDEX [IX_AccountReceivable_AccountReceivableDate]
    ON [Portfolio].[AccountReceivable]([AccountReceivableDate] ASC)
    INCLUDE([AccountReceivableType], [ThirdPartyId], [InvoiceId], [InvoiceNumber], [Term], [PortfolioStatus], [OpeningBalance], [NumberShares], [Value], [InvoiceCategoryId], [AccountWithoutRadicateId], [AccountRadicateId], [AccountObjectionRemediedId], [AccountLegalCollectionId], [AccountHardCollectionId], [CareGroupId], [CurrencyId], [DateAccountBecomeZero]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountReceivable__InvoiceNumber__AccountReceivableType]
    ON [Portfolio].[AccountReceivable]([InvoiceNumber] ASC, [AccountReceivableType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AccountReceivableDate]
    ON [Portfolio].[AccountReceivable]([AccountReceivableDate] ASC)
    INCLUDE([OperatingUnitId], [AccountReceivableType], [ThirdPartyId], [InvoiceId], [InvoiceNumber], [Term], [PortfolioStatus], [OpeningBalance], [NumberShares], [InvoiceCategoryId], [AccountWithoutRadicateId], [AccountRadicateId], [AccountLegalCollectionId], [AccountHardCollectionId], [CareGroupId]);


GO
CREATE NONCLUSTERED INDEX [InvoiceId_IDX]
    ON [Portfolio].[AccountReceivable]([InvoiceId] ASC)
    INCLUDE([Id]);


GO
-- =============================================
-- Author:		<Ruben Dario Scalante>
-- Create date: <16/12/2022>
-- Description:	<Trigger que se enfoca en detectar cuando haya una insercción en la tabla [AccountReceivable] 
--				 Esto con el fin de guardar en la tabla [AccountReceivableExchangeRate] datos como:
--				 *El id de la cuenta por cobrar.
--				 *La moneda (CurrencyId) registrada en la tabla [TRM] - que es distinta a la moneda registrada en la cuenta por cobrar.
--               *El valor de la moneda (CurrencyId) que es distinta a la cuenta por cobrar.
--				 *El valor de la moneda oficial que se relaciona con la cuenta por cobrar>
-- =============================================

CREATE TRIGGER [Portfolio].[TG_AccountReceivableRates]
ON [Portfolio].[AccountReceivable]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Today date = CONVERT(date, Common.GETDATE());
    /*
        Fast path para instalaciones con una sola moneda y para inserciones
        cuya CurrencyId sea NULL. Evita ejecutar el resto del trigger cuando
        no puede existir ninguna moneda destino.
    */
    IF NOT EXISTS
    (
        SELECT 1
        FROM inserted source
        WHERE source.CurrencyId IS NOT NULL
          AND EXISTS
          (
              SELECT 1
              FROM Common.Currency target
              WHERE target.Id <> source.CurrencyId
          )
    )
        RETURN;
    /* Sin tasas vigentes no puede generarse AccountReceivableExchangeRate. */
    IF NOT EXISTS
    (
        SELECT 1
        FROM Common.TRM commonRate
        WHERE commonRate.MeasurementDate = @Today
    )
    AND NOT EXISTS
    (
        SELECT 1
        FROM Billing.CustomTRM customRate
        WHERE @Today BETWEEN customRate.InitialMeasurementDate
                         AND customRate.FinalMeasurementDate
    )
        RETURN;
    ;WITH SettingsByOperatingUnit AS
    (
        SELECT
            sb.IdOperatingUnit,
            MAX(CASE WHEN sb.HasCustomTRM = 1 THEN 1 ELSE 0 END) AS HasCustomTRM
        FROM Billing.SettingsBilling sb
        GROUP BY sb.IdOperatingUnit
    ),
    EligibleCurrencyPairs AS
    (
        SELECT
            source.Id AS AccountReceivableId,
            source.CurrencyId AS SourceCurrencyId,
            target.Id AS TargetCurrencyId,
            source.OperatingUnitId,
            CASE
                WHEN invoice.Id IS NOT NULL
                 AND invoice.DocumentType NOT IN (6, 7)
                 AND ISNULL(settings.HasCustomTRM, 0) = 1
                    THEN 1
                ELSE 0
            END AS CanUseCustomTRM
        FROM inserted source
        CROSS JOIN Common.Currency target
        LEFT JOIN Billing.Invoice invoice
            ON invoice.Id = source.InvoiceId
        LEFT JOIN SettingsByOperatingUnit settings
            ON settings.IdOperatingUnit = source.OperatingUnitId
        WHERE source.CurrencyId IS NOT NULL
          AND target.Id <> source.CurrencyId
    ),
    RateCandidates AS
    (
        SELECT
            pair.AccountReceivableId,
            pair.TargetCurrencyId,
            1 AS RatePriority,
            customRate.Id AS RateId,
            CONVERT(numeric(20, 5), customRate.Value) AS RateValue,
            CONVERT(numeric(20, 5), customRate.ValueOfficialToCurrency) AS RateValueReverse
        FROM EligibleCurrencyPairs pair
        JOIN Billing.CustomTRM customRate
            ON pair.CanUseCustomTRM = 1
           AND customRate.OperatingUnitId = pair.OperatingUnitId
           AND customRate.CurrencyId = pair.SourceCurrencyId
           AND customRate.OfficialCurrencyId = pair.TargetCurrencyId
           AND @Today BETWEEN customRate.InitialMeasurementDate AND customRate.FinalMeasurementDate
        UNION ALL
        SELECT
            pair.AccountReceivableId,
            pair.TargetCurrencyId,
            2 AS RatePriority,
            customRate.Id AS RateId,
            CONVERT(numeric(20, 5), customRate.ValueOfficialToCurrency) AS RateValue,
            CONVERT(numeric(20, 5), customRate.Value) AS RateValueReverse
        FROM EligibleCurrencyPairs pair
        JOIN Billing.CustomTRM customRate
            ON pair.CanUseCustomTRM = 1
           AND customRate.OperatingUnitId = pair.OperatingUnitId
           AND customRate.OfficialCurrencyId = pair.SourceCurrencyId
           AND customRate.CurrencyId = pair.TargetCurrencyId
           AND @Today BETWEEN customRate.InitialMeasurementDate AND customRate.FinalMeasurementDate
        UNION ALL
        SELECT
            pair.AccountReceivableId,
            pair.TargetCurrencyId,
            3 AS RatePriority,
            commonRate.Id AS RateId,
            CONVERT(numeric(20, 5), commonRate.Value) AS RateValue,
            CONVERT(numeric(20, 5), commonRate.ValueOfficialToCurrency) AS RateValueReverse
        FROM EligibleCurrencyPairs pair
        JOIN Common.TRM commonRate
            ON commonRate.MeasurementDate = @Today
           AND commonRate.CurrencyId = pair.SourceCurrencyId
           AND commonRate.OfficialCurrencyId = pair.TargetCurrencyId
        UNION ALL
        SELECT
            pair.AccountReceivableId,
            pair.TargetCurrencyId,
            4 AS RatePriority,
            commonRate.Id AS RateId,
            CONVERT(numeric(20, 5), commonRate.ValueOfficialToCurrency) AS RateValue,
            CONVERT(numeric(20, 5), commonRate.Value) AS RateValueReverse
        FROM EligibleCurrencyPairs pair
        JOIN Common.TRM commonRate
            ON commonRate.MeasurementDate = @Today
           AND commonRate.OfficialCurrencyId = pair.SourceCurrencyId
           AND commonRate.CurrencyId = pair.TargetCurrencyId
    ),
    RankedRates AS
    (
        SELECT
            candidate.AccountReceivableId,
            candidate.TargetCurrencyId,
            candidate.RateValue,
            candidate.RateValueReverse,
            ROW_NUMBER() OVER
            (
                PARTITION BY candidate.AccountReceivableId, candidate.TargetCurrencyId
                ORDER BY candidate.RatePriority, candidate.RateId DESC
            ) AS SelectionOrder
        FROM RateCandidates candidate
    )
    INSERT INTO Portfolio.AccountReceivableExchangeRate
    (
        AccountReceivableId,
        CurrencyId,
        Value,
        ValueReverse
    )
    SELECT
        rate.AccountReceivableId,
        rate.TargetCurrencyId,
        rate.RateValue,
        rate.RateValueReverse
    FROM RankedRates rate
    WHERE rate.SelectionOrder = 1;
END;
GO

CREATE TRIGGER Portfolio.AccountReceivable_DateBecomeZero 
   ON  Portfolio.AccountReceivable 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if exists( select 1 from INSERTED where Balance =0 and DateAccountBecomeZero is null) BEGIN
			
			UPDATE ar set ar.DateAccountBecomeZero = common.GETDATE()
			from Portfolio.AccountReceivable ar WITH(NOLOCK)
			join INSERTED i on ar.Id=i.Id
			WHERE i.Balance =0 and i.DateAccountBecomeZero is null

	END

	if EXISTS (SELECT 1 from INSERTED where Balance >0 and DateAccountBecomeZero is not null) BEGIN
			
			UPDATE ar set ar.DateAccountBecomeZero = null
			from Portfolio.AccountReceivable ar WITH(NOLOCK)
			join INSERTED i on ar.Id=i.Id
			where i.Balance >0 and i.DateAccountBecomeZero is not null
	END

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) en que el saldo de la cuenta por cobrar llegó a cero; marca cuándo se saldó completamente la deuda o cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DateAccountBecomeZero';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha en la que el saldo de la cuenta por cobrar llego a 0.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DateAccountBecomeZero';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DateAccountBecomeZero';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de Cambio Representativa del Mercado (NUMERIC 20,5) con que se calculó el valor de la CxC; tipo cambio para conversión de moneda extranjera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TRM con el que se calculo el valor de la cxc', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la Moneda de la nota débito/crédito o factura; referencia a tabla Currency para saber si es COP, USD, EUR, etc.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda de la nota deb/cred', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del Grupo de Atención (cuidado, asistencia) asociado; FK a Contract.CareGroup para trazabilidad de unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año (INT) del último deterioro registrado en el año actual; año fiscal en que se reconoció el deterioro de cartera más reciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año del ultimo deterioro deteriorado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de deterioro (DECIMAL 18,2) reconocido en el año fiscal anterior; porción vencida o de difícil recaudo del período previo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo deteriorado en el año anterior.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor deteriorado (DECIMAL 18,2) de la factura en lo transcurrido del año actual; provisión por incobrabilidad acumulada año en curso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor deteriorado para la factura en lo transcurrido del año.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, modificación, confirmación o anulación; instante exacto de cambio en la BD para auditoría.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de anulación de la cuenta por cobrar; cuándo se revocó o canceló el registro de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que ejecutó la anulación; responsable de revocar o eliminar el registro de CxC.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de confirmación de la cuenta por cobrar; cuándo se validó o confirmó el registro en el ERP.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que confirmó la CxC; responsable de validar que el registro esté correcto y completo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de la última modificación; cuándo se ajustó, actualizó o cambió algún dato del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación; responsable del cambio más reciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de creación del registro; cuándo se originó la cuenta por cobrar en el sistema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro de CxC; responsable de ingreso inicial en el ERP.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del presupuesto de ingresos asociado; FK a Budget.Budget para control presupuestal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el grupo de atención afecta presupuesto (1=Sí, 0=No); al radicar factura crea reconocimiento presupuestal automáticamente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable para reversión de deterioro de período anterior; GL MainAccount para ajuste contable año previo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de cuenta de reversión de período anterior', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable para reversión de deterioro; GL MainAccount para contrarrestar deterioro reconocido.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de cuenta de reversión', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable de crédito para deterioro; GL MainAccount lado crédito de la provisión de incobrabilidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de la cuenta de crédito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable de débito para deterioro; GL MainAccount lado débito de la provisión de incobrabilidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de la cuenta de débito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable para provisión débito; GL MainAccount lado débito de la provisión de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable para provisión débito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable para provisión crédito; GL MainAccount lado crédito de la provisión de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta coontable para provisión crédito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta de orden de acreedores para glosas; solo sector público, para contabilización de glosas pendientes.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de orden de acreedores glosas, Solo para el sector publico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta de orden de glosas o reclamaciones; solo sector público, para registrar glosas generadas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta de orden de glosas, Solo para el sector publico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable para cobro de difícil recaudo; GL MainAccount cuando cartera entra en cobranza judicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de dificil recuado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable para cobro jurídico; solo empresas privadas, para cartera en proceso legal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable para cobro juridico, Solo para empresas privadas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta de conciliación; solo empresas privadas, para registrar diferencias conciliadas con terceros.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de conciliacion, solo para las empresas privadas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable para glosa subsanable; solo empresas privadas, para glosas que pueden corregirse.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Glosa Subsanable, Solo para las empresas privadas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable radicada; GL MainAccount cuando la factura está radicada/presentada a pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable radicada', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable sin radicar; GL MainAccount para CxC pendiente de presentación a pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable sin Radicar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de categoría de la factura o CxC; FK a Billing.InvoiceCategories para clasificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la categoria de la factura o CxC', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del centro de costo; origen facturación=grupo atención, saldo inicial=solicitado en formulario.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo, cuando la cuenta por cobrar fue generada desde facturacion esta es igual a la del grupo de atencion, y si es creada como saldo inicial esta se solicita en el formulario de saldos iniciales', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) de la CxC: 1=Registrado, 2=Confirmado, 3=Anulado; indica ciclo de vida del registro contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cuenta por cobrar (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (DECIMAL 18,2) pagado o disminuido de la provisión; reduce el saldo de provisión por cobros efectuados.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ProvisionPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los valores pagados o disminuidos de la provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ProvisionPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ProvisionPayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance (DECIMAL 18,2) de provisiones pendientes; saldo vigente de la provisión de cartera o incobrabilidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ProvisionBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Balance de provisiones', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ProvisionBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ProvisionBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto (DECIMAL 18,2) pagado o disminuido del deterioro; reduce el saldo de deterioro por cobros de cartera vencida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los valores pagados o disminuidos del deterioro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationPayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor deteriorado (DECIMAL 18,2) de la CxC; suma de DeteriorationBalancePreviousYear + DeteriorationBalanceCurrentYear.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor deteriorado de la cuenta por cobrar.  Este valor tambien corresponde a la suma los campos DeteriorationBalancePreviousYear + DeteriorationBalanceCurrentYear', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'DeteriorationBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo actual (NUMERIC 18,2) de la cuenta por cobrar; monto pendiente sin pagar después de abonos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor original (NUMERIC 18,2) de la cuenta por cobrar; monto inicial de la factura o documento.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número (INT) de cuotas o plazos; cantidad de pagos en que se divide la CxC.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de cuotas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'NumberShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de cuenta contable sin radicar; GL MainAccount para factura no presentada aún.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'MainAccountWithoutFilingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable sin Radicar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'MainAccountWithoutFilingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'MainAccountWithoutFilingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el registro está ajustado; marca si se han realizado rectificaciones posteriores.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'RegistrationAdjusted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el registro esta ajustado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'RegistrationAdjusted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'RegistrationAdjusted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si es acuerdo de pago; 1=convenio, 0=factura normal; afecta flujo de cobranza.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si es Acuerdo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PaymentAgreement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del reconocimiento presupuestal; FK a Budget para vinculación de ingresos reconocidos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reconocimiento presupuestal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'RecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si es saldo inicial de cartera; 1=saldo de apertura, 0=generado por factura nueva.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si es Saldo Inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'OpeningBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) de cartera: 1=Sin Radicar, 2=Radicada sin Confirmar, 3=Radicada Entidad, 4=Objetada/Glosada, 7=Certificada Parcial, 8=Certificada Total, 14=Devolución, 15=Difícil Recaudo, 16=Cobro Jurídico.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de cartera   1 - Sin Radicar  2 - Radicada sin Confirmar  3 - Radicada Entidad  4 - Objetada o Glosada  7 - Certificada Parcial  8 - Certificada Total  14 - Devolución Factura  15 - Cuenta de Dificil Recaudo  16 - Cobro Jurídico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto (VARCHAR 300) con observaciones o notas adicionales; descripción libre de eventos, inconsistencias o comentarios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de vencimiento de la CxC; cuándo vence el plazo de pago según término.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo (INT) en días de la cuenta por cobrar; número de días desde AccountReceivableDate hasta ExpiredDate.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo de la cuenta (En dias)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) de origen o emisión de la CxC; cuándo nace la obligación de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número (VARCHAR 20) de la factura; identificador comercial del documento de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la factura en tabla Billing.Invoice; se completa en interfaz y debe coincidir con InvoiceNumber.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura, debe ser llenada solo cuando se realiza por interfaz, y tambien se debe llenar el numero de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del vendedor o profesional de salud que generó la venta; FK a tabla de vendedores.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'SellerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del vendedor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'SellerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'SellerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del cliente o pagador; FK a Common.Customer, puede ser nulo si es ThirdPartyId.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tercero asociado (pagador, responsable, institución); FK a terceros para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo (TINYINT) de CxC: 1=Facturación Básica, 2=Facturación Ley 100, 3=IIC, 4=Pagarés, 5=Acuerdos Pago, 6=Cuota Moderadora, 7=Producto, 8=Predial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cuenta por cobrar   Facturación Básica = 1  Facturación Ley 100 = 2  Impuestos Industria y Comercio = 3  Pagarés = 4  Acuerdos de Pago = 5  Documento de Pago a Cuota Moderadora = 6  Factura de Producto = 7  Impuesto Predial = 8', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la Unidad Operativa o centro de atención que generó la CxC; FK para segregación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) único de la cuenta de cobro; identificador interno del registro de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la cuenta de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la cuenta por cobrar; clave primaria de AccountReceivable (Portfolio).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas por cobrar del módulo de cartera. Registra cada documento de cobro (factura, cuenta de cobro) emitido a un tercero o cliente, con su valor, saldo pendiente, estado de recaudo, provisiones por deterioro y las cuentas contables asociadas para el proceso de cobro, conciliación y gestión de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivable';

GO
CREATE NONCLUSTERED INDEX [IX_AccountReceivable_InvoiceId]
    ON [Portfolio].[AccountReceivable]([InvoiceId] ASC)
    INCLUDE([AccountReceivableType], [Balance], [CurrencyId], [Status]);
