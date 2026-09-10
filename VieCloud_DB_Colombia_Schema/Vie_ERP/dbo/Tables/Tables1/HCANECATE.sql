CREATE TABLE [dbo].[HCANECATE] (
    [CODCATEGO] CHAR (2)     NOT NULL,
    [NOMCATEGO] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_HCANECATE] PRIMARY KEY CLUSTERED ([CODCATEGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la categoría de anestesia; identificador textual (VARCHAR 60) para clasificación de tipos, técnicas o niveles anestésicos utilizados en procedimientos quirúrgicos y atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'NOMCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'NOMCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'NOMCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la categoría en anestesia; identificador único (CHAR 2) que clasifica tipos, técnicas o niveles anestésicos; clave primaria para referencia en procedimientos y registros de atención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Categoria en Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'CODCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de categorías de atención clínica. Guarda los tipos o clasificaciones de atención (por ejemplo: urgencias, hospitalización, consulta externa) utilizados en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE';
