CREATE TABLE [Maintenance].[EquipmentTypeMeasurementUnitDetail] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdTechnicalLogMeasurementUnitDetail] INT NOT NULL,
    [IdEquipmentType]                     INT NOT NULL,
    CONSTRAINT [PK_EquipmentTypeMeasurementUnitDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeMeasurementUnitDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_EquipmentTypeMeasurementUnitDetail_EquipmentTypeMeasurementUnitDetail1] FOREIGN KEY ([IdTechnicalLogMeasurementUnitDetail]) REFERENCES [Maintenance].[EquipmentTypeMeasurementUnitDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo (activo fijo) relacionado a esta unidad de medida; clave foránea a FixedAssetItemType para clasificar equipamiento médico, hospitalario o de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo relacionado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de unidad de medida del registro técnico; referencia recursiva a otra configuración de medición asociada en el log de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLogMeasurementUnitDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida asociada en el registro tecnico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLogMeasurementUnitDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLogMeasurementUnitDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (Identity) de la relación entre tipo de equipo y unidad de medida; clave primaria de la tabla de configuración de unidades de medida por equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de Unidades de Medida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los tipos de equipos médicos con las unidades de medida técnicas utilizadas en sus registros de mantenimiento y bitácora. Permite definir qué unidades de medida aplican a cada tipo de equipo para el seguimiento de parámetros técnicos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail';
