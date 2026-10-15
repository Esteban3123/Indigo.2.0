CREATE TABLE [Portfolio].[PortfolioTransferReceivableReduce] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioTransferId]            INT             NOT NULL,
    [AccountReceivableId]            INT             NOT NULL,
    [ProvisionValue]                 DECIMAL (18, 2) NOT NULL,
    [DeteriorationValue]             DECIMAL (18, 2) NOT NULL,
    [DeteriorationCurrentYearValue]  DECIMAL (18, 2) NOT NULL,
    [DeteriorationPreviousYearValue] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_PortfolioTransferReceivableReduce__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioTransferReceivableReduce_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioTransferReceivableReduce_PortfolioTransfer] FOREIGN KEY ([PortfolioTransferId]) REFERENCES [Portfolio].[PortfolioTransfer] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioTransferReceivableReduce] NOCHECK CONSTRAINT [FK_PortfolioTransferReceivableReduce_AccountReceivable];


GO
ALTER TABLE [Portfolio].[PortfolioTransferReceivableReduce] NOCHECK CONSTRAINT [FK_PortfolioTransferReceivableReduce_PortfolioTransfer];


GO
CREATE NONCLUSTERED INDEX [IX_PortfolioTransferReceivableReduce__PortfolioTransferId]
    ON [Portfolio].[PortfolioTransferReceivableReduce]([PortfolioTransferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PortfolioTransferReceivableReduce__AccountReceivableId]
    ON [Portfolio].[PortfolioTransferReceivableReduce]([AccountReceivableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de deterioro disminuido en el año anterior', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationPreviousYearValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de deterioro disminuido en el año actual', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationCurrentYearValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de deterioro disminuido', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de provisión disminuido', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'ProvisionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de anticipo vs cxc', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'PortfolioTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de detalle de valores disminuidos por anticipo vs cxc', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'Id';

