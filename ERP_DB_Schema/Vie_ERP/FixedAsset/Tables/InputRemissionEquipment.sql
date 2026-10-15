CREATE TABLE [FixedAsset].[InputRemissionEquipment] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdInputRemission]      INT           NOT NULL,
    [RemissionSource]       TINYINT       NOT NULL,
    [SourceCode]            VARCHAR (20)  NULL,
    [PurchaseOrderDetailId] INT           NULL,
    [IdInventoryType]       INT           NOT NULL,
    [IdEquipmentType]       INT           NOT NULL,
    [IdEquipment]           INT           NOT NULL,
    [IdIvaCode]             INT           NOT NULL,
    [Quantity]              INT           NOT NULL,
    [ProductValue]          NUMERIC (18)  NOT NULL,
    [TotalValue]            NUMERIC (18)  NOT NULL,
    [TaxesValue]            NUMERIC (18)  NOT NULL,
    [IdTrademark]           INT           NOT NULL,
    [Model]                 VARCHAR (100) NOT NULL,
    [IdPolize]              INT           NOT NULL,
    CONSTRAINT [PK_InputRemissionEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InputRemissionEquipment_InventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[InputRemissionEquipment] NOCHECK CONSTRAINT [FK_InputRemissionEquipment_InventoryType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id polize', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de impuesto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Código Iva', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el codigo de del documento el cua fue importado el item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id remisión de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'IdInputRemission';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del equipo de remisión de entrada ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Id';

