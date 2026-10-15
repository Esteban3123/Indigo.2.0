CREATE TABLE [Budget].[BudgetTransferDetail] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferId]    INT             NOT NULL,
    [BudgetId]      INT             NOT NULL,
    [RevenueTypeId] INT             NOT NULL,
    [Nature]        TINYINT         NOT NULL,
    [Value]         NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_TransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetTransferDetail_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_TransferDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id]),
    CONSTRAINT [FK_TransferDetail_Transfer] FOREIGN KEY ([TransferId]) REFERENCES [Budget].[BudgetTransfer] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetTransferDetail] NOCHECK CONSTRAINT [FK_BudgetTransferDetail_Budget];


GO
ALTER TABLE [Budget].[BudgetTransferDetail] NOCHECK CONSTRAINT [FK_TransferDetail_RevenueType];


GO
ALTER TABLE [Budget].[BudgetTransferDetail] NOCHECK CONSTRAINT [FK_TransferDetail_Transfer];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la trasferencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza ( Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de ingreso/ Egreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del presupuesto inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la trasferencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'TransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';

