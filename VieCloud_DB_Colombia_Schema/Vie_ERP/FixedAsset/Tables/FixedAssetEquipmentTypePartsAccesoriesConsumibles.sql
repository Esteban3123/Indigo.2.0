CREATE TABLE [FixedAsset].[FixedAssetEquipmentTypePartsAccesoriesConsumibles] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId]              INT NOT NULL,
    [PartsAccesoriesConsumiblesId] INT NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentTypePartsAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la parte, accesorio o consumible (FK a tabla de partes/accesorios/consumibles); referencia el catálogo de componentes, repuestos y suministros asociados al equipo médico o administrativo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PartsAccesoriesConsumiblesId Id de la parte o accesorio ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo médico, administrativo o de infraestructura (FK a tabla de tipos de equipos); establece la relación entre el equipo y sus partes, accesorios o consumibles compatibles.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo asociado al registro del equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY INT) de la relación entre tipo de equipo y parte/accesorio/consumible; clave primaria clustered de la tabla de asociación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los tipos de equipos de activos fijos con sus repuestos, accesorios y consumibles asociados. Permite saber qué partes o insumos corresponden a cada categoría de equipo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentTypePartsAccesoriesConsumibles';
