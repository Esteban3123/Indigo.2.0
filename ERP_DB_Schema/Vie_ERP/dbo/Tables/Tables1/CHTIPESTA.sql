CREATE TABLE [dbo].[CHTIPESTA] (
    [CODTIPEST] CHAR (3)     NOT NULL,
    [DESTIPEST] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTIPESTA] PRIMARY KEY CLUSTERED ([CODTIPEST] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Tipo de Estancia  En este campo se pueden especificar diferentes tipos de estancia, con esta funcionalidad podemos utilizar la misma cama pero liquidar diferentes valores de estancia dependiendo de las necesidades.    Ejemplo - UCI Neonatal Intensiva, UCI Neonatal Intermedia, UCI Neonatal Basica, Hospitalizacion General, Hospitalizacion Maternas, Patologias Especiales, etc. En este ejemplo podemos observar que la misma cuna nos puede servir para liquidar los tres tipos de estancia o en hospitalizacion facturar la estancia dependiendo de la patologia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'DESTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';

