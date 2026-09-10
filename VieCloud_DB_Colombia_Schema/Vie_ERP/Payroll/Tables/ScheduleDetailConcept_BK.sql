CREATE TABLE [Payroll].[ScheduleDetailConcept_BK] (
    [Id]                   INT     IDENTITY (1, 1) NOT NULL,
    [ScheduleDetailHourId] INT     NULL,
    [ConceptType]          TINYINT NOT NULL,
    [ConceptId]            INT     NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de respaldo (backup) que almacena conceptos asociados a detalles de horas en un esquema de nómina. Relaciona identificadores de detalle de horario con conceptos de pago clasificados por tipo, sugiriendo que respalda la configuración de conceptos salariales vinculados a jornadas o turnos programados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'ScheduleDetailConcept_BK';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'ScheduleDetailConcept_BK';
GO
