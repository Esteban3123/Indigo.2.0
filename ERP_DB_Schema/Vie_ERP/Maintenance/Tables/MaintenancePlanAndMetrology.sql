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
ALTER TABLE [Maintenance].[MaintenancePlanAndMetrology] NOCHECK CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceProtocol];


GO
ALTER TABLE [Maintenance].[MaintenancePlanAndMetrology] NOCHECK CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceProtocol1];


GO
ALTER TABLE [Maintenance].[MaintenancePlanAndMetrology] NOCHECK CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceResponsible];


GO
ALTER TABLE [Maintenance].[MaintenancePlanAndMetrology] NOCHECK CONSTRAINT [FK_MaintenancePlanAndMetrology_MaintenanceResponsible1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incluye Festivos mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'HolidaysMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incluye Domingos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SundaysMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incluye Sábados', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'SaturdaysMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final Metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de la duración del ciclo de metrología:  1 - Dia  2 - Mes  3 - Año  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración del ciclo de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de frecuencia para metrología:  1 - Dia  2 - Mes  3 - Año', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia para metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'FrequenceMetrologyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationsMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMetrologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del protocolo para metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMetrologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial de metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMetrology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EndDateMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de duración:  1 - Dia  2 - Mes  3 - Año  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationUnitMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'DurationValueMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes:  2 : Significativo: más de una cada seis meses  1 : Moderado: una cada 6-9 meses  0 : Usual: una cada 9-18 meses  -1 : Mínimo: una cada 18-30 meses  -2 : Insignificante: menos de una en los 30 meses anteriores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Backgrounds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Requerimiento Mantenimiento:  5 - Extensivo: calibración rutina y reemplazo de partes  4 - Superiores al promedio  3 - Promedio: verificación del desempeño y pruebas de seguridad  2 - Inferiores al promedio  1 - Mínimos: inspección visual', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'MaintenanceRequirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro asociado a aplicación:  5 - Posible Muerte  4 - Posible lesión del paciente o el usuario  3 - Terapia inapropiada o falso diagnóstico  2 - Daños en el equipo  1 - No se detectan riesgos significativos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'RegisterApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Funcion del Equipo:  10 - Soporte de Vida  9 - Cirugía y Cuidados Intensivos  8 - Terapia física y tratamiento  7 - Monitoreo quirúrgico y de cuidados intensivos  6 - Otros equipos para el monitoreo de variables fisiológicas y de diagnóstico  5 - Laboratorio analítico  4 - Accesorios de laboratorio  3 - Sistema de cómputo y equipos  2 - Equipos relacionados con los pacientes y otros equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'EquipmentFunction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para el mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ObservationMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable del mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ResponsibleMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Protocolo para mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'ProtocolMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'InitialDateMaintenance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanAndMetrology', @level2type = N'COLUMN', @level2name = N'Id';

