CREATE TABLE [dbo].[INCLASESP] (
    [CODCLAPROV] CHAR (2)  NOT NULL,
    [DESCLAPROV] CHAR (40) NOT NULL,
    CONSTRAINT [PK_INCLAPROVEE] PRIMARY KEY CLUSTERED ([CODCLAPROV] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la Clase de Proveedor (tipo/categoría): Inventarios, Activos Fijos, Dietas, Medicamentos, u otros servicios. Texto descriptivo de hasta 40 caracteres que clasifica la naturaleza o rubro del proveedor en el sistema de compras y suministros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP', @level2type = N'COLUMN', @level2name = N'DESCLAPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Clase de Proveedor    Nota: Ejemplo - Proveedor Inventarios, Proveedor Activos Fijos, Proveedor Dietas, Proveedor Medicamentos, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP', @level2type = N'COLUMN', @level2name = N'DESCLAPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP', @level2type = N'COLUMN', @level2name = N'DESCLAPROV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Clase de Proveedor: identificador único de 2 caracteres (alfanumérico) que clasifica proveedores por tipo (inventario, activos, servicios, medicamentos). Clave primaria de la tabla de catálogo de clases de proveedores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP', @level2type = N'COLUMN', @level2name = N'CODCLAPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Clase de Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP', @level2type = N'COLUMN', @level2name = N'CODCLAPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP', @level2type = N'COLUMN', @level2name = N'CODCLAPROV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de clases o categorías especiales de proveedores. Permite clasificar a los proveedores según su tipo o naturaleza (por ejemplo: persona natural, empresa, proveedor médico, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLASESP';
