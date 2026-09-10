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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad entregada o utilizada del producto en la atención; cantidad dispensada de medicamento, material o insumo (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANENTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Utilizada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANENTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANENTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto: 1=Medicamentos, 2=Materiales e insumos; clasificación del artículo consumido en la orden (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Producto  1: Medicamentos  2: Materiales e Insumos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida o solicitada del producto en la orden; cantidad requerida de medicamento, material o insumo (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto; identificador único del medicamento, material o insumo en catálogo (CHAR 20, FK a IHLISTPRO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo; identificador de la orden o documento de atención (NUMERIC 18, FK a HCCODAZUC, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos o insumos solicitados y entregados en una orden o despacho de historia clínica. Registra qué productos se pidieron, cuántos se entregaron y el tipo de producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZDI';
