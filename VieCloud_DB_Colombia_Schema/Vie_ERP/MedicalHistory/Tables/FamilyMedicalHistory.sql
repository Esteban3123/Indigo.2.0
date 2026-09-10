CREATE TABLE [MedicalHistory].[FamilyMedicalHistory] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [PatientCode]      VARCHAR (25)   NOT NULL,
    [AdmissionNumber]  CHAR (10)      NOT NULL,
    [NUMEFOLIO]        CHAR (10)      NOT NULL,
    [CenterAttention]  CHAR (10)      NOT NULL,
    [FunctionalUnit]   CHAR (10)      NOT NULL,
    [ProfessionalCode] CHAR (20)      NOT NULL,
    [DiagnosticCode]   CHAR (4)       NULL,
    [Relationship]     INT            NULL,
    [Observations]     VARCHAR (1000) NULL,
    [Status]           BIT            NOT NULL,
    [TypeHistory]      INT            NOT NULL,
    [DateRegistration] DATETIME       NOT NULL,
    [IDHCMOANULB]      CHAR (4)       NULL,
    CONSTRAINT [PK_FamilyMedicalHistory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo por el cual no se registró el antecedente médico o familiar (CHAR 4, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo motivo no registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del antecedente médico o familiar en el sistema (DATETIME, obligatorio).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'DateRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'DateRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'DateRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de antecedente registrado: 1=Médico (historial clínico personal), 2=Familiares (historia de familiares con diagnósticos relevantes) (INT, obligatorio).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'TypeHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo antecedente:
1. Medico
2. Familiares', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'TypeHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'TypeHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de antecedente: 0=Inactivo/Eliminado, 1=Activo/Vigente (BIT, obligatorio).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:
0. Inactivo
1. Activo
', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, observaciones clínicas adicionales, detalles o comentarios sobre el antecedente registrado (VARCHAR 1000, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parentesco del familiar con antecedente: 1=Padres, 2=Hermanos, 3=Tíos, 4=Abuelos (INT, opcional, FK a catálogo).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Relationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parentesco:
1. Padres
2. Hermanos
3. Tíos
4. Abuelos', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Relationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Relationship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 del diagnóstico médico o enfermedad asociada al antecedente (CHAR 4, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del profesional de la salud (médico, enfermera, etc.) que registró el antecedente (CHAR 20, obligatorio).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional de la salud que registro el antecedente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, área clínica, especialidad) donde se registró el antecedente (CHAR 10, obligatorio).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad funcional donde se registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución o IPS donde se registró el antecedente (CHAR 10, obligatorio).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'CenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion donde se registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'CenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'CenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o página de la HC (historia clínica) donde consta el registro del antecedente (CHAR 10, obligatorio).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de folio de la HC donde se registro el evento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso, atención o admisión del paciente vinculada a este antecedente (CHAR 10, obligatorio, FK).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a cédula, identificación, documento o número de afiliado (VARCHAR 25, obligatorio, FK, PII).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo, Identity) del registro de antecedente en la tabla (INT, clave primaria).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de antecedentes médicos y familiares del paciente en la HC, incluye historial clínico personal y parentesco con diagnósticos asociados, vinculado a ingreso, centro de atención y profesional registrador.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de antecedentes médicos y familiares', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'FamilyMedicalHistory';

