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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Autorizacion de Conceptos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Conceptos x Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'Id';

