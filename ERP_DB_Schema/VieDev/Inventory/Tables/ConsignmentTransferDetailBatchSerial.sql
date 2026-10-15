CREATE TABLE [Inventory].[ConsignmentTransferDetailBatchSerial] (
    [Id]                          INT IDENTITY (1, 1) NOT NULL,
    [ConsignmentTransferDetailId] INT NOT NULL,
    [PhysicalInventoryId]         INT NOT NULL,
    [Quantity]                    INT NOT NULL,
    CONSTRAINT [PK_ConsignmentTransferDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentTransferDetailBatchSerial_ConsignmentTransferDetail] FOREIGN KEY ([ConsignmentTransferDetailId]) REFERENCES [Inventory].[ConsignmentTransferDetail] ([Id]),
    CONSTRAINT [FK_ConsignmentTransferDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[ConsignmentTransferDetailBatchSerial] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetailBatchSerial_ConsignmentTransferDetail];


GO
ALTER TABLE [Inventory].[ConsignmentTransferDetailBatchSerial] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetailBatchSerial_PhysicalInventory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad inicial con la que se registro el detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del traslado en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentTransferDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

