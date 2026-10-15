CREATE TABLE [Payroll].[AuthorizationConceptGroup] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationConceptId] INT NOT NULL,
    [GroupId]                INT NOT NULL,
    [State]                  BIT NOT NULL,
    CONSTRAINT [PK_AuthorizationConceptGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationConceptGroup_AuthorizationConcept] FOREIGN KEY ([AuthorizationConceptId]) REFERENCES [Payroll].[AuthorizationConcept] ([Id]),
    CONSTRAINT [FK_AuthorizationConceptGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[AuthorizationConceptGroup] NOCHECK CONSTRAINT [FK_AuthorizationConceptGroup_AuthorizationConcept];


GO
ALTER TABLE [Payroll].[AuthorizationConceptGroup] NOCHECK CONSTRAINT [FK_AuthorizationConceptGroup_Group];

