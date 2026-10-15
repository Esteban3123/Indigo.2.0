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
ALTER TABLE [Maintenance].[ProtocolSupplier] NOCHECK CONSTRAINT [FK_ProtocolSupplier_InventoryProduct];


GO
ALTER TABLE [Maintenance].[ProtocolSupplier] NOCHECK CONSTRAINT [FK_ProtocolSupplier_MaintenanceProtocol];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto de tipo (Insumo)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave principal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolSupplier', @level2type = N'COLUMN', @level2name = N'Id';

