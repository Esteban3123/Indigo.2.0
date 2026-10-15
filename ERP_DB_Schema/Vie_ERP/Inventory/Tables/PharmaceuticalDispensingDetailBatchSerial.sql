CREATE TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] (
    [Id]                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingDetailId] INT NOT NULL,
    [PhysicalInventoryId]              INT NULL,
    [Quantity]                         INT NOT NULL,
    [OutstandingQuantity]              INT NOT NULL,
    [PhysicalInventoryCustodyId]       INT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDetailBatchSerial__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PharmaceuticalDispensingDetailBatchSerial] CHECK ([OutstandingQuantity]>=(0)),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PharmaceuticalDispensingDetail] FOREIGN KEY ([PharmaceuticalDispensingDetailId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetail] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody] FOREIGN KEY ([PhysicalInventoryCustodyId]) REFERENCES [Inventory].[PhysicalInventoryCustody] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetailBatchSerial];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PharmaceuticalDispensingDetail];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody];


GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingDetailBatchSerial__PharmaceuticalDispensingDetailId]
    ON [Inventory].[PharmaceuticalDispensingDetailBatchSerial]([PharmaceuticalDispensingDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de inventario fisico de custodia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea la dispensacion este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la dispensacion farmacutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

