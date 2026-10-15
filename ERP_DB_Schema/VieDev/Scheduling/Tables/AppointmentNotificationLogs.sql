CREATE TABLE [Scheduling].[AppointmentNotificationLogs] (
    [id]            INT      IDENTITY (1, 1) NOT NULL,
    [IdAppointment] INT      NOT NULL,
    [Type]          TINYINT  NOT NULL,
    [CreationDate]  DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

