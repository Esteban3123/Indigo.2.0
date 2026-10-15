CREATE TABLE [Cost].[CostLogisticsProductionCenterRecordDetail] (
    [Id]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LogisticsProductionCenterRecordId] INT             NOT NULL,
    [ProductionCenterId]                INT             NOT NULL,
    [InventoryMeasurementUnitId]        INT             NOT NULL,
    [Count]                             DECIMAL (18, 2) CONSTRAINT [DF_CostLogisticsProductionCenterRecordDetail_Count] DEFAULT ((0)) NOT NULL,
    [Import]                            BIT             CONSTRAINT [DF_CostLogisticsProductionCenterRecordDetail_Import] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostLogisticsProductionCenterRecordDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostLogisticsProductionCenterRecordDetail_CostLogisticsProductionCenterRecord] FOREIGN KEY ([LogisticsProductionCenterRecordId]) REFERENCES [Cost].[CostLogisticsProductionCenterRecord] ([Id]),
    CONSTRAINT [FK_CostLogisticsProductionCenterRecordDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostLogisticsProductionCenterRecordDetail_InventoryMeasurementUnit] FOREIGN KEY ([InventoryMeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostLogisticsProductionCenterRecordDetail] NOCHECK CONSTRAINT [FK_CostLogisticsProductionCenterRecordDetail_CostLogisticsProductionCenterRecord];


GO
ALTER TABLE [Cost].[CostLogisticsProductionCenterRecordDetail] NOCHECK CONSTRAINT [FK_CostLogisticsProductionCenterRecordDetail_CostProductionCenter];


GO
ALTER TABLE [Cost].[CostLogisticsProductionCenterRecordDetail] NOCHECK CONSTRAINT [FK_CostLogisticsProductionCenterRecordDetail_InventoryMeasurementUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el registro ya fue importado  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';

