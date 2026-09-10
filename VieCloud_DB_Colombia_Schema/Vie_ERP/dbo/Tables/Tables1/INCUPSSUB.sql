CREATE TABLE [dbo].[INCUPSSUB] (
    [CODGRUSUB]    CHAR (13)  NOT NULL,
    [CODGRUIPS]    CHAR (3)   NOT NULL,
    [CODSUBIPS]    CHAR (10)  NOT NULL,
    [DESSUBIPS]    CHAR (300) NOT NULL,
    [MDASHIMAG]    BIT        NOT NULL,
    [IDRISGRIMAGE] INT        NULL,
    CONSTRAINT [PK_INCUPSSUB] PRIMARY KEY CLUSTERED ([CODGRUSUB] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) de grupo de imagenología, referencia a tabla RISGRIMAGE para consultas de radiología, resonancia, tomografía y estudios de imagen. Clave foránea que vincula procedimientos de imagen con su configuración en RIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Identificador de grupo de imagenología, tabla RISGRIMAGE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'IDRISGRIMAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si el subgrupo de procedimiento se muestra en el dashboard de imagenología y lecturas radiológicas. Controla visibilidad en reportería de imagen diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'MDASHIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el subgrupo se lista en el dashboard de Imagenologia y Lecturas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'MDASHIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'MDASHIMAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (CHAR 300) del subgrupo de procedimiento o servicio en nomenclatura RIPS. Contiene el nombre legible del subgrupo para búsqueda de procedimientos, servicios clínicos y prestaciones de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'DESSUBIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del SubGrupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'DESSUBIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'DESSUBIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del subgrupo de procedimiento o servicio (CHAR 10) según nomenclatura RIPS. Identificador de clasificación de procedimientos, servicios diagnósticos, terapéuticos y quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODSUBIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del SubGrupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODSUBIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODSUBIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de procedimiento o servicio (CHAR 3) según nomenclatura RIPS. Clasificación jerárquica de mayor nivel que agrupa subgrupos de procedimientos, servicios e intervenciones sanitarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código concatenado (CHAR 13) que combina grupo y subgrupo de procedimiento RIPS. Identificador compuesto único para clasificación jerárquica de procedimientos, servicios y prestaciones en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo concatenado del  grupo y subgrupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB', @level2type = N'COLUMN', @level2name = N'CODGRUSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de subgrupos de servicios CUPS/IPS: clasifica los procedimientos y servicios de salud en subgrupos dentro de un grupo mayor, incluyendo si requieren manejo de imágenes diagnósticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSSUB';
