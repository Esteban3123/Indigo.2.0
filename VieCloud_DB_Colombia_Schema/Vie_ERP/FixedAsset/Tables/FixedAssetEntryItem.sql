CREATE TABLE [FixedAsset].[FixedAssetEntryItem] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryId]       INT             NOT NULL,
    [RemissionSource]         TINYINT         NOT NULL,
    [SourceCode]              VARCHAR (20)    NULL,
    [PurchaseOrderItemId]     INT             NULL,
    [RemissionEntranceItemId] INT             NULL,
    [ItemId]                  INT             NOT NULL,
    [IVAId]                   INT             NULL,
    [TrademarkId]             INT             NOT NULL,
    [Model]                   VARCHAR (100)   NOT NULL,
    [PolicyId]                INT             NOT NULL,
    [Quantity]                INT             NOT NULL,
    [OutstandingQuantity]     INT             NOT NULL,
    [UnitValue]               DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]           DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]           NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]      NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]              DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]           NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetEntryItem_RTFPercentage] DEFAULT ((0)) NOT NULL,
    [RTFValue]                NUMERIC (18, 2) NOT NULL,
    [Observation]             VARCHAR (1000)  NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetItem] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetPolicy] FOREIGN KEY ([PolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetPurchaseOrderItem] FOREIGN KEY ([PurchaseOrderItemId]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrderItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetRemissionEntranceItem] FOREIGN KEY ([RemissionEntranceItemId]) REFERENCES [FixedAsset].[FixedAssetRemissionEntranceItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetIngressEquipment] FOREIGN KEY ([FixedAssetEntryId]) REFERENCES [FixedAsset].[FixedAssetEntry] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetTrademark];




GO



GO



GO



GO



GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetTrademark];


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o nota del artículo en ingreso de activo fijo; se replica posteriormente en la tabla PhysicalAsset para seguimiento. VARCHAR(1000), nullable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la observación de los artículos, esta observación es la que posteriormente podemos ver en la tabla de Activos Fijos (PhysicalAsset)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de retención en la fuente (RTF) aplicado al ítem; DECIMAL(18,2). Calculado como SubTotalValue × RTFPercentage / 100.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retefuente que se cobrara al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención en la fuente (RTF) aplicado al artículo; NUMERIC(5,2), default 0. Determina el monto de RTF a retener.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del ítem = SubTotalValue - DiscountValue + IvaValue; DECIMAL(18,2). Importe final facturado del artículo en el ingreso.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de descuento = SubTotalValue × DiscountPercentage / 100; DECIMAL(18,2). Monto de rebaja aplicado al ítem.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento comercial aplicado; NUMERIC(5,2). Porcentaje de reducción del subtotal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA = SubTotalValue × IvaPercentage / 100; DECIMAL(18,2). Impuesto al valor agregado del artículo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA aplicado al artículo; NUMERIC(5,2). Tasa impositiva que se cobra sobre el subtotal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del ítem = Quantity × UnitValue; DECIMAL(18,2). Base para cálculo de descuentos e impuestos antes de IVA.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Subtotal del detalle de la orden de compra. Es la multiplicacion de la cantidad por el valor unitario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario del producto; DECIMAL(18,2). Precio por unidad del artículo en el ingreso de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor Unitario del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente por recibir o procesar; INT. Unidades no ingresadas aún del total solicitado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pendiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada del artículo; INT. Número de unidades del ítem en el ingreso de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la póliza de seguro o cobertura; INT, FK a FixedAssetPolicy. Vincula el ítem a su póliza de protección.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo o especificación técnica del artículo; VARCHAR(100). Descripción detallada del modelo del bien ingresado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la marca del artículo; INT, FK a FixedAssetTrademark. Referencia al fabricante o distribuidor del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración de IVA aplicable; INT, FK a GeneralLedgerIVA, nullable. Define la tasa impositiva del artículo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del artículo en catálogo; INT, FK a FixedAssetItem (required). Referencia única al bien en el sistema de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'ItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle en comprobante de entrada; INT, FK a FixedAssetRemissionEntranceItem, nullable. Se llena si el ítem proviene de una remisión de entrada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de comprobante de entrada, este campo solo se llena si el item fue importado de un comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle en orden de compra; INT, FK a FixedAssetPurchaseOrderItem, nullable. Se llena si el ítem fue importado de una orden de compra (remisión).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la orden de compra, este campo solo se llena si el item fue importado de una remision de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del documento de origen (comprobante/OC); VARCHAR(20), nullable. Identificador del documento de donde se importó el artículo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el codigo de del documento el cua fue importado el item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la remisión/ingreso; TINYINT (1=Ninguna, 2=Orden de Compra, 3=Remisión de Entrada). Indica la fuente del ítem en el ingreso de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3 - Remision de Entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ingreso/entrada de activo fijo; INT, FK a FixedAssetEntry (required). Agrupa todos los ítems de una misma entrada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Ingreso del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-numerado del detalle de ingreso; INT, IDENTITY(1,1), PK. Clave primaria de la tabla FixedAssetEntryItem.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ítems o líneas de detalle de una entrada de activos fijos. Registra cada bien o artículo incluido en un ingreso al inventario de activos fijos, con sus valores de compra, impuestos, descuentos y cantidades.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem';
