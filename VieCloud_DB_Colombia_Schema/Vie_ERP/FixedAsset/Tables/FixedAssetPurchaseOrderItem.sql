CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderId]         INT             NOT NULL,
    [ItemId]                  INT             NOT NULL,
    [TrademarkId]             INT             NOT NULL,
    [Model]                   VARCHAR (100)   NOT NULL,
    [IVAId]                   INT             NOT NULL,
    [Quantity]                INT             NOT NULL,
    [OutstandingQuantity]     INT             NOT NULL,
    [CancelledQuantity]       INT             NOT NULL,
    [UnitValue]               DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]           DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]           NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]      NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]              DECIMAL (18, 2) NOT NULL,
    [BranchOfficeId]          INT             NULL,
    [FunctionalUnitId]        INT             NULL,
    [PurchaseRequestDetailId] INT             NULL,
    [OrderSource]             TINYINT         CONSTRAINT [DF_FixedAssetPurchaseOrderItem_OrderSource] DEFAULT ((0)) NOT NULL,
    [SourceCode]              VARCHAR (20)    NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderEquipment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetEquipment] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetPurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrder] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderItem_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderItem_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderItem_PurchaseRequestDetail] FOREIGN KEY ([PurchaseRequestDetailId]) REFERENCES [Inventory].[PurchaseRequestDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetTrademark];




GO



GO



GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetTrademark];


GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de solicitud de compra, código de referencia del origen de la orden de activos fijos (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de solicitud de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la orden de compra de activos fijos: 0=Orden de compra directa, 1=Solicitud de compra (TINYINT, default=0)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'OrderSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen de la orden (0:Orden de compra, 1:Solicitud de compra)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'OrderSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'OrderSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de solicitud de compra vinculado, FK a Inventory.PurchaseRequestDetail', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'PurchaseRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de detalle de solicitud de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'PurchaseRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'PurchaseRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la unidad funcional destino del activo fijo, FK a Payroll.FunctionalUnit (puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la sucursal o centro de atención donde se asigna el activo fijo, FK a Payroll.BranchOffice (puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la sucursal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del renglón de orden de compra: SubTotalValue - DiscountValue + IvaValue (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del descuento aplicado al renglón, calculado como SubTotalValue × (DiscountPercentage/100) (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento sobre el subtotal del renglón de activo fijo (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del IVA aplicado, calculado como SubTotalValue × (IvaPercentage/100) (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA a cobrar sobre el artículo de activo fijo (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del renglón sin descuento ni impuesto: Quantity × UnitValue (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle de la orden de compra. Es la multiplicacion de la cantidad por el valor unitario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario del activo fijo, precio por unidad (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor Unitario del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades canceladas, anuladas o rechazadas de la orden (INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad Cancelada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'CancelledQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente de recibir o procesar del activo fijo (INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pendiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada del activo fijo en la orden de compra (INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de IVA aplicable, FK a GeneralLedger.GeneralLedgerIVA (INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo o especificación técnica del artículo de activo fijo (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el modelo del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la marca o fabricante del activo fijo, FK a FixedAsset.FixedAssetTrademark (INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la Marca del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del artículo/activo fijo, FK a FixedAsset.FixedAssetItem (INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'ItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de compra de activos fijos cabecera, FK a FixedAsset.FixedAssetPurchaseOrder (INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la orden de compra de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico del renglón de orden de compra de activos fijos (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ítems o líneas de detalle de las órdenes de compra de activos fijos. Registra cada artículo solicitado en una orden de compra, incluyendo cantidades, valores, impuestos, descuentos y la unidad funcional o sede a la que se destina el bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderItem';
