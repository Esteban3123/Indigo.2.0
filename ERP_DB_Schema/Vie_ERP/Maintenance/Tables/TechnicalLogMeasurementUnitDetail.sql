CREATE TABLE [Maintenance].[TechnicalLogMeasurementUnitDetail] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdTechnicalLog]    INT NOT NULL,
    [IdMeasurementUnit] INT NOT NULL,
    CONSTRAINT [PK_TechnicalLogMeasurementUnitDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalLogMeasurementUnitDetail_MeasurementUnit] FOREIGN KEY ([IdMeasurementUnit]) REFERENCES [Maintenance].[MeasurementUnit] ([Id]),
    CONSTRAINT [FK_TechnicalLogMeasurementUnitDetail_TechnicalLog] FOREIGN KEY ([IdTechnicalLog]) REFERENCES [Maintenance].[TechnicalLog] ([Id])
);


GO
ALTER TABLE [Maintenance].[TechnicalLogMeasurementUnitDetail] NOCHECK CONSTRAINT [FK_TechnicalLogMeasurementUnitDetail_MeasurementUnit];


GO
ALTER TABLE [Maintenance].[TechnicalLogMeasurementUnitDetail] NOCHECK CONSTRAINT [FK_TechnicalLogMeasurementUnitDetail_TechnicalLog];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida asociada a el registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro tecnico asociado a la unidad de medida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de relacion entre el registro tecnico y unidades de medida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';

