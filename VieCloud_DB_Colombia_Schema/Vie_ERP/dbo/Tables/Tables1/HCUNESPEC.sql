CREATE TABLE [dbo].[HCUNESPEC] (
    [CODCONCEC]  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UFUCODIGO]  CHAR (10) NOT NULL,
    [CODEXAME]   CHAR (3)  NOT NULL,
    [TIPHISTORI] CHAR (1)  NOT NULL,
    CONSTRAINT [PK_HCUNIESPEC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCUNESPEC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de historia clínica parametrizada; clasificación del registro clínico (consulta, urgencia, hospitalización, procedimiento). Char(1), referencia a catálogo de tipos de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'TIPHISTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de la historia clinica parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'TIPHISTORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'TIPHISTORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del examen físico; identificador del examen o evaluación clínica realizado. Char(3), clave para búsqueda de pruebas diagnósticas y hallazgos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODEXAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Examen Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODEXAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODEXAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional; identificador de centro de atención, servicio o departamento clínico donde se realiza el examen. Char(10), FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo; identificador único autoincremental del registro de examen físico por unidad funcional. INT Identity, clave primaria para trazabilidad de evaluaciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del consecutivo de la tabla de examen fisico por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de exámenes o especialidades no especificadas asociadas a la historia clínica, vinculando cada tipo de historia clínica con una unidad funcional y un código de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC';
