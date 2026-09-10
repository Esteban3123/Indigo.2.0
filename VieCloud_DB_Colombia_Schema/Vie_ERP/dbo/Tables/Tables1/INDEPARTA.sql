CREATE TABLE [dbo].[INDEPARTA] (
    [depcodigo] CHAR (2)     NOT NULL,
    [nomdepart] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [IDPAIS]    INT          NULL,
    CONSTRAINT [PK_INdepatar] PRIMARY KEY CLUSTERED ([depcodigo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID País (INT, FK a tabla Paises). Relación con la tabla de Países en VIE Cloud para identificar la nación a la que pertenece el departamento. Búsqueda: país, nación, código país, geografía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me relaciona el ID pais con la tabla de Paises en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Auditoría (NUMERIC 18). Indicador o referencia de auditoría interna para trazabilidad y control. Búsqueda: auditoría, código auditoria, control interno, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Departamento (CHAR 40). Denominación completa de la entidad territorial administrativa (ej: Antioquia, Bogotá, Valle del Cauca). Búsqueda: nombre región, departamento, división territorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'nomdepart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'nomdepart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'nomdepart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Departamento (CHAR 2). Identificador único de la región/departamento administrativo. Clave primaria. Búsqueda: código departamental, división territorial, región.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'depcodigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'depcodigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'depcodigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de departamentos o estados/provincias del país. Relaciona cada departamento con su código, nombre oficial y el país al que pertenece.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA';
