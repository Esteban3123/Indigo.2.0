CREATE TABLE [dbo].[CHTIPESTA] (
    [CODTIPEST] CHAR (3)     NOT NULL,
    [DESTIPEST] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTIPESTA] PRIMARY KEY CLUSTERED ([CODTIPEST] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Auditoria; indicador numérico (NUMERIC 18) que marca registro auditado o requerimiento de auditoría en estancia hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del Tipo de Estancia (VARCHAR 40); categorización de niveles de cuidado (UCI Neonatal Intensiva/Intermedia/Básica, Hospitalización General, Maternas, Patologías Especiales) para liquidar valores diferentes en la misma unidad funcional/cama según diagnóstico o necesidad clínica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'DESTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Tipo de Estancia  En este campo se pueden especificar diferentes tipos de estancia, con esta funcionalidad podemos utilizar la misma cama pero liquidar diferentes valores de estancia dependiendo de las necesidades.    Ejemplo - UCI Neonatal Intensiva, UCI Neonatal Intermedia, UCI Neonatal Basica, Hospitalizacion General, Hospitalizacion Maternas, Patologias Especiales, etc. En este ejemplo podemos observar que la misma cuna nos puede servir para liquidar los tres tipos de estancia o en hospitalizacion facturar la estancia dependiendo de la patologia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'DESTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'DESTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Tipo de Estancia (CHAR 3, PK); identificador único de la clasificación de estancia hospitalaria para facturación, RIPS y liquidación de servicios de hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de estado utilizados en la historia clínica. Define los posibles estados que puede tener un registro clínico, como activo, anulado, cerrado, entre otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA';
