CREATE TABLE [Billing].[ProductFeeDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [ProductAndServiceFeeId] INT             NOT NULL,
    [ProductId]              INT             NOT NULL,
    [RateType]               BIT             NOT NULL,
    [PercentageType]         BIT             NULL,
    [Observations]           VARCHAR (MAX)   NOT NULL,
    [InitialDate]            DATETIME        NOT NULL,
    [FinalDate]              DATETIME        NOT NULL,
    [SalePrice]              NUMERIC (18, 2) NULL,
    [Percentage]             NUMERIC (5, 2)  NULL,
    CONSTRAINT [PK_ProductFeeDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductFeeDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductFeeDetail_ProductAndServiceFee] FOREIGN KEY ([ProductAndServiceFeeId]) REFERENCES [Billing].[ProductAndServiceFee] ([Id])
);


GO
ALTER TABLE [Billing].[ProductFeeDetail] NOCHECK CONSTRAINT [FK_ProductFeeDetail_InventoryProduct];


GO
ALTER TABLE [Billing].[ProductFeeDetail] NOCHECK CONSTRAINT [FK_ProductFeeDetail_ProductAndServiceFee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de la tarifa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha limite final ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha limite inicial ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del detalle', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de porcentaje:      1. Costo Promedio Ponderado      2. Último Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'PercentageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de tarifa:      1. Tarifa Fija      2. Porcentaje', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cabecera - Tarifa de productos y servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Id';

