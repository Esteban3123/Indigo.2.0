CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipment] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrder] INT           NOT NULL,
    [IdInventoryType]           INT           NOT NULL,
    [IdEquipmentType]           INT           NOT NULL,
    [IdEquipment]               INT           NOT NULL,
    [IdIvaCode]                 INT           NOT NULL,
    [Quantity]                  INT           NOT NULL,
    [OutstandingQuantity]       INT           NOT NULL,
    [ProductValue]              NUMERIC (18)  NOT NULL,
    [TotalValue]                NUMERIC (18)  NOT NULL,
    [TaxesValue]                NUMERIC (18)  NOT NULL,
    [IdTrademark]               INT           NOT NULL,
    [Model]                     VARCHAR (100) NOT NULL,
    [IdPolize]                  INT           NOT NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetInventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipment] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetInventoryType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Polize', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la marca ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de impuestos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valot total', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad pendiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Iva Code', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Tipo de inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Orden de compra de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipment', @level2type = N'COLUMN', @level2name = N'Id';

