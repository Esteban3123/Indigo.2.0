CREATE TABLE [Payroll].[GroupEventConcept] (
    [Id]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]      INT     NOT NULL,
    [TypeDay]      TINYINT NOT NULL,
    [TypeSchedule] TINYINT NOT NULL,
    [ConceptId]    INT     NOT NULL,
    CONSTRAINT [PK_GroupEventConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupEventConcept_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_GroupEventConcept_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[GroupEventConcept] NOCHECK CONSTRAINT [FK_GroupEventConcept_Concept];


GO
ALTER TABLE [Payroll].[GroupEventConcept] NOCHECK CONSTRAINT [FK_GroupEventConcept_Group];

