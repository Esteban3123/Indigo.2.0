CREATE TABLE [Maintenance].[MaintenancesProtocolActivities] (
    [Id]   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code] VARCHAR (20)  NOT NULL,
    [Name] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_MaintenancesProtocolActivities__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_MaintenancesProtocolActivities__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la actividad del protocolo de mantenimiento; VARCHAR(500); texto libre que identifica la tarea, procedimiento o acción preventiva o correctiva a ejecutar', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la actividad del protocolo de mantenimiento; VARCHAR(20); identificador alfanumérico normalizado para búsqueda y reporte; clave única (UNIQUE)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (llave primaria) de la actividad del protocolo de mantenimiento; INT IDENTITY; generado automáticamente, no editable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de actividades que forman parte de los protocolos de mantenimiento. Registra cada tarea o paso definido dentro de un protocolo de mantenimiento, identificado por un código y un nombre descriptivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities';
