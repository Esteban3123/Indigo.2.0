CREATE TABLE [dbo].[HCLOGSUMD] (
    [ID]         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LOGSUMID]   INT           NOT NULL,
    [PROCODCUM]  VARCHAR (50)  NULL,
    [PROCODIGO]  CHAR (20)     NOT NULL,
    [PRONOMBRE]  VARCHAR (500) NOT NULL,
    [CODLOTE]    VARCHAR (50)  NOT NULL,
    [INNLOTSER]  INT           NULL,
    [CANTIDAD]   INT           NOT NULL,
    [INNMSUMPA]  INT           NULL,
    [NUMSUMINIS] VARCHAR (50)  NULL,
    [INNDOCUME]  INT           NULL,
    CONSTRAINT [PK_HCLOGSUMD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del documento en DGH (módulo farmacéutico); referencia PII en auditoría de suministros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de INNDOCUME en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de suministro generado automáticamente; identificador de transacción de dispensación/entrega de producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'NUMSUMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de suministro generado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'NUMSUMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'NUMSUMINIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro padre de suministro (INNSUMPA); clave de relación jerárquica en log de movimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNMSUMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el IdInnsumpa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNMSUMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNMSUMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto suministrado; campo numérico entero de control de inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Cantidad producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote en DGH; referencia a número de serie o lote farmacéutico para trazabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNLOTSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID lote en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNLOTSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNLOTSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del lote; número identificador único asignado al lote de medicamento/insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CODLOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo del lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CODLOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CODLOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del producto; descripción comercial o genérica del medicamento o insumo suministrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PRONOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Nombre del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PRONOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PRONOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto; clave única de identificación del artículo farmacéutico o insumo (FK a IHLISTPRO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PROCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PROCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PROCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUM del producto; código único de medicamento asignado por ente regulatorio nacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PROCODCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUM del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PROCODCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PROCODCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del log de suministro padre; FK a HCLOGSUMC para relación maestro-detalle de movimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'LOGSUMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda logos id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'LOGSUMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'LOGSUMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica; identificador único para cada línea de detalle en registro de suministro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ítems de suministros o medicamentos entregados a pacientes, registrando los productos, lotes, cantidades y referencias asociadas a cada línea de un resumen de suministro (log de suministros).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD';
