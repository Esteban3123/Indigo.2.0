CREATE TABLE [dbo].[HCHEMPROD] (
    [CODPRODUC] TINYINT    NOT NULL,
    [DESPRODUC] NCHAR (60) NOT NULL,
    [ESTPRODUC] BIT        NOT NULL,
    CONSTRAINT [PK_HCHEMPROD] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del producto hemoderivado: activo (1) o inactivo (0). Indicador BIT que determina disponibilidad para dispensación y transfusión; inactivo = producto descontinuado o retirado del inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'ESTPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Producto - Hemoderivado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'ESTPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'ESTPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de producto hemoderivado (sangre completa, plasma fresco congelado, concentrado plaquetario, glóbulos rojos, crioprecipitado, etc.). Texto NCHAR(60) que identifica el hemoproducto para reportes, transfusiones y gestión de inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'DESPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tipo de Producto - Hemoderivados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'DESPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'DESPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de producto hemoderivado (sangre, plasma, plaquetas, glóbulos rojos). Identificador único TINYINT (0-255), clave primaria, referencia para clasificar inventario y dispensación de hemoproductos en banco de sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Producto - Hemoderivados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de productos o elementos usados en la historia clínica (HC). Registra los tipos de productos disponibles para su uso en procesos clínicos, con su descripción y estado de activación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD';
