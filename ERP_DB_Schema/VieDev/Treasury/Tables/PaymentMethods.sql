CREATE TABLE [Treasury].[PaymentMethods] (
    [Id]                           INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashReceipt]                INT                                                                       NOT NULL,
    [PaymentMethodTypes]           TINYINT                                                                   NOT NULL,
    [Value]                        DECIMAL (18, 2)                                                           NOT NULL,
    [IdBank]                       INT                                                                       NULL,
    [CheckNumber]                  VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Check_Ofuscado", 0)')   NULL,
    [DepositDate]                  DATETIME                                                                  NULL,
    [IdEntityBankAccount]          INT                                                                       NULL,
    [DepositNumber]                VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Deposit_Ofuscado", 0)') NULL,
    [DepositType]                  TINYINT                                                                   NULL,
    [IdCard]                       INT                                                                       NULL,
    [CardNumber]                   VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Card_Ofuscado", 0)')    NULL,
    [BaseValue]                    DECIMAL (18, 2)                                                           NULL,
    [CommissionValue]              DECIMAL (18, 2)                                                           NULL,
    [PercentageCommission]         DECIMAL (5, 2)                                                            NULL,
    [RTFValue]                     DECIMAL (18, 2)                                                           NULL,
    [PercentageRTF]                DECIMAL (5, 2)                                                            NULL,
    [ICAValue]                     DECIMAL (18, 2)                                                           NULL,
    [PercentageICA]                DECIMAL (5, 2)                                                            NULL,
    [IdCostCenter]                 INT                                                                       NULL,
    [CurrencyId]                   INT                                                                       NULL,
    [TRM]                          NUMERIC (20, 5)                                                           NULL,
    [ValueInCurrencyHeader]        NUMERIC (20, 5)                                                           CONSTRAINT [DF__PaymentMe__Value__017A790C] DEFAULT ((0)) NOT NULL,
    [IdAgreementsRedemptionPoints] INT                                                                       NULL,
    [TransactionNumber]            VARCHAR (30)                                                              NULL,
    [TransactionDate]              DATETIME                                                                  NULL,
    [RedemptionPoints]             INT                                                                       NULL,
    CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentMethods_AgreementsRedemptionPoints] FOREIGN KEY ([IdAgreementsRedemptionPoints]) REFERENCES [Treasury].[AgreementsRedemptionPoints] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Bank] FOREIGN KEY ([IdBank]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Cards] FOREIGN KEY ([IdCard]) REFERENCES [Treasury].[Cards] ([Id]),
    CONSTRAINT [FK_PaymentMethods_CashReceipts] FOREIGN KEY ([IdCashReceipt]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_PaymentMethods_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentMethods_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_AgreementsRedemptionPoints];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_Bank];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_Cards];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_CashReceipts];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_CostCenter];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_Currency];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_EntityBankAccounts];


GO
CREATE NONCLUSTERED INDEX [IX_PaymentMethods_IdCashReceipt]
    ON [Treasury].[PaymentMethods]([IdCashReceipt] ASC)
    INCLUDE([PaymentMethodTypes], [Value]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de formas de pago: 1 = Efectivo, 2 = Cheque, 3 = Tarjeta, 4  = Consignación, 5 = Redencion de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';

