CREATE TABLE [Glosas].[DeadlinesExpiration NotificationParameters] (
    [Id]                TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdTimeParameters]  TINYINT       NOT NULL,
    [NotificationType]  CHAR (1)      NOT NULL,
    [NotificationValue] VARCHAR (100) NOT NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_DeadlinesExpiration NotificationParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeadlinesExpiration NotificationParameters_TimeParameters] FOREIGN KEY ([IdTimeParameters]) REFERENCES [Glosas].[TimeParameters] ([Id])
);


GO
ALTER TABLE [Glosas].[DeadlinesExpiration NotificationParameters] NOCHECK CONSTRAINT [FK_DeadlinesExpiration NotificationParameters_TimeParameters];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de notificación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - SMS, 2 - SMTP', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'NotificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Parámetros de tiempo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'IdTimeParameters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DeadlinesExpiration NotificationParameters', @level2type = N'COLUMN', @level2name = N'Id';

