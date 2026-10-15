CREATE TABLE [Budget].[AnnualizedCashFlowModificationDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACModificationId]    INT             NOT NULL,
    [AnnualizedCashFlowId] INT             NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PACModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualizedCashFlowModificationDetail_AnnualizedCashFlow] FOREIGN KEY ([AnnualizedCashFlowId]) REFERENCES [Budget].[AnnualizedCashFlow] ([Id]),
    CONSTRAINT [FK_PACModificationDetail_PACModification] FOREIGN KEY ([PACModificationId]) REFERENCES [Budget].[AnnualizedCashFlowModification] ([Id]),
    CONSTRAINT [UQ_AnnualizedCashFlowModificationDetail__PACModificationId__AnnualizedCashFlowId] UNIQUE NONCLUSTERED ([PACModificationId] ASC, [AnnualizedCashFlowId] ASC)
);


GO
ALTER TABLE [Budget].[AnnualizedCashFlowModificationDetail] NOCHECK CONSTRAINT [FK_AnnualizedCashFlowModificationDetail_AnnualizedCashFlow];


GO
ALTER TABLE [Budget].[AnnualizedCashFlowModificationDetail] NOCHECK CONSTRAINT [FK_PACModificationDetail_PACModification];


GO
CREATE NONCLUSTERED INDEX [IX_AnnualizedCashFlowModificationDetail__PACModificationId]
    ON [Budget].[AnnualizedCashFlowModificationDetail]([PACModificationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la modificacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la aplicacion (Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del PAC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera modificacion del pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'PACModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la modificacion del pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

