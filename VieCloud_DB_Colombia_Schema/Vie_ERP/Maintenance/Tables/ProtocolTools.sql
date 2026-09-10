CREATE TABLE [Maintenance].[ProtocolTools] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceProtocolId]     INT NOT NULL,
    [FixedAssetPhysicalAssetId] INT NOT NULL,
    CONSTRAINT [PK_ProtocolTools__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProtocolTools_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_ProtocolTools_MaintenanceProtocol] FOREIGN KEY ([MaintenanceProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [IX_ProtocolTools] UNIQUE NONCLUSTERED ([FixedAssetPhysicalAssetId] ASC, [MaintenanceProtocolId] ASC),
    CONSTRAINT [UQ_ProtocolTools__FixedAssetPhysicalAssetId__MaintenanceProtocolId] UNIQUE NONCLUSTERED ([FixedAssetPhysicalAssetId] ASC, [MaintenanceProtocolId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la herramienta/equipo utilizado en el protocolo, registrado como activo fijo físico en el inventario de la institución (FK a FixedAssetPhysicalAsset)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la herramienta utilizada (creada como activo fijo)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del protocolo de mantenimiento preventivo o correctivo al cual se asigna la herramienta/equipo (FK a MaintenanceProtocol)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de la relación entre herramienta y protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre protocolos de mantenimiento y los activos fijos o equipos físicos a los que aplica cada protocolo. Permite asociar uno o varios equipos (herramientas, maquinaria, dispositivos) a un protocolo de mantenimiento específico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools';
