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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de importación del registro de logística (1=Importado, 0=No importado). Booleano que marca si el detalle ya fue procesado en el sistema de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro ya fue importado  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad registrada en unidades de inventario. Valor decimal con precisión de dos decimales para registro de existencias en centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad registrada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Count';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (UOM) utilizada en el registro. FK a tabla InventoryMeasurementUnit; puede ser: unidad, kilogramo, litro, metro, etc.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'InventoryMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad de medida usada para el registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'InventoryMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'InventoryMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción, unidad funcional o departamento al cual se registran las cantidades de inventario. FK a CostProductionCenter.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de producción al que se le registran cantidades', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o encabezado del registro de logística de producción. FK a CostLogisticsProductionCenterRecord; agrupa múltiples detalles.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'LogisticsProductionCenterRecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'LogisticsProductionCenterRecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'LogisticsProductionCenterRecordId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de registro logístico de centro de producción. Clave primaria IDENTITY, autoincremental.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los registros logísticos por centro de producción en el módulo de costos. Cada fila indica la cantidad de un insumo o producto (con su unidad de medida) asociada a un registro logístico de un centro de producción específico, permitiendo el costeo y seguimiento de consumos por área productiva.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostLogisticsProductionCenterRecordDetail';
