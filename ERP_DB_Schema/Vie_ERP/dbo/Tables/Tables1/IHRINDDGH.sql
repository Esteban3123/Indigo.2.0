CREATE TABLE [dbo].[IHRINDDGH] (
    [IPRCODIGO] CHAR (20) NOT NULL,
    [CODPRODUC] CHAR (20) NOT NULL,
    [PROCONPRE] BIT       NOT NULL,
    CONSTRAINT [PK_IHRINDDGH] PRIMARY KEY CLUSTERED ([IPRCODIGO] ASC, [CODPRODUC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el Producto Maneja Control de Precios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'PROCONPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Producto en Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUM en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHRINDDGH', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';

