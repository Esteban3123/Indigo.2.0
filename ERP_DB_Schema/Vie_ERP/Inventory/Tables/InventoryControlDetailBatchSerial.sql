CREATE TABLE [Inventory].[InventoryControlDetailBatchSerial] (
    [Id]                       INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryControlDetailId] INT     NOT NULL,
    [BatchSerialId]            INT     NULL,
    [InventoryQuantity]        INT     NOT NULL,
    [Quantity]                 INT     NOT NULL,
    [Status]                   TINYINT NOT NULL,
    CONSTRAINT [PK_InventoryControlDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryControlDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_InventoryControlDetailBatchSerial_InventoryControlDetail] FOREIGN KEY ([InventoryControlDetailId]) REFERENCES [Inventory].[InventoryControlDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryControlDetailBatchSerial] NOCHECK CONSTRAINT [FK_InventoryControlDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[InventoryControlDetailBatchSerial] NOCHECK CONSTRAINT [FK_InventoryControlDetailBatchSerial_InventoryControlDetail];


GO
CREATE NONCLUSTERED INDEX [IX_InventoryControlDetailBatchSerial_InventoryControlDetailId]
    ON [Inventory].[InventoryControlDetailBatchSerial]([InventoryControlDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Estado del control de inventarios  0 - No Aplica (Para Saldos Iniciales)  1 - No Ajustado (Control de Inventarios)  2 - Ajustado (Control de Inventarios)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de los items con el lote y serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de items que se encuantran en el inventario fisico en el momento en que se crea el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del control de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

