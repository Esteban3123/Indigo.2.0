CREATE TABLE [InteropCost].[DirectDistributionSecondaryDetail] (
    [Id]                            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DirectDistributionSecondaryId] INT            NOT NULL,
    [ProductionCenterId]            INT            NOT NULL,
    [MeasurementUnitId]             INT            NOT NULL,
    [Percentage]                    NUMERIC (5, 2) CONSTRAINT [DF_DirectDistributionSecondaryDetail_Percentage] DEFAULT ((0)) NOT NULL,
    [Value]                         NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_DirectDistributionSecondaryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DirectDistributionSecondaryDetail_DirectDistributionSecondary] FOREIGN KEY ([DirectDistributionSecondaryId]) REFERENCES [InteropCost].[DirectDistributionSecondary] ([Id]),
    CONSTRAINT [FK_DirectDistributionSecondaryDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_DirectDistributionSecondaryDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_DirectDistributionSecondaryDetail_DirectDistributionSecondary];


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_DirectDistributionSecondaryDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_DirectDistributionSecondaryDetail_ProductionCenter];

