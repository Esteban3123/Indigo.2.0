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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de variacion del producto  1 - Aumento  2 - Descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'VariationType';

