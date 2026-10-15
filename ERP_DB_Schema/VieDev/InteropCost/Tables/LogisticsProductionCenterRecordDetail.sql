CREATE TABLE [InteropCost].[LogisticsProductionCenterRecordDetail] (
    [Id]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LogisticsProductionCenterRecordId] INT             NOT NULL,
    [ProductionCenterId]                INT             NOT NULL,
    [InventoryMeasurementUnitId]        INT             NOT NULL,
    [Count]                             DECIMAL (18, 2) CONSTRAINT [DF_LogisticsProductionCenterRecordDetail_Count] DEFAULT ((0)) NOT NULL,
    [Import]                            BIT             CONSTRAINT [DF_LogisticsProductionCenterRecordDetail_Import] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LogisticsProductionCenterRecordDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LogisticsProductionCenterRecordDetail_InventoryMeasurementUnit] FOREIGN KEY ([InventoryMeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_LogisticsProductionCenterRecordDetail_LogisticsProductionCenterRecord] FOREIGN KEY ([LogisticsProductionCenterRecordId]) REFERENCES [InteropCost].[LogisticsProductionCenterRecord] ([Id]),
    CONSTRAINT [FK_LogisticsProductionCenterRecordDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[LogisticsProductionCenterRecordDetail] NOCHECK CONSTRAINT [FK_LogisticsProductionCenterRecordDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [InteropCost].[LogisticsProductionCenterRecordDetail] NOCHECK CONSTRAINT [FK_LogisticsProductionCenterRecordDetail_LogisticsProductionCenterRecord];


GO
ALTER TABLE [InteropCost].[LogisticsProductionCenterRecordDetail] NOCHECK CONSTRAINT [FK_LogisticsProductionCenterRecordDetail_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el registro ya fue importado  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';

