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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de importación del registro de detalle (1=Ya importado, 0=Pendiente). Indica si la cantidad fue procesada e integrada en el sistema logístico. Tipo: BIT, Default: 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro ya fue importado  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Import';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad registrada de insumos, medicamentos o productos en el centro de producción. Expresada en la unidad de medida especificada. Tipo: DECIMAL(18,2), Default: 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad registrada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Count';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (unidad, caja, frasco, litro, etc.) utilizada para registrar la cantidad. Referencia a Inventory.InventoryMeasurementUnit. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'InventoryMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad de medida usada para el registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'InventoryMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'InventoryMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción, unidad funcional o área asistencial (farmacia, laboratorio, quirófano, etc.) donde se registran las cantidades de insumos. Referencia a InteropCost.ProductionCenter. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de producción al que se le registran cantidades', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o registro maestro de logística del centro de producción. Referencia a InteropCost.LogisticsProductionCenterRecord. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'LogisticsProductionCenterRecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'LogisticsProductionCenterRecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'LogisticsProductionCenterRecordId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de registro logístico del centro de producción. Clave primaria. Tipo: INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los registros de producción logística por centro: almacena la cantidad de unidades de un producto asignadas a cada centro de producción dentro de un registro logístico, indicando la unidad de medida de inventario utilizada y si el ítem fue importado.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecordDetail';
