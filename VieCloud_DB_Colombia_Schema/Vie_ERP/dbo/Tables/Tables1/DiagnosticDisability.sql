CREATE TABLE [dbo].[DiagnosticDisability] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [CODDIAGNO]   CHAR (4)     NULL,
    [IdHCINCAPAC] NUMERIC (18) NULL,
    CONSTRAINT [PK__Diagnost__3214EC077068491A] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiagnosticDisability_HCINCAPAC] FOREIGN KEY ([IdHCINCAPAC]) REFERENCES [dbo].[HCINCAPAC] ([CODCONSEC]),
    CONSTRAINT [FK_DiagnosticDisability_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[DiagnosticDisability] NOCHECK CONSTRAINT [FK_DiagnosticDisability_HCINCAPAC];




GO
ALTER TABLE [dbo].[DiagnosticDisability] NOCHECK CONSTRAINT [FK_DiagnosticDisability_HCINCAPAC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (NUMERIC 18) que relaciona el diagnóstico con la cabecera de incapacidades en HCINCAPAC. Clave foránea que vincula a la Historia Clínica de Incapacidades (CODCONSEC). Usado para asociar diagnósticos de discapacidad con periodos de incapacidad laboral o funcional del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para relacion con cabecera de incapacidades HCINCAPAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CHAR 4) según nomenclatura CIE-10 u estándar de clasificación clínica. Clave foránea que referencia INDIAGNOS. Identifica la condición de discapacidad, incapacidad permanente o limitación funcional del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la tabla DiagnosticDisability. Clave primaria que genera secuencial automático. Índice de búsqueda para registros de diagnósticos asociados a incapacidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona diagnósticos médicos (CIE-10) con incapacidades registradas en la historia clínica. Permite saber qué diagnóstico fundamenta cada incapacidad o discapacidad otorgada al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DiagnosticDisability';
