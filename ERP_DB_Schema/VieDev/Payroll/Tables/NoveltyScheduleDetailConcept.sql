CREATE TABLE [Payroll].[NoveltyScheduleDetailConcept] (
    [Id]                          INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NoveltyScheduleDetailHourId] INT     NOT NULL,
    [ConceptType]                 TINYINT NOT NULL,
    [ConceptId]                   INT     NOT NULL,
    CONSTRAINT [PK_NoveltyScheduleDetailConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoveltyScheduleDetailConcept_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetailConcept_NoveltyScheduleDetailHour] FOREIGN KEY ([NoveltyScheduleDetailHourId]) REFERENCES [Payroll].[NoveltyScheduleDetailHour] ([Id])
);


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetailConcept] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetailConcept_Concept];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetailConcept] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetailConcept_NoveltyScheduleDetailHour];

