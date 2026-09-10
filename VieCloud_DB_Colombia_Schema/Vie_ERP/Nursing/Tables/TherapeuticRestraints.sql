CREATE TABLE [Nursing].[TherapeuticRestraints] (
    [Id]                              INT                                                                              IDENTITY (1, 1) NOT NULL,
    [PatientCode]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [AdmissionNumber]                 CHAR (10)                                                                        NOT NULL,
    [HealthCenter]                    CHAR (10)                                                                        NOT NULL,
    [HealthUnit]                      CHAR (10)                                                                        NOT NULL,
    [OrderHealthcareProfessional]     CHAR (20)                                                                        NOT NULL,
    [OrderDate]                       DATETIME                                                                         NOT NULL,
    [Status]                          BIT                                                                              NOT NULL,
    [SuspendedHealthcareProfessional] CHAR (20)                                                                        NULL,
    [SuspendedDate]                   DATETIME                                                                         NULL,
    [CodeSuspendedJustification]      CHAR (4)                                                                         NULL,
    [SuspendedObservation]            VARCHAR (300)                                                                    NULL,
    CONSTRAINT [PK_TherapeuticRestraints] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Nursing].[TherapeuticRestraints].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o comentario registrado al momento de suspender la restricción terapéutica; campo de texto libre (VARCHAR 300) para documentar el motivo clínico o administrativo de la suspensión.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación suspendida de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de justificación de la suspensión de la restricción terapéutica; clasificador de 4 caracteres que identifica la razón (clínica, administrativa, médica) del levantamiento de la medida.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeSuspendedJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de la suspensión del código de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeSuspendedJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeSuspendedJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se suspendió o levantó la restricción terapéutica; DATETIME que registra cuándo el profesional sanitario autorizó el fin de la medida de contención.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de suspención de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del profesional de la salud (médico, enfermero, terapeuta) que ordenó la suspensión de la restricción terapéutica; CHAR 20.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedHealthcareProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional sanitario suspendido', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedHealthcareProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedHealthcareProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado o indicador activo/inactivo (BIT: 1=activa, 0=inactiva) de la restricción terapéutica en el momento actual del ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se ordenó o prescribió la restricción terapéutica; DATETIME que marca el inicio de la medida de contención o seguridad.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de pedido', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del profesional de la salud (médico responsable, terapeuta) que ordenó o prescribió la restricción terapéutica; CHAR 20.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderHealthcareProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden del profesional sanitario', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderHealthcareProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderHealthcareProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional, piso, área o servicio clínico (psiquiatría, cuidados intensivos, urgencias) donde se aplica la restricción terapéutica; CHAR 10.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de salud', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro, sede u hospital donde se registra la restricción terapéutica del paciente en ingreso; CHAR 10.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de salud', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso, admisión o radicado del paciente en el centro de atención; identificador CHAR 10 que vincula la restricción a un evento de hospitalización.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de admisión', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a número de cédula, documento de identidad u otro identificador único (PII/Identification_Ofuscado en búsquedas); VARCHAR 25 enmascarado.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo (IDENTITY) de cada registro de restricción terapéutica en la tabla; clave primaria INT para auditoría y trazabilidad clínica.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de restricciones terapéuticas (sujeciones o contenciones físicas) ordenadas a pacientes durante su hospitalización. Incluye quién ordenó la restricción, cuándo fue indicada y, si aplica, su suspensión con justificación.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints';
