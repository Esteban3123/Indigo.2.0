CREATE TABLE [Admissions].[ComplementaryAdmissionsDiagnostic] (
    [id]                        INT           IDENTITY (1, 1) NOT NULL,
    [code]                      VARCHAR (6)   NOT NULL,
    [diagnostico]               VARCHAR (200) NOT NULL,
    [IdComplementaryAdmissions] BIGINT        NULL,
    CONSTRAINT [PK__Compleme__3213E83F6BF560EA] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK__Complemen__IdCom__5B166FB0] FOREIGN KEY ([IdComplementaryAdmissions]) REFERENCES [Admissions].[ComplementaryAdmissions] ([id]),
    CONSTRAINT [UQ__Compleme__357D4CF9A16C5F04] UNIQUE NONCLUSTERED ([code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos complementarios asociados a admisiones o ingresos adicionales del paciente. Registra los códigos CIE-10 y descripciones de diagnóstico vinculados a una admisión complementaria.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de diagnóstico complementario.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico, generalmente en formato CIE-10 (código de enfermedad o condición clínica).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del diagnóstico complementario asociado al ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'diagnostico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'diagnostico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la admisión complementaria a la que pertenece este diagnóstico; relaciona el registro con el ingreso adicional del paciente.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'IdComplementaryAdmissions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ComplementaryAdmissionsDiagnostic', @level2type = N'COLUMN', @level2name = N'IdComplementaryAdmissions';
