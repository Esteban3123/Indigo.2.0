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
ALTER TABLE [Authorization].[AuthorizationScheduleDetail] NOCHECK CONSTRAINT [FK_AuthorizationScheduleDetail_AuthorizationSchedule];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del día', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de horas que se establezcan en el rango de fechas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa el día en el que se asigna el turno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horario:  1. Mañana  2. Tarde  3. Noche  4. Mañana - Tarde  5. Tarde - Noche  6. Mañana - Noche    Cuando el día no tiene un horario normal, sino que solo tiene un evento o una novedad  7. Evento  8. Novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Schedule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del turno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationScheduleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de la programación de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';

