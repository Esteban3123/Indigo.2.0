CREATE TABLE [InteropCost].[DistributionIntermediateDetail] (
    [Id]                         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionIntermediateId] INT            NOT NULL,
    [ProductionCenterId]         INT            NOT NULL,
    [Proportion]                 NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_DistributionIntermediateDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionIntermediateDetail_DistributionIntermediate] FOREIGN KEY ([DistributionIntermediateId]) REFERENCES [InteropCost].[DistributionIntermediate] ([Id]),
    CONSTRAINT [FK_DistributionIntermediateDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionIntermediateDetail] NOCHECK CONSTRAINT [FK_DistributionIntermediateDetail_DistributionIntermediate];


GO
ALTER TABLE [InteropCost].[DistributionIntermediateDetail] NOCHECK CONSTRAINT [FK_DistributionIntermediateDetail_ProductionCenter];

