CREATE TABLE [Payroll].[ScheduleDetailHour] (
    [Id]                        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleDetailId]          INT            NOT NULL,
    [DateTimeInitial]           DATETIME       NOT NULL,
    [DateTimeEnding]            DATETIME       NOT NULL,
    [TotalNumberHours]          NUMERIC (5, 2) NOT NULL,
    [NextDay]                   BIT            NOT NULL,
    [Event]                     BIT            NOT NULL,
    [Approved]                  BIT            NOT NULL,
    [AppliedLiquidationConcept] BIT            NOT NULL,
    [State]                     BIT            NOT NULL,
    [EventLastMonth]            BIT            CONSTRAINT [DF_ScheduleDetailHour_EventLastMonth] DEFAULT ((0)) NOT NULL,
    [PaidEventLastMonth]        DATE           NULL,
    CONSTRAINT [PK_ScheduleDetailHour__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleDetailHour_ScheduleDetail] FOREIGN KEY ([ScheduleDetailId]) REFERENCES [Payroll].[ScheduleDetail] ([Id])
);




GO





GO



GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetailHour__ScheduleDetailId]
    ON [Payroll].[ScheduleDetailHour]([ScheduleDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la nómina en que se pagará el evento del mes anterior. Solo se completa si EventLastMonth=1. Tipo DATE, referencia de liquidación diferida.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'PaidEventLastMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Debe ir la fecha de la Nómina en que se va a pagar el evento del mes anterior. Únicamente se llena cuando el campo EventLastMonth es TRUE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'PaidEventLastMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'PaidEventLastMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de evento diferido (1=evento del mes anterior, 0=evento del mes en curso o registro normal). Marca horas que se cancelarán en nómina posterior.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EventLastMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Es un evento de mes anterior, 0 - Registro Normal o Evento del Mes en curso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EventLastMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EventLastMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del registro (1=activo, 0=inactivo). Controla si el registro participa en procesos de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de liquidación aplicable (0=ordinario, 1=feriado). Determina el tipo de valor horario usado en cálculo de salario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto que aplica 0 - Ordinario 1 - Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de aprobación del registro horario (1=aprobado, 0=pendiente). Requiere validación antes de liquidación en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Approved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aprobado 1 Si - 0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Approved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Approved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de evento especial en la jornada (1=sí evento, 0=no evento). Identifica si hay concepto especial como festivo, compensatorio o recargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Event';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evento 1 si - 0 no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Event';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Event';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si las horas corresponden a jornada del día siguiente (1=sí, 0=no). Aplica cuando el turno cruza la medianoche.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el Item es de un dia siguiente o no 1-si 0-No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de horas calculadas entre DateTimeInitial y DateTimeEnding. Valor numérico decimal (5,2) usado para cálculo de nómina y liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de horas del rango de horas de initial y end', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización del período laborado o evento. Tipo DATETIME, marca el término del rango horario registrado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del período laborado o evento. Tipo DATETIME, marca el comienzo del rango horario registrado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia [Payroll].[ScheduleDetail]. Identifica el detalle de la programación a la cual pertenece este registro horario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'ScheduleDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Schedule Detail Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'ScheduleDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'ScheduleDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la hora programada. Clave primaria de la tabla ScheduleDetailHour.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetailHour_ScheduleDetailId]
    ON [Payroll].[ScheduleDetailHour]([ScheduleDetailId] ASC)
    INCLUDE([Id], [Event], [Approved], [DateTimeInitial], [NextDay]) WITH (FILLFACTOR = 90, PAD_INDEX = ON);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de horas por turno en la programación de nómina. Registra los intervalos de tiempo trabajados dentro de cada turno del empleado, incluyendo si el turno cruza al día siguiente, si generó un evento especial (recargo, festivo, etc.) y si las horas ya fueron aprobadas y liquidadas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour';
