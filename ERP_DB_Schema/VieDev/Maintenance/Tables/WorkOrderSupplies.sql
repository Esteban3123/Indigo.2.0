CREATE TABLE [Maintenance].[WorkOrderSupplies] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]      INT NOT NULL,
    [ProtocolSupplyId] INT NOT NULL,
    CONSTRAINT [PK_WorkOrderSupplies__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderSupplies_ProtocolSupplier] FOREIGN KEY ([ProtocolSupplyId]) REFERENCES [Maintenance].[ProtocolSupplier] ([Id]),
    CONSTRAINT [FK_WorkOrderSupplies_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);


GO
ALTER TABLE [Maintenance].[WorkOrderSupplies] NOCHECK CONSTRAINT [FK_WorkOrderSupplies_ProtocolSupplier];


GO
ALTER TABLE [Maintenance].[WorkOrderSupplies] NOCHECK CONSTRAINT [FK_WorkOrderSupplies_WorkOrder];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderSupplies]
    ON [Maintenance].[WorkOrderSupplies]([WorkOrderId] ASC, [ProtocolSupplyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del insumo de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'ProtocolSupplyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'Id';

