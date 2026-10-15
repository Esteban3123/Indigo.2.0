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
ALTER TABLE [Maintenance].[ProtocolConsumables] NOCHECK CONSTRAINT [FK_ProtocolConsumables_Consumable];


GO
ALTER TABLE [Maintenance].[ProtocolConsumables] NOCHECK CONSTRAINT [FK_ProtocolConsumables_MaintenanceProtocol];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del consumible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'ConsumableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolConsumables', @level2type = N'COLUMN', @level2name = N'Id';

