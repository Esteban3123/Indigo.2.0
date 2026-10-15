CREATE TABLE [dbo].[IHCODBARR] (
    [CODBARRAS] VARCHAR (50) NOT NULL,
    [IPRCODIGO] CHAR (20)    NOT NULL,
    [FECHCREAC] DATETIME     NOT NULL,
    CONSTRAINT [PK_IHCODBARR] PRIMARY KEY CLUSTERED ([CODBARRAS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creacion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'FECHCREAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUM del Producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Barras del Producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'CODBARRAS';

