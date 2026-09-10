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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de dosis unitaria (INT, FK a UnitDoseType). Clasifica la categoría de medicamento o preparado en dosis unitaria dispensado desde la estación de mezclado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paciente en centro externo (INT, FK a PatientExternalCareCenter). Se asigna cuando la cabecera de solicitud indica tipo paciente. Vincula el registro al paciente del centro de atención externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'PatientExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paciente externo, se asigna cuando en la cabecera el tipo de solicitud sea paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'PatientExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'PatientExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de solicitud (INT, FK a RequestUnitDoseExternalCareCenter). Referencia la solicitud principal de dosis unitaria desde el centro externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT, PK Identity). Clave primaria que identifica unívocamente cada solicitud de dosis unitaria asociada a paciente en centro externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pacientes de centros de atención externos asociados a solicitudes de dosis unitaria en la estación de mezclas. Relaciona cada solicitud de dosis unitaria externa con el paciente, la unidad funcional y la cama correspondiente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional externa (servicio o área de hospitalización) donde se encuentra el paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'ExternalFunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'ExternalFunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador de la cama asignada al paciente en el centro de atención externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'Bed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'Bed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nutrición parenteral total (NPT) asociada a la solicitud de dosis unitaria del paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'NptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseExternalCareCenterPatient', @level2type = N'COLUMN', @level2name = N'NptId';
