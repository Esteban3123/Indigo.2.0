CREATE TABLE [Cost].[CostDistributionBaseDetail] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionBaseId] INT             NOT NULL,
    [ProductionCenterId] INT             NOT NULL,
    [MainAccountId]      INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [Quantity]           NUMERIC (18, 4) NULL,
    CONSTRAINT [PK_CostDistributionBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionBaseDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionBaseDetail_CostDistributionBase] FOREIGN KEY ([DistributionBaseId]) REFERENCES [Cost].[CostDistributionBase] ([Id]),
    CONSTRAINT [FK_CostDistributionBaseDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionBaseDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionBaseDetail] NOCHECK CONSTRAINT [FK_CostDistributionBaseDetail_CostCenter];


GO
ALTER TABLE [Cost].[CostDistributionBaseDetail] NOCHECK CONSTRAINT [FK_CostDistributionBaseDetail_CostDistributionBase];


GO
ALTER TABLE [Cost].[CostDistributionBaseDetail] NOCHECK CONSTRAINT [FK_CostDistributionBaseDetail_CostProductionCenter];


GO
ALTER TABLE [Cost].[CostDistributionBaseDetail] NOCHECK CONSTRAINT [FK_CostDistributionBaseDetail_MainAccounts];

