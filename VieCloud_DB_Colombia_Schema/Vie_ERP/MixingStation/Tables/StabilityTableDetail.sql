CREATE TABLE [MixingStation].[StabilityTableDetail] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [StabilityTableId]     INT           NOT NULL,
    [ATCId]                INT           NOT NULL,
    [ProductId]            INT           NOT NULL,
    [UnitDoseTypeId]       INT           NOT NULL,
    [AllowableDoses]       INT           NOT NULL,
    [Observations]         VARCHAR (300) NULL,
    [HourStabilityProduct] INT           NULL,
    CONSTRAINT [PK_StabilityTableDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetail_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_StabilityTableDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_StabilityTableDetail_StabilityTable] FOREIGN KEY ([StabilityTableId]) REFERENCES [MixingStation].[StabilityTable] ([Id]),
    CONSTRAINT [FK_StabilityTableDetail_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas adicionales o comentarios sobre estabilidad del producto, tiempo de conservación, condiciones de almacenamiento o restricciones especiales (VARCHAR 300, nullable)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis permisibles, cantidad máxima de dosis unitarias permitidas del medicamento en la mezcla o preparación (INT, requerido)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'AllowableDoses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis permisibles', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'AllowableDoses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'AllowableDoses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de dosis unitaria (unidad, ampolla, vial, comprimido, etc.), clave foránea a MixingStation.UnitDoseType (INT, requerido)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto de inventario, medicamento o fármaco asociado en la tabla Inventory.InventoryProduct (INT, requerido)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto que tenga asociado el medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la clasificación ATC del medicamento, clave foránea a Inventory.ATC para código terapéutico farmacológico (INT, requerido)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla de estabilidad padre en MixingStation.StabilityTable, agrupa detalles de estabilidad de medicamentos (INT, requerido)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'StabilityTableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'StabilityTableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'StabilityTableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle de estabilidad, clave primaria (INT IDENTITY, autoincremental)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de estabilidad de productos farmacéuticos en la estación de mezclas. Registra, por cada medicamento y tipo de dosis unitaria, cuántas dosis son permitidas y durante cuántas horas el producto mantiene su estabilidad una vez preparado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas de estabilidad del producto farmacéutico después de su preparación o mezcla; indica el tiempo máximo en que el medicamento conserva sus propiedades y puede ser administrado de forma segura.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'HourStabilityProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'HourStabilityProduct';
