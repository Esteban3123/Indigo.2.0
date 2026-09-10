CREATE TABLE [Maintenance].[MaintenancePlanAndMetrology] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialDateMaintenance]   DATETIME      NULL,
    [ProtocolMaintenanceId]    INT           NULL,
    [ResponsibleMaintenanceId] INT           NULL,
    [ObservationMaintenance]   VARCHAR (500) NULL,
    [EquipmentFunction]        TINYINT       NULL,
    [RegisterApplication]      TINYINT       NULL,
    [MaintenanceRequirement]   TINYINT       NULL,
    [Backgrounds]              TINYINT       NULL,
    [DurationValueMaintenance] INT           NULL,
    [DurationUnitMaintenance]  TINYINT       NULL,
    [EndDateMaintenance]       DATETIME      NULL,
    [InitialDateMetrology]     DATETIME      NULL,
    [ProtocolMetrologyId]      INT           NULL,
    [ResponsibleMetrologyId]   INT           NULL,
    [ObservationsMetrology]    VARCHAR (500) NULL,
    [FrequenceMetrologyValue]  INT           NULL,
    [FrequenceMetrologyUnit]   TINYINT       NULL,
    [DurationValueMetrology]   INT           NULL,
    [DurationUnitMetrology]    TINYINT       NULL,
    [EndDateMetrology]         DATETIME      NULL,
    [SaturdaysMaintenance]     BIT           NULL,
    [SundaysMaintenance]       BIT           NULL,
    [HolidaysMaintenance]      BIT           NULL,
    CONSTRAINT [PK_MaintenancePlanAndMetrology] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceProtocol] FOREIGN KEY ([ProtocolMaintenanceId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceProtocol1] FOREIGN KEY ([ProtocolMetrologyId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceResponsible] FOREIGN KEY ([ResponsibleMaintenanceId]) REFERENCES [Maintenance].[MaintenanceResponsible] ([Id]),
    CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceResponsible1] FOREIGN KEY ([ResponsibleMetrologyId]) REFERENCES [Maintenance].[MaintenanceResponsible] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que incluye días festivos y feriados en el calendario de mantenimiento preventivo del equipo biomédico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'HolidaysMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluye Festivos mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'HolidaysMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'HolidaysMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que incluye domingos en el ciclo de programación y ejecución del mantenimiento preventivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SundaysMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluye Domingos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SundaysMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SundaysMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que incluye sábados en el cronograma de mantenimiento preventivo del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SaturdaysMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluye Sábados', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SaturdaysMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SaturdaysMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final (DATETIME) del ciclo de calibración y metrología del equipo biomédico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final Metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo (TINYINT: 1=Día, 2=Mes, 3=Año) que define la duración del ciclo de calibración metrológica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de la duración del ciclo de metrología:  1 - Dia  2 - Mes  3 - Año  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (INT) que especifica cuántos períodos dura el ciclo de metrología según la unidad definida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del ciclo de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo (TINYINT: 1=Día, 2=Mes, 3=Año) para establecer la frecuencia de repetición de calibración metrológica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de frecuencia para metrología:  1 - Dia  2 - Mes  3 - Año', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (INT) que define cada cuántos períodos se ejecuta el ciclo de metrología y calibración.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia para metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 500) para registrar hallazgos, notas técnicas y observaciones relevantes de la calibración metrológica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationsMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationsMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationsMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK a MaintenanceResponsible) del profesional responsable de ejecutar la metrología y calibración.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMetrologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMetrologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMetrologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK a MaintenanceProtocol) del protocolo estándar de metrología a aplicar al equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMetrologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del protocolo para metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMetrologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMetrologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial (DATETIME) de inicio del ciclo de calibración metrológica del equipo biomédico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final (DATETIME) del período vigente de mantenimiento preventivo del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Fin de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo (TINYINT: 1=Día, 2=Mes, 3=Año) que define la duración del ciclo de mantenimiento preventivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de duración:  1 - Dia  2 - Mes  3 - Año  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (INT) que especifica la cantidad de períodos de duración del mantenimiento preventivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación (TINYINT: 2=Significativo >6 meses, 1=Moderado 6-9 meses, 0=Usual 9-18 meses, -1=Mínimo 18-30 meses, -2=Insignificante >30 meses) del historial de fallas del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Backgrounds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes:  2 : Significativo: más de una cada seis meses  1 : Moderado: una cada 6-9 meses  0 : Usual: una cada 9-18 meses  -1 : Mínimo: una cada 18-30 meses  -2 : Insignificante: menos de una en los 30 meses anteriores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Backgrounds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Backgrounds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de criticidad (TINYINT: 5=Extensivo, 4=Superior promedio, 3=Promedio, 2=Inferior promedio, 1=Mínimo) requerido para mantenimiento preventivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'MaintenanceRequirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requerimiento Mantenimiento:  5 - Extensivo: calibración rutina y reemplazo de partes  4 - Superiores al promedio  3 - Promedio: verificación del desempeño y pruebas de seguridad  2 - Inferiores al promedio  1 - Mínimos: inspección visual', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'MaintenanceRequirement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'MaintenanceRequirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de riesgo clínico (TINYINT: 5=Muerte, 4=Lesión paciente, 3=Terapia inapropiada, 2=Daño equipo, 1=Sin riesgo) asociado a fallo del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'RegisterApplication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro asociado a aplicación:  5 - Posible Muerte  4 - Posible lesión del paciente o el usuario  3 - Terapia inapropiada o falso diagnóstico  2 - Daños en el equipo  1 - No se detectan riesgos significativos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'RegisterApplication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'RegisterApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación funcional (TINYINT: 10=Soporte vida, 9=Cirugía/UCI, 8=Terapia, 7=Monitoreo, 6=Diagnóstico, 5=Laboratorio analítico, 4=Accesorios lab, 3=Cómputo, 2=Paciente) del equipo biomédico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EquipmentFunction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Funcion del Equipo:  10 - Soporte de Vida  9 - Cirugía y Cuidados Intensivos  8 - Terapia física y tratamiento  7 - Monitoreo quirúrgico y de cuidados intensivos  6 - Otros equipos para el monitoreo de variables fisiológicas y de diagnóstico  5 - Laboratorio analítico  4 - Accesorios de laboratorio  3 - Sistema de cómputo y equipos  2 - Equipos relacionados con los pacientes y otros equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EquipmentFunction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EquipmentFunction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 500) para documentar hallazgos, recomendaciones y notas técnicas del mantenimiento preventivo realizado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para el mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK a MaintenanceResponsible) del técnico o profesional responsable de ejecutar el mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable del mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK a MaintenanceProtocol) del protocolo estándar de mantenimiento preventivo a aplicar.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Protocolo para mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial (DATETIME) de inicio del período vigente de mantenimiento preventivo del equipo biomédico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) que indexa cada registro de plan de mantenimiento y metrología en la tabla.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de mantenimiento y metrología de equipos biomédicos o de infraestructura. Registra las fechas, protocolos, responsables, duración y frecuencia tanto del mantenimiento preventivo/correctivo como de la calibración/metrología de cada equipo, incluyendo si aplica en días especiales como sábados, domingos y festivos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology';
