CREATE TABLE [Treasury].[ConsignmentDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsignmentTransferId] INT             NOT NULL,
    [CashRegisterId]        INT             NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [CostCenterId]          INT             NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [ValueInCurrencyHeader] DECIMAL (18, 2) CONSTRAINT [DF__Consignme__Value__37E176B5] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ConsignmentTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentTransferDetail_CashRegisters] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_ConsignmentTransferDetail_ConsignmentTransfer] FOREIGN KEY ([ConsignmentTransferId]) REFERENCES [Treasury].[Consignment] ([Id]),
    CONSTRAINT [FK_ConsignmentTransferDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ConsignmentTransferDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[ConsignmentDetail] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetail_CashRegisters];


GO
ALTER TABLE [Treasury].[ConsignmentDetail] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetail_ConsignmentTransfer];


GO
ALTER TABLE [Treasury].[ConsignmentDetail] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetail_CostCenter];


GO
ALTER TABLE [Treasury].[ConsignmentDetail] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetail_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en la moneda de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConsignmentDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';

