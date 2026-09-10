CREATE TABLE [Maintenance].[ProtocolConsumables] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceProtocolId] INT NOT NULL,
    [ConsumableId]          INT NOT NULL,
    CONSTRAINT [PK_ProtocolConsumables__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProtocolConsumables_Consumable] FOREIGN KEY ([ConsumableId]) REFERENCES [Maintenance].[Consumable] ([Id]),
    CONSTRAINT [FK_ProtocolConsumables_MaintenanceProtocol] FOREIGN KEY ([MaintenanceProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [UQ_ProtocolConsumables__MaintenanceProtocolId__ConsumableId] UNIQUE NONCLUSTERED ([MaintenanceProtocolId] ASC, [ConsumableId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del consumible (repuesto, material, insumo) asociado al protocolo de mantenimiento. Tipo INT, referencia foránea a [Maintenance].[Consumable]([Id]). Permite vincular materiales específicos requeridos en cada protocolo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'ConsumableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del consumible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'ConsumableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'ConsumableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del protocolo de mantenimiento al cual pertenece el consumible. Tipo INT, referencia foránea a [Maintenance].[MaintenanceProtocol]([Id]). Vincula consumibles a protocolos de mantenimiento preventivo o correctivo de equipos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY INT) de cada registro de asociación entre consumible y protocolo. Clave primaria clustered de la tabla [Maintenance].[ProtocolConsumables]. Generado automáticamente al insertar registros.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los consumibles (insumos, materiales o suministros) requeridos por cada protocolo de mantenimiento. Permite saber qué consumibles están asociados a un protocolo específico de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables';
