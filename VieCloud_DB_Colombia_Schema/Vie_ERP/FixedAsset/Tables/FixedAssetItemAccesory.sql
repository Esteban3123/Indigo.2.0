CREATE TABLE [FixedAsset].[FixedAssetItemAccesory] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId] INT NOT NULL,
    [AccesoryId]       INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemAccesory__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemAccesory_Accessory] FOREIGN KEY ([AccesoryId]) REFERENCES [Maintenance].[Accessory] ([Id]),
    CONSTRAINT [FK_FixedAssetItemAccesory_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [UQ_FixedAssetItemAccesory__AccesoryId__FixedAssetItemId] UNIQUE NONCLUSTERED ([AccesoryId] ASC, [FixedAssetItemId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del accesorio asociado al activo fijo (FK a Maintenance.Accessory). INT, no nulo. Referencia a componente, repuesto o accesorio que complementa el equipo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'AccesoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del accesorio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'AccesoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'AccesoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del artículo de activo fijo que posee el accesorio (FK a FixedAsset.FixedAssetItem). INT, no nulo. Enlace al equipo, máquina o bien patrimonial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave principal única de la relación accesorio-artículo. INT IDENTITY, no nulo. Identificador secuencial para registro de componentes incluidos en activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los accesorios asociados a cada ítem de activo fijo, permitiendo identificar qué componentes o complementos forman parte de un bien inventariado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemAccesory';
