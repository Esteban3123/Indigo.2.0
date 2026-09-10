CREATE TABLE [Authorization].[PatientConfirmation] (
    [Id]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PatientCode]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [AdmisionNumber]   CHAR (10)                                                                        NOT NULL,
    [ConfirmationUser] VARCHAR (20)                                                                     NOT NULL,
    [ConfirmationDate] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_PatientConfirmation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Authorization].[PatientConfirmation].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se completó y registró la confirmación del paciente. Marca el timestamp exacto de autorización o validación de ingreso.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referente a la fecha que se completo la confirmación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario (VARCHAR 20) que realizó la confirmación del paciente. Vinculado al profesional de salud o administrativo autorizante.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referente al codigo del usuario que realizó la confirmación del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión o ingreso del paciente (CHAR 10). Identifica el evento de atención, hospitalización o urgencia asociado a la confirmación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'AdmisionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referente a el numero de admisión del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'AdmisionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'AdmisionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, enmascarado PII). Equivalente a cédula, documento o identificación del paciente. Corresponde al campo IPCODPACI de tabla dbo.INPACIENT. Búsquedas: identificación, cédula, documento paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referente al codigo del paciente, este codigo es el mismo del campo IPCODPACI de la tabla dbo.INPACIENT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la confirmación del paciente. Valor numérico autoincrementable que registra cada confirmación realizada.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la confirmación del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de confirmaciones de pacientes en el proceso de autorización: guarda quién y cuándo confirmó la identidad o asistencia de un paciente para un ingreso específico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation';
