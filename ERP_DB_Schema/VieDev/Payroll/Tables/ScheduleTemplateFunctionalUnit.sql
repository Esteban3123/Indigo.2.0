CREATE TABLE [Payroll].[ScheduleTemplateFunctionalUnit] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateFunctionalUni] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateFunctionalUni_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ScheduleTemplateFunctionalUni_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleTemplateFunctionalUnit] NOCHECK CONSTRAINT [FK_ScheduleTemplateFunctionalUni_FunctionalUnit];


GO
ALTER TABLE [Payroll].[ScheduleTemplateFunctionalUnit] NOCHECK CONSTRAINT [FK_ScheduleTemplateFunctionalUni_ScheduleTemplate];

