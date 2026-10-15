CREATE TABLE [Maintenance].[WorkOrderTools] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]    INT NOT NULL,
    [ProtocolToolId] INT NOT NULL,
    CONSTRAINT [PK_WorkOrderTools__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderTools_ProtocolTools] FOREIGN KEY ([ProtocolToolId]) REFERENCES [Maintenance].[ProtocolTools] ([Id]),
    CONSTRAINT [FK_WorkOrderTools_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);


GO
ALTER TABLE [Maintenance].[WorkOrderTools] NOCHECK CONSTRAINT [FK_WorkOrderTools_ProtocolTools];


GO
ALTER TABLE [Maintenance].[WorkOrderTools] NOCHECK CONSTRAINT [FK_WorkOrderTools_WorkOrder];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderTools]
    ON [Maintenance].[WorkOrderTools]([WorkOrderId] ASC, [ProtocolToolId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la herramienta de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'ProtocolToolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'Id';

