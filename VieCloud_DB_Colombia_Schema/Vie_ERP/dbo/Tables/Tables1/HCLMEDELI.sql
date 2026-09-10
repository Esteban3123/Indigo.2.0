CREATE TABLE [dbo].[HCLMEDELI] (
    [CODMEDELI] CHAR (2)  NOT NULL,
    [DESMEDELI] CHAR (40) NOT NULL,
    CONSTRAINT [PK_HCLVIAELI] PRIMARY KEY CLUSTERED ([CODMEDELI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de líquido administrado al paciente (suero, solución salina, medicamento intravenoso, sangre, hemoderivado). Texto alfanumérico de hasta 40 caracteres para identificar el nombre o categoría del fluido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'DESMEDELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tipo de Liquido Administrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'DESMEDELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'DESMEDELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de líquido administrado (CHAR 2). Identificador único y clave primaria que clasifica el tipo de solución, medicamento o hemoderivado infundido al paciente durante la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'CODMEDELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Liqiuido Administrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'CODMEDELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'CODMEDELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de eliminaciones o exclusiones relacionadas con medicamentos. Registra los tipos o motivos de eliminación de un medicamento en el sistema clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI';
