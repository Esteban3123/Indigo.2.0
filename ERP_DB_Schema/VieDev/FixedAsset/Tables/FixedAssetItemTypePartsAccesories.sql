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

