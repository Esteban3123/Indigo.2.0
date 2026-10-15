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
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_Fee];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_FixedAssetPhysicalAsset];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_FixedAssetPhysicalAssetParts];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_FunctionalUnit];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_InventoryProduct];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_RetentionConcepts];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_RetentionConcepts1];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_BasicBillingDetail_Warehouse];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_SettingsBilling_SalesExecutive_SalesExecutiveId];


GO
ALTER TABLE [Billing].[BasicBillingDetail] NOCHECK CONSTRAINT [FK_SettingsBilling_Supplier_SupplierId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Actividad Economica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Unidad Funcional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Tarifa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'FeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Retención de ICA, el cual se obtiene de la siguiente operación:   (Value - ValueDiscount) * RetentionPercentageICA    Importante: Para que se almacene este valor debe superarse la base de la retencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Retención de la Fuente, el cual se obtiene de la siguiente operación:   (Value - ValueDiscount) * RetentionPercentageTax    Importante: Para que se almacene este valor debe superarse la base de la retencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor, el cual se obtiene de la siguiente operación:   Quantity * Price', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Detalle:      1. Productos       2. Servicios      3. Activos Fijos      4. Partes de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetail', @level2type = N'COLUMN', @level2name = N'DetailType';

