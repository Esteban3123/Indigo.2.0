CREATE TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableAccounting] (
    [Id]                                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceAccountReceivableId] INT             NOT NULL,
    [MainAccountId]                              INT             NOT NULL,
    [ThirdPartyId]                               INT             NULL,
    [CostCenterId]                               INT             NULL,
    [Value]                                      NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAccountReceivableAccounting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_PortfolioInitialBalanceAccountReceivable] FOREIGN KEY ([PortfolioInitialBalanceAccountReceivableId]) REFERENCES [Portfolio].[PortfolioInitialBalanceAccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableAccounting] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableAccounting] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableAccounting] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_PortfolioInitialBalanceAccountReceivable];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableAccounting] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_ThirdParty];

