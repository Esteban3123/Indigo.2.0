CREATE TABLE [Billing].[BasicBillingGifts] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [BasicBillingId] INT NOT NULL,
    [ProductId]      INT NULL,
    [Quantity]       INT NOT NULL,
    [WarehouseId]    INT NULL,
    CONSTRAINT [PK_BasicBillingGifts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingGifts_BasicBilling] FOREIGN KEY ([BasicBillingId]) REFERENCES [Billing].[BasicBilling] ([Id]),
    CONSTRAINT [FK_BasicBillingGifts_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Billing].[BasicBillingGifts] NOCHECK CONSTRAINT [FK_BasicBillingGifts_BasicBilling];


GO
ALTER TABLE [Billing].[BasicBillingGifts] NOCHECK CONSTRAINT [FK_BasicBillingGifts_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacen', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cabecera - Factura Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'BasicBillingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Id';

