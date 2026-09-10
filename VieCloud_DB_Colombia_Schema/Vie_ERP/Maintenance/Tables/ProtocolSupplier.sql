CREATE TABLE [Maintenance].[ProtocolSupplier] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceProtocolId] INT NOT NULL,
    [ProductId]             INT NOT NULL,
    CONSTRAINT [PK_ProtocolSupplier__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProtocolSupplier_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProtocolSupplier_MaintenanceProtocol] FOREIGN KEY ([MaintenanceProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [UQ_ProtocolSupplier__MaintenanceProtocolId__ProductId] UNIQUE NONCLUSTERED ([MaintenanceProtocolId] ASC, [ProductId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto/insumo vinculado al protocolo de mantenimiento. Referencia a Inventory.InventoryProduct. Tipo: INT. Componente de clave única junto con MaintenanceProtocolId.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto de tipo (Insumo)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del protocolo de mantenimiento asociado. Referencia a Maintenance.MaintenanceProtocol. Tipo: INT. Define qué protocolo requiere este insumo o producto.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria clustered. Identificador único secuencial (IDENTITY) de la asociación producto-protocolo. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los protocolos de mantenimiento con los proveedores o productos asociados a cada protocolo. Permite saber qué insumos o proveedores están vinculados a un protocolo de mantenimiento específico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier';
