CREATE TABLE [Billing].[BasicBillingDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BasicBillingId]         INT             NOT NULL,
    [DetailType]             TINYINT         NOT NULL,
    [ProductId]              INT             NULL,
    [BillingConceptId]       INT             NULL,
    [PhysicalAssetId]        INT             NULL,
    [PhysicalAssetPartId]    INT             NULL,
    [Quantity]               INT             NOT NULL,
    [Price]                  NUMERIC (18, 2) NOT NULL,
    [Value]                  NUMERIC (18, 2) NOT NULL,
    [PercentageDiscount]     NUMERIC (5, 2)  NOT NULL,
    [ValueDiscount]          NUMERIC (18, 2) CONSTRAINT [DF_BasicBillingDetail_ValueDiscount] DEFAULT ((0)) NOT NULL,
    [PercentageIVA]          NUMERIC (5, 2)  NOT NULL,
    [RetentionPercentageTax] DECIMAL (6, 3)  NOT NULL,
    [WithholdingTax]         NUMERIC (18, 2) NOT NULL,
    [RetentionPercentageICA] DECIMAL (6, 3)  NOT NULL,
    [WithholdingICA]         NUMERIC (18, 2) NOT NULL,
    [RetentionIdTax]         INT             NULL,
    [RetentionIdICA]         INT             NULL,
    [WarehouseId]            INT             NULL,
    [ServicesProvidedId]     INT             NULL,
    [SupplierId]             INT             NULL,
    [SalesExecutiveId]       INT             NULL,
    [FeeId]                  INT             NULL,
    [FunctionalUnitId]       INT             NULL,
    [EconomicActivityId]     INT             NULL,
    CONSTRAINT [PK_BasicBillingDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingDetail_BasicBilling] FOREIGN KEY ([BasicBillingId]) REFERENCES [Billing].[BasicBilling] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_Fee] FOREIGN KEY ([FeeId]) REFERENCES [Billing].[ProductAndServiceFee] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_FixedAssetPhysicalAssetParts] FOREIGN KEY ([PhysicalAssetPartId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAssetParts] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_RetentionConcepts] FOREIGN KEY ([RetentionIdTax]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_RetentionConcepts1] FOREIGN KEY ([RetentionIdICA]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_BasicBillingDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [FK_SettingsBilling_SalesExecutive_SalesExecutiveId] FOREIGN KEY ([SalesExecutiveId]) REFERENCES [Billing].[SalesExecutive] ([Id]),
    CONSTRAINT [FK_SettingsBilling_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_BasicBilling];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_FunctionalUnit];




GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_BasicBilling];


GO



GO



GO



GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_FunctionalUnit];


GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica del detalle de facturación; clasificación sectorial o rama de negocio asociada al producto o servicio facturado (INT, FK → Common.EconomicActivity)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Actividad Economica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional responsable del detalle; centro de costo, departamento o área operativa que genera la facturación (INT, FK → Payroll.FunctionalUnit)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tarifa o arancel aplicado al producto/servicio; precio base o fee negociado por producto y servicio (INT, FK → Billing.ProductAndServiceFee)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Tarifa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ejecutivo de ventas responsable; profesional o vendedor que realiza o gestiona la venta (INT, FK → Billing.SalesExecutive)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'SalesExecutiveId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Ejecutivo de Ventas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'SalesExecutiveId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'SalesExecutiveId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor o empresa proveedora; entidad que suministra productos, servicios o activos fijos facturados (INT, FK → Common.Supplier)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Proveedor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio prestado; referencia a servicios de salud, consulta, procedimiento o atención facturada (INT, puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ServicesProvidedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Servicio Prestado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ServicesProvidedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ServicesProvidedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén de origen; bodega o centro de inventario desde donde se despacha producto (INT, FK → Inventory.Warehouse)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Almacén.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención en ICA; tipo de impuesto retenido a nivel municipal/distrital (INT, FK → GeneralLedger.RetentionConcepts)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionIdICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto Retencion de ICA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionIdICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionIdICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención en la fuente; tipo de impuesto retenido sobre la facturación (INT, FK → GeneralLedger.RetentionConcepts)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionIdTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto Retencion de la Fuente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionIdTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionIdTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de retención de ICA calculado; = (Value - ValueDiscount) × RetentionPercentageICA, si supera base mínima (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Retención de ICA, el cual se obtiene de la siguiente operación:   (Value - ValueDiscount) * RetentionPercentageICA    Importante: Para que se almacene este valor debe superarse la base de la retencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención en ICA aplicable; tasa tributaria municipal descontada en la factura (NUMERIC 6,3)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentageICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Retención de ICA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentageICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentageICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de retención en la fuente; = (Value - ValueDiscount) × RetentionPercentageTax, si supera base mínima (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Retención de la Fuente, el cual se obtiene de la siguiente operación:   (Value - ValueDiscount) * RetentionPercentageTax    Importante: Para que se almacene este valor debe superarse la base de la retencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención en la fuente; tasa tributaria nacional descontada en la facturación (NUMERIC 6,3)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentageTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Retención en Fuente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentageTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentageTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA o impuesto al valor agregado; tasa tributaria estándar o diferencial aplicada (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PercentageIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PercentageIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PercentageIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor absoluto del descuento aplicado; monto en pesos deducido del valor total por conceptos comerciales (NUMERIC 18,2, default 0)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de Descuento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento comercial; rebaja porcentual sobre precio antes de retenciones (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PercentageDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PercentageDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PercentageDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de línea facturada; = Quantity × Price, base para cálculo de descuentos e impuestos (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor, el cual se obtiene de la siguiente operación:   Quantity * Price', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de producto o servicio; costo individual por unidad o acto antes de descuentos (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Price';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Price';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades, servicios o actos; número de productos, atenciones o procedimientos facturados (INT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la parte o componente del activo fijo; requerido si DetailType=4 (Partes de Activos Fijos) (INT, FK → FixedAsset.FixedAssetPhysicalAssetParts)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del la Parte, este debe ir definido si el tipo de detalle es Partes de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetPartId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del activo fijo completo; requerido si DetailType=3 (Activos Fijos), máquina o equipo facturado (INT, FK → FixedAsset.FixedAssetPhysicalAsset)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Activo Fijo, este debe ir definido si el tipo de detalle es Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de facturación; requerido si DetailType=2 (Servicios), tipo de servicio prestado (INT, puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Facturación, este debe ir definido si el tipo de detalle es Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto de inventario; requerido si DetailType=1 (Productos), artículo disponible en stock (INT, FK → Inventory.InventoryProduct)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Producto, este debe ir definido si el tipo de detalle es Productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de detalle de línea: 1=Productos, 2=Servicios, 3=Activos Fijos, 4=Partes de Activos Fijos; determina qué FK es obligatoria (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'DetailType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Detalle:      1. Productos       2. Servicios      3. Activos Fijos      4. Partes de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'DetailType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'DetailType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de factura básica o cabecera; referencia al documento de facturación padre (INT, FK → Billing.BasicBilling)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'BasicBillingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cabecera - Factura Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'BasicBillingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'BasicBillingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de facturación; clave primaria de la línea dentro de la factura (INT, IDENTITY 1,1)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de líneas de una factura o cuenta de cobro básica. Cada registro representa un ítem facturado (producto, servicio, activo físico o concepto de cobro) con sus cantidades, precios, descuenttos, impuestos (IVA, retención en la fuente, ICA) y la unidad funcional o ejecutivo de ventas asociado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail';
