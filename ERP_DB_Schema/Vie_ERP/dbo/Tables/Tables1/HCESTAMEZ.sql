CREATE TABLE [dbo].[HCESTAMEZ] (
    [CODPRODUC] CHAR (20) NOT NULL,
    [CODPRODIL] CHAR (20) NOT NULL,
    [TIEESTMEZ] INT       NOT NULL,
    CONSTRAINT [PK_HCESTAMEZ] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC, [CODPRODIL] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de Estabilidad de la Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'TIEESTMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto Diluyente de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODUC';

