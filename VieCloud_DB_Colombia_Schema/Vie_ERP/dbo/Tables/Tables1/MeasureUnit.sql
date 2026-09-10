CREATE TABLE [dbo].[MeasureUnit] (
    [Id]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]         VARCHAR (20)  NOT NULL,
    [Name]         VARCHAR (100) NOT NULL,
    [Abbreviation] VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_MeasureUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura de la unidad de medida (ej: mg, ml, cm, kg, unidad, dosis). Tipo: VARCHAR(10). Usado en recetas, laboratorio, exámenes, procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Abbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Abbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Abbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la unidad de medida (milígramo, mililitro, centímetro, kilogramo, unidad, dosis). Tipo: VARCHAR(100). Referencia en RIPS, facturación, órdenes médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único de la unidad de medida en sistema (ej: MG, ML, CM, KG, U, D). Tipo: VARCHAR(20) NOT NULL. Clave de búsqueda en recetas y exámenes de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial único (clave primaria). Tipo: INT IDENTITY. Referencia interna para asociar unidades a exámenes, laboratorios, medicamentos y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador secuencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de unidades de medida utilizadas en el sistema clínico y administrativo, como miligramos, mililitros, unidades, tabletas, entre otras. Permite estandarizar la medición de medicamentos, insumos y otros elementos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MeasureUnit';
