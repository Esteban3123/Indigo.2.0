CREATE TABLE [dbo].[IHPROINSU] (
    [CODPRODUC] CHAR (20)  NOT NULL,
    [CODINSUMO] CHAR (20)  NOT NULL,
    [DESPRODUC] CHAR (255) NOT NULL,
    [CANTIDAD]  TINYINT    NOT NULL,
    [DEFECTO]   BIT        NOT NULL,
    CONSTRAINT [PK_IHPROINSU] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC, [CODINSUMO] ASC),
    CONSTRAINT [FK_IHPROINSU_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[IHPROINSU] NOCHECK CONSTRAINT [FK_IHPROINSU_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica si el insumo se carga por defecto cuando se solicita el medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DEFECTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del insumo a solicitar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Corta del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DESPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODINSUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODPRODUC';

