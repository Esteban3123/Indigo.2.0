CREATE TABLE [Authorization].[AuthorizationScheduleTemplateUsers] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleTemplateId] INT          NOT NULL,
    [UserId]                          INT          NOT NULL,
    [UserCode]                        VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuthorizationScheduleTemplateUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationScheduleTemplateUsers_AuthorizationScheduleTemplate] FOREIGN KEY ([AuthorizationScheduleTemplateId]) REFERENCES [Authorization].[AuthorizationScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationScheduleTemplateUsers] NOCHECK CONSTRAINT [FK_AuthorizationScheduleTemplateUsers_AuthorizationScheduleTemplate];

