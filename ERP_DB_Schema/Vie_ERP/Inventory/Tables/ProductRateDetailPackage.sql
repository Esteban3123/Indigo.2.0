CREATE TABLE [Inventory].[ProductRateDetailPackage] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductRateDetailId] INT NOT NULL,
    [PackageDetailId]     INT NOT NULL,
    [ProductId]           INT NOT NULL,
    [Quantity]            INT NULL,
    [DoseNumber]          INT NULL,
    CONSTRAINT [PK_ProductRateDetailPackage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductRateDetailPackage_PackageDetailId] FOREIGN KEY ([PackageDetailId]) REFERENCES [MixingStation].[PackageDetail] ([Id]),
    CONSTRAINT [FK_ProductRateDetailPackage_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductRateDetailPackage_ProductRateDetail] FOREIGN KEY ([ProductRateDetailId]) REFERENCES [Inventory].[ProductRateDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductRateDetailPackage] NOCHECK CONSTRAINT [FK_ProductRateDetailPackage_PackageDetailId];


GO
ALTER TABLE [Inventory].[ProductRateDetailPackage] NOCHECK CONSTRAINT [FK_ProductRateDetailPackage_Product];


GO
ALTER TABLE [Inventory].[ProductRateDetailPackage] NOCHECK CONSTRAINT [FK_ProductRateDetailPackage_ProductRateDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'DoseNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del CUM de referencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del paquete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'PackageDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de tarifa de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Id';

