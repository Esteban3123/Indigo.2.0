CREATE TABLE [dbo].[IHBODEGAS] (
    [CODBODEGA] VARCHAR (20) NOT NULL,
    [DESBODEGA] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_IHBODEGAS] PRIMARY KEY CLUSTERED ([CODBODEGA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Bodegas de Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'DESBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Bodegas de Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'CODBODEGA';

