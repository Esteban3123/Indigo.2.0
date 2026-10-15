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

