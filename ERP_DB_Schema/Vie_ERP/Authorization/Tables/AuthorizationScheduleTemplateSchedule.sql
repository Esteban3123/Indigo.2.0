CREATE TABLE [Authorization].[AuthorizationScheduleTemplateSchedule] (
    [Id]                              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleTemplateId] INT            NOT NULL,
    [NextDay]                         BIT            NOT NULL,
    [InitialTime]                     TIME (0)       NOT NULL,
    [EndingTime]                      TIME (0)       NOT NULL,
    [NumberHour]                      NUMERIC (5, 2) NOT NULL,
    [Status]                          BIT            NOT NULL,
    CONSTRAINT [PK_AuthorizationScheduleTemplateSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationScheduleTemplateSchedule_AuthorizationScheduleTemplate] FOREIGN KEY ([AuthorizationScheduleTemplateId]) REFERENCES [Authorization].[AuthorizationScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationScheduleTemplateSchedule] NOCHECK CONSTRAINT [FK_AuthorizationScheduleTemplateSchedule_AuthorizationScheduleTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de horas que se establezcan en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora final en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'EndingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si el item pertenece al dia Siguiente o no de la fecha aplicada a la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de la plantilla del cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Id';

