CREATE TABLE [Maintenance].[ConsumableDetail] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdConsumable]    INT NOT NULL,
    [IdEquipmentType] INT NOT NULL,
    CONSTRAINT [PK_ConsumableDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsumableDetail_Consumable] FOREIGN KEY ([IdConsumable]) REFERENCES [Maintenance].[Consumable] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ConsumableDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo o activo fijo relacionado al consumible; clave foránea hacia FixedAssetItemType, define qué categoría de equipamiento utiliza este insumo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo relacionado al consumible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del consumible o insumo de mantenimiento relacionado; clave foránea hacia Consumable, referencia el producto/material consumible usado en equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdConsumable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del consumible relacionado al tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdConsumable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdConsumable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) de la relación muchos-a-muchos entre consumibles e tipos de equipo; clave primaria que vincula insumos de mantenimiento con categorías de activos fijos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la relacion entre consumibles y tipos de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del detalle de consumibles asociados a tipos de equipos en mantenimiento. Indica qué consumibles (repuestos, insumos) corresponden a cada tipo de equipo dentro del módulo de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail';
