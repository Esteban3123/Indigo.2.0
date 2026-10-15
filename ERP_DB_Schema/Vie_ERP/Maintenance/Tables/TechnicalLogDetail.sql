CREATE TABLE [Maintenance].[TechnicalLogDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentRegistration] INT          NOT NULL,
    [IdTechnicalLog]          INT          NOT NULL,
    [Name]                    VARCHAR (50) NOT NULL,
    [ValueMin]                VARCHAR (10) NULL,
    [ValueMax]                VARCHAR (10) NULL,
    [IdMeasurementUnit]       INT          NOT NULL,
    CONSTRAINT [PK_TechnicalLogDetailed] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalLogDetail_EquipmentRegistration] FOREIGN KEY ([IdEquipmentRegistration]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_TechnicalLogDetail_TechnicalLog] FOREIGN KEY ([IdTechnicalLog]) REFERENCES [Maintenance].[TechnicalLog] ([Id]),
    CONSTRAINT [FK_TechnicalLogDetailed_MeasurementUnit] FOREIGN KEY ([IdMeasurementUnit]) REFERENCES [Maintenance].[MeasurementUnit] ([Id])
);


GO
ALTER TABLE [Maintenance].[TechnicalLogDetail] NOCHECK CONSTRAINT [FK_TechnicalLogDetail_EquipmentRegistration];


GO
ALTER TABLE [Maintenance].[TechnicalLogDetail] NOCHECK CONSTRAINT [FK_TechnicalLogDetail_TechnicalLog];


GO
ALTER TABLE [Maintenance].[TechnicalLogDetail] NOCHECK CONSTRAINT [FK_TechnicalLogDetailed_MeasurementUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida asociada.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro tecnico cabecera asociado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la recepcion del equipo asociado  a el detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del detalle de registro tecnicop', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Id';

