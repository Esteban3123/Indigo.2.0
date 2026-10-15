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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de ajuste que se realizo  1 - Sobrante o Entrada  2 - Faltante o Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentControl', @level2type = N'COLUMN', @level2name = N'AdjustmentType';

