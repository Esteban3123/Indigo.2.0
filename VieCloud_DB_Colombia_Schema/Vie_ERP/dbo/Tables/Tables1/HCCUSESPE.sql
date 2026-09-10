CREATE TABLE [dbo].[HCCUSESPE] (
    [CODCONCEC] INT      NOT NULL,
    [CODESPECI] CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCCUSESPE] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODESPECI] ASC),
    CONSTRAINT [FK_HCCUSESPE_HCCUSTOMI] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCCUSTOMI] ([AUTO]),
    CONSTRAINT [FK_HCCUSESPE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica (ej: Cardiología, Pediatría, Cirugía). Tipo CHAR(3). Referencia a tabla INESPECIA. Búsqueda: especialidad, rama médica, disciplina clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/identificador único del concepto o centro de atención. Tipo INT. Referencia a tabla HCCUSTOMI. Búsqueda: centro de atención, unidad funcional, concepto asistencial, institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona conceptos de cuenta con especialidades médicas. Permite definir qué especialidades están asociadas a cada concepto de cobro o clasificación en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE';
