CREATE TABLE [FixedAsset].[FixedAssetIngressEquipment] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngress] INT           NOT NULL,
    [IdInventoryType]     INT           NOT NULL,
    [IdEquipmentType]     INT           NOT NULL,
    [IdEquipment]         INT           NOT NULL,
    [IdIvaCode]           INT           NOT NULL,
    [Quantity]            INT           NOT NULL,
    [ProductValue]        NUMERIC (18)  NOT NULL,
    [TotalValue]          NUMERIC (18)  NOT NULL,
    [TaxesValue]          NUMERIC (18)  NOT NULL,
    [IdTrademark]         INT           NOT NULL,
    [Model]               VARCHAR (100) NOT NULL,
    [IdPolize]            INT           NOT NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipment] FOREIGN KEY ([IdEquipment]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetInventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetPoliza] FOREIGN KEY ([IdPolize]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetTrademark] FOREIGN KEY ([IdTrademark]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_GeneralLedgerIVA] FOREIGN KEY ([IdIvaCode]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipment];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipmentType];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetInventoryType];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetPoliza];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetTrademark];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_GeneralLedgerIVA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdPolize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdTrademark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Impuesto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TaxesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'ProductValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Código Iva', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdIvaCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Ingreso del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipment', @level2type = N'COLUMN', @level2name = N'Id';

