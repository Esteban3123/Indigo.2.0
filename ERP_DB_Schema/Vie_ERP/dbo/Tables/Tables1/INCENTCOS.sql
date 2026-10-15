CREATE TABLE [dbo].[INCENTCOS] (
    [CODCENCOS] CHAR (14)    NOT NULL,
    [DESCENCOS] CHAR (80)    NOT NULL,
    [ESTCENCOS] BIT          NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INCENTCOS] PRIMARY KEY CLUSTERED ([CODCENCOS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del centro de Costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'ESTCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Centros de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'DESCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Centros de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENTCOS', @level2type = N'COLUMN', @level2name = N'CODCENCOS';

