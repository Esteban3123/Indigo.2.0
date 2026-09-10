CREATE TABLE [dbo].[HCPAREXFISESP] (
    [ID]         INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA] INT      NOT NULL,
    [CODESPECI]  CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCPAREXFISESP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAREXFISESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (FK a INESPECIA). Identificador de 3 caracteres que vincula la especialidad profesional del examen físico específico (ej: Cardiología, Pediatría, Cirugía). Usado en búsquedas por especialidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con cabecera del examen físico. Clave foránea que conecta el detalle de especialidad con el registro principal/padre de la historia clínica del examen físico específico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera; campo que me tiene la relacion con la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la tabla. Clave primaria que identifica cada asociación especialidad-cabecera en exámenes físicos específicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de especialidades asociadas a un examen físico especial dentro de la historia clínica. Permite vincular múltiples especialidades médicas a un mismo encabezado de valoración física especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISESP';
