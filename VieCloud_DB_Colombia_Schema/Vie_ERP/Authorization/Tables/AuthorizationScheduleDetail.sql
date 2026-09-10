CREATE TABLE [Authorization].[AuthorizationScheduleDetail] (
    [Id]                      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleId] INT            NOT NULL,
    [Schedule]                TINYINT        NOT NULL,
    [Day]                     INT            NOT NULL,
    [NumberHour]              NUMERIC (5, 2) NOT NULL,
    [Status]                  BIT            NOT NULL,
    CONSTRAINT [PK_AuthorizationScheduleDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationScheduleDetail_AuthorizationSchedule] FOREIGN KEY ([AuthorizationScheduleId]) REFERENCES [Authorization].[AuthorizationSchedule] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del día (activo/inactivo): indica si el turno o jornada del día está habilitado o deshabilitado en la programación de autorización. Tipo: BIT (booleano).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del día', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de horas asignadas en el rango de fechas para el turno: cantidad total de horas laborales establecidas para el día y horario especificado. Tipo: NUMERIC(5,2).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de horas que se establezcan en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día de la semana o número de día en el que se asigna el turno (jornada, atención, disponibilidad): representa la fecha o día específico de la programación de autorización. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el día en el que se asigna el turno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de horario o jornada: 1=Mañana, 2=Tarde, 3=Noche, 4=Mañana-Tarde, 5=Tarde-Noche, 6=Mañana-Noche, 7=Evento, 8=Novedad. Define la franja horaria de trabajo o evento especial en la autorización de turno. Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Schedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horario:  1. Mañana  2. Tarde  3. Noche  4. Mañana - Tarde  5. Tarde - Noche  6. Mañana - Noche    Cuando el día no tiene un horario normal, sino que solo tiene un evento o una novedad  7. Evento  8. Novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Schedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Schedule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de programación de turno (FK → AuthorizationSchedule.Id): referencia principal al registro maestro de autorización de horarios. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del turno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de programación de autorización (PK): clave primaria auto-incremental del registro de turno por día y horario. Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la programación de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los turnos o franjas horarias asociadas a un cronograma de autorización. Registra los días, horas y estado de cada bloque de tiempo dentro de un esquema de autorización de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail';
