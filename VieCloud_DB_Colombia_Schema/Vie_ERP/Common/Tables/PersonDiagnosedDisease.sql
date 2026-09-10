CREATE TABLE [Common].[PersonDiagnosedDisease] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]           INT NOT NULL,
    [DiagnosedDiseaseId] INT NOT NULL,
    CONSTRAINT [PK_PersonDiagnosedDisease__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonDiagnosedDisease_DiagnosedDisease] FOREIGN KEY ([DiagnosedDiseaseId]) REFERENCES [Payroll].[DiagnosedDisease] ([Id]),
    CONSTRAINT [FK_PersonDiagnosedDisease_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [UQ_PersonDiagnosedDisease__PersonId] UNIQUE NONCLUSTERED ([PersonId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la enfermedad diagnosticada (FK a [Payroll].[DiagnosedDisease]). Referencia el diagnóstico, patología o condición clínica registrada en el catálogo maestro de enfermedades diagnosticadas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'DiagnosedDiseaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave foranea con enfermedad diagnosticada', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'DiagnosedDiseaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'DiagnosedDiseaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la persona/paciente (FK a [Common].[Person]). Referencia el documento de identidad, cédula o código del paciente vinculado con la enfermedad diagnosticada. Campo UNIQUE: una sola enfermedad por persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave foranea con Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (INT IDENTITY) de la asociación entre persona y enfermedad diagnosticada. Clave primaria de la tabla de antecedentes patológicos o diagnósticos del paciente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de enfermedades diagnosticadas de la persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona personas con las enfermedades diagnosticadas que tienen registradas. Permite saber qué patologías o condiciones médicas han sido diagnosticadas a cada persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDiagnosedDisease';
