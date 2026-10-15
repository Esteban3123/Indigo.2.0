CREATE TABLE [Inventory].[ConsignmentTransferDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [ConsignmentTransferId] INT             NOT NULL,
    [WarehouseId]           INT             NOT NULL,
    [ProductId]             INT             NOT NULL,
    [Quantity]              INT             NOT NULL,
    [ProductCost]           DECIMAL (18, 2) NOT NULL,
    [Description]           VARCHAR (500)   NULL,
    CONSTRAINT [PK_ConsignmentTransferDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentTransferDetail_ConsignmentTransfer] FOREIGN KEY ([ConsignmentTransferId]) REFERENCES [Inventory].[ConsignmentTransfer] ([Id]),
    CONSTRAINT [FK_ConsignmentTransferDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ConsignmentTransferDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[ConsignmentTransferDetail] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetail_ConsignmentTransfer];


GO
ALTER TABLE [Inventory].[ConsignmentTransferDetail] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[ConsignmentTransferDetail] NOCHECK CONSTRAINT [FK_ConsignmentTransferDetail_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo promedio del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetail', @level2type = N'COLUMN', @level2name = N'ProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad a entregar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacén destino', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cabecera de traslado en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';

