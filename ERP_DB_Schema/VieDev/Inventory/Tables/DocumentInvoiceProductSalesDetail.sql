CREATE TABLE [Inventory].[DocumentInvoiceProductSalesDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentInvoiceProductSalesId] INT             NOT NULL,
    [ProductId]                     INT             NOT NULL,
    [Quantity]                      INT             NOT NULL,
    [HandlesBatch]                  BIT             NOT NULL,
    [SalePrice]                     DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]                 DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]            NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]                 DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]                 NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                      DECIMAL (18, 2) NOT NULL,
    [TotalValue]                    DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]                 DECIMAL (6, 3)  CONSTRAINT [DF__DocumentI__RTFPe__585AC9ED] DEFAULT ((0)) NOT NULL,
    [RTFValue]                      DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__RTFVa__594EEE26] DEFAULT ((0)) NOT NULL,
    [WithholdingICA]                DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5B373698] DEFAULT ((0)) NOT NULL,
    [WithholdingTax]                DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5C2B5AD1] DEFAULT ((0)) NOT NULL,
    [DistrictTax]                   DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Distr__5D1F7F0A] DEFAULT ((0)) NOT NULL,
    [ImportSource]                  TINYINT         NULL,
    [SourceCode]                    VARCHAR (20)    NULL,
    [InventoryProductCost]          DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Inven__765213CC] DEFAULT ((0)) NOT NULL,
    [EconomicActivityId]            INT             NULL,
    CONSTRAINT [PK_DocumentInvoiceProductSalesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_DocumentInvoiceProductSales] FOREIGN KEY ([DocumentInvoiceProductSalesId]) REFERENCES [Inventory].[DocumentInvoiceProductSales] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDetail] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_DocumentInvoiceProductSales];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDetail] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo del intatio del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto Distrital', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DistrictTax';

