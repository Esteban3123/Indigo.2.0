CREATE TABLE [InteropCost].[DistributionBaseDetail] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionBaseId] INT             NOT NULL,
    [ProductionCenterId] INT             NOT NULL,
    [MainAccountId]      INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [Quantity]           NUMERIC (18, 4) NOT NULL,
    CONSTRAINT [PK_DistributionBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionBaseDetail_DistributionBase] FOREIGN KEY ([DistributionBaseId]) REFERENCES [InteropCost].[DistributionBase] ([Id]),
    CONSTRAINT [FK_DistributionBaseDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionBaseDetail] NOCHECK CONSTRAINT [FK_DistributionBaseDetail_DistributionBase];


GO
ALTER TABLE [InteropCost].[DistributionBaseDetail] NOCHECK CONSTRAINT [FK_DistributionBaseDetail_ProductionCenter];

