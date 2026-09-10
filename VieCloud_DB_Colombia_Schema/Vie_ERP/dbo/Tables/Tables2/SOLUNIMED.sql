CREATE TABLE [dbo].[SOLUNIMED] (
    [UNIMEDAUT] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UNIMEDCOD] VARCHAR (20)  NOT NULL,
    [UNIMEDDES] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SOLUNIMED] PRIMARY KEY CLUSTERED ([UNIMEDAUT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la unidad de medida: mililitros, gramos, unidades, dosis, etc. Etiqueta legible para reportes y prescripciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la unidad de medida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la unidad de medida (ej: ML, GR, U, DOS). Identificador corto usado en medicamentos, exámenes y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincremental único (PK). Clave primaria técnica de la tabla SOLUNIMED para referencia en FK de otras tablas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de unidades de medida utilizadas en el sistema, como miligramos, mililitros, unidades, comprimidos, entre otros. Se usa para estandarizar la medición de medicamentos, insumos y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED';
