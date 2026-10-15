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
ALTER TABLE [Payroll].[ScheduleDetailHour] NOCHECK CONSTRAINT [FK_ScheduleDetailHour_ScheduleDetail];


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetailHour__ScheduleDetailId]
    ON [Payroll].[ScheduleDetailHour]([ScheduleDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Debe ir la fecha de la Nómina en que se va a pagar el evento del mes anterior. Únicamente se llena cuando el campo EventLastMonth es TRUE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'PaidEventLastMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Es un evento de mes anterior, 0 - Registro Normal o Evento del Mes en curso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EventLastMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto que aplica 0 - Ordinario 1 - Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aprobado 1 Si - 0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Approved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evento 1 si - 0 no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Event';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el Item es de un dia siguiente o no 1-si 0-No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total de horas del rango de horas de initial y end', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora final del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora inicial del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Schedule Detail Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'ScheduleDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';

