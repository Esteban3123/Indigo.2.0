CREATE TABLE [Inventory].[RemissionOutputDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionOutputId]      INT             NOT NULL,
    [ProductId]              INT             NOT NULL,
    [ProductDescription]     VARCHAR (300)   NOT NULL,
    [Quantity]               INT             NOT NULL,
    [OutstandingAmount]      INT             CONSTRAINT [DF_RemissionOutputDetail_OutstandingAmount] DEFAULT ((0)) NOT NULL,
    [VariationType]          TINYINT         NOT NULL,
    [PercentageVariation]    NUMERIC (5, 2)  NOT NULL,
    [SalePrice]              DECIMAL (18, 2) NOT NULL,
    [SalesPriceOriginal]     DECIMAL (18, 2) NOT NULL,
    [DetailDescription]      VARCHAR (300)   NULL,
    [SalePriceWithDiscount]  DECIMAL (18, 2) NOT NULL,
    [TotalPriceWithDiscount] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_RemissionOutputDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionOutputDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_RemissionOutputDetail_RemissionOutput] FOREIGN KEY ([RemissionOutputId]) REFERENCES [Inventory].[RemissionOutput] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionOutputDetail] NOCHECK CONSTRAINT [FK_RemissionOutputDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[RemissionOutputDetail] NOCHECK CONSTRAINT [FK_RemissionOutputDetail_RemissionOutput];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el precio total de los items con descuentos, es decir (SalePriceWithDiscount * Quantity)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'TotalPriceWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de venta con descuento, es decir que es ((SalePrice * PercentageVariation / 100) + SalePrice)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePriceWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la descripcion del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'DetailDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este es el precio de venta del producto sin modificarse su valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesPriceOriginal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el precio de venta del producto, el usuario podria alterar este valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de aumento o descuento que se le va aplicar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'PercentageVariation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de variacion del producto  1 - Aumento  2 - Descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'VariationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea la remision este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad que se va a despachar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto que se va a despachar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision  de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';

