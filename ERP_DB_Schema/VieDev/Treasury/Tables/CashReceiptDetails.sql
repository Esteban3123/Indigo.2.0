CREATE TABLE [Treasury].[CashReceiptDetails] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashReceipt]                 INT             NOT NULL,
    [IdThirdParty]                  INT             NOT NULL,
    [IdMainAccount]                 INT             NOT NULL,
    [IdCostCenter]                  INT             NULL,
    [Nature]                        TINYINT         NOT NULL,
    [IdCashReceiptConcept]          INT             NOT NULL,
    [CashReceiptConceptAffectation] TINYINT         NOT NULL,
    [Value]                         DECIMAL (18, 2) NOT NULL,
    [IdRetentionConcept]            INT             NULL,
    [PercentageRetention]           DECIMAL (5, 2)  NULL,
    [BillingValue]                  DECIMAL (18, 2) NULL,
    [BaseValue]                     DECIMAL (18, 2) NULL,
    [CardNumber]                    VARCHAR (30)    NULL,
    [Detail]                        VARCHAR (MAX)   NULL,
    [IdCashFlowConcept]             INT             NULL,
    [CurrencyId]                    INT             NULL,
    [TRM]                           NUMERIC (20, 5) NULL,
    [ValueInCurrencyHeader]         NUMERIC (20, 5) CONSTRAINT [DF__CashRecei__Value__026E9D45] DEFAULT ((0)) NOT NULL,
    [ThirdPartyBeneficiaryId]       INT             NULL,
    CONSTRAINT [PK_CashReceiptDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptDetails_CashReceiptConcepts] FOREIGN KEY ([IdCashReceiptConcept]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_CashReceipts] FOREIGN KEY ([IdCashReceipt]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_RetentionConcepts] FOREIGN KEY ([IdRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_CashReceiptConcepts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_CashReceipts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_CostCenter];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_Currency];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_IdCashFlowConcept];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_MainAccounts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_RetentionConcepts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el tercero beneficiario, este dato depende del tipo de concepto de recibo de caja seleccionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';

