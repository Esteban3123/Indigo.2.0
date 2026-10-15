CREATE TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatientDetails] (
    [Id]                                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestUnitDoseExternalCareCenterPatientId] INT             NOT NULL,
    [ATCId]                                      INT             NOT NULL,
    [Dosage]                                     DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId]                          INT             NOT NULL,
    [Quantity]                                   INT             NOT NULL,
    [Observations]                               VARCHAR (MAX)   NULL,
    [SendTo]                                     TINYINT         NULL,
    [AdministrationRouteId]                      INT             CONSTRAINT [DF_RequestUnitDoseExternalCareCenterPatientDetails_AdministrationRouteId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RequestUnitDoseExternalCareCenterPatientDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_RequestUnitDoseExternalCareCenterPatient] FOREIGN KEY ([RequestUnitDoseExternalCareCenterPatientId]) REFERENCES [MixingStation].[RequestUnitDoseExternalCareCenterPatient] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatientDetails] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_AdministrationRoute];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatientDetails] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_ATC];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatientDetails] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_InventoryMeasurementUnit];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatientDetails] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatientDetails_RequestUnitDoseExternalCareCenterPatient];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la vía de administración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que especifica a donde se envía el registro:  1 - Enviar para procesar en central de mezclas  2 - Enviar para procesar en dashboard farmacia    Este campo se asigna en la dashboard confirmación dosis unitarias(ERP) al momento de ver el detalle de aplicación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'SendTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación, se asigna al momento de guardar en detalle de aplicación en dashboard confirmación dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Dosage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterPatientId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatientDetails', @level2type = N'COLUMN', @level2name = N'Id';

