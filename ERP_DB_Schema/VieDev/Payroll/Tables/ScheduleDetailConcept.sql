CREATE TABLE [Payroll].[ScheduleDetailConcept] (
    [Id]                   INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleDetailHourId] INT     NULL,
    [ConceptType]          TINYINT NOT NULL,
    [ConceptId]            INT     NOT NULL,
    CONSTRAINT [PK_ScheduleDetailConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleDetailConcept_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ScheduleDetailConcept_ScheduleDetailHour] FOREIGN KEY ([ScheduleDetailHourId]) REFERENCES [Payroll].[ScheduleDetailHour] ([Id]) ON DELETE CASCADE
);


GO
ALTER TABLE [Payroll].[ScheduleDetailConcept] NOCHECK CONSTRAINT [FK_ScheduleDetailConcept_Concept];


GO
ALTER TABLE [Payroll].[ScheduleDetailConcept] NOCHECK CONSTRAINT [FK_ScheduleDetailConcept_ScheduleDetailHour];

