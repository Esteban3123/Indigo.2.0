CREATE TABLE [Maintenance].[WorkOrderActivities] (
    [Id]                 INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]        INT     NOT NULL,
    [ProtocolActivityId] INT     NOT NULL,
    [Time]               INT     NOT NULL,
    [Unit]               TINYINT NOT NULL,
    CONSTRAINT [PK_WorkOrderActivities__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderActivities_ProtocolActivities] FOREIGN KEY ([ProtocolActivityId]) REFERENCES [Maintenance].[ProtocolActivities] ([Id]),
    CONSTRAINT [FK_WorkOrderActivities_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);


GO
ALTER TABLE [Maintenance].[WorkOrderActivities] NOCHECK CONSTRAINT [FK_WorkOrderActivities_ProtocolActivities];


GO
ALTER TABLE [Maintenance].[WorkOrderActivities] NOCHECK CONSTRAINT [FK_WorkOrderActivities_WorkOrder];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderActivities]
    ON [Maintenance].[WorkOrderActivities]([WorkOrderId] ASC, [ProtocolActivityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad:  1 - Minutos  2 - Horas  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo en desarrollar la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Time';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la actividad de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'ProtocolActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Id';

