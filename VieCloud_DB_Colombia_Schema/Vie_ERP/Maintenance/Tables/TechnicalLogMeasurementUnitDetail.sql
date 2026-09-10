CREATE TABLE [Maintenance].[TechnicalLogMeasurementUnitDetail] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdTechnicalLog]    INT NOT NULL,
    [IdMeasurementUnit] INT NOT NULL,
    CONSTRAINT [PK_TechnicalLogMeasurementUnitDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalLogMeasurementUnitDetail_MeasurementUnit] FOREIGN KEY ([IdMeasurementUnit]) REFERENCES [Maintenance].[MeasurementUnit] ([Id]),
    CONSTRAINT [FK_TechnicalLogMeasurementUnitDetail_TechnicalLog] FOREIGN KEY ([IdTechnicalLog]) REFERENCES [Maintenance].[TechnicalLog] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la unidad de medida vinculada al registro técnico. Referencia a Maintenance.MeasurementUnit. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida asociada a el registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del registro técnico asociado a la unidad de medida. Referencia a Maintenance.TechnicalLog. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro tecnico asociado a la unidad de medida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado (PK) de la relación entre registro técnico y unidades de medida. IDENTITY(1,1). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de relacion entre el registro tecnico y unidades de medida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de unidades de medida asociadas a cada registro de bitácora técnica. Relaciona un log técnico con las unidades de medida utilizadas en ese registro de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail';
