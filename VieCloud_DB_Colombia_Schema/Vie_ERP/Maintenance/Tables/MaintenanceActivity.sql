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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de los pasos, instrucciones y procedimientos técnicos a ejecutar para realizar la actividad de mantenimiento (VARCHAR MAX, texto libre).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ActivityProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los procedimientos de las actividades', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ActivityProcedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ActivityProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo permitido o umbral superior en el monitoreo del mantenimiento predictivo; se valida cuando PredictiveMaintenance=3 o 4 (INT, rango de medición).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor maximo del mantenimiento predictivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo permitido o umbral inferior en el monitoreo del mantenimiento predictivo; se valida cuando PredictiveMaintenance=2 o 4 (INT, rango de medición).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor minimo del mantenimiento predictivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MinValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la unidad de medida asociada (kg, psi, rpm, temperatura, etc.); se completa solo si PredictiveMaintenance ≠ 1 (requiere medición) (TINYINT, PII controlado).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la unidad de medida    Este campo solo se llena si el mantemiento predictivo es diferente 1 es decir que Requiere alguna medicion', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de mantenimiento predictivo: 1=Sin medición, 2=Controlar límite mínimo, 3=Controlar límite máximo, 4=Controlar ambos límites (TINYINT, enumerado).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'PredictiveMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo del mantenimiento predictivo  1- No Requiere Medicion  2 - Controlar solo Limite Minimo  3 - Controlar solo limite Maximo  4 - Controlar limitesd Minimo y Maximo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'PredictiveMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'PredictiveMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días consecutivos que debe permanecer detenido/apagado el equipo o componente durante la actividad (INT, duración en días).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ShutdownDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de dias que debe estar apagada la maquina o parte', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ShutdownDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'ShutdownDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de si la actividad requiere parada, detención o apagado del equipo/componente durante su ejecución (0=No requiere, 1=Requiere).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'IsShutdown';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si requiere una detencion o apagado por ciertos dias de la parte que se le esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'IsShutdown';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'IsShutdown';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos de la actividad de mantenimiento (INT, fracción horaria de tiempo estimado).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberMinutes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de minutos de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberMinutes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberMinutes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en horas de la actividad de mantenimiento (INT, tiempo estimado de ejecución).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de horas de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'NumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de prioridad de la actividad: 1=Alta, 2=Media, 3=Baja (TINYINT, criticidad operacional).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Priority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la prioridad de la actividad  1 - Alta  2 - Media  3 - Baja', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Priority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para MaxTime: 1=Días, 2=Semanas, 3=Meses, 4=Años; rellenable solo si HandledControlTime=1 (TINYINT, enumerado temporal).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de unidad de tiempo  1 - Dias  2 - Semanas  3 - Mes  4 - Años', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo máximo permitido entre ejecuciones consecutivas de la actividad; se completa si se maneja control de tiempo (HandledControlTime=1) (INT, valor numérico).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especirfica el maximo de tiempo para la actividad    Este campo solo de llena si Maneja Control de Tiempo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaxTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de si se aplica control/validación de tiempo; rellenable cuando el plan de mantenimiento tiene régimen ''''Lecturas'''' (0=No controla, 1=Controla).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'HandledControlTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si maneja control de tiempo     Este campo solo se llena si Plan de mantemiento tiene el Regimen como Lecturas', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'HandledControlTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'HandledControlTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad temporal de frecuencia: 1=Días, 2=Semanas, 3=Meses, 4=Años, 5-11=Días semana (lun-dom); rellenable solo si régimen del plan es ''''Fecha'''' (TINYINT, enumerado).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de unidad de tiempo  1 - Dias  2 - Semanas  3 - Mes  4 - Años  5 - Lunes  6 - Martes  7 - Miercoles  8 - Jueves  9 - Viernes  10- Sabado  11 - Domingo    Nota: Este campo solo se llena si el plan de mantenimiento fue por el tipo de regimen "Fecha"', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'TimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de frecuencia: cada X unidades de tiempo (TimeUnit) se ejecuta la actividad (INT, ciclo de repetición).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Frequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la frecuencia con la que se realiza la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Frequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Frequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificador descriptivo de la actividad de mantenimiento (VARCHAR 200, texto búsqueda).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del componente, equipo o parte específica sobre la cual se ejecuta esta actividad (INT, relación jerárquica).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la parte que se esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del plan de mantenimiento padre que contiene el tipo de equipo y contexto global de esta actividad (INT, relación maestro).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el plan de mantenimiento que a su vez tiene el tipo de equipo que se le esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la actividad de mantenimiento en la tabla (INT PRIMARY KEY, clave primaria).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividades de mantenimiento definidas dentro de un plan de mantenimiento. Registra cada tarea o actividad (preventiva, predictiva o correctiva) con su frecuencia, prioridad, tiempos de ejecución, valores de medición esperados y el procedimiento a seguir.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceActivity';
