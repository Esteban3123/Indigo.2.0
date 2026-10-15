CREATE TABLE [MixingStation].[MixingStationSetting] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperativeUnitId]                    INT          NOT NULL,
    [InventoryAdjustmentConceptOutputId] INT          NOT NULL,
    [InventoryAdjustmentConceptInputId]  INT          NOT NULL,
    [ManufacturerId]                     INT          NOT NULL,
    [ExpirationDays]                     INT          NOT NULL,
    [MeasurementUnitId]                  INT          NOT NULL,
    [ProductTypeId]                      INT          NOT NULL,
    [PackageUnitId]                      INT          NOT NULL,
    [TransitWarehouseId]                 INT          NOT NULL,
    [CreationUser]                       VARCHAR (20) NOT NULL,
    [CreationDate]                       DATETIME     NOT NULL,
    [ModificationUser]                   VARCHAR (20) NULL,
    [ModificationDate]                   DATETIME     NULL,
    [Timestamp]                          ROWVERSION   NOT NULL,
    CONSTRAINT [PK_MixingStationSetting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MixingStationSetting_AdjustmentConcept_Input] FOREIGN KEY ([InventoryAdjustmentConceptInputId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_MixingStationSetting_AdjustmentConcept_Output] FOREIGN KEY ([InventoryAdjustmentConceptOutputId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_MixingStationSetting_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_MixingStationSetting_Manufacturer] FOREIGN KEY ([ManufacturerId]) REFERENCES [Inventory].[Manufacturer] ([Id]),
    CONSTRAINT [FK_MixingStationSetting_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_MixingStationSetting_PackagingUnit] FOREIGN KEY ([PackageUnitId]) REFERENCES [Inventory].[PackagingUnit] ([Id]),
    CONSTRAINT [FK_MixingStationSetting_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [Inventory].[ProductType] ([Id]),
    CONSTRAINT [FK_MixingStationSetting_Warehouse] FOREIGN KEY ([TransitWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [IX_MixingStationSetting] UNIQUE NONCLUSTERED ([OperativeUnitId] ASC)
);


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_AdjustmentConcept_Input];


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_AdjustmentConcept_Output];


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_InventoryMeasurementUnit];


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_Manufacturer];


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_OperatingUnit];


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_PackagingUnit];


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_ProductType];


GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id  Almacén de Transito ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'TransitWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de empaque', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'PackageUnitId';

