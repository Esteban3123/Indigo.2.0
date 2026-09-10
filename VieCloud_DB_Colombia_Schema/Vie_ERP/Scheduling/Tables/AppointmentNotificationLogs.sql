CREATE TABLE [Scheduling].[AppointmentNotificationLogs] (
    [id]            INT      IDENTITY (1, 1) NOT NULL,
    [IdAppointment] INT      NOT NULL,
    [Type]          TINYINT  NOT NULL,
    [CreationDate]  DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de notificaciones enviadas para citas médicas agendadas. Permite rastrear qué tipo de notificación (recordatorio, confirmación, cancelación, etc.) fue generada para cada cita y en qué momento.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de notificación.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la cita médica agendada a la que corresponde la notificación.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'IdAppointment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'IdAppointment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o canal de notificación enviada (por ejemplo: recordatorio, confirmación, cancelación).', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se generó o envió la notificación de la cita.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentNotificationLogs', @level2type = N'COLUMN', @level2name = N'CreationDate';
