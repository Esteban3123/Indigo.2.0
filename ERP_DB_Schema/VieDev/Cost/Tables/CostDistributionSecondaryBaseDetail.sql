CREATE TABLE [Cost].[CostDistributionSecondaryBaseDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT             NOT NULL,
    [ProductionCenterId]          INT             NOT NULL,
    [Quantity]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryBaseDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostDistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [Cost].[CostDistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionSecondaryBaseDetail] NOCHECK CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostDistributionSecondaryBase];


GO
ALTER TABLE [Cost].[CostDistributionSecondaryBaseDetail] NOCHECK CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostProductionCenter];

