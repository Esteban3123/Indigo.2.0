CREATE TABLE [Payroll].[AuthorizationConcept] (
    [Id]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptId] INT NOT NULL,
    [State]     BIT NOT NULL,
    CONSTRAINT [PK_AutomaticConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AutomaticConcept_PayrollConcept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[AuthorizationConcept] NOCHECK CONSTRAINT [FK_AutomaticConcept_PayrollConcept];

