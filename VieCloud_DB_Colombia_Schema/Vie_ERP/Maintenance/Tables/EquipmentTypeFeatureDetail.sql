CREATE TABLE [Maintenance].[EquipmentTypeFeatureDetail] (
    [Id]              INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentType] INT         NOT NULL,
    [TagControl]      VARCHAR (3) NOT NULL,
    CONSTRAINT [PK_EquipmentTypeFeatureDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeFeatureDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiqueta de control (VARCHAR 3) que identifica el tipo de control a visualizar en el registro del equipo; código de presentación de la característica técnica en interfaz.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'TagControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tag del control que se debe mostrar en el registro del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'TagControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'TagControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo (FK a FixedAssetItemType) asociado a la característica técnica; representa la clasificación o categoría del activo fijo relacionado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de equipo relacionado a la caracteristica tecnica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) que actúa como clave primaria y consecutivo de la relación entre características técnicas y tipos de equipo en mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo d ela tabla de relacion entre Caracteristicas y Tipo de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de características o atributos de control asociados a cada tipo de equipo médico o de mantenimiento. Permite definir qué etiquetas o controles aplican según la categoría del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail';
