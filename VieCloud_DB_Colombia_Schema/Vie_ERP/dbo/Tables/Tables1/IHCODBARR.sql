CREATE TABLE [dbo].[IHCODBARR] (
    [CODBARRAS] VARCHAR (50) NOT NULL,
    [IPRCODIGO] CHAR (20)    NOT NULL,
    [FECHCREAC] DATETIME     NOT NULL,
    CONSTRAINT [PK_IHCODBARR] PRIMARY KEY CLUSTERED ([CODBARRAS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de código de barras; timestamp de auditoría que documenta cuándo se asoció el código de barras al producto en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'FECHCREAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'FECHCREAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'FECHCREAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUM (Código Único de Medicamento) del producto; identificador farmacéutico estándar que vincula el medicamento o insumo a su registro sanitario y especificaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUM del Producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de barras del producto; identificador único de presentación comercial (EAN/UPC) que facilita el escaneo, inventario y trazabilidad de medicamentos e insumos en farmacias y almacenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'CODBARRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Barras del Producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'CODBARRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR', @level2type = N'COLUMN', @level2name = N'CODBARRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de códigos de barras asignados a insumos, productos o ítems de inventario/farmacia. Vincula cada código de barras con el código interno del ítem y la fecha en que se creó la asociación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHCODBARR';
