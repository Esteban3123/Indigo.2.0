CREATE TABLE [Authorization].[AuthorizationScheduleTemplateSchedule] (
    [Id]                              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationScheduleTemplateId] INT            NOT NULL,
    [NextDay]                         BIT            NOT NULL,
    [InitialTime]                     TIME (0)       NOT NULL,
    [EndingTime]                      TIME (0)       NOT NULL,
    [NumberHour]                      NUMERIC (5, 2) NOT NULL,
    [Status]                          BIT            NOT NULL,
    CONSTRAINT [PK_AuthorizationScheduleTemplateSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationScheduleTemplateSchedule_AuthorizationScheduleTemplate] FOREIGN KEY ([AuthorizationScheduleTemplateId]) REFERENCES [Authorization].[AuthorizationScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationScheduleTemplateSchedule] NOCHECK CONSTRAINT [FK_AuthorizationScheduleTemplateSchedule_AuthorizationScheduleTemplate];

