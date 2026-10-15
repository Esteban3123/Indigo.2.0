CREATE TABLE [Maintenance].[ProtocolActivities] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceProtocolId] INT           NOT NULL,
    [Activity]              VARCHAR (100) NOT NULL,
    [Time]                  INT           NULL,
    [Unit]                  TINYINT       NULL,
    CONSTRAINT [PK_ProtocolActivities__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceProtocolActivities_MaintenanceProtocol] FOREIGN KEY ([MaintenanceProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [UQ_ProtocolActivities__Activity__MaintenanceProtocolId] UNIQUE NONCLUSTERED ([Activity] ASC, [MaintenanceProtocolId] ASC)
);


GO
ALTER TABLE [Maintenance].[ProtocolActivities] NOCHECK CONSTRAINT [FK_MaintenanceProtocolActivities_MaintenanceProtocol];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad:  1 - Minutos  2 - Horas  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo en desarrollar la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Time';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Activity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave Principal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';

