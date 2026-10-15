CREATE TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] (
    [Id]                                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationPharmaceuticalDispensingDetailId] INT NOT NULL,
    [PhysicalInventoryId]                       INT NULL,
    [Quantity]                                  INT NOT NULL,
    [OutstandingQuantity]                       INT NOT NULL,
    [PhysicalInventoryCustodyId]                INT NULL,
    CONSTRAINT [PK_QuotationPharmaceuticalDispensingDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody] FOREIGN KEY ([PhysicalInventoryCustodyId]) REFERENCES [Inventory].[PhysicalInventoryCustody] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_QuotationPharmaceuticalDispensingDetail] FOREIGN KEY ([QuotationPharmaceuticalDispensingDetailId]) REFERENCES [Billing].[QuotationPharmaceuticalDispensingDetail] ([Id])
);


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_QuotationPharmaceuticalDispensingDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de inventario fisico de custodia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea la dispensacion este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la dispensacion farmacutica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'QuotationPharmaceuticalDispensingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

