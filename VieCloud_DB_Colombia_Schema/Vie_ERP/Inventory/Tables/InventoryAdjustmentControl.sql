CREATE TABLE [Inventory].[InventoryAdjustmentControl] (
    [Id]                                  INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryAdjustmentId]               INT     NOT NULL,
    [InventoryControlDetailBatchSerialId] INT     NOT NULL,
    [AdjustmentType]                      TINYINT NOT NULL,
    [QuantityAdjustment]                  INT     NOT NULL,
    CONSTRAINT [PK_InventoryAdjustmentControl] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryAdjustmentControl_InventoryAdjustment] FOREIGN KEY ([InventoryAdjustmentId]) REFERENCES [Inventory].[InventoryAdjustment] ([Id]),
    CONSTRAINT [FK_InventoryAdjustmentControl_InventoryControlDetailBatchSerial] FOREIGN KEY ([InventoryControlDetailBatchSerialId]) REFERENCES [Inventory].[InventoryControlDetailBatchSerial] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad ajustada (INT): número de unidades sumadas (entrada/sobrante) o restadas (salida/faltante) del inventario. Valor absoluto del movimiento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'QuantityAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad que se a ajustado ya sea de entrada o de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'QuantityAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'QuantityAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ajuste (TINYINT): 1=Sobrante/Entrada (incremento de stock), 2=Faltante/Salida (decremento de stock). Determina si es adición o sustracción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'AdjustmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de ajuste que se realizo  1 - Sobrante o Entrada  2 - Faltante o Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'AdjustmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'AdjustmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK INT) al lote/serie de control de inventario. Identifica el producto, lote o número de serie específico que se ajusta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de serie del lote de detalles de control de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK INT) al identificador de cabecera del ajuste de inventarios. Vincula el detalle de control al movimiento principal de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la cabecera del ajuste de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la fila de control de ajuste de inventario. Clave primaria que permite rastrear cada ajuste realizado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de ajustes de inventario por lote o serial. Detalla cada movimiento de ajuste aplicado a un ítem específico del inventario, indicando el tipo de ajuste y la cantidad modificada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl';
