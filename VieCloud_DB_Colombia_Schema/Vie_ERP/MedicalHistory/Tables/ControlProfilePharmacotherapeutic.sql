CREATE TABLE [MedicalHistory].[ControlProfilePharmacotherapeutic] (
    [id]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PatientCode]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PharmaProfileChange] BIT                                                                              NOT NULL,
    [CreateDate]          DATETIME                                                                         NOT NULL,
    [CreateProfessional]  CHAR (20)                                                                        NOT NULL,
    [ModifyDate]          DATETIME                                                                         NULL,
    [ModifyProfessional]  CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_ControlProfilePharmacotherapeutic] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPACIENT] FOREIGN KEY ([PatientCode]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPROFSAL] FOREIGN KEY ([CreateProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPROFSAL1] FOREIGN KEY ([ModifyProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MedicalHistory].[ControlProfilePharmacotherapeutic].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que realizó la última modificación del perfil farmacoterapéutico (médico, farmacéutico, enfermero). FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda quien modifico  ( profesional)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de control del perfil farmacoterapéutico del paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que creó el registro de control del perfil farmacoterapéutico (médico, farmacéutico, enfermero). FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de control del perfil farmacoterapéutico del paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=Sí hubo cambio en el perfil farmacoterapéutico, 0=No hubo cambio. Registra modificaciones en medicamentos prescritos.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PharmaProfileChange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cambio de perfil farmacéutico  1= si     0 = No', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PharmaProfileChange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PharmaProfileChange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, identificación, documento de identidad). PII ofuscado. FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (IDENTITY) del registro de control farmacoterapéutico en la tabla.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control y auditoría de cambios en el perfil farmacoterapéutico de cada paciente, indicando si hubo modificaciones en su perfil de medicamentos y quién realizó el registro o la última actualización.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic';
