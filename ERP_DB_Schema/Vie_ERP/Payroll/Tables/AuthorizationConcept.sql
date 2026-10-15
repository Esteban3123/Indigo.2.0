CREATE TABLE [Payroll].[AuthorizationConcept] (
    [Id]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptId] INT NOT NULL,
    [State]     BIT NOT NULL,
    CONSTRAINT [PK_AutomaticConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AutomaticConcept_PayrollConcept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[AuthorizationConcept] NOCHECK CONSTRAINT [FK_AutomaticConcept_PayrollConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConcept', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de los conceptos automaticos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConcept', @level2type = N'COLUMN', @level2name = N'Id';

