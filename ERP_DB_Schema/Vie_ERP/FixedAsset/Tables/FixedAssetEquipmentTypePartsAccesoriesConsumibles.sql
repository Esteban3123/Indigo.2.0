CREATE TABLE [FixedAsset].[FixedAssetEquipmentTypePartsAccesoriesConsumibles] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId]              INT NOT NULL,
    [PartsAccesoriesConsumiblesId] INT NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentTypePartsAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PartsAccesoriesConsumiblesId Id de la parte o accesorio ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo asociado al registro del equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';

