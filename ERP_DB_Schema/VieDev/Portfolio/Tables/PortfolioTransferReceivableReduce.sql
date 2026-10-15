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

