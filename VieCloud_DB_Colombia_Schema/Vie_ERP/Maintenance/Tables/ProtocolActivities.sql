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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo de la duración (TINYINT, nullable): 1=Minutos, 2=Horas, 3=Días. Clasificador de la escala temporal de ejecución.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Unit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad:  1 - Minutos  2 - Horas  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Unit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en unidades de tiempo (INT, nullable) para completar la actividad. Valor numérico que se interpreta según la columna Unit.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Time';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en desarrollar la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Time';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Time';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la actividad de mantenimiento (VARCHAR 100). Nombre o tarea a ejecutar dentro del protocolo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Activity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Activity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Activity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del protocolo de mantenimiento (INT, FK → [Maintenance].[MaintenanceProtocol]). Referencia al protocolo padre.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK, IDENTITY). Llave principal de la actividad de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave Principal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividades o tareas que conforman un protocolo de mantenimiento. Cada registro representa un paso o actividad específica dentro de un protocolo, con su duración estimada y unidad de tiempo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolActivities';
