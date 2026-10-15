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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Autorización de Conceptos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Autorizacion de Conceptos x Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';

