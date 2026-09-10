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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si el insumo/material se carga automáticamente por defecto al solicitar el medicamento o producto en farmacia/dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DEFECTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica si el insumo se carga por defecto cuando se solicita el medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DEFECTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DEFECTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (TINYINT) del insumo o material a solicitar/dispensar por cada unidad de producto asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del insumo a solicitar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción corta (VARCHAR 255) del producto o medicamento vinculado; texto de búsqueda para identificación rápida en catálogo farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DESPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Corta del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DESPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'DESPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno único (CHAR 20) del insumo, material o componente; identificador del recurso auxiliar en estructura de productos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODINSUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODINSUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODINSUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno único (CHAR 20) del producto o medicamento; referencia a tabla IHLISTPRO para catálogo farmacéutico de atención clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre productos e insumos: registra qué insumos o materiales componen cada producto, con su cantidad requerida y si es el insumo por defecto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPROINSU';
