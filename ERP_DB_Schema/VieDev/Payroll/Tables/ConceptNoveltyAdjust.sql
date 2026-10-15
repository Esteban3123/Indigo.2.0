CREATE TABLE [Payroll].[ConceptNoveltyAdjust] (
    [Id]                INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPayrollSettings] INT        NOT NULL,
    [IdConcept]         INT        NOT NULL,
    [IdAdjustConcept]   INT        NOT NULL,
    [Timestamp]         ROWVERSION NOT NULL,
    CONSTRAINT [PK_ConceptNoveltyAdjust] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptNoveltyAdjust_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ConceptNoveltyAdjust_Concept1] FOREIGN KEY ([IdAdjustConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ConceptNoveltyAdjust_PayrollSettings] FOREIGN KEY ([IdPayrollSettings]) REFERENCES [Payroll].[PayrollSettings] ([Id])
);


GO
ALTER TABLE [Payroll].[ConceptNoveltyAdjust] NOCHECK CONSTRAINT [FK_ConceptNoveltyAdjust_Concept];


GO
ALTER TABLE [Payroll].[ConceptNoveltyAdjust] NOCHECK CONSTRAINT [FK_ConceptNoveltyAdjust_Concept1];


GO
ALTER TABLE [Payroll].[ConceptNoveltyAdjust] NOCHECK CONSTRAINT [FK_ConceptNoveltyAdjust_PayrollSettings];

