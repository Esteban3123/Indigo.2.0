CREATE TABLE [Payroll].[ScheduleTemplateConcept] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateId] INT            NOT NULL,
    [Consecutive]        TINYINT        NOT NULL,
    [NextDay]            BIT            NOT NULL,
    [InitialTime]        TIME (0)       NOT NULL,
    [EndingTime]         TIME (0)       NOT NULL,
    [NumberHour]         NUMERIC (5, 2) NOT NULL,
    [State]              BIT            NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateConcept_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleTemplateConcept] NOCHECK CONSTRAINT [FK_ScheduleTemplateConcept_ScheduleTemplate];

