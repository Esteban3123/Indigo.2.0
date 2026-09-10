CREATE TABLE [dbo].[CHMOTDEMI] (
    [CODMOTDEI] CHAR (2)     NOT NULL,
    [DESMOTDEI] CHAR (250)   NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHMOTDEMI] PRIMARY KEY CLUSTERED ([CODMOTDEI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o indicador de auditoría; flag numérico que marca registros auditados o sujetos a revisión de conformidad normativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de demanda insatisfecha; descripción textual del porqué un paciente no fue atendido, rechazado o quedó sin cobertura (negación, ausencia de cita, falta de recurso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'DESMOTDEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'DESMOTDEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'DESMOTDEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de demanda insatisfecha; identificador alfanumérico (2 caracteres) que clasifica la razón de no prestación del servicio de salud al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Demanda Insatisfecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI', @level2type = N'COLUMN', @level2name = N'CODMOTDEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos de demanda o solicitud de atención médica. Registra los posibles motivos por los cuales un paciente solicita o requiere una atención, consulta o ingreso al sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHMOTDEMI';
