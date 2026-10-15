CREATE TABLE [Payroll].[ScheduleTemplateConcept] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateId] INT            NOT NULL,
    [Consecutive]        TINYINT        NOT NULL,
    [NextDay]            BIT            NOT NULL,
    [InitialTime]        TIME (0)       NOT NULL,
    [EndingTime]         TIME (0)       NOT NULL,
    [NumberHour]         NUMERIC (5, 2) NOT NULL,
    [State]              BIT            NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateConcept_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleTemplateConcept] NOCHECK CONSTRAINT [FK_ScheduleTemplateConcept_ScheduleTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del concepto de la plantilla de cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de horas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora final en la que aplica el concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'EndingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial en la que aplica el concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si el item pertenece al dia Siguiente o no de la fecha aplicada a la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'consecutivo para saber el orden de los items', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla del cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de la plantilla del cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Id';

