CREATE TABLE [Billing].[BasicBillingGiftsItem] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [BasicBillingGiftsId] INT NOT NULL,
    [PhysicalInventoryId] INT NOT NULL,
    [Quantity]            INT NOT NULL,
    CONSTRAINT [PK_BasicBillingGiftsItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingGiftsItem_BasicBillingGifts] FOREIGN KEY ([BasicBillingGiftsId]) REFERENCES [Billing].[BasicBillingGifts] ([Id]),
    CONSTRAINT [FK_BasicBillingGiftsItem_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Billing].[BasicBillingGiftsItem] NOCHECK CONSTRAINT [FK_BasicBillingGiftsItem_BasicBillingGifts];


GO
ALTER TABLE [Billing].[BasicBillingGiftsItem] NOCHECK CONSTRAINT [FK_BasicBillingGiftsItem_PhysicalInventory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cabecera - Factura Basica Obsequios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'BasicBillingGiftsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Id';

