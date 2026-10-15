CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrder] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]            INT           CONSTRAINT [DF_FixedAssetPurchaseOrder_OperatingUnitId] DEFAULT ((14)) NOT NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [PurchaseOrderDate]          DATE          NOT NULL,
    [DeliverDate]                DATE          CONSTRAINT [DF_FixedAssetPurchaseOrder_DeliverDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [SupplierDistributionLineId] INT           NOT NULL,
    [Detail]                     VARCHAR (300) NOT NULL,
    [Status]                     TINYINT       NOT NULL,
    [CreationUser]               VARCHAR (20)  NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [ConfirmationUser]           VARCHAR (20)  NULL,
    [ConfirmationDate]           DATETIME      NULL,
    [AnnulmentUser]              VARCHAR (20)  NULL,
    [AnnulmentDate]              DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NULL,
    [QuotationNumber]            VARCHAR (20)  NULL,
    [OrderType]                  TINYINT       NULL,
    [OtherOrderType]             VARCHAR (20)  NULL,
    [WayPay]                     VARCHAR (20)  NULL,
    [Warranty]                   VARCHAR (20)  NULL,
    [CurrencyId]                 INT           CONSTRAINT [DF__FixedAsse__Curre__03838866] DEFAULT ((1)) NOT NULL,
    [RequestedFunctionalUnitId]  INT           NULL,
    [DeliveryPlace]              VARCHAR (22)  NULL,
    [DeadlineDays]               VARCHAR (20)  NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrder__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrder_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrder_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrder_RequestedFunctionalUnitId] FOREIGN KEY ([RequestedFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrder_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [UQ_FixedAssetPurchaseOrder__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrder] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrder_Currency_CurrencyId];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrder] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrder_OperatingUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrder] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrder_RequestedFunctionalUnitId];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrder] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrder_SuppliersDistributionLines];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'muestra los dias de plazo del proveedor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrder', @level2type = N'COLUMN', @level2name = N'DeadlineDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'lugar de entrega', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrder', @level2type = N'COLUMN', @level2name = N'DeliveryPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la unidad funcional solicitada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrder', @level2type = N'COLUMN', @level2name = N'RequestedFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrder', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Orden  0: Compra/Artículos  1: Servicios/Mantenimiento  2: Otro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrder', @level2type = N'COLUMN', @level2name = N'OrderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de la orden de compra  1 - Registrado  2 - Confirmada  3 - Anulada  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrder', @level2type = N'COLUMN', @level2name = N'Status';

