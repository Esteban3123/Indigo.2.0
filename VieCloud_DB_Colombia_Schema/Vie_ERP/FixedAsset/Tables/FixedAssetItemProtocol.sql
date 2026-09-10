CREATE TABLE [FixedAsset].[FixedAssetItemProtocol] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId]      INT NOT NULL,
    [MaintenanceProtocolId] INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemProtocol__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemProtocol_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetItemProtocol_MaintenanceProtocol] FOREIGN KEY ([MaintenanceProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [UQ_FixedAssetItemProtocol__FixedAssetItemId__MaintenanceProtocolId] UNIQUE NONCLUSTERED ([FixedAssetItemId] ASC, [MaintenanceProtocolId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Protocolo de Mantenimiento asociado al equipo o artículo (FK a Maintenance.MaintenanceProtocol). Define el plan, frecuencia y procedimientos de mantenimiento preventivo o correctivo aplicables.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Protocolo de Mantenimiento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Equipo, Artículo o Bien Fijo (FK a FixedAsset.FixedAssetItem). Vincula la relación con el activo físico específico que requiere seguimiento de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Equipo/Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave principal (Identity INT). Identificador único de la asociación entre el equipo y su protocolo de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada activo fijo con los protocolos de mantenimiento que le aplican. Permite saber qué planes o rutinas de mantenimiento están asignados a un bien o equipo específico.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol';
