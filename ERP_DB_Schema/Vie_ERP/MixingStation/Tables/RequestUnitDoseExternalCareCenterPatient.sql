CREATE TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatient] (
    [Id]                                  INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestUnitDoseExternalCareCenterId] INT         NOT NULL,
    [PatientExternalCareCenterId]         INT         NOT NULL,
    [UnitDoseTypeId]                      INT         NOT NULL,
    [ExternalFunctionalUnitCode]          VARCHAR (1) NULL,
    [Bed]                                 VARCHAR (1) NULL,
    [NptId]                               INT         NULL,
    CONSTRAINT [PK_RequestUnitDoseExternalCareCenterPatient_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatient_PatientExternalCareCenter] FOREIGN KEY ([PatientExternalCareCenterId]) REFERENCES [MixingStation].[PatientExternalCareCenter] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatient_RequestUnitDoseExternalCareCenter1] FOREIGN KEY ([RequestUnitDoseExternalCareCenterId]) REFERENCES [MixingStation].[RequestUnitDoseExternalCareCenter] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatient_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatient] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatient_PatientExternalCareCenter];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseExternalCareCenterPatient] NOCHECK CONSTRAINT [FK_RequestUnitDoseExternalCareCenterPatient_RequestUnitDoseExternalCareCenter1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paciente externo, se asigna cuando en la cabecera el tipo de solicitud sea paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'PatientExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'Id';

