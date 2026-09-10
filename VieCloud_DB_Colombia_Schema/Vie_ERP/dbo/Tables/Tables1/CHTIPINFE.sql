CREATE TABLE [dbo].[CHTIPINFE] (
    [CODTIPINFE] CHAR (3)  NOT NULL,
    [DESTIPINFE] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHTIPINFE] PRIMARY KEY CLUSTERED ([CODTIPINFE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de infección (nosocomiales, comunitarias, oportunistas, etc.). Texto de 40 caracteres que clasifica la categoría de infección para diagnóstico, epidemiología y control de infecciones. PII Sensible - Salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'DESTIPINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tipo de Infecciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'DESTIPINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'DESTIPINFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de infección. Identificador único de 3 caracteres alfanuméricos que referencia la clasificación de infecciones en el sistema (bacteriana, viral, fúngica, parasitaria). Clave primaria. Dominio de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'CODTIPINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Infecciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'CODTIPINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'CODTIPINFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de infección o informe epidemiológico clínico. Permite clasificar las categorías de infecciones o eventos de vigilancia en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE';
