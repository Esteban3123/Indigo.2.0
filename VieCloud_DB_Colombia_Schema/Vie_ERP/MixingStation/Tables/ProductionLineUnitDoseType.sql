CREATE TABLE [MixingStation].[ProductionLineUnitDoseType] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Id_ProductionLine] INT NOT NULL,
    [Id_UnitDoseType]   INT NOT NULL,
    CONSTRAINT [PK_ProductionLineUnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionLineUnitDoseType_ProductionLine] FOREIGN KEY ([Id_ProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id]),
    CONSTRAINT [FK_ProductionLineUnitDoseType_UniDoseType] FOREIGN KEY ([Id_UnitDoseType]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de unidad de dosis (FK a UnitDoseType). Referencia la categoría o clasificación de dosis unitaria asociada a la línea de producción, como dosis individual, dosis doble, etc. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Tipo de dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de línea de producción (FK a ProductionLine). Referencia la línea productiva de la estación de mezcla donde se preparan unidades de dosis. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (PRIMARY KEY). Clave única que identifica cada relación entre línea de producción y tipo de unidad de dosis. Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre líneas de producción y tipos de dosis unitaria en la estación de mezcla. Indica qué tipos de dosis unitaria puede procesar cada línea de producción farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType';
