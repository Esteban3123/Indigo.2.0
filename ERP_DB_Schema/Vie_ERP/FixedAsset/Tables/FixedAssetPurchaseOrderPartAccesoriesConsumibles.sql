CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderPartAccesoriesConsumibles] (
    [Id]                                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrderEquipmentDetail] INT          NOT NULL,
    [IdEquipment]                              INT          NOT NULL,
    [IdPartAccesoriesConsumibles]              INT          NOT NULL,
    [DepreciatePart]                           BIT          NULL,
    [Value]                                    NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderPartAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([IdPartAccesoriesConsumibles]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPurchaseOrderEquipment] FOREIGN KEY ([IdFixedAssetPurchaseOrderEquipmentDetail]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderPartAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPartsAccesoriesConsumables];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderPartAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPurchaseOrderEquipment];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parte depreciada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Parte Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Detalle de equipo de orden de compra de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipmentDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderPartAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';

