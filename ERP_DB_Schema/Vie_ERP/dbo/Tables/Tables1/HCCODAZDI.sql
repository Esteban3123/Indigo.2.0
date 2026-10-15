CREATE TABLE [dbo].[HCCODAZDI] (
    [CODCONSEC] NUMERIC (18) NOT NULL,
    [CODPRODUC] CHAR (20)    NOT NULL,
    [CANPEDPRO] INT          NOT NULL,
    [TIPPRODUC] CHAR (1)     NOT NULL,
    [CANENTPRO] INT          NULL,
    CONSTRAINT [PK_HCCODAZDI] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCCODAZDI_HCCODAZUC] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[HCCODAZUC] ([CODCONSEC]),
    CONSTRAINT [FK_HCCODAZDI_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCCODAZDI] NOCHECK CONSTRAINT [FK_HCCODAZDI_HCCODAZUC];


GO
ALTER TABLE [dbo].[HCCODAZDI] NOCHECK CONSTRAINT [FK_HCCODAZDI_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Utilizada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANENTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Producto  1: Medicamentos  2: Materiales e Insumos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

