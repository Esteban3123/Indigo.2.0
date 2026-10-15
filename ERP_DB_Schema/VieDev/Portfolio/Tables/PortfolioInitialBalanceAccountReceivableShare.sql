CREATE TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableShare] (
    [Id]                                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceAccountReceivableId] INT          NOT NULL,
    [Number]                                     INT          NOT NULL,
    [ExpiredDate]                                DATETIME     NOT NULL,
    [Value]                                      NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAccountReceivableShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableShare_PortfolioInitialBalanceAccountReceivable] FOREIGN KEY ([PortfolioInitialBalanceAccountReceivableId]) REFERENCES [Portfolio].[PortfolioInitialBalanceAccountReceivable] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableShare] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableShare_PortfolioInitialBalanceAccountReceivable];

