CREATE TABLE [FixedAsset].[FixedAssetItemPart] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId] INT NOT NULL,
    [PartId]           INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemPart__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemPart_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetItemPart_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [UQ_FixedAssetItemPart__FixedAssetItemId__PartId] UNIQUE NONCLUSTERED ([FixedAssetItemId] ASC, [PartId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la parte, pieza, accesorio o consumible asociado al activo fijo. Referencia a tabla FixedAssetPartsAccesoriesConsumables. Tipo: INT. Componente que integra un artículo de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'PartId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'PartId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'PartId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del artículo o bien de activo fijo que contiene o utiliza la parte. Referencia a tabla FixedAssetItem. Tipo: INT. Equipo, máquina o recurso inventariable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave principal única de la relación entre artículo de activo fijo y su parte componente. Identificador secuencial autoincremental. Tipo: INT. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre activos fijos y sus partes o componentes. Registra qué piezas o repuestos conforman cada ítem de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart';
