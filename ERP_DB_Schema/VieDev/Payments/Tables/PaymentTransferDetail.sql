CREATE TABLE [Payments].[PaymentTransferDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PaymentTransferId]      INT             NOT NULL,
    [AccountPayableId]       INT             NOT NULL,
    [AccountPayableShareId]  INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [CostCenterId]           INT             NULL,
    [Value]                  DECIMAL (18, 2) NOT NULL,
    [RetentionConceptRTFId]  INT             NULL,
    [PercentageRTF]          NUMERIC (5, 2)  NULL,
    [ValueRTF]               DECIMAL (18, 2) NULL,
    [RetentionConceptICAId]  INT             NULL,
    [ValueICA]               DECIMAL (18, 2) NULL,
    [PercentageICA]          NUMERIC (5, 2)  NULL,
    [ValueDiscount]          DECIMAL (18, 2) NULL,
    [ValueOther]             DECIMAL (18, 2) NULL,
    [ValueUse]               DECIMAL (18, 2) NULL,
    [TRMValue]               NUMERIC (20, 5) CONSTRAINT [DF__PaymentTr__TRMVa__1FE9F193] DEFAULT ((0)) NOT NULL,
    [ValueInCurrencyInvoice] NUMERIC (18, 2) CONSTRAINT [DF__PaymentTr__Value__144334BD] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PaymentTransferDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentTransferDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_AccountPayableShares] FOREIGN KEY ([AccountPayableShareId]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_PaymentTransfer] FOREIGN KEY ([PaymentTransferId]) REFERENCES [Payments].[PaymentTransfer] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_RetentionConcepts] FOREIGN KEY ([RetentionConceptRTFId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_RetentionConcepts_ICA] FOREIGN KEY ([RetentionConceptICAId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentTransferDetail] NOCHECK CONSTRAINT [FK_PaymentTransferDetail_AccountPayable];


GO
ALTER TABLE [Payments].[PaymentTransferDetail] NOCHECK CONSTRAINT [FK_PaymentTransferDetail_AccountPayableShares];


GO
ALTER TABLE [Payments].[PaymentTransferDetail] NOCHECK CONSTRAINT [FK_PaymentTransferDetail_CostCenter];


GO
ALTER TABLE [Payments].[PaymentTransferDetail] NOCHECK CONSTRAINT [FK_PaymentTransferDetail_MainAccounts];


GO
ALTER TABLE [Payments].[PaymentTransferDetail] NOCHECK CONSTRAINT [FK_PaymentTransferDetail_PaymentTransfer];


GO
ALTER TABLE [Payments].[PaymentTransferDetail] NOCHECK CONSTRAINT [FK_PaymentTransferDetail_RetentionConcepts];


GO
ALTER TABLE [Payments].[PaymentTransferDetail] NOCHECK CONSTRAINT [FK_PaymentTransferDetail_RetentionConcepts_ICA];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PaymentTransferDetail__PaymentTransferId__AccountPayableId__AccountPayableShareId]
    ON [Payments].[PaymentTransferDetail]([PaymentTransferId] ASC, [AccountPayableId] ASC, [AccountPayableShareId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la factura con la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'valor de la Tasa Representativa del Mercado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de las Cuentas por pagar compartidas', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';

