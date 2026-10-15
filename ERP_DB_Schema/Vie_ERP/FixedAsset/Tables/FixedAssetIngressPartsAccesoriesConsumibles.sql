CREATE TABLE [FixedAsset].[FixedAssetIngressPartsAccesoriesConsumibles] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngressEquipment] INT          NOT NULL,
    [IdEquipment]                  INT          NOT NULL,
    [IdPartsAccesoriesConsumibles] INT          NOT NULL,
    [DepreciatePart]               BIT          NULL,
    [Value]                        NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetIngressPartsAccesoriesConsumibles_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetIngressPartsAccesoriesConsumibles_FixedAssetEquipment] FOREIGN KEY ([IdEquipment]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressPartsAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_FixedAssetIngressPartsAccesoriesConsumibles_FixedAssetEquipment];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parte a depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Parts Accesories Consumibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumibles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Equipo de entrada de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';

