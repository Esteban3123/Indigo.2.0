CREATE TABLE [Authorization].[AuthorizationScheduleDetailHour] (
    [Id]                            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleDetailId] INT            NOT NULL,
    [NextDay]                       BIT            NOT NULL,
    [InitialTime]                   TIME (0)       NOT NULL,
    [EndingTime]                    TIME (0)       NOT NULL,
    [NumberHour]                    NUMERIC (5, 2) NOT NULL,
    [Type]                          TINYINT        CONSTRAINT [DF_AuthorizationScheduleDetailHour_Type] DEFAULT ((1)) NOT NULL,
    [NoveltyType]                   TINYINT        CONSTRAINT [DF_AuthorizationScheduleDetailHour_IsEvent] DEFAULT ((0)) NULL,
    [Status]                        BIT            NOT NULL,
    CONSTRAINT [PK_AuthorizationScheduleDetailHour] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationScheduleDetailHour_AuthorizationScheduleDetail] FOREIGN KEY ([AuthorizationScheduleDetailId]) REFERENCES [Authorization].[AuthorizationScheduleDetail] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationScheduleDetailHour] NOCHECK CONSTRAINT [FK_AuthorizationScheduleDetailHour_AuthorizationScheduleDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de las horas del turno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de novedad:  1 - Incapacidad  2 - Permiso  3 - Vacaciones  4 - Otro    Este campo se asigna si el campo Type es 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de turno:  1 - Turno Normal  2 - Evento  3 - Novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de horas que se establezcan en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora final en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EndingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si el item pertenece al dia Siguiente o no de la fecha aplicada a la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de la hora de la Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Registro de la hora de detalle de la programación de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';

