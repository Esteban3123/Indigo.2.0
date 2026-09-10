CREATE TABLE [Inventory].[ProductoSubgrupos] (
    [DCI]               NVARCHAR (255) NULL,
    [DCIInvima]         NVARCHAR (255) NULL,
    [CodATCN]           NVARCHAR (255) NULL,
    [ATC]               NVARCHAR (255) NULL,
    [Grupo]             NVARCHAR (255) NULL,
    [Subgrupo]          NVARCHAR (255) NULL,
    [Descripcion ATC]   NVARCHAR (255) NULL,
    [CUMS]              NVARCHAR (255) NULL,
    [descripcion Cums]  NVARCHAR (255) NULL,
    [Pos (Si-No)]       NVARCHAR (255) NULL,
    [FORMAFARMACEUTICA] NVARCHAR (255) NULL,
    [VIASUNICAS]        NVARCHAR (255) NULL,
    [REGISTROSANITARIO] NVARCHAR (255) NULL,
    [FECHAVENCIMIENTO]  NVARCHAR (255) NULL,
    [ConcentracionF]    NVARCHAR (255) NULL,
    [Costo]             NVARCHAR (255) NULL,
    [PrecioVenta]       NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta del producto farmacéutico, valor monetario de comercialización', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'PrecioVenta';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'PrecioVenta';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'PrecioVenta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario del producto, valor de adquisición o producción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Costo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Costo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Costo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración farmacéutica del principio activo, expresada en unidades de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ConcentracionF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ConcentracionF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ConcentracionF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento del producto, fecha límite de uso seguro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro Sanitario INVIMA, número de autorización sanitaria del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vías de administración únicas autorizadas, rutas de aplicación del fármaco', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'VIASUNICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unicas Vías', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'VIASUNICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'VIASUNICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma farmacéutica del producto: tableta, cápsula, inyectable, solución, crema', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FORMAFARMACEUTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma Farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FORMAFARMACEUTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FORMAFARMACEUTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inclusión en Plan Obligatorio de Salud, cobertura obligatoria en Colombia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Pos (Si-No)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Pos ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Pos (Si-No)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Pos (Si-No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del medicamento en CUMS, información normalizada del fármaco', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'descripcion Cums';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Cums', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'descripcion Cums';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'descripcion Cums';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Medicamentos y Servicios, identificador nacional de medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CUMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CUMS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CUMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CUMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la clasificación anatómica terapéutica química del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Descripcion ATC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Descripcion ATC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Descripcion ATC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subgrupo de clasificación farmacológica, categoría específica dentro del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Subgrupo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'SubGrupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Subgrupo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Subgrupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo de clasificación farmacológica, categoría general de medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Grupo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Grupo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación Anatómica Terapéutica Química, código internacional de fármacos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ATC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ATC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ATC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ATC numérico, identificador de la clasificación anatómica terapéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CodATCN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CodATCN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CodATCN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Denominación Común Internacional validada por INVIMA, nombre genérico regulado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCIInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Invima del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCIInvima';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCIInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Denominación Común Internacional, nombre genérico del principio activo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de medicamentos e insumos clasificados por subgrupo farmacológico, con información de DCI (nombre genérico), código ATC, clasificación POS, registro sanitario INVIMA, forma farmacéutica, vías de administración, CUMS y precios. Permite identificar si un medicamento está en el Plan de Beneficios en Salud (PBS/POS) y consultar su costo y precio de venta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos';
