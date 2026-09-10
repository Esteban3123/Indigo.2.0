CREATE TABLE [FixedAsset].[FixedAssetItemTypePartsAccesories] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId]              INT NOT NULL,
    [PartsAccesoriesConsumiblesId] INT NOT NULL,
    CONSTRAINT [PK_EquipmentTypePartsAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_EquipmentType] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_PartsAccesoriesConsumables] FOREIGN KEY ([PartsAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la parte, accesorio o consumible asociado al tipo de equipo. Referencia FK a FixedAssetPartsAccesoriesConsumables. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la parte o accesorio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo (categoría) que utiliza esta parte, accesorio o consumible. Referencia FK a FixedAssetItemType. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo asociado al registro del equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la relación entre tipo de equipo y parte/accesorio/consumible. Clave primaria. Tipo INT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre tipos de equipos de activos fijos y sus partes, accesorios o consumibles asociados. Permite saber qué componentes o insumos corresponden a cada tipo de equipo registrado en el inventario de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTypePartsAccesories';
