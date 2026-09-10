CREATE TABLE [dbo].[IHBODEGAS] (
    [CODBODEGA] VARCHAR (20) NOT NULL,
    [DESBODEGA] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_IHBODEGAS] PRIMARY KEY CLUSTERED ([CODBODEGA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría/formulario (NUMERIC 18). Marca o bandera para registros auditados, control de conformidad o estado de validación en procesos de inventario y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la bodega de inventarios (CHAR 40). Texto que especifica la ubicación, función o características del almacén (ej: Farmacia Central, Quirófanos, Almacén de Suministros).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'DESBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Bodegas de Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'DESBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'DESBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de bodega/almacén de inventarios (VARCHAR 20, clave primaria). Identificador del lugar de almacenamiento de medicamentos, insumos y material médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Bodegas de Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de bodegas o almacenes del sistema de inventario. Registra cada bodega disponible para el manejo de medicamentos, insumos y materiales dentro de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHBODEGAS';
