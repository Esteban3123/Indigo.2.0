CREATE TABLE [Authorization].[AuthorizationSchedule] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleTemplateId] INT          NOT NULL,
    [Month]                           INT          NOT NULL,
    [Year]                            INT          NOT NULL,
    [UserId]                          INT          NOT NULL,
    [UserCode]                        VARCHAR (50) NOT NULL,
    [Status]                          BIT          NOT NULL,
    CONSTRAINT [PK_AuthorizationSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationSchedule_AuthorizationScheduleTemplate] FOREIGN KEY ([AuthorizationScheduleTemplateId]) REFERENCES [Authorization].[AuthorizationScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationSchedule] NOCHECK CONSTRAINT [FK_AuthorizationSchedule_AuthorizationScheduleTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la programación de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationSchedule', @level2type = N'COLUMN', @level2name = N'Id';

