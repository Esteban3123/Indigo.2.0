CREATE TABLE [dbo].[HCREFESPECIALIDADES] (
    [ID]          INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCREFCONT] INT      NOT NULL,
    [CODESPECI]   CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCREFESPECIALIDADES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFESPECIALIDADES_HCREFCONT] FOREIGN KEY ([IDHCREFCONT]) REFERENCES [dbo].[HCREFCONT] ([AUTO]),
    CONSTRAINT [FK_HCREFESPECIALIDADES_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[HCREFESPECIALIDADES] NOCHECK CONSTRAINT [FK_HCREFESPECIALIDADES_HCREFCONT];




GO
ALTER TABLE [dbo].[HCREFESPECIALIDADES] NOCHECK CONSTRAINT [FK_HCREFESPECIALIDADES_HCREFCONT];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica (3 caracteres). Referencia a catálogo de especialidades clínicas (cardiología, pediatría, etc.). Clave foránea a tabla INESPECIA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la referencia de salud. Vinculación con registro maestro de referencias médicas en tabla HCREFCONT. Permite asociar especialidades a cada referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'IDHCREFCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de referencia HCREFCONT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'IDHCREFCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'IDHCREFCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY). Clave primaria de la tabla. Consecutivo automático de relaciones especialidad-referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidades médicas asociadas a las referencias o remisiones clínicas del paciente. Cada registro vincula una remisión con la especialidad a la que fue referido el paciente (por ejemplo, cardiología, ortopedia, neurología).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES';
