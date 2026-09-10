CREATE TABLE [FixedAsset].[FixedAssetItemTechnicalLog] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId] INT NOT NULL,
    [TechnicalLogId]   INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemTechnicalLog__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemTechnicalLog_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetItemTechnicalLog_TechnicalLog] FOREIGN KEY ([TechnicalLogId]) REFERENCES [Maintenance].[TechnicalLog] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetItemTechnicalLog__FixedAssetItemId__TechnicalLogId]
    ON [FixedAsset].[FixedAssetItemTechnicalLog]([FixedAssetItemId] ASC, [TechnicalLogId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro técnico de mantenimiento o servicio del activo fijo. Referencia a la tabla Maintenance.TechnicalLog. Tipo: INT. Vincula el log técnico con el artículo del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro tecnico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del artículo o bien del activo fijo. Referencia a la tabla FixedAsset.FixedAssetItem. Tipo: INT. Asocia cada registro técnico con el equipo, máquina o instrumento específico.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico primario de la tabla de relación. Tipo: INT IDENTITY(1,1). Clave única que agrupa asociaciones entre activos fijos y sus registros técnicos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de la relación entre activos fijos individuales y sus bitácoras o registros técnicos (mantenimientos, revisiones, novedades técnicas). Permite consultar el historial técnico de cada activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog';
