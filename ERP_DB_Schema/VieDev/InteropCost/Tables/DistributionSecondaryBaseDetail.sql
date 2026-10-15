CREATE TABLE [InteropCost].[DistributionSecondaryBaseDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT             NOT NULL,
    [ProductionCenterId]          INT             NOT NULL,
    [Quantity]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_DistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [InteropCost].[DistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionSecondaryBaseDetail] NOCHECK CONSTRAINT [FK_DistributionSecondaryBaseDetail_DistributionSecondaryBase];


GO
ALTER TABLE [InteropCost].[DistributionSecondaryBaseDetail] NOCHECK CONSTRAINT [FK_DistributionSecondaryBaseDetail_ProductionCenter];

