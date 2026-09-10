CREATE TABLE [Inventory].[AdjustedKardex] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [KardexId]         INT             NOT NULL,
    [QuantityPrevious] INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ValuePrevious]    DECIMAL (18, 2) NOT NULL,
    [Value]            DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AdjustedKardex] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdjustedKardex_Kardex] FOREIGN KEY ([KardexId]) REFERENCES [Inventory].[Kardex] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario ajustado del movimiento de inventario; monto en pesos del ajuste actual de kardex (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario anterior antes del ajuste; monto en pesos previo al cambio en kardex (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'ValuePrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Anterior', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'ValuePrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'ValuePrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades ajustadas en el movimiento; número de insumos, medicamentos o productos en inventario tras ajuste (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad anterior de unidades antes del ajuste; stock previo de insumos, medicamentos o productos en inventario (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad anterior', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del kardex relacionado; FK a [Inventory].[Kardex] que vincula el ajuste al registro de movimiento de inventario (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'KardexId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Kardex', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'KardexId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'KardexId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla AdjustedKardex; clave primaria que identifica cada registro de ajuste (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ajustes realizados al kardex de inventario. Guarda el historial de correcciones de cantidad y valor de ítems, conservando los valores anteriores y los nuevos para trazabilidad de modificaciones en el inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex';
