CREATE TABLE [MixingStation].[LineClearanceCriteriaUnitDoseType] (
    [Id]                      INT IDENTITY (1, 1) NOT NULL,
    [LineClearanceCriteriaId] INT NOT NULL,
    [UnitDoseTypeId]          INT NOT NULL,
    CONSTRAINT [PK_LineClearanceCriteriaUnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LineClearanceCriteriaUnitDoseType_LineClearanceCriteria] FOREIGN KEY ([LineClearanceCriteriaId]) REFERENCES [MixingStation].[LineClearanceCriteria] ([Id]),
    CONSTRAINT [FK_LineClearanceCriteriaUnitDoseType_UniDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de unidad de dosis (FK a UnitDoseType). Clasifica la presentación o formato de dosis unitaria en estaciones de mezcla farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Tipo de dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de criterio de despeje de línea (FK a LineClearanceCriteria). Referencia los estándares de limpieza y validación requeridos entre corridas de producción en líneas de mezclado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'LineClearanceCriteriaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id criterio de despeje de linea', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'LineClearanceCriteriaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'LineClearanceCriteriaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) de la asociación entre criterios de despeje y tipos de dosis unitaria en la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los criterios de limpieza de línea (line clearance) con los tipos de dosis unitaria aplicables en la estación de mezcla. Permite definir qué tipos de preparación en dosis unitaria deben cumplir cada criterio de verificación antes de iniciar o continuar el proceso de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType';
