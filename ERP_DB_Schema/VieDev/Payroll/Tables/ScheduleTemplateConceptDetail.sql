CREATE TABLE [Payroll].[ScheduleTemplateConceptDetail] (
    [Id]                        INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateConceptId] INT     NOT NULL,
    [ConceptType]               TINYINT NOT NULL,
    [ConceptId]                 INT     NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateConceptDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateConceptDetail_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ScheduleTemplateConceptDetail_ScheduleTemplateConcept] FOREIGN KEY ([ScheduleTemplateConceptId]) REFERENCES [Payroll].[ScheduleTemplateConcept] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleTemplateConceptDetail] NOCHECK CONSTRAINT [FK_ScheduleTemplateConceptDetail_Concept];


GO
ALTER TABLE [Payroll].[ScheduleTemplateConceptDetail] NOCHECK CONSTRAINT [FK_ScheduleTemplateConceptDetail_ScheduleTemplateConcept];

