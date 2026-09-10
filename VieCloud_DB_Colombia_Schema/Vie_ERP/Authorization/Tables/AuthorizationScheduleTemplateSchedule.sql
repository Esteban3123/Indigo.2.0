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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del rango horario en la plantilla de autorización; indica si el turno o franja está vigente (1=Activo, 0=Inactivo)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas asignadas en el rango de fechas; duración en horas y decimales del turno o franja horaria de la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de horas que se establezcan en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final (TIME) en que termina la aplicación del turno; límite superior de la franja horaria establecida en la plantilla de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'EndingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora final en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'EndingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'EndingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial (TIME) en que inicia la aplicación del turno; límite inferior de la franja horaria establecida en la plantilla de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el turno aplica al día siguiente de la fecha base; permite extender horarios que cruzan medianoche en la plantilla de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el item pertenece al dia Siguiente o no de la fecha aplicada a la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la plantilla maestra de cuadro de turnos; referencia a AuthorizationScheduleTemplate cabecera', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la fila de horario dentro de la plantilla; clave primaria del detalle de turno en autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de la plantilla del cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Franjas horarias detalladas de una plantilla de agendamiento de autorizaciones. Cada registro define un bloque de tiempo (hora inicio, hora fin, duración) dentro de una plantilla, indicando si aplica al día siguiente y si está vigente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleTemplateSchedule';
