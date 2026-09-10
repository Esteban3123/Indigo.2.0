CREATE TABLE [MixingStation].[DefectsUnitDoseType] (
    [Id]                           INT IDENTITY (1, 1) NOT NULL,
    [Id_DefectsClassificationItem] INT NOT NULL,
    [Id_UnitDoseType]              INT NOT NULL,
    CONSTRAINT [PK_DefectsUnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DefectsUnitDoseType_DefectClassificationItem] FOREIGN KEY ([Id_DefectsClassificationItem]) REFERENCES [MixingStation].[DefectClassificationItem] ([Id]),
    CONSTRAINT [FK_DefectsUnitDoseType_UniDoseType] FOREIGN KEY ([Id_UnitDoseType]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Unidad de Tipo de Dosis (FK a MixingStation.UnitDoseType). Referencia la presentación/formato de la dosis unitaria (comprimido, cápsula, inyectable, etc.). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Tipo de dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Defecto o Clasificación de Defecto (FK a MixingStation.DefectClassificationItem). Referencia el tipo/categoría de defecto detectado en la preparación de dosis unitarias. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_DefectsClassificationItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Defecto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_DefectsClassificationItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_DefectsClassificationItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica (IDENTITY 1,1) de la tabla de asociación entre defectos y tipos de dosis unitarias. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre tipos de defectos clasificados y tipos de dosis unitaria en la estación de mezclas; permite asociar qué categorías de defectos aplican a cada tipo de unidad de dosis preparada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType';
