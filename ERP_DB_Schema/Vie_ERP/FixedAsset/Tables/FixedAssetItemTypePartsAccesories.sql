CREATE TABLE [FixedAsset].[FixedAssetItemTypePartsAccesories] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId]              INT NOT NULL,
    [PartsAccesoriesConsumiblesId] INT NOT NULL,
    CONSTRAINT [PK_EquipmentTypePartsAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_EquipmentType] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_PartsAccesoriesConsumables] FOREIGN KEY ([PartsAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemTypePartsAccesories] NOCHECK CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_EquipmentType];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemTypePartsAccesories] NOCHECK CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_PartsAccesoriesConsumables];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la parte o accesorio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo asociado al registro del equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'Id';

