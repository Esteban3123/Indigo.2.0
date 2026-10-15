CREATE TABLE [Billing].[BasicBillingDetailItem] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BasicBillingDetailId] INT NOT NULL,
    [PhysicalInventoryId]  INT NOT NULL,
    [Quantity]             INT NOT NULL,
    CONSTRAINT [PK_BasicBillingDetailItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingDetailItem_BasicBillingDetail] FOREIGN KEY ([BasicBillingDetailId]) REFERENCES [Billing].[BasicBillingDetail] ([Id]),
    CONSTRAINT [FK_BasicBillingDetailItem_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Billing].[BasicBillingDetailItem] NOCHECK CONSTRAINT [FK_BasicBillingDetailItem_BasicBillingDetail];


GO
ALTER TABLE [Billing].[BasicBillingDetailItem] NOCHECK CONSTRAINT [FK_BasicBillingDetailItem_PhysicalInventory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cabecera - Factura Basica Detalle', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Id';

