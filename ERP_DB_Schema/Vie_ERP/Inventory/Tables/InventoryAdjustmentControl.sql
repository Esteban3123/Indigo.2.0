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
ALTER TABLE [Inventory].[InventoryAdjustmentControl] NOCHECK CONSTRAINT [FK_InventoryAdjustmentControl_InventoryAdjustment];


GO
ALTER TABLE [Inventory].[InventoryAdjustmentControl] NOCHECK CONSTRAINT [FK_InventoryAdjustmentControl_InventoryControlDetailBatchSerial];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad que se a ajustado ya sea de entrada o de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'QuantityAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de ajuste que se realizo  1 - Sobrante o Entrada  2 - Faltante o Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'AdjustmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de serie del lote de detalles de control de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la cabecera del ajuste de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'Id';

