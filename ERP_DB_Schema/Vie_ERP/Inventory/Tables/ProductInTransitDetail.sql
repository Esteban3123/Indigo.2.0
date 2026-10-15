CREATE TABLE [Inventory].[ProductInTransitDetail] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [ProductInTransitId] INT             NOT NULL,
    [ProductCode]        VARCHAR (20)    NOT NULL,
    [ProductId]          INT             NULL,
    [Quantity]           INT             NOT NULL,
    [UnitValue]          DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]      DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]      NUMERIC (5, 2)  NOT NULL,
    [IvaValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]         DECIMAL (18, 2) NOT NULL,
    [GrossUnitValue]     DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage] NUMERIC (5, 2)  NOT NULL,
    [NetDiscount]        DECIMAL (18, 2) NOT NULL,
    [ProductName]        VARCHAR (400)   NOT NULL,
    [CodeAlternative]    VARCHAR (30)    CONSTRAINT [DF__ProductIn__CodeA__6A0DCF26] DEFAULT ('') NULL,
    CONSTRAINT [PK_ProductInTransitDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductInTransitDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductInTransitDetail_ProductInTransit] FOREIGN KEY ([ProductInTransitId]) REFERENCES [Inventory].[ProductInTransit] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductInTransitDetail] NOCHECK CONSTRAINT [FK_ProductInTransitDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[ProductInTransitDetail] NOCHECK CONSTRAINT [FK_ProductInTransitDetail_ProductInTransit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Alternativo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'ProductName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'descuento neto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'NetDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario bruto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'GrossUnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal del detalle del contrato. Es la multiplicacion de la cantidad por el valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto que va ingresar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera  del producto en transito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'ProductInTransitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del producto en transito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductInTransitDetail', @level2type = N'COLUMN', @level2name = N'Id';

