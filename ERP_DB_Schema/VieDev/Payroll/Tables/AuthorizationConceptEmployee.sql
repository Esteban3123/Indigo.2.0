CREATE TABLE [Payroll].[AuthorizationConceptEmployee] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationConceptId] INT NOT NULL,
    [EmployeeId]             INT NOT NULL,
    [State]                  BIT NOT NULL,
    CONSTRAINT [PK_AuthorizationConceptEmployee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationConceptEmployee_AuthorizationConcept] FOREIGN KEY ([AuthorizationConceptId]) REFERENCES [Payroll].[AuthorizationConcept] ([Id]),
    CONSTRAINT [FK_AuthorizationConceptEmployee_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[AuthorizationConceptEmployee] NOCHECK CONSTRAINT [FK_AuthorizationConceptEmployee_AuthorizationConcept];


GO
ALTER TABLE [Payroll].[AuthorizationConceptEmployee] NOCHECK CONSTRAINT [FK_AuthorizationConceptEmployee_Employee];

