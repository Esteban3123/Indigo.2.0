CREATE TABLE [Maintenance].[MaintenanceActivity] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenancePlanId]       INT           NULL,
    [MaintenancePlanDetailId] INT           NULL,
    [Name]                    VARCHAR (200) NOT NULL,
    [Frequency]               INT           NOT NULL,
    [TimeUnit]                TINYINT       NULL,
    [HandledControlTime]      BIT           NOT NULL,
    [MaxTime]                 INT           NULL,
    [MaxTimeUnit]             TINYINT       NULL,
    [Priority]                TINYINT       NOT NULL,
    [NumberHours]             INT           NOT NULL,
    [NumberMinutes]           INT           NOT NULL,
    [IsShutdown]              BIT           NOT NULL,
    [ShutdownDays]            INT           NULL,
    [PredictiveMaintenance]   TINYINT       NOT NULL,
    [MeasurementUnitId]       TINYINT       NULL,
    [MinValue]                INT           NULL,
    [MaxValue]                INT           NULL,
    [ActivityProcedure]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_MaintenanceActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceActivity_MaintenancePlan] FOREIGN KEY ([MaintenancePlanId]) REFERENCES [Maintenance].[MaintenancePlan] ([Id]),
    CONSTRAINT [FK_MaintenanceActivity_MaintenancePlanDetail] FOREIGN KEY ([MaintenancePlanDetailId]) REFERENCES [Maintenance].[MaintenancePlanDetail] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceActivity] NOCHECK CONSTRAINT [FK_MaintenanceActivity_MaintenancePlan];


GO
ALTER TABLE [Maintenance].[MaintenanceActivity] NOCHECK CONSTRAINT [FK_MaintenanceActivity_MaintenancePlanDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los procedimientos de las actividades', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ActivityProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor maximo del mantenimiento predictivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor minimo del mantenimiento predictivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MinValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la unidad de medida    Este campo solo se llena si el mantemiento predictivo es diferente 1 es decir que Requiere alguna medicion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo del mantenimiento predictivo  1- No Requiere Medicion  2 - Controlar solo Limite Minimo  3 - Controlar solo limite Maximo  4 - Controlar limitesd Minimo y Maximo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'PredictiveMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de dias que debe estar apagada la maquina o parte', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ShutdownDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si requiere una detencion o apagado por ciertos dias de la parte que se le esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'IsShutdown';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero de minutos de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberMinutes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero de horas de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la prioridad de la actividad  1 - Alta  2 - Media  3 - Baja', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de unidad de tiempo  1 - Dias  2 - Semanas  3 - Mes  4 - Años', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especirfica el maximo de tiempo para la actividad    Este campo solo de llena si Maneja Control de Tiempo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si maneja control de tiempo     Este campo solo se llena si Plan de mantemiento tiene el Regimen como Lecturas', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'HandledControlTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de unidad de tiempo  1 - Dias  2 - Semanas  3 - Mes  4 - Años  5 - Lunes  6 - Martes  7 - Miercoles  8 - Jueves  9 - Viernes  10- Sabado  11 - Domingo    Nota: Este campo solo se llena si el plan de mantenimiento fue por el tipo de regimen "Fecha"', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'TimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la frecuencia con la que se realiza la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Frequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el nombre de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la parte que se esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el plan de mantenimiento que a su vez tiene el tipo de equipo que se le esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Id';

