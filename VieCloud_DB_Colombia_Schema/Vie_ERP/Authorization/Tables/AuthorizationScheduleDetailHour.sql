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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo de las horas del turno (1=Activo, 0=Inactivo). Indica si el bloque horario está vigente en la programación de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de las horas del turno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de novedad o evento especial asignado: 1=Incapacidad, 2=Permiso, 3=Vacaciones, 4=Otro. Se registra solo cuando Type=3 (Novedad). Aplica para ausencias o ajustes en la programación laboral.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de novedad:  1 - Incapacidad  2 - Permiso  3 - Vacaciones  4 - Otro    Este campo se asigna si el campo Type es 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de turno o bloque horario: 1=Turno Normal, 2=Evento, 3=Novedad. Clasifica la naturaleza de la jornada laboral asignada a la autorización de programación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de turno:  1 - Turno Normal  2 - Evento  3 - Novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas del turno (NUMERIC 5,2). Representa el total de horas establecidas en el rango horario. Usado para cálculo de contratación y facturación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de horas que se establezcan en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final de cierre del turno (TIME). Marca el término del bloque horario dentro de la jornada laboral autorizada.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EndingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora final en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EndingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'EndingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial de apertura del turno (TIME). Marca el inicio del bloque horario dentro de la jornada laboral autorizada.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial en la que aplica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el bloque pertenece al día siguiente (1=Sí, 0=No). Identifica turnos que cruzan la medianoche respecto a la fecha de plantilla base.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el item pertenece al dia Siguiente o no de la fecha aplicada a la plantilla', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de programación de autorización padre. Referencia a [Authorization].[AuthorizationScheduleDetail]. Vincula la hora específica a su detalle de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la hora de la Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de hora en detalle de programación de autorización. Clave primaria de la tabla. Código único para cada bloque horario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro de la hora de detalle de la programación de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las horas programadas dentro de un turno o jornada de autorización de agenda. Registra los rangos de tiempo (hora inicio y fin), duración en horas, tipo de turno y novedades asociadas a cada franja horaria de una programación autorizada.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetailHour';
