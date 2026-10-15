CREATE TABLE [Inventory].[ProductBarcode] (
    [Id]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductId]    INT           NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [Barcode]      VARCHAR (300) NOT NULL,
    CONSTRAINT [PK_ProductBarcode__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductBarcode_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductBarcode] NOCHECK CONSTRAINT [FK_ProductBarcode_Product];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductBarcode__Barcode]
    ON [Inventory].[ProductBarcode]([Barcode] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de barras', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductBarcode', @level2type = N'COLUMN', @level2name = N'Barcode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductBarcode', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductBarcode', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductBarcode', @level2type = N'COLUMN', @level2name = N'Id';

