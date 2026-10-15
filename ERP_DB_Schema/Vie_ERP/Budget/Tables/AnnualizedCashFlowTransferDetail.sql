CREATE TABLE [Budget].[AnnualizedCashFlowTransferDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACTransferId]        INT             NOT NULL,
    [AnnualizedCashFlowId] INT             NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PACTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualizedCashFlowTransferDetail_AnnualizedCashFlow] FOREIGN KEY ([AnnualizedCashFlowId]) REFERENCES [Budget].[AnnualizedCashFlow] ([Id]),
    CONSTRAINT [FK_PACTransferDetail_PACTransfer] FOREIGN KEY ([PACTransferId]) REFERENCES [Budget].[AnnualizedCashFlowTransfer] ([Id]),
    CONSTRAINT [UQ_AnnualizedCashFlowTransferDetail__PACTransferId__AnnualizedCashFlowId] UNIQUE NONCLUSTERED ([PACTransferId] ASC, [AnnualizedCashFlowId] ASC)
);


GO
ALTER TABLE [Budget].[AnnualizedCashFlowTransferDetail] NOCHECK CONSTRAINT [FK_AnnualizedCashFlowTransferDetail_AnnualizedCashFlow];


GO
ALTER TABLE [Budget].[AnnualizedCashFlowTransferDetail] NOCHECK CONSTRAINT [FK_PACTransferDetail_PACTransfer];


GO
CREATE NONCLUSTERED INDEX [IX_AnnualizedCashFlowTransferDetail__PACTransferId]
    ON [Budget].[AnnualizedCashFlowTransferDetail]([PACTransferId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la modificacion (Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del PAC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del traslado al pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'PACTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del traslado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';

