CREATE TABLE [Maintenance].[WorkOrderConsumables] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]          INT NOT NULL,
    [ProtocolConsumableId] INT NOT NULL,
    CONSTRAINT [PK_WorkOrderConsumables__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderConsumables_ProtocolConsumables] FOREIGN KEY ([ProtocolConsumableId]) REFERENCES [Maintenance].[ProtocolConsumables] ([Id]),
    CONSTRAINT [FK_WorkOrderConsumables_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);


GO
ALTER TABLE [Maintenance].[WorkOrderConsumables] NOCHECK CONSTRAINT [FK_WorkOrderConsumables_ProtocolConsumables];


GO
ALTER TABLE [Maintenance].[WorkOrderConsumables] NOCHECK CONSTRAINT [FK_WorkOrderConsumables_WorkOrder];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderConsumables]
    ON [Maintenance].[WorkOrderConsumables]([WorkOrderId] ASC, [ProtocolConsumableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del consumible de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'ProtocolConsumableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'Id';

