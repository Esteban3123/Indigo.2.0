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
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_ProductType];




GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [MixingStation].[MixingStationSetting] NOCHECK CONSTRAINT [FK_MixingStationSetting_ProductType];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén de tránsito o almacén temporal donde se almacenan productos en proceso de mezcla antes de su distribución final. Referencia a Inventory.Warehouse.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'TransitWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id  Almacén de Transito ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'TransitWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'TransitWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de empaque o presentación del producto (caja, blíster, frasco, etc.). Referencia a Inventory.PackagingUnit, define cómo se empaca el producto mezclado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'PackageUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de empaque', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'PackageUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'PackageUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de la estación de mezcla (mixing station): define los parámetros operativos y contables para la preparación de mezclas o fórmulas, incluyendo la unidad operativa responsable, el fabricante, los conceptos de ajuste de inventario de entrada y salida, la unidad de medida y el tipo de producto que se maneja.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de configuración de la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad operativa o servicio al que pertenece esta configuración de estación de mezcla (por ejemplo, farmacia, nutrición).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable o de inventario usado para registrar las salidas de insumos durante el proceso de mezcla (consumo, egreso de bodega).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentConceptOutputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentConceptOutputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable o de inventario usado para registrar las entradas o devoluciones de insumos al inventario en el proceso de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentConceptInputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentConceptInputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Laboratorio o fabricante asociado a los productos preparados en esta estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de vigencia o vida útil asignado por defecto a los productos preparados en la estación de mezcla (fecha de vencimiento del preparado).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ExpirationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ExpirationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida principal utilizada para los productos o mezclas preparados (por ejemplo, mililitros, gramos, unidades).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto resultante de la mezcla (por ejemplo, nutrición parenteral, mezcla oncológica, preparado magistral).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de configuración de la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó la configuración de la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación a la configuración de la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación realizada a la configuración de la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de versión interno del registro, usado para detectar cambios concurrentes y garantizar integridad de los datos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSetting', @level2type = N'COLUMN', @level2name = N'Timestamp';
