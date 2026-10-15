CREATE TABLE [dbo].[INDEPARTA] (
    [depcodigo] CHAR (2)     NOT NULL,
    [nomdepart] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [IDPAIS]    INT          NULL,
    CONSTRAINT [PK_INdepatar] PRIMARY KEY CLUSTERED ([depcodigo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me relaciona el ID pais con la tabla de Paises en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'nomdepart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDEPARTA', @level2type = N'COLUMN', @level2name = N'depcodigo';

